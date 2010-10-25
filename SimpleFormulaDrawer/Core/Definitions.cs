using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class DSet
        {
            private Dictionary<int, object> Arr;

            public DSet(params object[] What)
            {
                Arr=new Dictionary<int,object>();
                if (What == null) { return; }
                for (int i = 0; i < What.Length; i++)
                {
                    if (What[i] == null) { throw new Exception("Missing " + i.ToString() + " argument!"); }
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
                    for (int i = 0; i < Arr.Count; i++)
                    {
                        if (Object.Equals(Arr.ElementAt(i), What))
                        {
                            Arr.Remove(i);
                            return;
                        }
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
                this.C = new TStateDescription(Code, Description);
            }
        }
}
