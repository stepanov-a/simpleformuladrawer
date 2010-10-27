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
        public int FuncNumber=0;
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
        public void Add(string Func,bool Is3D)
        {
            if (!Is3D)
            {
                Text += @"
                    public static double Func" + FuncNumber.ToString() + @"(double x)
                    {
                        return " + Func + @";
                    }
            ";
            }
            else
            {
                Text += @"
                    public static double Func" + FuncNumber.ToString() + @"(double x, double y)
                    {
                        return " + Func + @";
                    }
            ";
            }
            FuncNumber++;
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

        public string GetSource()
        {
            return Source.GetSourceString();
        }

        public LibraryManager()
        {
            CompilerDirectives = new Dictionary<string, string>();
            CompilerDirectives.Add("Compiler Version", "v4.0");
            Source=new SourceManager();
        }

        //Returns true if Function has 3D graph
        public bool AddFunction(string Function)
        {
            string ParsedFunction = ParseFunction(Function);
            bool Is3D = Check3D(ParsedFunction);
            Source.Add(ParsedFunction, Is3D);
            return Is3D;
        }

        public void CompileSource()
        {
            Source.CompleteSource();
            Compile();
        }

        private bool Check3D(string Function)
        {
            int State = 0;
            DSet Operators=new DSet("+-*/()^");
            for (int i = 0; i < Function.Length; i++)
            {
                switch (State)
                {
                    case 0:
                        {
                            if (Function[i] == 'y') { State = 1; }
                            if (Function[i] == 'M') { State = 2; }
                            break;
                        }
                    case 1:
                        {
                            if (Operators.Exists(Function[i])) {return true;}
                            State = 0;
                            break;
                        }
                    case 2:
                        {
                            if (Operators.Exists(Function[i])) { State = 0; }
                            break;
                        }
                }
            }
            return State==1?true:false;
        }

        private void MakeStep(ref string Func, ref string TMP, ref int i)
        {
            int State;
            int o;
            DSet Num = new DSet("0.123456789.");
            DSet OPS=new DSet("+-*/");
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
                            if (Func[i] == '(') { State++; } else
                            if (Func[i] == ')') { State--; } else
                            if (Func[i]=='^') {MakeStep(ref Func,ref TMP,ref i);} else
                            {
                                TMP+=Func[i];
                                i++;
                            }
                        }
                        TMP += ')';
                        break;
                    }
            }
            #endregion
        }

        private string ParseFunction(string Func)
        {
            string TMP="";
            int State = 0;
            bool Need = false;
            DSet Operators=new DSet("1234567890)");
            DSet OperatorsEx = new DSet("1234567890+-*/^");
            DSet Letters = new DSet("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.");
            DSet VarLetters=new DSet("xyXY");

            #region FirstStep
            for (int i = 0; i < Func.Length; i++)
            {
                if (Operators.Exists(Func[i])) {State=1;}
                else if (Func[i] == '^') { State = 0; }
                else if (State == 1 && !OperatorsEx.Exists(Func[i]))
                {
                    if (Func[i] != ')') { State = 0; }
                    TMP += '*';
                }
                TMP += Char.ToLower(Func[i]);
            }
            #endregion

            #region SecondStep
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
            for (int i = 0; i < Func.Length; i++)
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

        private void Compile()
        {
            CSharpCodeProvider Compiler = new CSharpCodeProvider(CompilerDirectives);
            CompilerParameters CPR=new CompilerParameters();
            CPR.GenerateInMemory=true;
            CompilerResults CR=Compiler.CompileAssemblyFromSource(CPR,Source.GetSourceString());
            Functions = new MethodInfo[Source.FuncNumber];
            if (CR.Errors.Count == 0)
            {
                for (int i = 0; i < Source.FuncNumber; i++)
                {
                    Functions[i] = CR.CompiledAssembly.GetType("FunctionDll.Functions").GetMethod("Func" + i.ToString());
                }
            }
            else
            {
                WorldStates.AddState(2, "Formula Error");
                Functions = new MethodInfo[0];
            }
            return;
        }

        public float[] Funcs(float x, float y)
        {
            object[] INum = new object[2];
            object ONum = new object();
            float[] RES = new float[Functions.Length];
            INum[0] = x;
            INum[1] = y;
            for (int i = 0; i < Functions.Length; i++)
            {
                ONum = Functions[i].Invoke(null, INum);
                RES[i] = float.Parse(ONum.ToString());
            }
            return RES;
        }

        public float[] Funcs(float x)
        {
            object[] INum = new object[1];
            object ONum=new object();
            float[] RES = new float[Functions.Length];
            INum[0] = x;
            for (int i = 0; i < Functions.Length; i++)
            {
                ONum = Functions[i].Invoke(null, INum);
                RES[i] = float.Parse(ONum.ToString());
            }
            return RES;
        }
    }
}
