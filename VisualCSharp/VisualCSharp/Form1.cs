using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VALUE = System.Int32;
using ID = System.UInt32; // ulong

namespace VisualCSharp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            this.Text = System.String.Format("Visual C#, {0}를 만나다", Constants.SCRIPT);

            this.groupBox1.Text = System.String.Format("1장 - C#에서 {0} 호출하기", Constants.SCRIPT);
            this.button1.Text = System.String.Format("예제 1 - {0} 스크립트\n파일 실행하기", Constants.SCRIPT);
            this.button2.Text = System.String.Format("예제 2 - {0} 전역 변수\n 조작하기", Constants.SCRIPT);
            this.button3.Text = System.String.Format("예제 3 - {0} 함수\n호출하기", Constants.SCRIPT);


            this.groupBox2.Text = System.String.Format("2장 - {0}에서 C# 호출하기", Constants.SCRIPT);
            this.button4.Text = System.String.Format("예제 4 - {0}에서 호출 가능한\nC# 함수 만들기 1", Constants.SCRIPT);
            this.button5.Text = System.String.Format("예제 5 - {0}에서 호출 가능한\nC# 함수 만들기 2", Constants.SCRIPT);
            this.button6.Text = System.String.Format("예제 6 - {0}에서 호출 가능한\nC# 함수 만들기 3", Constants.SCRIPT);


            this.groupBox3.Text = System.String.Format("3장 - {0} 확장 모듈(DLL) 만들기", Constants.SCRIPT);
            this.button7.Text = System.String.Format("예제 7 - MyDbgString을 DLL로\n구현하기", Constants.SCRIPT);
            this.button8.Text = System.String.Format("예제 8 - DLL을 {0} 스크립트에서\n직접 호출할 수 있도록 만들기", Constants.SCRIPT);


            win32.SetDllDirectory("C:/Script/Ruby/bin");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            string SCRIPT_FILE_1 = "C:/Temp/Script/Sample01.rb";
            IntPtr ScriptFile;

            try
            {
                bSuccess = Script.InitRuby();

                ScriptFile = Script.rb_str_new_cstr(SCRIPT_FILE_1);
                Script.rb_load(ScriptFile, 0);

                bSuccess = true;
            }
            finally
            {
                Script.ruby_cleanup(0);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            string SCRIPT_FILE_1 = "C:/Temp/Script/Sample02.rb";
            IntPtr ScriptFile;

            VALUE WelcomMessage = 0;
            string  szWelcomMessage;

            VALUE WhoamI = 0;
            string szWhoamI;

            VALUE Version = 0;
            long nVersion;


            try
            {
                unsafe { 
                    bSuccess = Script.InitRuby();

                    ScriptFile = Script.rb_str_new_cstr(SCRIPT_FILE_1);
                    Script.rb_load(ScriptFile, 0);

                    WelcomMessage = Script.rb_gv_get("szWelcomMessage");
                    szWelcomMessage = Script.StringValue2String(WelcomMessage);

                    WhoamI = Script.rb_gv_get("szWhoamI");
                    szWhoamI = Script.StringValue2String(WhoamI);

                    Version = Script.rb_gv_get("nVersion");
                    nVersion = Script.rb_num2int(Version);


                    System.Diagnostics.Trace.WriteLine("szWelcomMessage = " + szWelcomMessage);
                    System.Diagnostics.Trace.WriteLine("szWhoamI = " + szWhoamI);
                    System.Diagnostics.Trace.WriteLine("nVersion = " + nVersion.ToString());


                    //IntPtr pWhoamI = Script.rb_str_new_cstr("Visual C#");
                    Script.rb_gv_set("szWhoamI", Script.rb_str_new_cstr("Visual C#").ToInt32());

                    WhoamI = Script.rb_gv_get("szWhoamI");
                    szWhoamI = Script.StringValue2String(WhoamI);


                    System.Diagnostics.Trace.WriteLine("szWhoamI = " + szWhoamI);

                    bSuccess = true;
                }

                bSuccess = true;
            }
            finally
            {
                Script.ruby_cleanup(0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            string SCRIPT_FILE_1 = "C:/Temp/Script/Sample03.rb";
            IntPtr ScriptFile;

            VALUE WelcomMessage = 0;
            string szWelcomMessage;

            VALUE WhoamI = 0;
            string szWhoamI;

            ID myfunc_0 = 0;
            ID myfunc_1 = 0;
            ID myfunc_2 = 0;
            ID myfunc_3 = 0;
            ID myfunc_4 = 0;
            ID myfunc_5 = 0;

            try
            {
                unsafe
                {
                    bSuccess = Script.InitRuby();

                    ScriptFile = Script.rb_str_new_cstr(SCRIPT_FILE_1);
                    Script.rb_load(ScriptFile, 0);

                    WelcomMessage = Script.rb_gv_get("szWelcomMessage");
                    szWelcomMessage = Script.StringValue2String(WelcomMessage);

                    WhoamI = Script.rb_gv_get("szWhoamI");
                    szWhoamI = Script.StringValue2String(WhoamI);


                    System.Diagnostics.Trace.WriteLine("szWelcomMessage = " + szWelcomMessage);
                    System.Diagnostics.Trace.WriteLine("szWhoamI = " + szWhoamI);



                    ///////////////////////////////////////////////////////////////////
                    // 함수(myfunc_0) 호출하기
                    myfunc_0 = Script.rb_intern("myfunc_0");
                    VALUE ReturnValue = Script.rb_funcall(0, myfunc_0, 0, __arglist());

                    System.Diagnostics.Trace.WriteLine("[myfunc_0]ReturnValue = " + Script.StringValue2String(ReturnValue));
                    System.Diagnostics.Trace.WriteLine("[myfunc_0]szWhoamI = " + Script.StringValue2String(Script.rb_gv_get("szWhoamI")));



                    ///////////////////////////////////////////////////////////////////
                    // 함수(myfunc_1) 호출하기
                    myfunc_1 = Script.rb_intern("myfunc_1");
                    ReturnValue = Script.rb_funcall(0, myfunc_1, 0, __arglist());

                    if(ruby_special_consts.RUBY_Qnil == (ruby_special_consts)ReturnValue)
                    {
                        System.Diagnostics.Trace.WriteLine("[myfunc_1]ReturnValue = NIL");
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine("[myfunc_1]ReturnValue = " + Script.StringValue2String(ReturnValue));
                    }

                    System.Diagnostics.Trace.WriteLine("[myfunc_1]szWhoamI = " + Script.StringValue2String(Script.rb_gv_get("szWhoamI")));


                    ///////////////////////////////////////////////////////////////////
                    // 함수(myfunc_2) 호출하기
                    myfunc_2 = Script.rb_intern("myfunc_2");
                    ReturnValue = Script.rb_funcall(0, myfunc_2, 0, __arglist());

                    if (ruby_special_consts.RUBY_Qnil == (ruby_special_consts)ReturnValue)
                    {
                        System.Diagnostics.Trace.WriteLine("[myfunc_2]ReturnValue = NIL");
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine("[myfunc_2]ReturnValue = " + Script.StringValue2String(ReturnValue));
                    }

                    System.Diagnostics.Trace.WriteLine("[myfunc_2]szWhoamI = " + Script.StringValue2String(Script.rb_gv_get("szWhoamI")));

                    ///////////////////////////////////////////////////////////////////
                    // 함수(myfunc_3) 호출하기
                    myfunc_3 = Script.rb_intern("myfunc_3");
                    ReturnValue = Script.rb_funcall(0, myfunc_3, 0, __arglist());

                    if (ruby_special_consts.RUBY_Qnil == (ruby_special_consts)ReturnValue)
                    {
                        System.Diagnostics.Trace.WriteLine("[myfunc_3]ReturnValue = NIL");
                    }
                    else
                    {
                        string bReturn = (ruby_special_consts.RUBY_Qtrue == (ruby_special_consts)Script.rb_ary_entry(ReturnValue, 1)) ? "true" : "false";
                        System.Diagnostics.Trace.WriteLine("[myfunc_3]ReturnValue = [" + Script.StringValue2String(Script.rb_ary_entry(ReturnValue, 0)) + ", " +
                             bReturn + "]");
                    }

                    System.Diagnostics.Trace.WriteLine("[myfunc_3]szWhoamI = " + Script.StringValue2String(Script.rb_gv_get("szWhoamI")));

                    ///////////////////////////////////////////////////////////////////
                    // 함수(myfunc_4) 호출하기
                    myfunc_4 = Script.rb_intern("myfunc_4");
                    ReturnValue = Script.rb_funcall(0, myfunc_4, 1, __arglist(Script.rb_str_new_cstr("myfunc_4")));

                    if (ruby_special_consts.RUBY_Qnil == (ruby_special_consts)ReturnValue)
                    {
                        System.Diagnostics.Trace.WriteLine("[myfunc_4]ReturnValue = NIL");
                    }
                    else
                    {
                        string bReturn = (ruby_special_consts.RUBY_Qtrue == (ruby_special_consts)Script.rb_ary_entry(ReturnValue, 1)) ? "true" : "false";
                        System.Diagnostics.Trace.WriteLine("[myfunc_4]ReturnValue = [" + Script.StringValue2String(Script.rb_ary_entry(ReturnValue, 0)) + ", " +
                             bReturn + "]");
                    }

                    System.Diagnostics.Trace.WriteLine("[myfunc_4]szWhoamI = " + Script.StringValue2String(Script.rb_gv_get("szWhoamI")));

                    ///////////////////////////////////////////////////////////////////
                    // 함수(myfunc_5) 호출하기
                    myfunc_5 = Script.rb_intern("myfunc_5");
                    ReturnValue = Script.rb_funcall(0, myfunc_5, 2, __arglist(Script.rb_str_new_cstr("myfunc_5"), ruby_special_consts.RUBY_Qfalse));

                    if (ruby_special_consts.RUBY_Qnil == (ruby_special_consts)ReturnValue)
                    {
                        System.Diagnostics.Trace.WriteLine("[myfunc_5]ReturnValue = NIL");
                    }
                    else
                    {
                        string bReturn = (ruby_special_consts.RUBY_Qtrue == (ruby_special_consts)Script.rb_ary_entry(ReturnValue, 1)) ? "true" : "false";
                        System.Diagnostics.Trace.WriteLine("[myfunc_5]ReturnValue = [" + Script.StringValue2String(Script.rb_ary_entry(ReturnValue, 0)) + ", " +
                             bReturn + "]");
                    }

                    System.Diagnostics.Trace.WriteLine("[myfunc_5]szWhoamI = " + Script.StringValue2String(Script.rb_gv_get("szWhoamI")));

                    bSuccess = true;
                }

                bSuccess = true;
            }
            finally
            {
                Script.ruby_cleanup(0);
            }
        }



        public VALUE MyDbgString1()
        {
            System.Diagnostics.Trace.WriteLine("MyDbgString 호출됨");

            return (VALUE)ruby_special_consts.RUBY_Qnil;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            string SCRIPT_FILE_1 = "C:/Temp/Script/Sample04.rb";
            IntPtr ScriptFile;

            try
            {
                bSuccess = Script.InitRuby();

                ScriptFile = Script.rb_str_new_cstr(SCRIPT_FILE_1);

                Script.rb_define_global_function("DbgString", MyDbgString1, 0);

                Script.rb_load(ScriptFile, 0);

                bSuccess = true;
            }
            finally
            {
                Script.ruby_cleanup(0);
            }
        }


        public VALUE MyDbgString2(VALUE self, VALUE DbgString)
        {
            System.Diagnostics.Trace.WriteLine("MyDbgString 호출됨");

            System.Diagnostics.Trace.WriteLine(Script.StringValue2String(DbgString));

            return (VALUE)ruby_special_consts.RUBY_Qnil;
        }

        unsafe public VALUE MySum2(int argc, VALUE * NumArray, VALUE self)
        {
            int nType = 0;
            int nStart = 0;
            int nEnd = 0;
            int nSum = 0;

            nStart = (int)Script.rb_num2int(NumArray[0]);
            nEnd = (int)Script.rb_num2int(NumArray[1]);
            //nEnd = Script.rb_num2int(NumArray[1]);

            if (nStart <= nEnd)
            {
                for (int nIndex = nStart; nIndex <= nEnd; nIndex++) nSum += nIndex;
            }
            else
            {
                for (int nIndex = nEnd; nIndex <= nStart; nIndex++) nSum += nIndex;
            }

            return Script.rb_int2inum(nSum);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            string SCRIPT_FILE_1 = "C:/Temp/Script/Sample05.rb";
            IntPtr ScriptFile;

            try
            {
                bSuccess = Script.InitRuby();

                ScriptFile = Script.rb_str_new_cstr(SCRIPT_FILE_1);

                Script.rb_define_global_function("DbgString", MyDbgString2, 1);
                unsafe { Script.rb_define_global_function("Sum", MySum2, -1); }

                Script.rb_load(ScriptFile, 0);

                bSuccess = true;
            }
            finally
            {
                Script.ruby_cleanup(0);
            }
        }



        public VALUE MyDbgString3(VALUE self, VALUE DbgString)
        {
            System.Diagnostics.Trace.WriteLine("MyDbgString 호출됨");

            System.Diagnostics.Trace.WriteLine(Script.StringValue2String(DbgString));

            return (VALUE)ruby_special_consts.RUBY_Qnil;
        }

        unsafe public VALUE MySum3(VALUE self, VALUE NumArray)
        {
            int nType = 0;
            int nStart = 0;
            int nEnd = 0;
            int nSum = 0;

            nStart = (int)Script.rb_num2int(Script.rb_ary_entry(NumArray, 0));
            nEnd = (int)Script.rb_num2int(Script.rb_ary_entry(NumArray, 1));

            if (nStart <= nEnd)
            {
                for (int nIndex = nStart; nIndex <= nEnd; nIndex++) nSum += nIndex;
            }
            else
            {
                for (int nIndex = nEnd; nIndex <= nStart; nIndex++) nSum += nIndex;
            }

            return Script.rb_int2inum(nSum);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool bSuccess = false;
            string SCRIPT_FILE_1 = "C:/Temp/Script/Sample06.rb";
            IntPtr ScriptFile;

            try
            {
                bSuccess = Script.InitRuby();

                ScriptFile = Script.rb_str_new_cstr(SCRIPT_FILE_1);

                Script.rb_define_global_function("DbgString", MyDbgString3, 1);
                unsafe { Script.rb_define_global_function("Sum", MySum3, -2); }

                Script.rb_load(ScriptFile, 0);

                bSuccess = true;
            }
            finally
            {
                Script.ruby_cleanup(0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
