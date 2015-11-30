﻿using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace KPP_NetHtml5
{
    public partial class Form1 : Form
    {

        private ChromiumWebBrowser browser;

        //        WebSocket ws = new WebSocket("ws://localhost:4649/Echo");
        //     Notifier nf = new Notifier();

        WebSocketServer wssv = new WebSocketServer(4649);
        DataProvider dp = new DataProvider();
        Echo echo = new Echo();
        public Form1()
        {
            InitializeComponent();
            ResizeBegin += (s, e) => SuspendLayout();
            ResizeEnd += (s, e) => ResumeLayout(true);
        }

        private void InitializeWS()
        {

            
#if DEBUG
            // To change the logging level.
            wssv.Log.Level = LogLevel.Trace;
            wssv.Log.File = "D:\\wslo.txt";

            // To change the wait time for the response to the WebSocket Ping or Close.
            wssv.WaitTime = TimeSpan.FromSeconds(2);
#endif
            /* To provide the secure connection.
            var cert = ConfigurationManager.AppSettings["ServerCertFile"];
            var passwd = ConfigurationManager.AppSettings["CertFilePassword"];
            wssv.SslConfiguration.ServerCertificate = new X509Certificate2 (cert, passwd);
             */

            /* To provide the HTTP Authentication (Basic/Digest).
            wssv.AuthenticationSchemes = AuthenticationSchemes.Basic;
            wssv.Realm = "WebSocket Test";
            wssv.UserCredentialsFinder = id => {
              var name = id.Name;
              // Return user name, password, and roles.
              return name == "nobita"
                     ? new NetworkCredential (name, "password", "gunfighter")
                     : null; // If the user credentials aren't found.
            };
             */

            // Not to remove the inactive sessions periodically.
            wssv.KeepClean = true;

            // To resolve to wait for socket in TIME_WAIT state.
            //wssv.ReuseAddress = true;

            // Add the WebSocket services.
            //wssv.AddWebSocketService("",)
            //wssv.AddWebSocketService<Echo>("/Echo",()=>echo);
            wssv.AddWebSocketService<DataProvider>("/DataProvider",()=>dp);
            
            //wssv.A
            //wssv.WebSocketServices.TryGetServiceHost()
            //wssv.AddWebSocketService<Chat>("/Chat");

            /* Add the WebSocket service with initializing.
            wssv.AddWebSocketService<Chat> (
              "/Chat",
              () => new Chat ("Anon#") {
                Protocol = "chat",
                // To emit a WebSocket.OnMessage event when receives a Ping.
                EmitOnPing = true,
                // To ignore the Sec-WebSocket-Extensions header.
                IgnoreExtensions = true,
                // To validate the Origin header.
                OriginValidator = val => {
                  // Check the value of the Origin header, and return true if valid.
                  Uri origin;
                  return !val.IsNullOrEmpty () &&
                         Uri.TryCreate (val, UriKind.Absolute, out origin) &&
                         origin.Host == "localhost";
                },
                // To validate the Cookies.
                CookiesValidator = (req, res) => {
                  // Check the Cookies in 'req', and set the Cookies to send to the client with 'res'
                  // if necessary.
                  foreach (Cookie cookie in req) {
                    cookie.Expired = true;
                    res.Add (cookie);
                  }
                  return true; // If valid.
                }
              });
             */

            wssv.Start();
            //
            if (wssv.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);
                foreach (var path in wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

            #region Client

            //            ws.OnOpen += Ws_OnOpen;

            //            ws.OnMessage += (sender, e) =>
            //              nf.Notify(
            //                new NotificationMessage
            //                {
            //                    Summary = "WebSocket Message",
            //                    //Body = !e.Type ? e.Data : "Received a ping.",
            //                    //Body = !e.Type ? e.Data : "Received a ping.",
            //                    Icon = "notification-message-im"
            //                });

            //            ws.OnError += (sender, e) =>
            //              nf.Notify(
            //                new NotificationMessage
            //                {
            //                    Summary = "WebSocket Error",
            //                    Body = e.Message,
            //                    Icon = "notification-message-im"
            //                });

            //            ws.OnClose += (sender, e) =>
            //              nf.Notify(
            //                new NotificationMessage
            //                {
            //                    Summary = String.Format("WebSocket Close ({0})", e.Code),
            //                    Body = e.Reason,
            //                    Icon = "notification-message-im"
            //                });

            //#if DEBUG
            //            // To change the logging level.
            //            ws.Log.Level = LogLevel.Trace;

            //            // To change the wait time for the response to the Ping or Close.
            //            ws.WaitTime = TimeSpan.FromSeconds(10);

            //            // To emit a WebSocket.OnMessage event when receives a ping.
            //            ws.EmitOnPing = true;
            //#endif
            //            // To enable the Per-message Compression extension.
            //            //ws.Compression = CompressionMethod.Deflate;

            //            /* To validate the server certificate.
            //            ws.SslConfiguration.ServerCertificateValidationCallback =
            //              (sender, certificate, chain, sslPolicyErrors) => {
            //                ws.Log.Debug (
            //                  String.Format (
            //                    "Certificate:\n- Issuer: {0}\n- Subject: {1}",
            //                    certificate.Issuer,
            //                    certificate.Subject));
            //                return true; // If the server certificate is valid.
            //              };
            //             */

            //            // To send the credentials for the HTTP Authentication (Basic/Digest).
            //            //ws.SetCredentials ("nobita", "password", false);

            //            // To send the Origin header.
            //            //ws.Origin = "http://localhost:4649";

            //            // To send the Cookies.
            //            //ws.SetCookie (new Cookie ("name", "nobita"));
            //            //ws.SetCookie (new Cookie ("roles", "\"idiot, gunfighter\""));

            //            // To connect through the HTTP Proxy server.
            //            //ws.SetProxy ("http://localhost:3128", "nobita", "password");

            //            // To enable the redirection.
            //            //ws.EnableRedirection = true;

            //            // Connect to the server.
            //            ws.Connect();

            //            // Connect to the server asynchronously.
            //            //ws.ConnectAsync ();

            #endregion
        }

        private void Ws_OnOpen(object sender, EventArgs e)
        {
//            ws.Send("Hi, there!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            button3_Click(this, null);
            wssv.Stop();
        }


    
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            
            
        }

        double cosd(double angle)
        {
            double angleradians = angle * Math.PI/ 180.0f;
            return Math.Cos(angleradians);
        }

        private bool stopsend = false;
        double x=0;
        private void DoWork()
        {
            Random random = new Random();
            
            while (stopsend==false)
            {
                int randomNumber = random.Next(0, 10);
                var data = Math.Round(cosd(x),2).ToString().Replace(',','.');

                //dp.SendTest(data);
                browser.ExecuteScriptAsync("DrawPointVal("+data.ToString()+");");
                x =x+10;
                if (x==360)
                {
                    x = 0;

                }
                Thread.Sleep(10);
            }
            stopsend = false;
        }
        Thread workerThread = null;
        private void button2_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button2.Enabled = false;
            workerThread = new Thread(DoWork);
            workerThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateBrowser();
        }

        private void CreateBrowser()
        {
            browser = new ChromiumWebBrowser("file:///C:/Users/automacao.KEYEU/Documents/GitHub/KPP_NetHtml5/KPP_NetHtml5/pages/index.html")
            {
                Dock = DockStyle.Fill,
            };
            __htmlContainer.Controls.Add(browser);

            browser.LoadingStateChanged += OnBrowserLoadingStateChanged;
            browser.ConsoleMessage += OnBrowserConsoleMessage;
            browser.StatusMessage += OnBrowserStatusMessage;
            browser.TitleChanged += OnBrowserTitleChanged;
            browser.AddressChanged += OnBrowserAddressChanged;
            var bdobject = new BoundObject();
            bdobject.MyProperty = 1;
            browser.RegisterJsObject("bound", bdobject);

        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
           // DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
         //   this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnBrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            if (args.IsLoading==false)
            {
                browser.ShowDevTools();

            }
            //SetCanGoBack(args.CanGoBack);
            //SetCanGoForward(args.CanGoForward);

            //this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            //this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            InitializeWS();
            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (workerThread!=null)
            {

                button2.Enabled = true;
                button3.Enabled = false;
                stopsend = true;
                workerThread.Join();
                workerThread = null; 

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            browser.ExecuteScriptAsync("DrawPointVal(10);");
        }
    }
}