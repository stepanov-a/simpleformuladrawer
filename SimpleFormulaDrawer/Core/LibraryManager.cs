using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;

namespace Core
{
    public class SourceManager
    {
        private string Text;
        private int FuncNumber=0;
        public SourceManager()
        {
            FuncNumber = 0;
            Text=@"using System;
            namespace FunctionDll
            {
                public class Functions
                {
                    static void Main() {}
            ";
        }
        public void Add(string Func)
        {
            Text += @"
                    public static double Func" + FuncNumber.ToString() + @"(double x)
                    {
                        return " + Func + @";
                    }
            ";
        }
        public void CompleteSource()
        {
            Text += @"
                }
            }
            ";
        }
        public string GetSourceString()
        {
            return Text;
        }
    }

    public class LibraryManager
    {
        private SourceManager Source;
        private Dictionary<string, string> CompilerDirectives;
        private MethodInfo[] Functions;

        public LibraryManager()
        {
            CompilerDirectives = new Dictionary<string, string>();
            CompilerDirectives.Add("Compiler Version", "v4.0");
            Functions=new MethodInfo[1];
            Source=new SourceManager();
        }

        public void AddFunction(string Function)
        {
            Source.Add(ParseFunction(Function));
        }

        public void CompileSource()
        {
            Source.CompleteSource();
            Compile();
        }

        private string ParseFunction(string Func)
        {
            string TMP="";
            int State = 0;
            bool Need = false;
            DSet Operators=new DSet('0','1','2','3','4','5','6','7','8','9',')');

            #region FirstStep
            for (int i = 1; i < Func.Length; i++)
            {
                if (Need==false) {State=1;}

            }
            #endregion
            return Func;
        }

        private void Compile()
        {
            return;
        }
    }
}
