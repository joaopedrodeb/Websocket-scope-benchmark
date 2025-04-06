namespace WebSocketScope
{
    partial class MainForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.NumericUpDown numericConexoes;
        private System.Windows.Forms.NumericUpDown numericSegundos;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Label lblConexoes;
        private System.Windows.Forms.Label lblSegundos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.numericConexoes = new System.Windows.Forms.NumericUpDown();
            this.numericSegundos = new System.Windows.Forms.NumericUpDown();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnParar = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.lblConexoes = new System.Windows.Forms.Label();
            this.lblSegundos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericConexoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSegundos)).BeginInit();
            this.SuspendLayout();

            // 
            // numericConexoes
            // 
            this.numericConexoes.Location = new System.Drawing.Point(20, 40);
            this.numericConexoes.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numericConexoes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericConexoes.Name = "numericConexoes";
            this.numericConexoes.Size = new System.Drawing.Size(120, 23);
            this.numericConexoes.Value = new decimal(new int[] { 1000, 0, 0, 0 });

            // 
            // numericSegundos
            // 
            this.numericSegundos.Location = new System.Drawing.Point(160, 40);
            this.numericSegundos.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numericSegundos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericSegundos.Name = "numericSegundos";
            this.numericSegundos.Size = new System.Drawing.Size(120, 23);
            this.numericSegundos.Value = new decimal(new int[] { 5, 0, 0, 0 });

            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(300, 40);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(100, 23);
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);

            // 
            // btnParar
            // 
            this.btnParar.Location = new System.Drawing.Point(420, 40);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(100, 23);
            this.btnParar.Text = "Parar";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);

            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 15;
            this.listBoxLog.Location = new System.Drawing.Point(20, 80);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(500, 250);

            // 
            // lblConexoes
            // 
            this.lblConexoes.AutoSize = true;
            this.lblConexoes.Location = new System.Drawing.Point(20, 20);
            this.lblConexoes.Name = "lblConexoes";
            this.lblConexoes.Text = "Conexões:";

            // 
            // lblSegundos
            // 
            this.lblSegundos.AutoSize = true;
            this.lblSegundos.Location = new System.Drawing.Point(160, 20);
            this.lblSegundos.Name = "lblSegundos";
            this.lblSegundos.Text = "Duração (s):";

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(550, 350);
            this.Controls.Add(this.lblSegundos);
            this.Controls.Add(this.lblConexoes);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.numericSegundos);
            this.Controls.Add(this.numericConexoes);
            this.Name = "MainForm";
            this.Text = "WebSocketScope - Simulador";
            ((System.ComponentModel.ISupportInitialize)(this.numericConexoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSegundos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
