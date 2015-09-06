using System;
using System.Collections.Generic;
using System.Text;

namespace NFMT.WorkFlow
{
    public class Utility
    {
        #region Calculate

        /// <summary>
        /// 动态编译
        /// </summary>
        /// <param name="formula">需编译的内容</param>
        /// <param name="returnType">返回类型</param>
        /// <param name="obj">参数</param>
        /// <returns></returns>
        public static object Calculate(string formula, string returnType, object obj)
        {
            try
            {
                string paramExp = obj.GetType().ToString() + " obj";
                object calculated = null;

                Microsoft.CSharp.CSharpCodeProvider provider = new Microsoft.CSharp.CSharpCodeProvider();
                System.CodeDom.Compiler.CompilerParameters parameter = new System.CodeDom.Compiler.CompilerParameters();
                parameter.ReferencedAssemblies.Add("System.dll");
                parameter.GenerateExecutable = false; //<--不要生成 EXE 文件
                parameter.GenerateInMemory = true; //在内存中运行
                string codeDom = GenerateCodeBlocks(formula, returnType, paramExp);
                System.CodeDom.Compiler.CompilerResults result = provider.CompileAssemblyFromSource(parameter,codeDom);//动态编译后的结果

                if (result.Errors.Count > 0)
                {
                    return null;
                }
                System.Reflection.MethodInfo newMethod = result.CompiledAssembly.GetType("Maike.Calculation").GetMethod("dowork");
                calculated = result.CompiledAssembly.GetType("Maike.Calculation").GetMethod("dowork").Invoke(null, new object[] { obj });

                return calculated;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 生成动态编译的代码块
        /// </summary>
        /// <param name="formula">要编译的公式或者表达式</param>
        /// <param name="returnType">返回的类型</param>
        /// <param name="paramExp">参数名称</param>
        /// <returns></returns>
        private static string GenerateCodeBlocks(string formula, string returnType, string paramExp)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace Maike");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("     public static class Calculation");
            sb.Append(Environment.NewLine);
            sb.Append("     {");
            sb.Append(Environment.NewLine);
            sb.Append("             public static ");
            sb.Append(returnType);
            sb.Append(" dowork(");
            sb.Append(paramExp);
            sb.Append(")");
            sb.Append(Environment.NewLine);
            sb.Append("             { ");
            sb.Append(Environment.NewLine);
            sb.Append("                  return ");
            sb.Append(formula);
            sb.Append(";");
            sb.Append(Environment.NewLine);
            sb.Append("             }");
            sb.Append(Environment.NewLine);
            sb.Append("      }");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// 获取逻辑运算符
        /// </summary>
        /// <param name="LogicType">逻辑条件</param>
        /// <returns></returns>
        public static string GetLogicType(LogicType logicType)
        {
            switch (logicType)
            {
                case LogicType.与:
                    return "&&";
                case LogicType.或:
                    return "||";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="ConditionType">条件类型</param>
        /// <returns></returns>
        public static string GetConditionType(ConditionType conditionType)
        {
            switch (conditionType)
            {
                case ConditionType.等于:
                    return "==";
                case ConditionType.不等于:
                    return "!=";
                case ConditionType.大于:
                    return ">";
                case ConditionType.大于等于:
                    return ">=";
                case ConditionType.小于:
                    return "<";
                case ConditionType.小于等于:
                    return "<=";
                default:
                    return string.Empty;
            }
        }
    }
}
