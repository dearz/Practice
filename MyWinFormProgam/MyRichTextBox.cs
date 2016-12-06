using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyProgram
{
    public enum WindowsMessage
    {
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115
    }

    public delegate void SendMessage(Message msg);

    public class MyRichTextBox : RichTextBox
    {
        public event SendMessage SendMessageEvent;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg ==(Int32) WindowsMessage.WM_HSCROLL || m.Msg == (Int32)WindowsMessage.WM_VSCROLL)
            {
                if (SendMessageEvent != null)
                {
                    SendMessageEvent(m);
                }
            }
            base.WndProc(ref m);
        }
        public void Scroll(Message m)
        {
            m.HWnd = this.Handle;
            WndProc(ref m);
        }
    }
}
