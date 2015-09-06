using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse.TaskProvider
{
    public class StockInTaskProvider : NFMT.WorkFlow.ITaskProvider
    {
        public Common.ResultModel Create(Common.UserModel user, Common.IModel model)
        {
            NFMT.Common.ResultModel result = new Common.ResultModel();

            try
            {
                WorkFlow.Model.Task task = new WorkFlow.Model.Task();
                Model.StockIn stockIn = model as Model.StockIn;

                task.TaskName = "入库登记审核";

                NFMT.Data.Model.Asset asset = NFMT.Data.BasicDataProvider.Assets.SingleOrDefault(a => a.AssetId == stockIn.AssetId);
                if (asset == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取品种失败";
                    return result;
                }
                NFMT.Data.Model.Brand brand = NFMT.Data.BasicDataProvider.Brands.SingleOrDefault(a => a.BrandId == stockIn.BrandId);
                if (brand == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取品牌失败";
                    return result;
                }
                //获取单位
                NFMT.Data.Model.MeasureUnit mu = NFMT.Data.BasicDataProvider.MeasureUnits.SingleOrDefault(a => a.MUId == stockIn.UintId);
                if (mu == null)
                {
                    result.ResultStatus = -1;
                    result.Message = "获取单位失败";
                    return result;
                }

                task.TaskConnext = string.Format("{0} 于 {1} 提交审核。毛重：{2}{3} {4} {5}", user.EmpName, DateTime.Now.ToString(), stockIn.GrossAmount, mu.MUName, asset.AssetName, brand.BrandName);

                result.ReturnValue = task;
                result.ResultStatus = 0;
            }
            catch (Exception ex)
            {
                result.ResultStatus = -1;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
