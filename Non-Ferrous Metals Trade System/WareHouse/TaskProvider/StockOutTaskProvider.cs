﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFMT.WareHouse.TaskProvider
{
    public class StockOutTaskProvider:NFMT.WorkFlow.ITaskProvider
     {
         public Common.ResultModel Create(Common.UserModel user, Common.IModel model)
         {
             NFMT.Common.ResultModel result = new Common.ResultModel();

             try
             {
                 WorkFlow.Model.Task task = new WorkFlow.Model.Task();
                 Model.StockOut stockOut = model as Model.StockOut;

                 task.TaskName = "出库审核";

                 task.TaskConnext = string.Format("{0} 于 {1} 提交审核。", user.EmpName, DateTime.Now.ToString());

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