﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    static class WorldStates
    {
        private static TState SStart;

        public static void AddState(int Code, string Description)
        {
            if (Code<0 && Code!=-2 && Code!=-100)
            {
                FlushStates();
            }
            TState CState;
            if (SStart == null)
            {
                SStart = new TState(Code, Description);
            }
            else
            {
                CState = SStart;
                while (CState != null)
                {
                    CState = CState.N;
                }
                CState = new TState(Code, Description);
            }
        }

        public static TStateDescription GetState()
        {
            if (SStart == null)
            {
                return new TStateDescription(-1, "EMPTY");
            }
            return SStart.C;
        }

        public static void CompleteState()
        {
            if (SStart != null)
            {
                SStart = SStart.N;
            }
        }

        private static void FlushStates()
        {
            SStart = new TState(-2, "0");
            System.GC.Collect();
        }

        public static string Serialize(Object[] What)
        {
            if (What == null)
            {
                return "";
            }
            string Res = "";
            for (int i = 0; i<What.Length; i++)
            {
                if (What[i] != null) { Res += What[i].ToString(); }
                if (i < What.Length - 1) { Res += ";"; }
            }
            return Res;
        }

        public static Object[] UnSerialize(string What, string SerializeMask)
        {
            Object[] Res;
            Res = new object[SerializeMask.Length];
            string[] TMP;
            TMP = What.Split(';');
            if (TMP.Length != SerializeMask.Length)
            {
                return null;
            }
            for (var i = 1; i < SerializeMask.Length; i++)
            {
                switch (SerializeMask[i])
                {
                    case 'i':
                        {
                            Res[i] = int.Parse(TMP[i]);
                            break;
                        }
                    case 'f':
                        {
                            Res[i] = float.Parse(TMP[i]);
                            break;
                        }
                    case 'b':
                        {
                            Res[i] = bool.Parse(TMP[i]);
                            break;
                        }
                    case 's':
                        {
                            Res[i] = TMP[i];
                            break;
                        }
                }
            }
            return Res;
        }//UNSAFE

        public static int GetStateParamsCount()
        {
            if (SStart==null) {return -1;}
            return SStart.C.Description.Split(';').Length;
        }
    }
}