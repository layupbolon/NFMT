using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.WorkFlow.Handler
{
    /// <summary>
    /// SubmitAuditHandler 的摘要说明
    /// </summary>
    public class SubmitAuditHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;

            context.Response.ContentType = "text/plain";

            int masterId = 0;
            if (string.IsNullOrEmpty(context.Request.Form["mid"]) || !int.TryParse(context.Request.Form["mid"], out masterId) || masterId <= 0)
            {
                context.Response.Write("流程模版序号错误");
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["model"]))
            {
                context.Response.Write("实体不可为空");
                context.Response.End();
            }

            string taskName = context.Request.Form["tname"];
            if (string.IsNullOrEmpty(taskName))
            {
                context.Response.Write("任务名称不可为空");
                context.Response.End();
            }

            string taskConnext = context.Request.Form["tConnext"];
            if (string.IsNullOrEmpty(taskConnext))
            {
                context.Response.Write("任务内容不可为空");
                context.Response.End();
            }

            var obj = serializer.Deserialize<NFMT.Common.AuditModel>(context.Request.Form["model"]);
            if (obj == null)
            {
                context.Response.Write("实体转换出错");
                context.Response.End();
            }

            //if (obj.Status != NFMT.Common.StatusEnum.已录入)
            //{
            //    context.Response.Write("非已录入状态下不能提交审核");
            //    context.Response.End();
            //}

            NFMT.WorkFlow.BLL.FlowMasterBLL flowMasterBLL = new NFMT.WorkFlow.BLL.FlowMasterBLL();
            NFMT.Common.ResultModel result = flowMasterBLL.Get(user, masterId);
            if (result.ResultStatus != 0)
            {
                context.Response.Write(result.Message);
                context.Response.End();
            }

            NFMT.WorkFlow.Model.FlowMaster flowMaster = result.ReturnValue as NFMT.WorkFlow.Model.FlowMaster;

            NFMT.WorkFlow.Model.DataSource source = new NFMT.WorkFlow.Model.DataSource()
            {
                BaseName = obj.DataBaseName,
                TableCode = obj.TableName,
                DataStatus = NFMT.Common.StatusEnum.待审核,
                RowId = obj.Id,
                ViewUrl = flowMaster.ViewUrl,
                EmpId = user.EmpId,
                ApplyTime = DateTime.Now,
                ApplyTitle = string.Empty,
                ApplyMemo = string.Empty,
                ApplyInfo = string.Empty,
                ConditionUrl = flowMaster.ConditionUrl,
                RefusalUrl = flowMaster.RefusalUrl,
                SuccessUrl = flowMaster.SuccessUrl,
                DalName = obj.DalName,
                AssName = obj.AssName
            };

            NFMT.WorkFlow.Model.Task task = new NFMT.WorkFlow.Model.Task()
            {
                MasterId = masterId,
                TaskName = taskName,
                TaskConnext = taskConnext
            };

            NFMT.WorkFlow.FlowOperate flowOperate = new NFMT.WorkFlow.FlowOperate();
            result = flowOperate.AuditAndCreateTask(user, obj, flowMaster, source, task);
            if (result.ResultStatus == 0)
                context.Response.Write("提交审核成功");
            else
                context.Response.Write(result.Message);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}