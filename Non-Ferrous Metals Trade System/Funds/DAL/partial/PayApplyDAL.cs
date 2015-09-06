using NFMT.Common;
using NFMT.DBUtility;
using NFMT.Funds.IDAL;
using NFMT.Funds.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.Funds.DAL
{
    public partial class PayApplyDAL : ApplyOperate, IPayApplyDAL
    {
        #region 新增方法

        public override ResultModel Update(UserModel user, IModel obj)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (obj == null)
                {
                    result.Message = "更新对象不能为null";
                    return result;
                }

                obj.LastModifyId = user.EmpId;

                int i = SqlHelper.ExecuteNonQuery(ConnectString, CommandType.StoredProcedure, string.Format("{0}Update", obj.TableName), CreateUpdateParameters(obj).ToArray());

                if (i == 1)
                {
                    result.Message = "更新成功";
                    result.ResultStatus = 0;
                }
                else
                    result.Message = "更新失败";

                result.AffectCount = i;
                result.ReturnValue = i;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public ResultModel GetByApplyId(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            if (applyId < 1)
            {
                result.Message = "序号不能小于1";
                return result;
            }

            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = new SqlParameter("@applyId", SqlDbType.Int, 4);
            para.Value = applyId;
            paras.Add(para);

            SqlDataReader dr = null;

            try
            {
                string cmdText = "select * from dbo.Fun_PayApply where ApplyId=@applyId";
                dr = SqlHelper.ExecuteReader(ConnectString, CommandType.Text, cmdText, paras.ToArray());

                PayApply payapply = new PayApply();

                if (dr.Read())
                {
                    int indexPayApplyId = dr.GetOrdinal("PayApplyId");
                    payapply.PayApplyId = Convert.ToInt32(dr[indexPayApplyId]);

                    int indexApplyId = dr.GetOrdinal("ApplyId");
                    if (dr["ApplyId"] != DBNull.Value)
                    {
                        payapply.ApplyId = Convert.ToInt32(dr[indexApplyId]);
                    }

                    int indexPayApplySource = dr.GetOrdinal("PayApplySource");
                    if (dr["PayApplySource"] != DBNull.Value)
                    {
                        payapply.PayApplySource = Convert.ToInt32(dr[indexPayApplySource]);
                    }

                    int indexRecBlocId = dr.GetOrdinal("RecBlocId");
                    if (dr["RecBlocId"] != DBNull.Value)
                    {
                        payapply.RecBlocId = Convert.ToInt32(dr[indexRecBlocId]);
                    }

                    int indexRecCorpId = dr.GetOrdinal("RecCorpId");
                    if (dr["RecCorpId"] != DBNull.Value)
                    {
                        payapply.RecCorpId = Convert.ToInt32(dr[indexRecCorpId]);
                    }

                    int indexRecBankId = dr.GetOrdinal("RecBankId");
                    if (dr["RecBankId"] != DBNull.Value)
                    {
                        payapply.RecBankId = Convert.ToInt32(dr[indexRecBankId]);
                    }

                    int indexRecBankAccountId = dr.GetOrdinal("RecBankAccountId");
                    if (dr["RecBankAccountId"] != DBNull.Value)
                    {
                        payapply.RecBankAccountId = Convert.ToInt32(dr[indexRecBankAccountId]);
                    }

                    int indexRecBankAccount = dr.GetOrdinal("RecBankAccount");
                    if (dr["RecBankAccount"] != DBNull.Value)
                    {
                        payapply.RecBankAccount = Convert.ToString(dr[indexRecBankAccount]);
                    }

                    int indexApplyBala = dr.GetOrdinal("ApplyBala");
                    if (dr["ApplyBala"] != DBNull.Value)
                    {
                        payapply.ApplyBala = Convert.ToDecimal(dr[indexApplyBala]);
                    }

                    int indexCurrencyId = dr.GetOrdinal("CurrencyId");
                    if (dr["CurrencyId"] != DBNull.Value)
                    {
                        payapply.CurrencyId = Convert.ToInt32(dr[indexCurrencyId]);
                    }

                    int indexPayMode = dr.GetOrdinal("PayMode");
                    if (dr["PayMode"] != DBNull.Value)
                    {
                        payapply.PayMode = Convert.ToInt32(dr[indexPayMode]);
                    }

                    int indexPayDeadline = dr.GetOrdinal("PayDeadline");
                    if (dr["PayDeadline"] != DBNull.Value)
                    {
                        payapply.PayDeadline = Convert.ToDateTime(dr[indexPayDeadline]);
                    }

                    int indexSpecialDesc = dr.GetOrdinal("SpecialDesc");
                    if (dr["SpecialDesc"] != DBNull.Value)
                    {
                        payapply.SpecialDesc = Convert.ToString(dr[indexSpecialDesc]);
                    }

                    int indexPayMatter = dr.GetOrdinal("PayMatter");
                    if (dr["PayMatter"] != DBNull.Value)
                    {
                        payapply.PayMatter = Convert.ToInt32(dr[indexPayMatter]);
                    }

                    int indexRealPayCorpId = dr.GetOrdinal("RealPayCorpId");
                    if (dr["RealPayCorpId"] != DBNull.Value)
                    {
                        payapply.RealPayCorpId = Convert.ToInt32(dr[indexRealPayCorpId]);
                    }

                    int indexCreatorId = dr.GetOrdinal("CreatorId");
                    if (dr["CreatorId"] != DBNull.Value)
                    {
                        payapply.CreatorId = Convert.ToInt32(dr[indexCreatorId]);
                    }

                    int indexCreateTime = dr.GetOrdinal("CreateTime");
                    if (dr["CreateTime"] != DBNull.Value)
                    {
                        payapply.CreateTime = Convert.ToDateTime(dr[indexCreateTime]);
                    }

                    int indexLastModifyId = dr.GetOrdinal("LastModifyId");
                    if (dr["LastModifyId"] != DBNull.Value)
                    {
                        payapply.LastModifyId = Convert.ToInt32(dr[indexLastModifyId]);
                    }

                    int indexLastModifyTime = dr.GetOrdinal("LastModifyTime");
                    if (dr["LastModifyTime"] != DBNull.Value)
                    {
                        payapply.LastModifyTime = Convert.ToDateTime(dr[indexLastModifyTime]);
                    }

                    result.AffectCount = 1;
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = payapply;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.AffectCount = 0;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
            }
            return result;
        }

        public ResultModel GetCondition(UserModel user, int applyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select * ");
                sb.Append(" from dbo.Fun_PayApply p with (nolock) ");
                sb.Append(" left join dbo.Apply a with (nolock) on p.ApplyId = a.ApplyId ");
                //sb.Append(" left join dbo.Fun_ContractPayApply_Ref detail with (nolock) on detail.PayApplyId = p.PayApplyId ");
                //sb.Append(" left join dbo.Con_Contract c with (nolock) on detail.ContractId = c.ContractId ");
                //sb.Append(" left join dbo.Con_ContractSub sub with (nolock) on detail.ContractSubId = sub.SubId ");
                sb.AppendFormat(" where p.ApplyId = {0}", applyId);

                DataTable dt = SqlHelper.ExecuteDataTable(ConnectString, sb.ToString(), null, CommandType.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    result.Message = "读取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = dt;
                }
                else
                {
                    result.Message = "读取失败或无数据";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            return result;
        }

        public override IAuthority Authority
        {
            get
            {
                NFMT.Authority.ContractAuth auth = new Authority.ContractAuth();
                return auth;
            }
        }

        public override int MenuId
        {
            get
            {
                return 52;
            }
        }

        /// <summary>
        /// 校验子合约中付款申请是否全部确认完成
        /// </summary>
        /// <param name="user"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public ResultModel CheckContractSubPayApplyConfirm(UserModel user, int subId)
        {
            ResultModel result = new ResultModel();

            string cmdText = "select COUNT(*) from NFMT.dbo.Fun_ContractPayApply_Ref pac inner join NFMT.dbo.Fun_PayApply pa on pa.PayApplyId = pac.PayApplyId inner join NFMT.dbo.Apply app on pa.ApplyId = app.ApplyId where pac.ContractSubId =@subId and app.ApplyStatus in (@entryStatus,@readyStatus)";

            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@subId", subId);
            paras[1] = new SqlParameter("@entryStatus", (int)NFMT.Common.StatusEnum.已录入);
            paras[1] = new SqlParameter("@readyStatus", (int)NFMT.Common.StatusEnum.已生效);

            object obj = DBUtility.SqlHelper.ExecuteScalar(this.ConnectString, CommandType.Text, cmdText, paras);
            int i = 0;
            if (obj == null || !int.TryParse(obj.ToString(), out i) || i <= 0)
            {
                result.ResultStatus = -1;
                result.Message = "检验付款申请失败";
                return result;
            }
            if (i > 0)
            {
                result.ResultStatus = -1;
                result.Message = "子合约中含有未完成的付款申请，不能进行确认完成操作。";
                return result;
            }

            result.ResultStatus = 0;
            result.Message = "付款申请全部完成";

            return result;
        }

        public ResultModel GetContractBalancePayment(UserModel user, int subContractId, int payApplyId)
        {
            ResultModel result = new ResultModel();

            try
            {
                List<SqlParameter> paras = new List<SqlParameter>();

                SqlParameter paraSubContractId = new SqlParameter("@SubContractId", SqlDbType.Int, 4);
                paraSubContractId.Value = subContractId;
                paras.Add(paraSubContractId);

                SqlParameter paraPayApplyId = new SqlParameter("@PayApplyId", SqlDbType.Int, 4);
                paraPayApplyId.Value = payApplyId;
                paras.Add(paraPayApplyId);

                SqlParameter paraAmout = new SqlParameter();
                paraAmout.Direction = ParameterDirection.Output;
                paraAmout.SqlDbType = SqlDbType.Money;
                paraAmout.ParameterName = "@Amout";
                paraAmout.Size = 4;
                paras.Add(paraAmout);

                SqlParameter paraCurrencyName = new SqlParameter();
                paraCurrencyName.Direction = ParameterDirection.Output;
                paraCurrencyName.SqlDbType = SqlDbType.VarChar;
                paraCurrencyName.ParameterName = "@CurrencyName";
                paraCurrencyName.Size = 50;
                paras.Add(paraCurrencyName);

                object obj = SqlHelper.ExecuteScalar(this.ConnectString, CommandType.StoredProcedure, "dbo.ContractBalancePayment", paras.ToArray());

                if (obj != null)
                {
                    result.Message = "获取成功";
                    result.ResultStatus = 0;
                    result.ReturnValue = paraAmout.Value;
                }
                else
                {
                    result.Message = "获取失败";
                    result.ResultStatus = -1;
                }
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }

            return result;
        }

        #endregion
    }
}
