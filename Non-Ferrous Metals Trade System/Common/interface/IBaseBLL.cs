using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public interface IBaseBLL
    {
        IOperate Operate { get; }

        ResultModel Insert(UserModel user, IModel obj);

        ResultModel Get(UserModel user, int id);

        ResultModel Load(UserModel user);

        ResultModel Load(UserModel user, SelectModel select);

        ResultModel Update(UserModel user, IModel obj);

        ResultModel Submit(UserModel user, IModel obj);

        ResultModel Audit(UserModel user, IModel obj, bool isPass);

        ResultModel GoBack(UserModel user, IModel obj);

        ResultModel Invalid(UserModel user, IModel obj);

        ResultModel Close(UserModel user, IModel obj);

        string CreateExcel(System.Data.DataTable source, string modelPath, string filePath, ReportType reportType);
    }
}
