﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using SimpleFormulaDrawer.interfac;
using System.Windows.Shapes;

namespace SimpleFormulaDrawer.Core
{
    public static class Forms
    {
        public static MainWindow StartupWindow;
        public static readonly DebugForm DF = new DebugForm();
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
        
    public static class Crest
    {
        private readonly static Line[] ToRet=new Line[4];
        private readonly static Brush Strk=new SolidColorBrush(Colors.White);
        private readonly static Line[] DCrest = new []
                                                   {
                                                       new Line {X1 = -10, X2 = -2, Y1 = 0, Y2 = 0, Stroke = Strk},
                                                       new Line {X1 = 0, X2 = 0, Y1 = 10, Y2 = 2, Stroke = Strk},
                                                       new Line {X1 = 2, X2 = 10, Y1 = 0, Y2 = 0, Stroke = Strk},
                                                       new Line {X1 = 0, X2 = 0, Y1 = -2, Y2 = -10, Stroke = Strk}
                                                   };
        private static Line MoveLine(Line What,double x,double y)
        {
            return new Line { X1 = What.X1 + x, X2 = What.X2 + x, Y1 = What.Y1 + y, Y2 = What.Y2 + y, Stroke = Strk };
        }

        public static Line[] CrestToPoint(double x,double y)
        {
            ToRet[0] = MoveLine(DCrest[0],x,y);
            ToRet[1] = MoveLine(DCrest[1], x, y);
            ToRet[2] = MoveLine(DCrest[2], x, y);
            ToRet[3] = MoveLine(DCrest[3], x, y);
            return ToRet;
        }
    }
}
