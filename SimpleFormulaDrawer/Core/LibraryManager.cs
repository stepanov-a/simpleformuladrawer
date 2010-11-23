using System;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;

namespace SimpleFormulaDrawer.Core
{
    public class SourceManager
    {
        private readonly string Text;
        public SourceManager(string Func)
        {
            Text=@"using System;
            namespace FunctionDll
            {
                public class Functions
                {
                    static void Main() {}";
            Text +=string.Format(
                        @"  
                    public static double Func(double x, double y)
                    {{
                        return {0};
                    }}",Func);
            Text += "\r\n                }\r\n            }\r\n            ";
        }

        public string GetSourceString()
        {
            return Text;
        }
    }

    public class LibraryManager
    {
        private static readonly Dictionary<string, string> CompilerDirectives = new Dictionary<string, string> { { "Compiler Version", "v4.0" } };
        private readonly List<MethodInfo> Functions;

        public LibraryManager()
        {
            Functions=new List<MethodInfo>();
        }

        public FunctionParameters AddFunction(string Function)
        {
            /*Здесь может быть проблема безопастности, так как компилируется то, что пользователь напишет в текстбоксе
             * НО ко всем функциям добавится Math и я пока что не нашел способа как это обходить
             * следовательно безопастность не нарушается
             * */
            var ParsedFunction = ParseFunction(Function);
            var Is3D = Check3D(ParsedFunction);
            var TSource = new SourceManager(ParsedFunction);
            var Compiler = new CSharpCodeProvider(CompilerDirectives);
            var CPR = new CompilerParameters { GenerateInMemory = true };
            var CR = Compiler.CompileAssemblyFromSource(CPR, TSource.GetSourceString());
            if (CR.Errors.Count==0)
            {
                Functions.Add(CR.CompiledAssembly.GetType("FunctionDll.Functions").GetMethod("Func"));
            }
            FunctionParameters toRet;
            toRet.Errors = CR.Errors;
            toRet.Is3D = Is3D;
            Compiler.Dispose();
            return toRet;
        }

        public void RemoveFunction(int Index)
        {
            Functions.RemoveAt(Index);
        }

        public static int ErrorPosition(int Pos)
        {
            return Pos - 32;
        }

        public static bool Check3D(IEnumerable<char> Function)
        {
            var State = 0;
            var Operators=new DSet("+-*/()^");
            foreach (var t in Function)
            {
                switch (State)
                {
                    case 0:
                        {
                            if (t == 'y') { State = 1; }
                            if (t == 'M') { State = 2; }
                            break;
                        }
                    case 1:
                        {
                            if (Operators.Exists(t)) {return true;}
                            State = 0;
                            break;
                        }
                    case 2:
                        {
                            if (Operators.Exists(t)) { State = 0; }
                            break;
                        }
                }
            }
            return (State==1);
        }

        private static void MakeStep(ref string Func, ref string TMP, ref int i)
        {
            int State;
            int o;
            var Num = new DSet("0.123456789");
            var OPS=new DSet("+-*/");
            #region PREPOWER
            switch (Func[i-1])
            {
                case 'x':
                    {
                        TMP = TMP.Insert(i - 1, "pow(");
                        break;
                    }
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    {
                        State = 0;
                        while (Num.Exists(Func[i - State]) && i - State > 0) { State++; }
                        TMP = TMP.Insert(i - State-1, "pow(");
                        break;
                    }
                case ')':
                    {
                        State = -1;
                        o = 2;
                        while (State != 0)
                        {
                            if (Func[i - o] == '(') { State++; }
                            if (Func[i - o] == ')') { State--; }
                            o++;
                        }
                        while (i - o >= 0 && !OPS.Exists(Func[i - o])) { o++; }
                        TMP = TMP.Insert(i - o, "pow(");
                        break;
                    }
            }
            #endregion
            Func.Remove(i, 1);
            Func.Insert(i, ",");
            TMP += ',';
            #region POSTPOWER
            switch (Func[i+1])
            {
                case 'x':
                    {
                        TMP += "x)";
                        i++;
                        i++;
                        if (i < Func.Length) {TMP += Func[i];}
                        break;
                    }
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    {
                        i++;
                        while (i<Func.Length && Num.Exists(Func[i])) 
                        {
                            TMP+=Func[i];
                            i++;
                        }
                        TMP+=')';
                        if (i<Func.Length) {TMP+=Func[i];}
                        break;
                    }
                case '(':
                    {
                        TMP+=Func[i+1];
                        State=1;
                        i++; i++;
                        while (State!=0) 
                        {
                            switch (Func[i])
                            {
                                case '(':
                                    State++;
                                    break;
                                case ')':
                                    State--;
                                    break;
                                case '^':
                                    MakeStep(ref Func,ref TMP,ref i);
                                    break;
                                default:
                                    TMP+=Func[i];
                                    i++;
                                    break;
                            }
                        }
                        TMP += ')';
                        break;
                    }
            }
            #endregion
        }

        private static string ParseFunction(string Func)
        {
            var TMP = "";
            var State = 0;
            var Operators=new DSet("1234567890)");
            var OperatorsEx = new DSet("1234567890+-*/^");
            var Letters = new DSet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.");
            var VarLetters = new DSet("xyXY");

            #region FirstStep
            foreach (var t in Func)
            {
                if (Operators.Exists(t)) {State=1;}
                else if (t == '^') { State = 0; }
                else if (State == 1 && !OperatorsEx.Exists(t))
                {
                    if (t != ')') { State = 0; }
                    TMP += '*';
                }
                TMP += Char.ToLower(t);
            }
            #endregion

            #region SecondStep

            bool Need;
            while (Func.IndexOf('^') != -1)
            {
                State = 0;
                Need = true;
                Func = TMP;
                TMP = "";
                while (State < Func.Length)
                {
                    if (Need && Func[State] == '^')
                    {
                        MakeStep(ref Func, ref TMP, ref State);
                        Need = false;
                    }
                    else { TMP += Func[State]; }
                    State++;
                }
            }
            #endregion

            #region ThirdStep
            TMP = "";
            State = 0;
            for (var i = 0; i < Func.Length; i++)
            {
                switch (State)
                {
                    case 0:
                        {
                            if (Letters.Exists(Func[i]) && !VarLetters.Exists(Func[i]))
                            {
                                TMP += "Math.";
                                TMP += char.ToUpper(Func[i]);
                                State = 1;
                            }
                            else { TMP += Func[i]; }
                            break;
                        }
                    case 1:
                        {
                            if (Func[i] == 'i' && Func[i - 1] == 'P') { TMP += char.ToUpper(Func[i]); }
                            else
                            {
                                if (!Letters.Exists(Func[i])) {State=0;}
                                TMP+=Func[i];
                            }
                            break;
                        }
                }
            }
            #endregion
            return TMP;
        }

        public float[] Funcs(params double[] Variables)
        {
            var INum = new object[2];
            object ONum;
            var RES = new float[Functions.Count];
            INum[0] = Variables[0];
            if (Variables.Length > 1)
            {
                INum[1] = Variables[1];
            } 
            else
            {
                INum[1] = null;
            }
            var i = 0;
            foreach(var MF in Functions)
            {
                ONum = MF.Invoke(null, INum);
                RES[i] = float.Parse(ONum.ToString());
                i++;
            }
            return RES;
        }
    }
}
