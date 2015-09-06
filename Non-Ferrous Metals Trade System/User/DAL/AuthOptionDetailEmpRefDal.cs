using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFMT.Common;
using NFMT.User.Model;
using NFMT.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace NFMT.User.DAL
{
    public class AuthOptionDetailEmpRefDal : DataOperate
    {
        public override ResultModel Get(UserModel user, int id)
        {
            throw new NotImplementedException();
        }

        public override ResultModel Load(UserModel user)
        {
            throw new NotImplementedException();
        }

        public ResultModel Load(UserModel user, int empId)
        {
            ResultModel result = new ResultModel();
            try
            {
                string cmdText = "select * from dbo.AuthOptionDetailEmp_Ref r where r.EmpId=@empId";

                SqlParameter[] paras = new SqlParameter[1];
                paras[0] = new SqlParameter("@empId", empId);

                DataTable dt = SqlHelper.ExecuteDataTable(SqlHelper.ConnectionStringUser, cmdText, paras, CommandType.Text);

                List<AuthOptionDetailEmpRef> ls = new List<AuthOptionDetailEmpRef>();
                foreach (DataRow dr in dt.Rows)
                {
                    AuthOptionDetailEmpRef model = new AuthOptionDetailEmpRef();

                    model.RefId = Convert.ToInt32(dr["RefId"]);
                    if (dr["EmpId"] != DBNull.Value)
                        model.EmpId = Convert.ToInt32(dr["EmpId"]);
                    if (dr["DetailId"] != DBNull.Value)
                        model.DetailId = Convert.ToInt32(dr["DetailId"]);
                    if (dr["CreatorId"] != DBNull.Value)
                        model.CreatorId = Convert.ToInt32(dr["CreatorId"]);
                    if (dr["CreateTime"] != DBNull.Value)
                        model.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (dr["LastModifyId"] != DBNull.Value)
                        model.LastModifyId = Convert.ToInt32(dr["LastModifyId"]);
                    if (dr["LastModifyTime"] != DBNull.Value)
                        model.LastModifyTime = Convert.ToDateTime(dr["LastModifyTime"]);
                    ls.Add(model);
                }
                result.AffectCount = dt.Rows.Count;
                result.Message = "获取列表成功";
                result.ResultStatus = 0;
                result.ReturnValue = ls;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public override ResultModel Insert(UserModel user, IModel obj)
        {
            throw new NotImplementedException();
        }

        public override ResultModel Update(UserModel user, IModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
