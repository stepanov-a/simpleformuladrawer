using System;
using System.Windows.Controls;
using System.Collections.Generic;
using SimpleFormulaDrawer.Core;
using System.CodeDom.Compiler;

namespace SimpleFormulaDrawer.interfac
{
    public class Pictogramm : Button //класс-наследник от кнопки, содержащий в себе граф. форму (сюда же и листбокс надо копировать, по идее)
    {

        private ListBox FormulList=new ListBox(); //передается конструктором, но может быть изменен.
        private GraphForm GraphForm; //форма, с графиком
        private double Minx, Maxx, Miny, Maxy; //GraphBorders
        private int Quality; //Quality of drawing
        private LibraryManager LMGR=new LibraryManager(); //Текущий менеджер библиотек.
        private List<bool> List3D=new List<bool>(); //Какие из функций 3дшные где 3дшная там true
        private bool Show3D = true; // Флаг, который показывает отображать ли 3ю ось

        public Pictogramm(int Minx, int Maxx , int Miny, int Maxy, int Quality)//constructor
        {
            this.Minx = Minx;
            this.Maxx = Maxx;
            this.Miny = Miny;
            this.Maxy = Maxy;
            this.Quality = Quality;
            this.GraphForm = new GraphForm(Minx,Maxx,Miny,Maxy,Quality);
            this.GraphForm.Show();
        }

        private void RedrawFunctions()
        {
            //throw new NotImplementedException();
        }

        public void ChangeQuality(int How)
        {
            Quality = How;
            RedrawFunctions();
        }

        public CompilerErrorCollection AddFunction(ListBoxItem What)
        {
            FormulList.Items.Add(What);
            var FParams = LMGR.AddFunction(What.Content.ToString());
            List3D.Add(FParams.Is3D);
            RedrawFunctions();
            return FParams.Errors;
        }

        public void RemoveFunction(ListBoxItem What)
        {
            try
            {
                LMGR.RemoveFunction(FormulList.Items.IndexOf(What));
                List3D.RemoveAt(FormulList.Items.IndexOf(What));
                FormulList.Items.Remove(What);
                RedrawFunctions();
            }
            catch (Exception)
            {
                return;
            }
        }

        public void RemoveFunction(int Index)
        {
            try
            {
                FormulList.Items.RemoveAt(Index);
                List3D.RemoveAt(Index);
                LMGR.RemoveFunction(Index);
            }
            catch (Exception)
            {
                return;
            }
        }
        
        public void ChangeDrawingBorders(int Flag,params int[] How)
        {
            /*
             Flag can containt that values or their combinations:
             * Values
             * 0x0:Unhandled
             * 0x1:MinX
             * 0x2:MaxX
             * 0x4:MinY
             * 0x8:MaxY
             * Combinations (Sample):
             * 0xF:AllBorders
             * 0x3:MinX and MaxX
             * NOTE: IF HOW VARIABLE HAS INVALID LENGTH, PROGRAM FAILS. I'LL CHANGE IT LATER.
             * Order of How Elements:MinX,MaxX,MinY,MaxY. Some may be deleted.
             */
            int ParamNum = 0;
            try
            {
                if ((Flag & 0x1) == Flag)
                {
                    this.Minx = How[ParamNum];
                    ParamNum++;
                }
                if ((Flag & 0x2) == Flag)
                {
                    this.Maxx = How[ParamNum];
                    ParamNum++;
                }
                if ((Flag & 0x4) == Flag)
                {
                    this.Miny = How[ParamNum];
                    ParamNum++;
                }
                if ((Flag & 0x8) == Flag)
                {
                    this.Maxy = How[ParamNum];
                }
            }
            catch (Exception)
            {}
            RedrawFunctions();
        }
    }
}