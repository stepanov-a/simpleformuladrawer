using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;

namespace SimpleFormulaDrawer.Core
{
    public static class ConfigurationSystem
    {
        private static StreamReader ConfigFile;
        private static readonly Dictionary<string,string> ConfigParameters = new Dictionary<string, string>();
        private static MessageBoxResult MR;

        static ConfigurationSystem()
        {
            ReloadConfig();
        }
        
        public static void ReloadConfig()
        {
            string Temp;
            var Params = new string[] { };
            try
            {
                ConfigFile = new StreamReader("config.conf");
            }
            catch
            {
                MR = MessageBox.Show("Нет конфигурационного файла. Будут использованы значения по умолчанию.");
            }
            if (ConfigFile != null)
            {
                while (!ConfigFile.EndOfStream)
                {
                    Temp = ConfigFile.ReadLine();
                    if (Temp == null)
                    {
                        MR = MessageBox.Show("Ошибка чтения конфигурационного файла");
                    }
                    else
                    {
                        if (Temp[0] != '#')
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
            }
        }

        public static T ReadConfig<T>(string What)
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

        public static T ReadConfig<T>(string What,T Def)
        {
            object TMP;
            string OUTVAR;
            if (ConfigParameters.TryGetValue(What, out OUTVAR))
            {
                TMP = OUTVAR;
                return (T)TMP;
            }
            else
            {
                ConfigParameters.Add(What,(Def as object).ToString());
                WriteConfigFile();
                return Def;
            }            
        }

        private static void WriteConfigFile()
        {
            var Config = new StreamWriter("config.conf");
            try
            {
                Config.WriteLine("#Configuration");
            }
            catch (Exception)
            {
                MR =MessageBox.Show(
                        @"Невозможно обновить конфигурационный файл 
 Проверьте, есть ли доступ на чтение к папке программы.");
            }

            foreach (var Item in ConfigParameters)
            {
                Config.WriteLine(Item.Key,'=',Item.Value);
            }
            return;
        }
    }
}