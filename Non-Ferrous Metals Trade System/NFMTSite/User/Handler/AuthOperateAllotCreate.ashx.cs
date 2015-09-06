using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFMTSite.User.Handler
{
    /// <summary>
    /// AuthOperateAllotCreate 的摘要说明
    /// </summary>
    public class AuthOperateAllotCreate : IHttpHandler
    {
        public List<AllotInfo> infos = null;
        public int empId = 0;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            string selectStr = context.Request.Form["select"];
            if (string.IsNullOrEmpty(selectStr))
            {
                result.ResultStatus = -1;
                result.Message = "菜单不能为空";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            if (string.IsNullOrEmpty(context.Request.Form["empId"]) || !int.TryParse(context.Request.Form["empId"], out empId) || empId <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "员工信息错误";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                context.Response.End();
            }

            string exceptItemIds = context.Request.Form["exceptItemIds"];

            try
            {
                NFMT.User.BLL.AuthOperateBLL bll = new NFMT.User.BLL.AuthOperateBLL();
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                infos = serializer.Deserialize<List<AllotInfo>>(selectStr);
                if (infos == null || !infos.Any())
                {
                    result = bll.InvalidAll(user, empId, exceptItemIds);
                    if (result.ResultStatus == 0)
                        result.Message = "分配成功";
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    context.Response.End();
                }
                List<NFMT.User.Model.AuthOperate> authOperates = new List<NFMT.User.Model.AuthOperate>();
                List<NFMT.User.Model.EmpMenu> empMenus = new List<NFMT.User.Model.EmpMenu>();

                foreach (AllotInfo info in infos)
                {
                    if (!string.IsNullOrEmpty(info.id) && info.id.StartsWith("op_"))
                    {
                        //AddParentInfo(info, ref authOperates, ref empMenus);

                        authOperates.Add(new NFMT.User.Model.AuthOperate()
                        {
                            //OperateCode = string.Format("{0}-{1}", parentInfo.label, info.label),
                            //OperateName = string.Format("{0}-{1}", parentInfo.label, info.label),
                            OperateType = Convert.ToInt32(info.id.Split('_')[2]),
                            MenuId = Convert.ToInt32(info.parentId),
                            EmpId = empId,
                            AuthOperateStatus = NFMT.Common.StatusEnum.已生效
                        });

                        if (!empMenus.Any(a => a.MenuId == Convert.ToInt32(info.parentId)))
                        {
                            empMenus.Add(new NFMT.User.Model.EmpMenu()
                            {
                                EmpId = empId,
                                MenuId = Convert.ToInt32(info.parentId),
                                RefStatus = NFMT.Common.StatusEnum.已生效
                            });
                        }
                    }
                    else if (!string.IsNullOrEmpty(info.id))
                    {
                        int i;
                        if (!int.TryParse(info.id, out i))
                            continue;

                        if (!empMenus.Any(a => a.MenuId == Convert.ToInt32(info.id)))
                        {
                            empMenus.Add(new NFMT.User.Model.EmpMenu()
                            {
                                EmpId = empId,
                                MenuId = Convert.ToInt32(info.id),
                                RefStatus = NFMT.Common.StatusEnum.已生效
                            });
                        }
                    }
                }

                //if (authOperates == null || !authOperates.Any())
                //{
                //    result.ResultStatus = -1;
                //    result.Message = "数据转换错误";
                //    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                //    context.Response.End();
                //}

                result = bll.Create(user, empId, authOperates, empMenus, exceptItemIds);
                if (result.ResultStatus == 0)
                {
                    result.Message = "分配成功";
                }
                //NFMT.User.MenuProvider.RefreshByEmpId(empId);
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class AllotInfo
        {
            /// <summary>
            /// 菜单名称或操作权限名称
            /// </summary>
            public string label { get; set; }

            /// <summary>
            /// 菜单id或权限id  其中以op_开头的为操作权限
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// 上级id
            /// </summary>
            public string parentId { get; set; }

            /// <summary>
            /// 0是一级菜单，1是二级菜单，2是操作权限
            /// </summary>
            public int level { get; set; }
        }

        private void AddParentInfo(AllotInfo info, ref List<NFMT.User.Model.AuthOperate> authOperates, ref List<NFMT.User.Model.EmpMenu> empMenus)
        {
            if (info.parentId == "0") return;

            AllotInfo parentInfo = infos.SingleOrDefault(a => a.id == info.parentId);
            if (parentInfo == null)
            {
                empMenus.Add(new NFMT.User.Model.EmpMenu()
                    {
                        EmpId = empId,
                        MenuId = Convert.ToInt32(info.parentId),
                        RefStatus = NFMT.Common.StatusEnum.已生效
                    });
            }
            
            AddParentInfo(parentInfo, ref authOperates, ref empMenus);
        }
    }
}