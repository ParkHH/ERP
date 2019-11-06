using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TripERP.Common
{
    /**
    * 출처 : https://m.blog.naver.com/PostView.nhn?blogId=silent1002&logNo=10044272919&proxyReferer=https%3A%2F%2Fwww.google.com%2F
    * 버튼 2개만 보이도록 수정
    */
    public class MessageBoxEx
    {
        delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int hook, HookProc callback, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr GetDlgItem(IntPtr hDlg, DialogResult nIDDlgItem);

        [DllImport("user32.dll")]
        static extern bool SetDlgItemText(IntPtr hDlg, DialogResult nIDDlgItem, string lpString);

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        static IntPtr g_hHook;

        static string yes;
        static string cancel;
        static string no;



        /// <summary>
        /// 메시지 박스를 띠웁니다.
        /// </summary>
        /// <param name="text">텍스트 입니다.</param>
        /// <param name="caption">캡션 입니다.</param>
        /// <param name="yes">예 문자열 입니다.</param>
        /// <param name="no">아니오 문자열 입니다.</param>
        /// <param name="cancel">취소 문자열 입니다.</param>
        /// <returns></returns>
        //public static DialogResult Show(string text, string caption, string yes, string no, string cancel)
        public static DialogResult Show(string text, string caption, string yes, string no)
        {
            MessageBoxEx.yes = yes;
            //MessageBoxEx.cancel = cancel;
            MessageBoxEx.no = no;
            g_hHook = SetWindowsHookEx(5, new HookProc(HookWndProc), IntPtr.Zero, GetCurrentThreadId());
            //return MessageBox.Show(text, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        static int HookWndProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            IntPtr hChildWnd;

            if (nCode == 5)
            {
                hChildWnd = wParam;

                if (GetDlgItem(hChildWnd, DialogResult.Yes) != null)
                    SetDlgItemText(hChildWnd, DialogResult.Yes, MessageBoxEx.yes);

                if (GetDlgItem(hChildWnd, DialogResult.No) != null)
                    SetDlgItemText(hChildWnd, DialogResult.No, MessageBoxEx.no);

                if (GetDlgItem(hChildWnd, DialogResult.Cancel) != null)
                    SetDlgItemText(hChildWnd, DialogResult.Cancel, MessageBoxEx.cancel);

                UnhookWindowsHookEx(g_hHook);
            }
            else
                CallNextHookEx(g_hHook, nCode, wParam, lParam);

            return 0;
        }
    }
}
