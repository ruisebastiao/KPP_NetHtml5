using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace KPP_NetHtml5
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            
            var name = Context.QueryString["name"];
            var msg = !String.IsNullOrEmpty(name) ? String.Format("'{0}' to {1}", e.Data, name) : e.Data;
            Send(msg);
        }
    }
}
