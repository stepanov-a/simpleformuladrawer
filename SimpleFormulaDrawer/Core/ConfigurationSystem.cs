using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;

namespace SimpleFormulaDrawer.Core
{
    static class ConfigurationSystem
    {
        private readonly static StreamReader ConfigFile;
        private readonly static Dictionary<string,string> ConfigParameters = new Dictionary<string, string>();
        
        static ConfigurationSystem()
        {
            string Temp;
            var Params = new string[] {};
            MessageBoxResult MR;
            try
            {
                ConfigFile = new StreamReader("config.conf");
            }
            catch (Exception E)
            {
                MR = MessageBox.Show(E.Message);
                App.Current.Shutdown();
            }

            while (!ConfigFile.EndOfStream)
            {
                Temp=ConfigFile.ReadLine();
                if (Temp == null)
                {
                    MR = MessageBox.Show("Ошибка чтения конфигурационного файла");
                }
                else
                {
                    if (Temp.Split('=').Length == 2)
                    {
                        Params = Temp.Split('=');
                        Params[0] = Params[0].Trim();
                        Params[1] = Params[1].Trim();
                        ConfigParameters.Add(Params[0], Params[1]);
                    }
                    else
                    {
                        MR = MessageBox.Show(
                            string.Format(
                                "Параметр {0} прочитан неверно. Возможно, конфигурационный файл испорчен.",
                                Params[0]));
                    }
                }
            }
        }
        
        static T ReadConfig<T>(string What)
        {
            object TMP;
            string OUTVAR;
            if (ConfigParameters.TryGetValue(What,out OUTVAR))
            {
                TMP = OUTVAR;
                return (T)TMP;
            }
            else
            {
                return default(T);
            }
        }
    }
}