using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MovingObject
{
    public partial class ServerForm : Form
    {
        private Pen red = new Pen(Color.Red);
        private Rectangle rect = new Rectangle(20, 20, 30, 30);
        private SolidBrush fillBlue = new SolidBrush(Color.Blue);
        private int slide = 10;

        private TcpListener server;
        private Thread serverThread;
        private bool isRunning = true; 

        public ServerForm()
        {
            InitializeComponent();
            timer1.Interval = 50;
            timer1.Enabled = true;
            StartServer();
        }

        private void StartServer()
        {
            serverThread = new Thread(() =>
            {
                server = new TcpListener(IPAddress.Any, 8888);
                server.Start();
                while (isRunning)
                {
                    try
                    {
                        TcpClient client = server.AcceptTcpClient();
                        Thread clientThread = new Thread(() => HandleClient(client));
                        clientThread.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error accepting client: " + ex.Message);
                    }
                }
            });
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void HandleClient(TcpClient client)
        {
            using (NetworkStream ns = client.GetStream())
            {
                try
                {
                    while (isRunning)
                    {
                        string message = slide.ToString();
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        ns.Write(data, 0, data.Length);
                        Thread.Sleep(50); 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error handling client: " + ex.Message);
                }
            }
        }

        private void ServerForm_Tick(object sender, EventArgs e)
        {
            back();

            rect.X += slide;
            Invalidate();  
        }

        private void back()
        {
            if (rect.X >= this.Width - rect.Width * 2)
                slide = -10;
            else if (rect.X <= rect.Width / 2)
                slide = 10;
        }

        private void ServerForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangle(red, rect);
            g.FillRectangle(fillBlue, rect);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false; 
            if (server != null)
            {
                server.Stop();
            }
            if (serverThread != null && serverThread.IsAlive)
            {
                serverThread.Join(); 
            }
            base.OnFormClosing(e);
        }
    }
}
