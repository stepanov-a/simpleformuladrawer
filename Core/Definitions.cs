using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    static class Definitions
    {
        class Set
        {
            private Dictionary<int, object> Arr;
            Set(params object[] What)
            {
                for (int i = 1; i < What.Length; i++)
                {
                    Arr.Add(Arr.Count, What[i]);
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
    }
}
