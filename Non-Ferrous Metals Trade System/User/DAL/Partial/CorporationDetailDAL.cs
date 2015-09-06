using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFMT.User.Model;
using NFMT.DBUtility;
using NFMT.User.IDAL;
using NFMT.Common;

namespace NFMT.User.DAL
{
    public partial class CorporationDetailDAL : DataOperate, ICorporationDetailDAL
    {
        #region 新增方法

        public ResultModel GetByCorpId(UserModel user, int corpId)
        {
            ResultModel result = new ResultModel();

            string sql = string.Format("select * from dbo.CorporationDetail where CorpId = {0} and DetailStatus <> {1} ", corpId, (int)Common.StatusEnum.已作废);
            result = this.Load<Model.CorporationDetail>(user, System.Data.CommandType.Text, sql);
            List<Model.CorporationDetail> corpDetails = result.ReturnValue as List<Model.CorporationDetail>;
            if (corpDetails == null || !corpDetails.Any())
            {
                result.ResultStatus = -1;
                result.Message = "获取失败";
            }
            else
            {
                result.ResultStatus = 0;
                result.Message = "获取成功";
                result.ReturnValue = corpDetails.First();
            }

            return result;
        }

        #endregion
    }
}
