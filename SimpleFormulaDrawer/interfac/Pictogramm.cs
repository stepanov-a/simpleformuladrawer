using System;
using System.Windows.Controls;

namespace SimpleFormulaDrawer.interfac
{
    public class Pictogramm : Button //класс-наследник от кнопки, содержащий в себе граф. форму (сюда же и листбокс надо копировать, по идее)
    {

        private System.Windows.Controls.ListBox FornulList; //передается конструктором, но может быть изменен.
        public GraphForm GraphForm; //форма, с графиком
        //диапазон построения
        private Double xmin;
        private double ymin;
        private double xmax;
        private double ymax;
        //диапазон построения

        //constructor
        public Pictogramm(Double xmin, double ymin, double xmax, double ymax, System.Windows.Controls.ListBox formullist)
        { //конструктор забивает в форму диапазон построения. Если диапазон изменился-форма пересоздается (все равно все перерисовывать)
            this.xmin = xmin;
            this.xmax = xmax;
            this.ymin = ymin;
            this.ymax = ymax;
            this.FornulList = formullist;
            this.GraphForm = new GraphForm();
            this.GraphForm.Show();
        }//End Pictogramm Constructor

    }
}