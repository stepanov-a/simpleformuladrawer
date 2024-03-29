﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using System.CodeDom.Compiler;
using System.Windows.Controls;
using System.Drawing;
using SimpleFormulaDrawer.Interface;

namespace SimpleFormulaDrawer.Core
{
    public static class Forms
    {
        public static readonly MainForm MF = new MainForm();
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
        private readonly static System.Windows.Media.Brush Strk=new SolidColorBrush(Colors.White);
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

    public struct FunctionParameters
    {
        public bool Is3D;
        public CompilerErrorCollection Errors;
    }

    public struct Point3DF
    {
        public static readonly Point3DF Empty =new Point3DF();
        private float x, y, z;

        public Point3DF(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public bool IsEmpty
        {
            get
            {
                return x == 0f && y == 0f && z == 0f;
            }
        }
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
    }

    public struct MainFormContent
    {
        public double MinX, MaxX, MinY, MaxY, MinZ, MaxZ,Quality;
        public bool Show3DBox;
        public ListBox FormulListBox;
    }

    public class ObjectList:ArrayList
    {
        /// <summary>
        /// Инициализация ObjectList - а по диапазону чисел. хранить будет числа.
        /// </summary>
        /// <param name="Start">Начальное значение</param>
        /// <param name="End">Конечное значение</param>
        /// <param name="Step">Шаг</param>
        public ObjectList(double Start,double End,double Step)
        {
            var C=new DoubleCollection(); //Извращение, но так быстрее.
            for (var i = Start; i <= End;i+=Step )
            {
                C.Add(i);
            }
            this.AddRange(C);
            C = null;
        }

        public ObjectList()
        {
            this.Clear();
        }
    }
}

namespace SimpleFormulaDrawer.Interface
{
    public partial class Pictogramm
    {
        private static bool HasFlag(int What,int Flag)
        {
            return ((What & Flag) == What);
        }
    }
}