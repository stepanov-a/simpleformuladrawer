using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;


using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public void AddFunction(string Function)
        {
            Source.Add(ParseFunction(Function));
        }

        public void CompileSource()
        {
            Source.CompleteSource();
            Compile();
        }

        private void MakeStep(ref string Func, ref string TMP, ref int i)
        {
            int State;
            int o;
            DSet Num = new DSet('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.');
            DSet OPS=new DSet('+','-','*','/');
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
            DSet Letters = new DSet("abcdefghijklmnopqrstuvwyz");

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

            TMP = "";
            State = 0;
            #region ThirdStep
            for (int i = 0; i < Func.Length; i++)
            {
                switch (State)
                {
                    case 0:
                        {
                            if (Letters.Exists(Func[i]))
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
            return;
        }
    }
}
