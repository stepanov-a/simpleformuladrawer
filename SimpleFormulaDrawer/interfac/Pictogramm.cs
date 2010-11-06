using System;
using System.Windows.Controls;

namespace SimpleFormulaDrawer.interfac
{
    public class Pictogramm : Button //класс-наследник от кнопки, содержащий в себе граф. форму (сюда же и листбокс надо копировать, по идее)
    {

        private System.Windows.Controls.ListBox FornulList; //передается конструктором, но может быть изменен.
        public GraphForm GraphForm; //форма, с графиком

        //constructor
        public Pictogramm(ListBox formullist)
        { //конструктор забивает в форму диапазон построения. Если диапазон изменился-форма пересоздается (все равно все перерисовывать)

            this.FornulList = formullist;
            this.GraphForm = new GraphForm();
            this.GraphForm.Show();
        }//End Pictogramm Constructor

    }
}