using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RGiesecke.DllExport;

namespace RbExt
{
    public class Class1
    {
        [DllExport("Init_RbExt", CallingConvention = CallingConvention.Cdecl)]
        public static void Init_RbExt()
        {
            System.Diagnostics.Trace.WriteLine("Init_RbExt");
        }

        [DllExport("DbgString", CallingConvention = CallingConvention.Cdecl)]
        public static void DbgString(string szDbgString)
        {
            System.Diagnostics.Trace.WriteLine(szDbgString);
        }

        [DllExport("Sum", CallingConvention = CallingConvention.Cdecl)]
        public static int Sum(int nStart, int nEnd)
        {
            int nSum = 0;

            if (nStart <= nEnd)
            {
                for (int nIndex = nStart; nIndex <= nEnd; nIndex++) nSum += nIndex;
            }
            else
            {
                for (int nIndex = nEnd; nIndex <= nStart; nIndex++) nSum += nIndex;
            }

            return nSum;
        }
    }
}
