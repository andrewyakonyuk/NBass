using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NBass
{
    /// <summary>
    /// Summary description for Helper.
    /// </summary>
    internal class Helper
    {
        private Helper()
        {
        }

        #region Functions

        public static int MakeLong(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        public static int HiWord(int Number)
        {
            return (Number >> 16) & 0xffff;
        }

        public static int LoWord(int Number)
        {
            return Number & 0xffff;
        }

        public static int Bool2Int(bool input)
        {
            int output = 0;
            if (input) output++;
            return output;
        }

        public static bool Int2Bool(int input)
        {
            bool output = true;
            if (input == 0) output = false;
            return output;
        }

        #endregion Functions

        public static string PrintFlags(object enumvalue)
        {
            Type t = enumvalue.GetType();
            string output = "";
            foreach (string enumName in (string[])Enum.GetNames(t))
            {
                // We add the enum name, but only if it is selected.
                if ((((int)enumvalue) & (int)Enum.Parse(t, enumName)) != 0)
                    output += "[" + enumName + "]";
            }
            return output;
        }

        public static string IntPtrAsStringUtf8(IntPtr utf8Ptr)
        {
            if (utf8Ptr != IntPtr.Zero)
            {
                int num = Length(utf8Ptr);
                if (num != 0)
                {
                    byte[] array = new byte[num];
                    Marshal.Copy(utf8Ptr, array, 0, num);
                    return Encoding.UTF8.GetString(array, 0, num);
                }
            }
            return null;
        }

        public static string IntPtrAsStringAnsi(IntPtr ansiPtr)
        {
            if (ansiPtr != IntPtr.Zero)
            {
                return Marshal.PtrToStringAnsi(ansiPtr);
            }
            return null;
        }

        private static int Length(IntPtr A_0)
        {
            int num = 0;
            while (Marshal.ReadByte(A_0, num) != 0)
            {
                num++;
            }
            return num;
        }
    }
}