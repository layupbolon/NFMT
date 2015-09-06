/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 文件名：SysTypeProvider.cs
// 文件功能描述：系统类型构造器。
// 创建人：pekah.chow
// 创建时间： 2014-04-28
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.Common
{
    public class SysTypeProvider
    {
        public static System.Type GetType(string typeName)
        {
            Type t = null;

            if (string.IsNullOrEmpty(typeName))
                return t;

            
            typeName = typeName.ToLower().Trim();
            switch (typeName)
            {
                case "int":
                    t = typeof(int);
                    break;
                case "long":
                    t = typeof(long);
                    break;
                case "numeric":
                    t = typeof(decimal);
                    break;
                case "decimal":
                    t = typeof(decimal);
                    break;
                case "string":
                    t = typeof(string);
                    break;
                case "bool":
                    t = typeof(bool);
                    break;
                case "boolean":
                    t = typeof(Boolean);
                    break;
                default:
                    t = typeof(string);
                    break;
            }

            return t;
        }
    }
}
