using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VALUE = System.Int32;
using ID = System.UInt32; // ulong


namespace VisualCSharp
{
    static class Constants
    {
        public const string SCRIPT = "Ruby";
    }
    public enum ruby_special_consts
    {
        RUBY_Qfalse = 0,		/* ...0000 0000 */

        RUBY_Qtrue = 2,		/* ...0000 0010 */

        RUBY_Qnil = 4,		/* ...0000 0100 */

        RUBY_Qundef = 6,		/* ...0000 0110 */
    }
    static class win32
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetDllDirectory(string lpPathName);
    }

    class Script
    {
        [UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl‌​)]
        unsafe public delegate VALUE RubyFunction0();
        [UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl‌​)]
        unsafe public delegate VALUE RubyFunction1(VALUE self, VALUE argv);
        [UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.Cdecl‌​)]
        unsafe public delegate VALUE RubyFunction2(int argc, VALUE * argv, VALUE self);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "ruby_init_stack", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ruby_init_stack(ref ulong stack_frame);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "ruby_sysinit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void ruby_sysinit(ref int argc, char*[] argv);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "ruby_init", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void ruby_init();

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "ruby_init_loadpath", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void ruby_init_loadpath();

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "ruby_cleanup", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int ruby_cleanup(int nState);


        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_str_new_cstr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern IntPtr rb_str_new_cstr(string path);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_str_new_cstr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rb_eval_string_protect(string script, ref VALUE pstate);


        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_gv_set", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern VALUE rb_gv_set(string script, VALUE pValue);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_gv_get", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern VALUE rb_gv_get(string script);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_string_value_ptr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern string rb_string_value_ptr(ref VALUE pValue);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_string_value_cstr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern IntPtr rb_string_value_cstr(ref VALUE pValue);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_num2int", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int rb_num2int(VALUE nValue);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_int2inum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern VALUE rb_int2inum(int nValue);

        

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_intern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern ID rb_intern(string function);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_funcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern VALUE  rb_funcall(VALUE id, ID func, int nArg, __arglist);


        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_ary_entry", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern VALUE rb_ary_entry(VALUE array, long index);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_define_global_function", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void rb_define_global_function(string function, RubyFunction0 rb_function, int nType);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_define_global_function", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void rb_define_global_function(string function, RubyFunction1 rb_function, int nType);

        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_define_global_function", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void rb_define_global_function(string function, RubyFunction2 rb_function, int nType);

        unsafe public static string StringValue2String(VALUE value)
        {
            int length = 0;
            IntPtr ptr = Script.rb_string_value_cstr(ref value);
            byte* p = (byte*)ptr;
            while (*p != 0) 
            {
                length++;
                p++;
            }
            byte[] bytes = new byte[length];
            Marshal.Copy(ptr, bytes, 0, length);
            return Encoding.UTF8.GetString(bytes);
        }


        [DllImport("msvcrt-ruby230.dll", EntryPoint = "rb_load", CallingConvention = CallingConvention.Cdecl)]
        public static extern void rb_load(IntPtr fname, VALUE wrap);


        public static bool InitRuby()
        {
            
            ulong variable_in_this_stack_frame = 0;
            bool bSuccess = false;

            try
            {
                unsafe
                {
                    int argc = 0;
                    char * [] argv = new char*[] { null };

                    ruby_sysinit(ref argc, argv);
                    ruby_init_stack(ref variable_in_this_stack_frame);
                    ruby_init();
                    ruby_init_loadpath();

                    bSuccess = true;
                }
            }
            finally
            {
                if(false == bSuccess)
                       ruby_cleanup(0);
            }
            return bSuccess;
        }

    }
}
