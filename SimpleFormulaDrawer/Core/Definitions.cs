using System;
using System.Collections.Generic;
using System.Linq;
using SimpleFormulaDrawer.interfac;

namespace SimpleFormulaDrawer.Core
{
    public static class Forms
    {
        public static DebugForm DF = new DebugForm();
        //ConfigForm CF=new ConfigForm();
    }

    public class DSet
        {
            private readonly Dictionary<int, object> Arr;

            public DSet(string What)
            {
                Arr = new Dictionary<int, object>();
                if (What==null) {return;}
                for (var i = 0; i < What.Length; i++)
                {
                    Arr.Add(i, What[i]);
                }
            }
            public DSet(params object[] What)
            {
                Arr=new Dictionary<int,object>();
                if (What == null) { return; }
                for (var i = 0; i < What.Length; i++)
                {
                    Arr.Add(i, What[i]);
                }
            }
            public bool Exists(object What)
            {
                return Arr.ContainsValue(What);
            }
            public void Add(object What)
            {
                if (!Arr.ContainsValue(What))
                {
                    Arr.Add(Arr.Count, What);
                }
            }
            public void Remove(object What)
            {
                if (Arr.ContainsValue(What))
                {
                    for (var i = 0; i < Arr.Count; i++)
                    {
                        if (!Equals(Arr.ElementAt(i), What)) continue;
                        Arr.Remove(i);
                        return;
                    }
                }
                return;
            }
        }
        
    public class TStateDescription
        {
            public int Code;
            public string Description;
            public TStateDescription(int Code, string Description)
            {
                this.Code = Code;
                this.Description = Description;
            }
        }

    public class TState
        {
            public TStateDescription C;
            public TState N;
            public TState(int Code, string Description)
            {
                C = new TStateDescription(Code, Description);
            }
        }
}
