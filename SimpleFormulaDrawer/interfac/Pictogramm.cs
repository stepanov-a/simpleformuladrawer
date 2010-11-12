using System;
using System.Windows.Controls;
using System.Collections.Generic;
using SimpleFormulaDrawer.Core;
using System.CodeDom.Compiler;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace SimpleFormulaDrawer.interfac
{
    public class Pictogramm : System.Windows.Controls.Button //класс-наследник от кнопки, содержащий в себе граф. форму (сюда же и листбокс надо копировать, по идее)
    {
        private static readonly int CPUCount = Environment.ProcessorCount; //Не угадаете что
        private System.Windows.Controls.ListBox FormulList = new System.Windows.Controls.ListBox(); //передается конструктором, но может быть изменен.
        private GraphForm GraphForm; //форма, с графиком
        private double Minx, Maxx, Miny, Maxy,Minz,Maxz; //GraphBorders
        private int Quality; //Quality of drawing
        private LibraryManager LMGR=new LibraryManager(); //Текущий менеджер библиотек.
        private int Count3D = 0;//Количество 3х мерных функций
        private bool Show3D = true; // Флаг, который показывает отображать ли 3ю ось

        public Pictogramm(double Minx, double Maxx , double Miny, double Maxy,double Minz, double Maxz, int Quality)//constructor
        {//в качестве параметров передаются границы области определения и качество
            this.Minx = Minx;
            this.Maxx = Maxx;
            this.Miny = Miny;
            this.Maxy = Maxy;
            this.Minz=Minz;
            this.Maxz = Maxz;
            this.Quality = Quality;
            this.GraphForm = new GraphForm(this);
            this.GraphForm.Show();
            this.Click += Click_event;
        }

        public void ClosePictogramm()
        {
            Forms.MF.PictlistBox.Items.Remove(this.Parent);
            Forms.MF.ArrPictogramm.Remove(this);
        }

        private void Click_event(object sender, RoutedEventArgs e)
        {
            GraphForm.BringToFront();
            switch (GraphForm.FormState)
            {
                case 0:
                    {
                        GraphForm.BringToFront();
                        GraphForm.FormState = 1;
                        break;
                    }
                case 1:
                    {
                        GraphForm.Hide();
                        GraphForm.FormState = 2;
                        break;
                    }
                case 2:
                    {
                        GraphForm.Show();
                        GraphForm.FormState = 1;
                        break;
                    }
            }
        }

        private void RedrawFunctions()
        {
            GraphForm.Set3DRendering(Count3D==0);
            GraphForm.DrawAxis(Minx,Maxx,Miny,Maxy,Minz,Maxz);
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
            if (FParams.Is3D) Count3D++;
            if (FParams.Errors.Count==0) RedrawFunctions();
            return FParams.Errors;
        }

        public void RemoveFunction(ListBoxItem What)
        {
            try
            {
                LMGR.RemoveFunction(FormulList.Items.IndexOf(What));
                if (LibraryManager.Check3D(What.Content.ToString())) Count3D--;
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
                if (LibraryManager.Check3D(FormulList.Items[Index].ToString())) Count3D--;
                LMGR.RemoveFunction(Index);
                RedrawFunctions();
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
             * NOTE: IF HOW VARIABLE HAS INVALID LENGTH (Less then need or >4), NOTHING HAPPENS.
             * Order of How Elements:MinX,MaxX,MinY,MaxY. Some may be deleted.
             */
            if (How.Length > 4) return;
            var ParamNum = 0;
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