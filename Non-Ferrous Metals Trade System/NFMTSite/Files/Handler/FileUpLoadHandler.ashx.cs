using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace NFMTSite.Files.Handler
{
    /// <summary>
    /// FileUpLoadHandler 的摘要说明
    /// </summary>
    public class FileUpLoadHandler : IHttpHandler
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(FileUpLoadHandler));

        public void ProcessRequest(HttpContext context)
        {
            NFMT.Common.UserModel user = Utility.UserUtility.CurrentUser;
            NFMT.Common.ResultModel result = new NFMT.Common.ResultModel();

            try
            {
                int id = 0;
                if (string.IsNullOrEmpty(context.Request.QueryString["id"]) || !int.TryParse(context.Request.QueryString["id"], out id) || id <= 0)
                {
                    context.Response.Write("序号错误");
                    context.Response.End();
                }

                int type = 0;
                if (string.IsNullOrEmpty(context.Request.QueryString["t"]) || !int.TryParse(context.Request.QueryString["t"], out type) || type <= 0)
                {
                    context.Response.Write("类型错误");
                    context.Response.End();
                }

                NFMT.Common.IAttachModel obj = null;
                NFMT.Common.IOperate operate = null;
                string dir = string.Empty;

                switch (type)
                {
                    case (int)NFMT.Operate.AttachType.CashInAttach:
                        obj = new NFMT.Funds.Model.ReceivableAttach();
                        operate = new NFMT.Funds.DAL.ReceivableAttachDAL();
                        dir = "CashIn";
                        break;
                    case (int)NFMT.Operate.AttachType.ContractAttach:
                        obj = new NFMT.Contract.Model.ContractAttach();
                        operate = new NFMT.Contract.DAL.ContractAttachDAL();
                        dir = "Contract";
                        break;
                    case (int)NFMT.Operate.AttachType.InvoiceAttach:
                        obj = new NFMT.Invoice.Model.InvoiceAttach();
                        operate = new NFMT.Invoice.DAL.InvoiceAttachDAL();
                        dir = "Invoice";
                        break;
                    case (int)NFMT.Operate.AttachType.PaymentAttach:
                        obj = new NFMT.Funds.Model.PaymentAttach();
                        operate = new NFMT.Funds.DAL.PaymentAttachDAL();
                        dir = "Payment";
                        break;
                    case (int)NFMT.Operate.AttachType.StockAttach:
                        obj = new NFMT.WareHouse.Model.StocktAttach();
                        operate = new NFMT.WareHouse.DAL.StocktAttachDAL();
                        dir = "Stock";
                        break;
                    case (int)NFMT.Operate.AttachType.StockInAttach:
                    case (int)NFMT.Operate.AttachType.BillAttach:
                        obj = new NFMT.WareHouse.Model.StockInAttach((NFMT.Operate.AttachType)type);
                        operate = new NFMT.WareHouse.DAL.StockInAttachDAL();
                        dir = "StockIn";
                        break;
                    case (int)NFMT.Operate.AttachType.StockLogAttach:
                        obj = new NFMT.WareHouse.Model.StockLogAttach();
                        operate = new NFMT.WareHouse.DAL.StockInAttachDAL();
                        dir = "StockLog";
                        break;
                    case (int)NFMT.Operate.AttachType.StockOutAttach:
                        obj = new NFMT.WareHouse.Model.StockOutAttach();
                        operate = new NFMT.WareHouse.DAL.StockOutAttachDAL();
                        dir = "StockOut";
                        break;
                    case (int)NFMT.Operate.AttachType.SplitDocAttach:
                        obj = new NFMT.WareHouse.Model.SplitDocAttach();
                        operate = new NFMT.WareHouse.DAL.SplitDocAttachDAL();
                        dir = "SplitDoc";
                        break;
                    case (int)NFMT.Operate.AttachType.CustomApplyAttach:
                        obj = new NFMT.WareHouse.Model.CustomsApplyAttach();
                        operate = new NFMT.WareHouse.DAL.CustomsApplyAttachDAL();
                        dir = "CustomApply";
                        break;
                    case (int)NFMT.Operate.AttachType.CustomAttach:
                        obj = new NFMT.WareHouse.Model.CustomsAttach();
                        operate = new NFMT.WareHouse.DAL.CustomsAttachDAL();
                        dir = "Custom";
                        break;
                    case (int)NFMT.Operate.AttachType.SubAttach:
                        obj = new NFMT.Contract.Model.ContractSubAttach();
                        operate = new NFMT.Contract.DAL.ContractSubAttachDAL();
                        dir = "Sub";
                        break;
                    case (int)NFMT.Operate.AttachType.OrderAttach:
                        obj = new NFMT.Document.Model.DocumentOrderAttach();
                        operate = new NFMT.Document.DAL.DocumentOrderAttachDAL();
                        dir = "DocumentOrder";
                        break;
                    case (int)NFMT.Operate.AttachType.PledgeAttach:
                        obj = new NFMT.WareHouse.Model.PledgeAttach();
                        operate = new NFMT.WareHouse.DAL.PledgeAttachDAL();
                        dir = "Pledge";
                        break;
                    case (int)NFMT.Operate.AttachType.BusinessLiceneseAttach:
                    case (int)NFMT.Operate.AttachType.TaxAttach:
                    case (int)NFMT.Operate.AttachType.OrganizationAttach:
                    case (int)NFMT.Operate.AttachType.CertifyAttach:
                        obj = new NFMT.User.Model.CorpDetailAttach((NFMT.Operate.AttachType)type);
                        operate = new NFMT.User.DAL.CorpDetailAttachDAL();
                        dir = "CorpDetail";
                        break;
                    case (int)NFMT.Operate.AttachType.采购双签合同:
                    case (int)NFMT.Operate.AttachType.采购合同:
                    case (int)NFMT.Operate.AttachType.在库提单附件:
                    case (int)NFMT.Operate.AttachType.在途提单附件:
                    case (int)NFMT.Operate.AttachType.发票扫描件:
                    case (int)NFMT.Operate.AttachType.点价确认单:
                    case (int)NFMT.Operate.AttachType.合同结算单:
                    case (int)NFMT.Operate.AttachType.费用明细清单:
                        obj = new NFMT.Funds.Model.PayApplyAttach((NFMT.Operate.AttachType)type);
                        operate = new NFMT.Funds.DAL.PayApplyAttachDAL();
                        dir = "PayApply";
                        break;
                    default:
                        break;
                }

                string path = context.Server.MapPath("../");
                if (!string.IsNullOrEmpty(dir))
                    path = string.Format("{0}{1}", path, dir);

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(path);
                    dirInfo.Create();
                }

                HttpFileCollection files = context.Request.Files;
                List<NFMT.Operate.Model.Attach> attachs = new List<NFMT.Operate.Model.Attach>();

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile postFile = files[i];
                        string newFileName = DateTime.Now.Ticks.ToString() + Guid.NewGuid().ToString() + new Random().Next().ToString() + System.IO.Path.GetExtension(postFile.FileName).ToLower();
                        //string fileName = string.Format("{0}{1}", path, newFileName);
                        string fileName = path + "\\" + newFileName;
                        postFile.SaveAs(fileName);

                        string attachPath = string.Format("../Files/{0}/{1}", dir, newFileName);

                        attachs.Add(new NFMT.Operate.Model.Attach()
                            {
                                AttachName = postFile.FileName,
                                ServerAttachName = fileName,
                                AttachExt = System.IO.Path.GetExtension(fileName).ToLower(),
                                AttachType = type,
                                AttachInfo = string.Empty,
                                AttachLength = postFile.ContentLength,
                                AttachPath = attachPath,
                                AttachStatus = NFMT.Common.StatusEnum.已生效
                            });
                    }

                    NFMT.Operate.BLL.AttachBLL bll = new NFMT.Operate.BLL.AttachBLL();
                    result = bll.AddAttach(user, attachs, id, obj, operate);
                }
                else
                {
                    result.ResultStatus = -1;
                    result.Message = "无附件上传";
                }
            }
            catch (Exception e)
            {
                result.ResultStatus = -1;
                result.Message = e.Message;
            }
            finally
            {
                if (result.ResultStatus != 0)
                    this.log.ErrorFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
                else if (this.log.IsInfoEnabled)
                    this.log.InfoFormat("{0} {1}，序号:{2}", user.EmpName, result.Message, result.ReturnValue);
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            context.Response.End();
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