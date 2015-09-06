/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：FlowOperate.cs
// 文件功能描述：工作流操作类。
// 创建人：pekah.chow
// 创建时间： 2014-04-28
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Script.Serialization;
using NFMT.Common;
using NFMT.Data;
using NFMT.Operate.BLL;
using NFMT.Sms;
using NFMT.Sms.DAL;
using NFMT.Sms.Model;
using NFMT.WorkFlow.BLL;
using NFMT.WorkFlow.DAL;
using NFMT.WorkFlow.Model;

namespace NFMT.WorkFlow
{
    public class FlowOperate : IFlowOperate
    {
        /// <summary>
        /// 默认用户
        /// </summary>
        /// <returns></returns>
        public static UserModel DefaultUser()
        {
            UserModel user = new UserModel
            {
                AccountId = 102,
                AccountName = "WorkFlow Default User"
            };

            user.EmpId = user.AccountId;
            user.EmpName = user.AccountName;

            return user;
        }
        private UserModel User = DefaultUser();

        //节点存放
        public static Dictionary<string, DataTable> nodeCollection = new Dictionary<string, DataTable>();
        //节点判断条件存放
        public static Dictionary<int, DataTable> conditionCollection = new Dictionary<int, DataTable>();

        public static void RefreshNode()
        {
            if (nodeCollection == null)
                return;
            lock (nodeCollection)
            {
                foreach (KeyValuePair<string, DataTable> kvp in nodeCollection.Where(kvp => kvp.Value != null))
                {
                    kvp.Value.Clear();
                }
            }
            lock (nodeCollection)
            {
                nodeCollection.Clear();
            }
        }

        public static void RefreshCondition()
        {
            if (conditionCollection == null)
                return;
            lock (conditionCollection)
            {
                foreach (KeyValuePair<int, DataTable> kvp in conditionCollection.Where(kvp => kvp.Value != null))
                {
                    kvp.Value.Clear();
                }
            }
            lock (conditionCollection)
            {
                conditionCollection.Clear();
            }
        }

        /// <summary>
        /// 提交审核并创建任务和提醒消息
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="model">提交审核数据实体</param>
        /// <param name="master">模版</param>
        /// <param name="source">数据源</param>
        /// <param name="task">任务</param>
        /// <returns></returns>
        public ResultModel AuditAndCreateTask(UserModel user, IModel model, FlowMaster master, DataSource source, Task task)
        {
            ResultModel result = new ResultModel();

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DataSourceDAL dataSourceDAL = new DataSourceDAL();
                    result = dataSourceDAL.CheckHasSource(new DataSource() { TableCode = model.TableName, RowId = model.Id });
                    if (result.ResultStatus != 0 || result.AffectCount > 0)
                    {
                        result.Message = "不能重复提交审核";
                        result.ResultStatus = -1;
                        return result;
                    }

                    Common.Operate operate = Common.Operate.CreateOperate(model, true);
                    if (operate == null)
                    {
                        result.Message = "模版不存在";
                        result.ResultStatus = -1;
                        return result;
                    }

                    result = operate.Get(user, model.Id);
                    if (result.ResultStatus != 0)
                        return result;

                    IModel resultModel = result.ReturnValue as IModel;
                    if (resultModel == null || resultModel.Id <= 0)
                    {
                        result.ResultStatus = -1;
                        result.Message = "数据不存在，不能提交审核";
                        return result;
                    }

                    //判断是否可以直接审核通过
                    FlowMasterConfigDAL flowMasterConfigDAL = new FlowMasterConfigDAL();
                    result = flowMasterConfigDAL.GetByMasterId(user, master.MasterId);
                    if (result.ResultStatus == 0)
                    {
                        FlowMasterConfig flowMasterConfig = result.ReturnValue as FlowMasterConfig;
                        if (flowMasterConfig != null && flowMasterConfig.ConfigId > 0 && flowMasterConfig.CanPassAudit)
                        {
                            result = RequestCallBackUrlForPass(user, source, true);

                            if (result.ResultStatus != 0)
                                return result;

                            if (result.ResultStatus == 0)
                                scope.Complete();

                            return result;
                        }
                    }

                    result = operate.Submit(user, resultModel);
                    if (result.ResultStatus != 0)
                        return result;

                    result = this.CreateTask(user, master, source, task);
                    if (result.ResultStatus != 0)
                        return result;

                    int taskId = (int)result.ReturnValue;

                    string aids = string.Empty;

                    AttachBLL attachBLL = new AttachBLL();
                    result = attachBLL.GetAttachIds(user, resultModel);
                    if (result.ResultStatus == 0)
                    {
                        DataTable dt = result.ReturnValue as DataTable;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            aids = dt.Rows.Cast<DataRow>().Aggregate(aids, (current, dr) => current + (dr["AttachId"].ToString() + ","));

                            if (!string.IsNullOrEmpty(aids))
                                aids = aids.Substring(0, aids.Length - 1);
                        }

                        TaskAttachBLL taskAttachBLL = new TaskAttachBLL();
                        result = taskAttachBLL.InsertAttach(user, taskId, aids);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    else
                        result.ResultStatus = 0;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }
            return result;
        }

