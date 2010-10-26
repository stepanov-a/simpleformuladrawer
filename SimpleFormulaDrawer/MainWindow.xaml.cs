﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;

namespace SimpleFormulaDrawer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            LibraryManager LMGR=new LibraryManager();
            LMGR.AddFunction("x^x+5SIN(x)-5^x",false);
            LMGR.AddFunction("x*y", true);
            LMGR.AddFunction("System.Diagnostics.Process.Start(\"notepad.exe\")",false);
            LMGR.CompileSource();
            this.textBlock1.Text = LMGR.GetSource();
        }
    }
}
