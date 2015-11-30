using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace KPP_NetHtml5
{
    public class DataProvider: WebSocketBehavior
    {
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        public DataProvider()
        {
           //this.
        }
        protected override void OnClose(CloseEventArgs e)
        {

            base.OnClose(e);

            this.Sessions.Sweep();
            
        }
        public void SendTest(String val)
        {
        
            Send(val);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }
    }
}
