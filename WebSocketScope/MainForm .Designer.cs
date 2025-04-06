using System;
using System.Net.Mail;
using System.Windows.Forms;

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
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnConfigurarEmail;

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
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnConfigurarEmail = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numericConexoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSegundos)).BeginInit();
            this.SuspendLayout();

            // lblConexoes
            this.lblConexoes.AutoSize = true;
            this.lblConexoes.Location = new System.Drawing.Point(20, 20);
            this.lblConexoes.Name = "lblConexoes";
            this.lblConexoes.Text = "Conexões:";

            // numericConexoes
            this.numericConexoes.Location = new System.Drawing.Point(20, 40);
            this.numericConexoes.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numericConexoes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericConexoes.Name = "numericConexoes";
            this.numericConexoes.Size = new System.Drawing.Size(100, 23);
            this.numericConexoes.Value = new decimal(new int[] { 1000, 0, 0, 0 });

            // lblSegundos
            this.lblSegundos.AutoSize = true;
            this.lblSegundos.Location = new System.Drawing.Point(140, 20);
            this.lblSegundos.Name = "lblSegundos";
            this.lblSegundos.Text = "Duração (s):";

            // numericSegundos
            this.numericSegundos.Location = new System.Drawing.Point(140, 40);
            this.numericSegundos.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numericSegundos.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericSegundos.Name = "numericSegundos";
            this.numericSegundos.Size = new System.Drawing.Size(100, 23);
            this.numericSegundos.Value = new decimal(new int[] { 5, 0, 0, 0 });

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(260, 20);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Text = "E-mail (opcional):";

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(260, 40);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(220, 23);

            // btnIniciar
            this.btnIniciar.Location = new System.Drawing.Point(500, 40);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(100, 23);
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);

            // btnParar
            this.btnParar.Location = new System.Drawing.Point(610, 40);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(100, 23);
            this.btnParar.Text = "Parar";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);

            // btnConfigurarEmail
            this.btnConfigurarEmail.Location = new System.Drawing.Point(720, 40);
            this.btnConfigurarEmail.Name = "btnConfigurarEmail";
            this.btnConfigurarEmail.Size = new System.Drawing.Size(140, 23);
            this.btnConfigurarEmail.Text = "Configurar E-mail";
            this.btnConfigurarEmail.UseVisualStyleBackColor = true;
            this.btnConfigurarEmail.Click += new System.EventHandler(this.btnConfigurarEmail_Click);

            // listBoxLog
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 15;
            this.listBoxLog.Location = new System.Drawing.Point(20, 80);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(840, 364);
            this.listBoxLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // MainForm
            this.ClientSize = new System.Drawing.Size(900, 480);
            this.MinimumSize = new System.Drawing.Size(920, 520);
            this.Controls.Add(this.lblConexoes);
            this.Controls.Add(this.numericConexoes);
            this.Controls.Add(this.lblSegundos);
            this.Controls.Add(this.numericSegundos);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.btnConfigurarEmail);
            this.Controls.Add(this.listBoxLog);
            this.Name = "MainForm";
            this.Text = "WebSocketScope - Simulador";

            ((System.ComponentModel.ISupportInitialize)(this.numericConexoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSegundos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