        /// <summary>
        /// 根据流程模版，创建任务及任务的第一节点，返回任务的id
        /// </summary>
        /// <param name="user">当前用户</param>
        /// <param name="master">流程模版</param>
        /// <param name="source">数据源</param>
        /// <param name="task">任务</param>
        /// <returns></returns>
        private ResultModel CreateTask(UserModel user, FlowMaster master, DataSource source, Task task)
        {
            ResultModel result = new ResultModel();

            try
            {
                //写入DataSource                   
                DataSourceDAL dataSourceDAL = new DataSourceDAL();
                result = dataSourceDAL.Insert(user, source);
                if (result.ResultStatus != 0)
                    return result;

                int sourceId = Convert.ToInt32(result.ReturnValue);

                //写入task
                TaskDAL taskDAL = new TaskDAL();
                task.DataSourceId = sourceId;
                result = taskDAL.Insert(user, task);
                if (result.ResultStatus != 0)
                    return result;

                int taskId = Convert.ToInt32(result.ReturnValue);
                task.TaskId = taskId;

                //在审核流程中添加提交审核人
                NodeDAL nodeDAL = new NodeDAL();
                result = nodeDAL.Load<Node>(user, CommandType.Text, string.Format("select * from dbo.Wf_Node where MasterId = {0} and NodeStatus = {1}", master.MasterId, (int)StatusEnum.已生效));
                if (result.ResultStatus != 0)
                    return result;

                List<Node> nodes = result.ReturnValue as List<Node>;
                if (nodes != null)
                {
                    Node firstNode = nodes.OrderBy(a => a.NodeLevel).First();
                    //如果存在NodeLevel为0（提交人）
                    if (firstNode.NodeLevel == 0)
                    {
                        TaskNodeDAL taskNodeDAL = new TaskNodeDAL();
                        result = taskNodeDAL.Insert(user, new TaskNode()
                        {
                            NodeId = firstNode.NodeId,
                            TaskId = taskId,
                            NodeLevel = firstNode.NodeLevel,
                            NodeStatus = StatusEnum.已生效,
                            EmpId = source.EmpId,
                            AuditTime = DefaultValue.DefaultTime
                        });

                        int taskNodeId = (int)result.ReturnValue;

                        TaskOperateLogDAL taskOperateLogDAL = new TaskOperateLogDAL();
                        result = taskOperateLogDAL.Insert(user, new TaskOperateLog()
                        {
                            TaskNodeId = taskNodeId,
                            EmpId = source.EmpId,
                            Memo = "",
                            LogTime = DateTime.Now,
                            LogResult = ""
                        });
                    }
                }

                result = CreateTaskNodes(user, task, 1, source);
                if (result.ResultStatus != 0)
                    return result;

                result.ReturnValue = taskId;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ResultStatus = -1;
            }

            return result;
        }

