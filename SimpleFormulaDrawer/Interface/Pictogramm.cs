﻿using System;
using System.Drawing;
using System.Windows.Controls;
using SimpleFormulaDrawer.Core;
using System.CodeDom.Compiler;
using System.Threading;

namespace SimpleFormulaDrawer.Interface
{
    public partial class Pictogramm : Button //класс-наследник от кнопки, содержащий в себе граф. форму (сюда же и листбокс надо копировать, по идее)
    {
        private static readonly int CPUCount = Environment.ProcessorCount; //Не угадаете что
        private LibraryManager LMGR=new LibraryManager(); //Текущий менеджер библиотек.
        private int Count3D;//Количество 3х мерных функций
        public MainFormContent Datastore;
        public GraphForm GR; // Грфаическая форма.

        public Pictogramm(MainFormContent DataStore)//constructor
        {//в качестве параметров передается структура, описывающая главную форму приложения
            this.Datastore = DataStore;
            this.GR=new GraphForm(this);
            GR.Show();
        }

        public void ClosePictogramm()
        {
            Forms.MF.PictlistBox.Items.Remove(this.Parent);
        }

        private void DrawThreadPart(object ThreadNum)
        {
            try
            {
                var Num = (int)ThreadNum;
            }
            catch (Exception)
            {
                return;
            }
            GR.Redraw();
        }


        private void RedrawFunctions()
        {
            GraphFormHelper.FlushBuffer(GR);
            //Сейчас начнется многопоточная жесть
            for (var i = 0; i < CPUCount;i++ )
            {
                var ComputeThread=new Thread(DrawThreadPart);
                ComputeThread.Start(i);
            }
            //Сейчас многопоточная жесть заканчивается
            GR.Redraw();
        }

        public CompilerErrorCollection AddFunction(ListBoxItem What)
        {
            var FParams = LMGR.CompileFunction(What.Content.ToString());
            if (FParams.Is3D) Count3D++;
            if (FParams.Errors.Count==0) RedrawFunctions();
            RedrawFunctions();
            return FParams.Errors;
        }

        public void RemoveFunction(ListBoxItem What)
        {
            try
            {
                LMGR.RemoveFunction(Datastore.FormulListBox.Items.IndexOf(What));
                if (LibraryManager.Check3D(What.Content.ToString())) Count3D--;
                Datastore.FormulListBox.Items.Remove(What);
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
                Datastore.FormulListBox.Items.RemoveAt(Index);
                if (LibraryManager.Check3D(Datastore.FormulListBox.Items[Index].ToString())) Count3D--;
                LMGR.RemoveFunction(Index);
                RedrawFunctions();
            }
            catch (Exception)
            {
                return;
            }
        }
        
        /// <summary>
        /// Функция для изменения границ рисования
        /// </summary>
        /// <param name="Flag">Комбинация границ</param>
        /// <param name="How">Новые цифры MinX,MaxX,MinY,MaxY,MinZ,MaxZ</param>
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
             * 0x10:MinZ
             * 0x20:MaxZ
             * Combinations (Sample):
             * 0x3F:AllBorders
             * 0x3:MinX and MaxX
             * NOTE: IF HOW VARIABLE HAS INVALID LENGTH (Less then need or >6), NOTHING HAPPENS.
             * Order of How Elements:MinX,MaxX,MinY,MaxY,MinZ,MaxZ. Some may be deleted.
             */
            if (How.Length > 6) return;
            var ParamNum = 0;
            try
            {
                if (Flag == 0x0) throw new AccessViolationException();
                if (HasFlag(Flag,0x1))
                {
                    this.Datastore.MinX = How[ParamNum];
                    ParamNum++;
                }
                if (HasFlag(Flag, 0x2))
                {
                    this.Datastore.MaxX = How[ParamNum];
                    ParamNum++;
                }
                if (HasFlag(Flag, 0x4))
                {
                    this.Datastore.MinY = How[ParamNum];
                    ParamNum++;
                }
                if (HasFlag(Flag, 0x8))
                {
                    this.Datastore.MaxY = How[ParamNum];
                    ParamNum++;
                }
                if (HasFlag(Flag, 0x10))
                {
                    this.Datastore.MinZ = How[ParamNum];
                    ParamNum++;
                }
                if (HasFlag(Flag, 0x20))
                {
                    this.Datastore.MaxZ = How[ParamNum];
                }
            }
            catch (Exception E)
            {
                Forms.DF.AddMessage(E.Message,System.Windows.Media.Colors.Red);
            }
            RedrawFunctions();
        }
    }
}