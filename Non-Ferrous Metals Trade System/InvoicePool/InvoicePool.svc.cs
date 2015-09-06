using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace InvoicePool
{
    public class Service1 : IInvoice
    {
        private NFMT.Invoice.BLL.InvoiceBLL invoiceBLL = new NFMT.Invoice.BLL.InvoiceBLL();

        /// <summary>
        /// 发票开具，返回发票序号
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="invoice">发票</param>
        /// <param name="invoiceAttach">发票附件</param>
        /// <returns></returns>
        public int InvoiceIssue(NFMT.Common.UserModel user, NFMT.Invoice.Model.Invoice invoice, List<NFMT.Invoice.Model.InvoiceAttach> invoiceAttachs)
        {
            NFMT.Common.ResultModel result = invoiceBLL.InvoiceHandle(user, invoice, invoiceAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.发票方向)["开具"]);
            if (result.ResultStatus != 0)
                return -1;
            else
                return (int)result.ReturnValue;
        }

        /// <summary>
        /// 发票收取，返回发票序号
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="invoice">发票</param>
        /// <param name="invoiceAttach">发票附件</param>
        /// <returns></returns>
        public int InvoiceReceive(NFMT.Common.UserModel user, NFMT.Invoice.Model.Invoice invoice, List<NFMT.Invoice.Model.InvoiceAttach> invoiceAttachs)
        {
            NFMT.Common.ResultModel result = invoiceBLL.InvoiceHandle(user, invoice, invoiceAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.发票方向)["收取"]);
            if (result.ResultStatus != 0)
                return -1;
            else
                return (int)result.ReturnValue;
        }

        /// <summary>
        /// 发票作废,返回发票序号
        /// </summary>
        /// <param name="user"></param>
        /// <param name="invoice"></param>
        /// <param name="invoiceAttachs"></param>
        /// <returns></returns>
        public int InvoiceInvalid(NFMT.Common.UserModel user, NFMT.Invoice.Model.Invoice invoice, List<NFMT.Invoice.Model.InvoiceAttach> invoiceAttachs)
        {
            NFMT.Common.ResultModel result = invoiceBLL.InvoiceHandle(user, invoice, invoiceAttachs, NFMT.Data.DetailProvider.Details(NFMT.Data.StyleEnum.发票方向)["作废"]);
            if (result.ResultStatus != 0)
                return -1;
            else
                return (int)result.ReturnValue;
        }
    }
}
