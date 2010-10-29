using System;
using System.IO;

namespace SimpleFormulaDrawer.Core
{
    public static class LogManager
    {
        private class Log
        {
            private readonly StreamWriter File;

            public Log(string fileName)
            {
                File = new StreamWriter(fileName) {AutoFlush = true};
            }

            private static string Time()
            {
                return DateTime.Now.ToString();
            }

            public void Write(string what)
            {
                var STR = string.Format("{0}||{1}", Time(), what);
                File.WriteLine(STR);
                WorldStates.AddState(2, STR);
            }
        }
        private static Log Debug;
        private static Log Error;
        public static void Init(string debugName,string errorName)
        {
            Debug = new Log(debugName);
            Error=new Log(errorName);
            Directory.CreateDirectory("Logs");
            Directory.SetCurrentDirectory("Logs");
        }
        public static void WriteDebug(string what)
        {
            Debug.Write(what);
        }
        public static void WriteError(string what)
        {
            Error.Write(what);
            Debug.Write(what);
        }
    }
}