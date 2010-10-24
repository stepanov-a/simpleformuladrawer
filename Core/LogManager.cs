using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Core
{
    private class Log
    {
        private StreamWriter File;

        public Log(string FileName)
        {
            File = new StreamWriter(FileName);
        }

        private ~Log()
        {
            File.Close();
        }

        private string Time()
        {
            return DateTime.Now.ToString();
        }

        public void Write(string What)
        {
            string STR = string.Format("{0}||{1}", Time(), What);
            File.WriteLine(STR);
            WorldStates.AddState(3, STR);
            File.Flush();
        }
    }

    public static class LogManager
    {
        private static Log Debug;
        private static Log Error;
        public static void init(string DebugName,string ErrorName)
        {
            Debug = new Log(DebugName);
            Error=new Log(ErrorName);
            Directory.CreateDirectory("Logs");
            Directory.SetCurrentDirectory("Logs");
        }
        public static void WriteDebug(string What)
        {
            Debug.Write(What);
        }
        public static void WriteError(string What)
        {
            Error.Write(What);
            Debug.Write(What);
        }
        public static ~LogManager() {}
    }
}