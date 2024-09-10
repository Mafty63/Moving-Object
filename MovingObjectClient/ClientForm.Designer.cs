
namespace MovingObject
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of managed resources.
                if (components != null)
                {
                    components.Dispose();
                }
                // Dispose of other managed resources.
                if (ns != null)
                {
                    ns.Close();
                    ns.Dispose();
                }
                if (client != null)
                {
                    client.Close();
                    client.Dispose();
                }
            }
            // Call base class Dispose to clean up unmanaged resources.
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            //  
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.ClientForm_Tick);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 344);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ClientForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
    }
}