        /// <summary>
        /// 审核工作流任务
        /// </summary>
        /// <param name="user">当前审核人</param>
        /// <param name="taskNode">任务节点</param>
        /// <param name="log">任务操作记录</param>
        /// <param name="isPass">true表示通过，false表示不通过</param>
        /// <returns></returns>
        public ResultModel AuditTaskNode(UserModel user, TaskNode taskNode, TaskOperateLog log, List<TaskAttachOperateLog> taskAttachOperateLogs, bool isPass)
        {
            ResultModel result = new ResultModel();

            try
            {
                //插入附言
                TaskOperateLogDAL taskOperateLogDAL = new TaskOperateLogDAL();
                result = taskOperateLogDAL.Insert(user, log);
                if (result.ResultStatus != 0)
                    return result;

                int logId = (int)result.ReturnValue;

                //任务附件操作记录表
                if (taskAttachOperateLogs != null && taskAttachOperateLogs.Any())
                {
                    TaskAttachOperateLogDAL taskAttachOperateLogDAL = new TaskAttachOperateLogDAL();
                    foreach (TaskAttachOperateLog TaskAttachOperateLog in taskAttachOperateLogs)
                    {
                        TaskAttachOperateLog.LogId = logId;
                        result = taskAttachOperateLogDAL.Insert(user, TaskAttachOperateLog);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }

                //修改taskNode状态
                TaskNodeDAL taskNodeDAL = new TaskNodeDAL();
                result = taskNodeDAL.Audit(user, taskNode, isPass);
                if (result.ResultStatus != 0)
                    return result;

                //获取任务节点对应的任务
                TaskDAL taskDAL = new TaskDAL();
                result = taskDAL.Get(user, taskNode.TaskId);
                if (result.ResultStatus != 0)
                    return result;
                Task task = result.ReturnValue as Task;

                //获取task对应的datasource
                DataSourceDAL dataSourceDAL = new DataSourceDAL();
                result = dataSourceDAL.Get(user, task.DataSourceId);
                if (result.ResultStatus != 0)
                    return result;
                DataSource source = result.ReturnValue as DataSource;

                if (isPass)
                {
                    //获取流程模版配置
                    FlowMasterConfigDAL flowMasterConfigDAL = new FlowMasterConfigDAL();
                    result = flowMasterConfigDAL.GetByMasterId(user, task.MasterId);
                    if (result.ResultStatus == 0)
                    {
                        FlowMasterConfig flowMasterConfig = result.ReturnValue as FlowMasterConfig;
                        //如果同级节点只需一人审核通过就行，则将其他审核人作废
                        if (!flowMasterConfig.IsSeries)
                        {
                            result = taskNodeDAL.UpdateTaskNodeStatusByLevelIdExceptSelf(user, taskNode, StatusEnum.已作废);
                            //if (result.ResultStatus != 0)
                            //    return result;
                        }
                    }

                    result = this.JudgeSameLevelSuccessHandle(user, task, taskNode.NodeLevel, source);
                    if (result.ResultStatus != 0)
                        return result;
                }
                else
                {
                    //修改任务状态(改为已生效，该任务视为结束)，并审核拒绝回调
                    result = taskDAL.Complete(user, task);
                    if (result.ResultStatus != 0)
                        return result;

                    ////修改数据源状态（改为已完成）
                    //result = dataSourceDAL.DataSourceComplete(user, source);
                    //if (result.ResultStatus != 0)
                    //    return result;
                    result = dataSourceDAL.Audit(user, source, false);
                    if (result.ResultStatus != 0)
                        return result;

                    result = RequestCallBackUrl(user, source, false);
                    if (result.ResultStatus != 0)
                        return result;

                    //获取或新增消息类型
                    SmsTypeDAL smsTypeDAL = new SmsTypeDAL();
                    result = smsTypeDAL.InsertOrGet(user, task.MasterId);
                    if (result.ResultStatus != 0)
                        return result;

                    int smsTypeId = (int)result.ReturnValue;

                    //找到该任务的发起人，给他发消息提醒
                    Sms.Model.Sms sms = new Sms.Model.Sms()
                    {
                        SmsTypeId = smsTypeId,
                        SmsHead = string.Format("{0}已被退回", task.TaskName),
                        SmsStatus = (int)SmsStatusEnum.待处理消息,
                        SmsBody = task.TaskConnext,
                        SmsRelTime = DateTime.Now,
                        SmsLevel = 1,
                        SourceId = source.RowId
                    };

                    List<SmsDetail> smsDetails = new List<SmsDetail>
                    {
                        new SmsDetail()
                        {
                            EmpId = source.EmpId, //任务的发起人
                            ReadTime = DefaultValue.DefaultTime
                        }
                    };

                    SmsDAL smsDAL = new SmsDAL();
                    result = smsDAL.AddSms(user, sms, smsDetails);
                    if (result.ResultStatus != 0)
                        return result;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
                result.ReturnValue = ex;
            }
            return result;
        }

        /// <summary>
        /// 判断同级节点是否全部通过，若通过则生成下级节点，若无下级节点则任务审核通过
        /// </summary>
        /// <param name="user"></param>
        /// <param name="task"></param>
        /// <param name="nodeLevel"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private ResultModel JudgeSameLevelSuccessHandle(UserModel user, Task task, int nodeLevel, DataSource source)
        {
            ResultModel result = new ResultModel();
            TaskDAL taskDAL = new TaskDAL();
            DataSourceDAL dataSourceDAL = new DataSourceDAL();

            try
            {
                //判断同级节点是否全部通过
                result = IsSameLevelNodeSuccess(user, task, nodeLevel);
                if (result.ResultStatus != 0)
                    return result;

                bool allSuccess = (bool)result.ReturnValue;
                if (allSuccess)
                {
                    //判断所在流程模版中是否有下一级节点或是否为最终节点
                    NodeDAL nodeDAL = new NodeDAL();
                    result = nodeDAL.Load(user, new SelectModel()
                    {
                        ColumnName = "NodeId",
                        TableName = "Wf_Node",
                        WhereStr = string.Format("MasterId = {0} and NodeLevel = {1} and NodeStatus = {2}", task.MasterId, nodeLevel + 1, (int)StatusEnum.已生效),
                        OrderStr = "NodeId",
                        PageIndex = 1,
                        PageSize = 20
                    });
                    if (result.ResultStatus != 0)
                        return result;

                    DataTable dt = result.ReturnValue as DataTable;

                    //如果存在下一级节点，则生成相应的任务节点
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = this.CreateTaskNodes(user, task, nodeLevel + 1, source);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                    else//如果不存在，则修改任务的状态，并审核通过回调
                    {
                        result = taskDAL.Complete(user, task);
                        if (result.ResultStatus != 0)
                            return result;

                        //修改数据源状态（改为已完成）
                        result = dataSourceDAL.DataSourceComplete(user, source);
                        if (result.ResultStatus != 0)
                            return result;

                        result = this.RequestCallBackUrl(user, source, true);
                        if (result.ResultStatus != 0)
                            return result;
                    }
                }
                else
                {
                    result.Message = "同级中存在未审核节点";
                    result.ReturnValue = false;
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }

            return result;
        }

        /// <summary>
        /// 创建任务节点
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="nodeLevel">节点级别</param>
        /// <returns></returns>
        public ResultModel CreateTaskNodes(UserModel user, Task task, int nodeLevel, DataSource source)
        {
            ResultModel result = new ResultModel();
            bool IsCreate = false;

            try
            {
                //通过外部请求获取当前审核数据源
                result = GetConditionUrl(source);
                if (result.ResultStatus != 0)
                    return result;
                Dictionary<string, object> conditionDic = result.ReturnValue as Dictionary<string, object>;

                NodeDAL nodeDAL = new NodeDAL();
                NodeOperateDAL nodeOperateDAL = new NodeOperateDAL();
                NodeOperate nodeOperate = null;
                TaskOperate taskOperate = null;

                DataTable dt = null;
                //获取模板中当前层级的所有节点
                lock (nodeCollection)
                {
                    string key = string.Format("{0}{1}", task.MasterId, nodeLevel);
                    if (nodeCollection.ContainsKey(key))
                        dt = nodeCollection[key];
                    else
                    {
                        SelectModel nodeSelect = new SelectModel();
                        nodeSelect.ColumnName = "*";
                        nodeSelect.OrderStr = "NodeId";
                        nodeSelect.PageIndex = 1;
                        nodeSelect.PageSize = 500;
                        nodeSelect.TableName = "dbo.Wf_Node";
                        nodeSelect.WhereStr = string.Format("MasterId={0} and NodeLevel={1} and NodeStatus = {2}", task.MasterId, nodeLevel, (int)StatusEnum.已生效);
                        result = nodeDAL.Load(user, nodeSelect);
                        if (result.ResultStatus != 0)
                            return result;
                        dt = result.ReturnValue as DataTable;
                        nodeCollection.Add(key, dt);
                    }
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    TaskNodeDAL taskNodeDAL = new TaskNodeDAL();
                    foreach (DataRow dr in dt.Rows)
                    {
                        //判断是否符合条件
                        result = JudgeNodeCondition(Convert.ToInt32(dr["NodeId"]), task.TaskId, conditionDic);
                        if (result.ResultStatus != 0)
                            return result;

                        bool judge = false;
                        if (result.ResultStatus == 0 && result.ReturnValue != null)
                            bool.TryParse(result.ReturnValue.ToString(), out judge);
                        if (judge)
                        {
                            int nodeId = Convert.ToInt32(dr["NodeId"]);

                            //获取该节点
                            result = nodeDAL.Get(user, nodeId);
                            if (result.ResultStatus != 0)
                                return result;

                            Node node = result.ReturnValue as Node;
                            if (node == null)
                                return result;

                            //判断该节点是否需要操作
                            result = nodeOperateDAL.GetByNodeId(user, node.NodeId);
                            if (result.ResultStatus == 0)
                                nodeOperate = result.ReturnValue as NodeOperate;

                            //获取审核人
                            AuditEmpDAL auditEmpDAl = new AuditEmpDAL();
                            result = auditEmpDAl.GetEmpIdsByAuditEmpId(user, node.AuditEmpId, source);
                            if (result.ResultStatus != 0)
                                return result;

                            DataTable dtvalue = result.ReturnValue as DataTable;
                            //如果不存在审核人，则转到下一个节点
                            if (dtvalue == null || dtvalue.Rows.Count < 1)
                            {
                                //result.ResultStatus = -1;
                                //return result;
                                continue;
                            }

                            //如果存在审核人，则生成相应审核节点
                            foreach (DataRow drValue in dtvalue.Rows)
                            {
                                if (Convert.ToInt32(drValue["EmpId"]) <= 0) continue;

                                TaskNode taskNode = new TaskNode()
                                {
                                    NodeLevel = nodeLevel,
                                    NodeStatus = StatusEnum.待审核,
                                    TaskId = task.Id,
                                    NodeId = nodeId,
                                    AuditTime = DefaultValue.DefaultTime,
                                    EmpId = Convert.ToInt32(drValue["EmpId"])
                                };

                                //如果在一个任务中存在相同的审核人且审核通过，则将新生成的审核人节点状态改为已生效(视为审核通过)
                                result = taskNodeDAL.GetTaskNodeIdBySameAuditEmp(user, task.TaskId, Convert.ToInt32(drValue["EmpId"]));
                                DataTable resultDatatable = result.ReturnValue as DataTable;
                                if (result.ResultStatus == 0 && resultDatatable != null && resultDatatable.Rows.Count > 0 && nodeOperate == null)
                                {
                                    taskNode.NodeStatus = StatusEnum.已生效;
                                    result = taskNodeDAL.Insert(user, taskNode);
                                    if (result.ResultStatus != 0)
                                        return result;

                                    int taskNodeIdValue = (int)result.ReturnValue;

                                    //插入附言
                                    TaskOperateLogDAL taskOperateLogDAL = new TaskOperateLogDAL();
                                    result = taskOperateLogDAL.GetLogByTaskNodeIdAndEmpId(user, Convert.ToInt32(resultDatatable.Rows[0]["TaskNodeId"]), Convert.ToInt32(drValue["EmpId"]));
                                    if (result.ResultStatus != 0)
                                        return result;

                                    TaskOperateLog taskOperateLog = result.ReturnValue as TaskOperateLog;
                                    if (taskOperateLog != null)
                                    {
                                        taskOperateLog.TaskNodeId = taskNodeIdValue;

                                        result = taskOperateLogDAL.Insert(user, taskOperateLog);
                                    }
                                    if (result.ResultStatus != 0)
                                        return result;

                                    IsCreate = true;
                                }
                                else if (result.ResultStatus == 0)//否则添加消息提醒
                                {
                                    result = taskNodeDAL.Insert(user, taskNode);
                                    if (result.ResultStatus != 0)
                                        return result;

                                    int taskNodeIdValue = (int)result.ReturnValue;

                                    if (nodeOperate != null)
                                    {
                                        taskOperate = new TaskOperate()
                                        {
                                            TaskNodeId = taskNodeIdValue,
                                            OperateUrl = nodeOperate.OperateUrl,
                                            OperateStatus = StatusEnum.已生效
                                        };

                                        TaskOperateDAL taskOperateDAL = new TaskOperateDAL();
                                        result = taskOperateDAL.Insert(user, taskOperate);
                                        if (result.ResultStatus != 0)
                                            return result;
                                    }

                                    //添加消息提醒
                                    Sms.Model.Sms sms = new Sms.Model.Sms()
                                    {
                                        SmsTypeId = 1,//to do list
                                        SmsHead = task.TaskName,
                                        SmsStatus = (int)SmsStatusEnum.待处理消息,
                                        SmsBody = task.TaskConnext,
                                        SmsRelTime = DateTime.Now,
                                        SmsLevel = 1,
                                        SourceId = taskNodeIdValue //taskNodeId
                                    };

                                    List<SmsDetail> smsDetails = new List<SmsDetail>();
                                    smsDetails.Add(new SmsDetail()
                                    {
                                        EmpId = Convert.ToInt32(drValue["EmpId"]),
                                        ReadTime = DefaultValue.DefaultTime
                                    });

                                    SmsDAL smsDAL = new SmsDAL();
                                    result = smsDAL.AddSms(user, sms, smsDetails);
                                    if (result.ResultStatus != 0)
                                        return result;

                                    IsCreate = true;
                                }
                            }
                        }
                    }

                    if (!IsCreate)
                    {
                        result.Message = "当前审核层级无审核人员";
                        result.ResultStatus = -1;

                        //递归生成下级节点
                        result = CreateTaskNodes(user, task, ++nodeLevel, source);
                    }
                    else
                    {
                        //如果创建了审核节点，则判断是否所有审核节点都通过了
                        result = this.JudgeSameLevelSuccessHandle(user, task, nodeLevel, source);
                    }
                }
                else
                {
                    result.Message = "不存在下级节点";
                    result.ResultStatus = -1;

                    //是否判断在无下级节点后审核流程结束？审核结果为通过？

                    TaskDAL taskDAL = new TaskDAL();
                    result = taskDAL.Complete(user, task);
                    if (result.ResultStatus != 0)
                        return result;

                    //修改数据源状态（改为已完成）
                    DataSourceDAL dataSourceDAL = new DataSourceDAL();
                    result = dataSourceDAL.DataSourceComplete(user, source);
                    if (result.ResultStatus != 0)
                        return result;

                    result = this.RequestCallBackUrl(user, source, true);
                    if (result.ResultStatus != 0)
                        return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ReturnValue = ex;
                result.ResultStatus = -1;
            }

            return result;
        }

        /// <summary>
        /// 判断节点条件是否符合,True表示符合，false表示不符合
        /// </summary>
        /// <param name="NodeId">节点Id</param>
        /// <param name="TaskId">任务Id</param>
        /// <param name="conditionDic"></param>
        /// <returns></returns>
        private ResultModel JudgeNodeCondition(int NodeId, Dictionary<string, object> conditionDic)
        {
            return JudgeNodeCondition(NodeId, 0, conditionDic);
        }

        /// <summary>
        /// 判断节点条件是否符合,True表示符合，false表示不符合
        /// </summary>
        /// <param name="NodeId">节点Id</param>
        /// <param name="TaskId">任务Id</param>
        /// <param name="conditionDic"></param>
        /// <returns></returns>
        private ResultModel JudgeNodeCondition(int NodeId, int TaskId, Dictionary<string, object> conditionDic)
        {
            ResultModel result = new ResultModel();

            try
            {
                //加载节点下的所有判断条件
                DataTable conditionTable = null;
                lock (conditionCollection)
                {
                    if (conditionCollection.ContainsKey(NodeId))
                        conditionTable = conditionCollection[NodeId];
                    else
                    {
                        NodeConditionBLL bll = new NodeConditionBLL();
                        SelectModel select = new SelectModel();
                        select.ColumnName = "*";
                        select.OrderStr = " ConditionId Asc";
                        select.PageIndex = 1;
                        select.PageSize = 500;
                        select.TableName = "NFMT_WorkFlow.dbo.Wf_NodeCondition";
                        select.WhereStr = string.Format(" NodeId = {0} and ConditionStatus = {1} ", NodeId, (int)StatusEnum.已生效);
                        result = bll.Load(new UserModel(), select);
                        //如果加载判断条件失败，直接判断条件返回结果为false
                        if (result.ResultStatus != 0)
                            return result;

                        conditionTable = result.ReturnValue as DataTable;
                        conditionCollection.Add(NodeId, conditionTable);
                    }
                }

                StringBuilder sb = new StringBuilder();//动态编译字符串
                if (conditionTable != null && conditionTable.Rows.Count > 0)
                {
                    for (int i = 0; i < conditionTable.Rows.Count; i++)
                    {
                        DataRow dr = conditionTable.Rows[i];

                        int conditionStatus = (int)dr["ConditionStatus"];
                        if (conditionStatus == (int)StatusEnum.已生效)
                        {
                            string fieldName = dr["FieldName"].ToString();
                            string fieldValue = dr["fieldValue"].ToString();
                            ConditionType conditionType = (ConditionType)dr["ConditionType"];
                            LogicType logicType = (LogicType)dr["LogicType"];

                            //如果获取外部数据中没有提供比对的字段，则判断条件返回结果为false
                            if (!conditionDic.ContainsKey(fieldName))
                                return result;

                            if (i != 0)
                                sb.AppendFormat(" {0} ", Utility.GetLogicType(logicType));

                            if (conditionType == ConditionType.等于)
                                sb.AppendFormat("(\"{0}\" {1} \"{2}\")", fieldValue, Utility.GetConditionType(conditionType), conditionDic[fieldName]);
                            else
                                sb.AppendFormat("({0} {1} {2})", conditionDic[fieldName], Utility.GetConditionType(conditionType), fieldValue);
                        }
                    }
                }
                else
                {
                    result.ResultStatus = 0;
                    result.ReturnValue = true;
                    result.Message = "当前节点下没有判断条件";
                    return result;//如果节点下没有判断条件，则判断条件返回结果为true
                }

                object obj = Utility.Calculate(sb.ToString(), "bool", new object());
                if (obj != null)
                {
                    bool flag = false;
                    if (bool.TryParse(obj.ToString(), out flag))
                        result.ReturnValue = flag;
                    else
                        result.ReturnValue = false;
                    result.ResultStatus = 0;
                    result.Message = "满足条件";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ReturnValue = false;
            }

            return result;
        }

        /// <summary>
        /// 任务同级节点是否全部通过
        /// </summary>
        /// <param name="user"></param>
        /// <param name="task">任务</param>
        /// <param name="nodeLevel">节点级别</param>
        /// <returns></returns>
        public ResultModel IsSameLevelNodeSuccess(UserModel user, Task task, int nodeLevel)
        {
            ResultModel result = new ResultModel();

            try
            {
                SelectModel select = new SelectModel();
                select.ColumnName = "tn.TaskNodeId";
                select.OrderStr = "tn.TaskNodeId";
                select.PageIndex = 1;
                select.TableName = string.Format(" dbo.Wf_TaskNode tn inner join dbo.Wf_Node n on tn.NodeId = n.NodeId and n.NodeType <> {0} ", DetailProvider.Details(StyleEnum.NodeType)["Notify"].StyleDetailId);
                select.WhereStr = string.Format(" tn.NodeLevel ={0} and tn.NodeStatus ={1} and tn.TaskId = {2} ", nodeLevel, (int)StatusEnum.待审核, task.Id);
                select.PageSize = 20;

                TaskNodeDAL dal = new TaskNodeDAL();
                result = dal.Load(user, select);
                if (result.ResultStatus != 0)
                    return result;

                DataTable dt = new DataTable();
                if (result.ReturnValue != null)
                {
                    dt = (DataTable)result.ReturnValue;
                    result.AffectCount = dt.Rows.Count;
                }
                else
                {
                    result.ReturnValue = false;
                    return result;
                }

                result.Message = "查询成功";
                result.ResultStatus = 0;

                if (dt.Rows.Count > 0)
                    result.ReturnValue = false;
                else
                    result.ReturnValue = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ReturnValue = ex;
            }

            return result;
        }

        /// <summary>
        /// 任务同级节点是否有一个通过
        /// </summary>
        /// <param name="user"></param>
        /// <param name="task">任务</param>
        /// <param name="nodeLevel">节点级别</param>
        /// <returns></returns>
        public ResultModel IsSameLevelNodeSuccessOnce(UserModel user, Task task, int nodeLevel)
        {
            ResultModel result = new ResultModel();

            try
            {
                SelectModel select = new SelectModel();
                select.ColumnName = "tn.TaskNodeId";
                select.OrderStr = "tn.TaskNodeId";
                select.PageIndex = 1;
                select.TableName = string.Format(" dbo.Wf_TaskNode tn inner join dbo.Wf_Node n on tn.NodeId = n.NodeId and n.NodeType <> {0} ", DetailProvider.Details(StyleEnum.NodeType)["Notify"].StyleDetailId);
                select.WhereStr = string.Format(" tn.NodeLevel ={0} and tn.NodeStatus ={1} and tn.TaskId = {2} ", nodeLevel, (int)StatusEnum.已生效, task.Id);
                select.PageSize = 20;

                TaskNodeDAL dal = new TaskNodeDAL();
                result = dal.Load(user, select);
                if (result.ResultStatus != 0)
                    return result;

                DataTable dt = new DataTable();
                if (result.ReturnValue != null)
                {
                    dt = (DataTable)result.ReturnValue;
                    result.AffectCount = dt.Rows.Count;
                }
                else
                {
                    result.ReturnValue = false;
                    return result;
                }

                result.Message = "查询成功";
                result.ResultStatus = 0;

                if (dt.Rows.Count > 0)
                    result.ReturnValue = true;
                else
                    result.ReturnValue = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ReturnValue = ex;
            }

            return result;
        }

        /// <summary>
        /// 请求回调,若通过，则判断该任务的所有附件是否都被审核。
        /// </summary>
        /// <param name="user"></param>
        /// <param name="source">数据源对象</param>
        /// <param name="isPass">是否通过。true表示通过；false表示不通过</param>
        /// <returns></returns>
        public ResultModel RequestCallBackUrl(UserModel user, DataSource source, bool isPass)
        {
            ResultModel result = new ResultModel();

            string msg = string.Empty;
            string callBackUrl = string.Empty;

            //设置URL
            if (isPass)
            {
                #region 若通过，则判断该任务的所有附件是否都被审核。

                TaskDAL taskDAL = new TaskDAL();
                result = taskDAL.GetTaskByDataSourceId(user, source.SourceId);
                if (result.ResultStatus != 0)
                    return result;

                Task task = result.ReturnValue as Task;
                if (task == null || task.TaskId <= 0)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取任务失败";
                    return result;
                }

                //获取任务附件
                TaskAttachDAL taskAttachDAL = new TaskAttachDAL();
                result = taskAttachDAL.GetTaskAttachByTaskId(user, task.TaskId);
                if (result.ResultStatus != 0)
                    return result;

                List<TaskAttach> taskAttachs = result.ReturnValue as List<TaskAttach>;
                if (taskAttachs != null && taskAttachs.Any())
                {
                    //若存在任务附件，则判断是否全部被审核
                    result = taskAttachDAL.JudgeAllAttachsAudited(user, task.TaskId);
                    if (result.ResultStatus != 0)
                        return result;
                }

                #endregion

                msg = "审核通过";
                if (!string.IsNullOrEmpty(source.SuccessUrl))
                    callBackUrl = string.Format("{0}{1}", DefaultValue.NfmtSiteName, source.SuccessUrl);
                else
                    callBackUrl = "";
            }
            else
            {
                msg = "审核拒绝";
                if (!string.IsNullOrEmpty(source.RefusalUrl))
                    callBackUrl = string.Format("{0}{1}", DefaultValue.NfmtSiteName, source.RefusalUrl);
                else
                    callBackUrl = "";
            }

            //create post data
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonData = serializer.Serialize(source);

            //create cookie
            CookieCollection col = new CookieCollection();

            if (HttpContext.Current.Request.Cookies != null)//判断条件后改为key=MK的cookie判断
            {
                for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)//each (System.Web.HttpCookie c in System.Web.HttpContext.Current.Request.Cookies)
                {
                    HttpCookie c = HttpContext.Current.Request.Cookies[i];
                    if (c.Domain == DefaultValue.Domain)
                    {
                        Cookie cookie = new Cookie();
                        cookie.Domain = c.Domain;
                        cookie.Expires = c.Expires;
                        cookie.Name = c.Name;
                        cookie.Path = c.Path;
                        cookie.Secure = c.Secure;
                        cookie.Value = c.Value;
                        cookie.HttpOnly = c.HttpOnly;

                        col.Add(cookie);
                    }
                }
            }
            else
            {
                //手动创建cookie
                //验证登录
            }
            CookieContainer container = new CookieContainer();
            container.Add(col);

            string resource = string.Format("source={0}&ispass={1}", jsonData, isPass.ToString());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(callBackUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = Encoding.UTF8.GetByteCount(resource);
            request.CookieContainer = container;

            Stream requestStream = request.GetRequestStream();
            //System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(requestStream, Encoding.GetEncoding("utf-8"));
            StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.GetEncoding("GBK"));
            streamWriter.Write(resource);
            streamWriter.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            response.Cookies = container.GetCookies(response.ResponseUri);
            Stream responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string retString = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();

                result = serializer.Deserialize<ResultModel>(retString);
            }

            if (result.ResultStatus == 0)
            {
                //操作成功
                result.Message = string.Format("{0}成功", msg);
                result.ResultStatus = 0;
            }
            else
            {
                //操作失败
                result.ResultStatus = -1;
                result.Message = string.Format("{0}失败", msg);
            }

            return result;
        }

        /// <summary>
        /// 获取当前DataSource对应的ConditionDictionary
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private ResultModel GetConditionUrl(DataSource source)
        {
            ResultModel result = new ResultModel();

            string url = source.ConditionUrl;
            if (string.IsNullOrEmpty(url))
                url = "BasicData/Handler/ConditionHandler.ashx";

            string conditionUrl = string.Format("{0}{1}", DefaultValue.NfmtSiteName, url);

            //create post data
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonData = serializer.Serialize(source);

            //create cookie
            CookieCollection col = new CookieCollection();

            if (HttpContext.Current.Request.Cookies != null)//判断条件后改为key=MK的cookie判断
            {
                for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)//each (System.Web.HttpCookie c in System.Web.HttpContext.Current.Request.Cookies)
                {
                    HttpCookie c = HttpContext.Current.Request.Cookies[i];

                    if (c.Domain == DefaultValue.Domain)
                    {
                        Cookie cookie = new Cookie();
                        cookie.Domain = c.Domain;
                        cookie.Expires = c.Expires;
                        cookie.Name = c.Name;
                        cookie.Path = c.Path;
                        cookie.Secure = c.Secure;
                        cookie.Value = c.Value;
                        cookie.HttpOnly = c.HttpOnly;

                        col.Add(cookie);
                    }
                }
            }
            else
            {
                //手动创建cookie
                //验证登录
            }
            CookieContainer container = new CookieContainer();
            container.Add(col);

            string resource = string.Format("source={0}", jsonData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(conditionUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = Encoding.UTF8.GetByteCount(resource);
            request.CookieContainer = container;

            Stream requestStream = request.GetRequestStream();
            //System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(requestStream, Encoding.GetEncoding("utf-8"));
            StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.GetEncoding("GBK"));
            streamWriter.Write(resource);
            streamWriter.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            response.Cookies = container.GetCookies(response.ResponseUri);
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string retString = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();

            result = serializer.Deserialize<ResultModel>(retString);

            return result;
        }

        /// <summary>
        /// 请求回调,若通过，则判断该任务的所有附件是否都被审核。
        /// </summary>
        /// <param name="source">数据源对象</param>
        /// <param name="isPass">是否通过。true表示通过；false表示不通过</param>
        /// <returns></returns>
        private ResultModel RequestCallBackUrlForPass(UserModel user, DataSource source, bool isPass)
        {
            ResultModel result = new ResultModel();

            string msg = string.Empty;
            string callBackUrl = string.Empty;

            //设置URL
            if (isPass)
            {
                msg = "审核通过";
                if (!string.IsNullOrEmpty(source.SuccessUrl))
                    callBackUrl = string.Format("{0}{1}", DefaultValue.NfmtSiteName, source.SuccessUrl);
                else
                    callBackUrl = "";
            }
            else
            {
                msg = "审核拒绝";
                if (!string.IsNullOrEmpty(source.RefusalUrl))
                    callBackUrl = string.Format("{0}{1}", DefaultValue.NfmtSiteName, source.RefusalUrl);
                else
                    callBackUrl = "";
            }

            //create post data
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonData = serializer.Serialize(source);

            //create cookie
            CookieCollection col = new CookieCollection();

            if (HttpContext.Current.Request.Cookies != null)//判断条件后改为key=MK的cookie判断
            {
                for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)//each (System.Web.HttpCookie c in System.Web.HttpContext.Current.Request.Cookies)
                {
                    HttpCookie c = HttpContext.Current.Request.Cookies[i];
                    if (c.Domain == DefaultValue.Domain)
                    {
                        Cookie cookie = new Cookie();
                        cookie.Domain = c.Domain;
                        cookie.Expires = c.Expires;
                        cookie.Name = c.Name;
                        cookie.Path = c.Path;
                        cookie.Secure = c.Secure;
                        cookie.Value = c.Value;
                        cookie.HttpOnly = c.HttpOnly;

                        col.Add(cookie);
                    }
                }
            }
            else
            {
                //手动创建cookie
                //验证登录
            }
            CookieContainer container = new CookieContainer();
            container.Add(col);

            string resource = string.Format("source={0}&ispass={1}", jsonData, isPass.ToString());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(callBackUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = Encoding.UTF8.GetByteCount(resource);
            request.CookieContainer = container;

            Stream requestStream = request.GetRequestStream();
            //System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(requestStream, Encoding.GetEncoding("utf-8"));
            StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.GetEncoding("GBK"));
            streamWriter.Write(resource);
            streamWriter.Close();

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            response.Cookies = container.GetCookies(response.ResponseUri);
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string retString = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();

            result = serializer.Deserialize<ResultModel>(retString);

            if (result.ResultStatus == 0)
            {
                //操作成功
                result.Message = string.Format("{0}成功", msg);
                result.ResultStatus = 0;
            }
            else
            {
                //操作失败
                result.ResultStatus = -1;
                result.Message = string.Format("{0}失败", msg);
            }

            return result;
        }
    }
}