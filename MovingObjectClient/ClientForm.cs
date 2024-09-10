using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MovingObject
{
    public partial class ClientForm : Form
    {
        private Pen red = new Pen(Color.Red);
        private Rectangle rect = new Rectangle(20, 20, 30, 30);
        private SolidBrush fillBlue = new SolidBrush(Color.Blue);
        private int slide = 10;

        private TcpClient client;
        private NetworkStream ns;
        private System.Windows.Forms.Timer timer;

        public ClientForm()
        {
            InitializeComponent();
            timer1.Interval = 50;
            timer1.Enabled = true;
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            client = new TcpClient("127.0.0.1", 8888);
            ns = client.GetStream();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            timer.Tick += ClientForm_Tick;
            timer.Start();
        }

        private void ClientForm_Tick(object sender, EventArgs e)
        {
            ReceiveSlide();
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

        private void ReceiveSlide()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = ns.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            if (int.TryParse(message, out int receivedSlide))
            {
                slide = receivedSlide;
            }
        }

        private void ClientForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangle(red, rect);
            g.FillRectangle(fillBlue, rect);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ns.Close();
            client.Close();
            base.OnFormClosing(e);
        }
    }
}
