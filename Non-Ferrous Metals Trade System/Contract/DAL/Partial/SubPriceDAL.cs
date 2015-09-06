using NFMT.Common;
using NFMT.Contract.IDAL;
using NFMT.Contract.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Contract.DAL
{
    public partial class SubPriceDAL:DataOperate,ISubPriceDAL
    {
        #region 新增方法

        public ResultModel GetPriceBySubId(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            if (subId < 1)
            {
                result.Message = "合约序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@subId", SqlDbType.Int, 4);
            para.Value = subId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.Con_SubPrice where SubId =@subId";
                result = Get(user, CommandType.Text, cmdText, paras.ToArray());
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }

            return result;
        }

        #endregion
    }
}
