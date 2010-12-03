using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SimpleFormulaDrawer.Core;

namespace SimpleFormulaDrawer.Interface
{
    static class GraphFormHelper
    {
        public static void FlushBuffer(GraphForm GR)
        {
            GR.GBMP.Clear(Color.White);
        }

        public static void Update(GraphForm GR)
        {
            GR.Redraw();
        }

        public static void DrawAllContent(GraphForm GR, ObjectList Points)
        {
            var Colors = new ObjectList();
            var Rnd = new Random();
            foreach (var T in Points)
            {
                Colors.Add(Color.FromArgb(Rnd.Next(16777216)));
            }
            DrawAllContent(GR,Points,Colors);
        }

        public static void DrawAllContent(GraphForm GR,ObjectList Points,ObjectList Colors)
        {
            //TODO
        }

        public static void DrawLine(GraphForm GR,ObjectList Points,Color CLR)
        {
            //TODO
        }
    }
}
