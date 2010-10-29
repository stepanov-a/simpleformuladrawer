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
        private static readonly string ConfigFileName =/* Application.Current.StartupUri.AbsolutePath+*/"config.conf";

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
                ConfigFile = new StreamReader(ConfigFileName);
            }
            catch
            {
                MessageBox.Show("Нет конфигурационного файла. Будут использованы значения по умолчанию.");
            }
            if (ConfigFile == null) return;
            while (!ConfigFile.EndOfStream)
            {
                Temp = ConfigFile.ReadLine();
                if (Temp == null)
                {
                    MessageBox.Show("Ошибка чтения конфигурационного файла");
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
                            MessageBox.Show(
                                string.Format(
                                    "Параметр {0} прочитан неверно. Возможно, конфигурационный файл испорчен.",
                                    Params[0]));
                        }
                    }
                }
            }
            ConfigFile.Close();
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
            ConfigParameters.Add(What, "");
            WriteConfigFile();
            return default(T);
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
            return Def;
        }

        private static void WriteConfigFile()
        {
            var Config = new StreamWriter(ConfigFileName);
            try
            {
                Config.WriteLine("#Configuration");
            }
            catch (Exception)
            {
                MessageBox.Show(
                        @"Невозможно обновить конфигурационный файл 
 Проверьте, есть ли доступ на чтение к папке программы.");
                return;
            }

            foreach (var Item in ConfigParameters)
            {
                Config.WriteLine(String.Format("{0}={1}",Item.Key,Item.Value));
            }
            Config.Close();
            return;
        }
    }
}