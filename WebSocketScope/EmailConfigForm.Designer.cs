using System;
using System.Windows.Forms;

namespace WebSocketScope
{
    partial class EmailConfigForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblSmtp;
        private TextBox txtSmtp;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblSenha;
        private TextBox txtSenha;
        private Button btnSalvar;
        private Button btnCancelar;

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
            this.lblSmtp = new Label();
            this.txtSmtp = new TextBox();
            this.lblUsuario = new Label();
            this.txtUsuario = new TextBox();
            this.lblSenha = new Label();
            this.txtSenha = new TextBox();
            this.btnSalvar = new Button();
            this.btnCancelar = new Button();

            this.SuspendLayout();

            // lblSmtp
            this.lblSmtp.AutoSize = true;
            this.lblSmtp.Location = new System.Drawing.Point(20, 20);
            this.lblSmtp.Name = "lblSmtp";
            this.lblSmtp.Text = "Servidor SMTP:";

            // txtSmtp
            this.txtSmtp.Location = new System.Drawing.Point(20, 40);
            this.txtSmtp.Size = new System.Drawing.Size(300, 23);
            this.txtSmtp.Name = "txtSmtp";

            // lblUsuario
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(20, 80);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuário (e-mail):";

            // txtUsuario
            this.txtUsuario.Location = new System.Drawing.Point(20, 100);
            this.txtUsuario.Size = new System.Drawing.Size(300, 23);
            this.txtUsuario.Name = "txtUsuario";

            // lblSenha
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(20, 140);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Text = "Senha (ou senha de app):";

            // txtSenha
            this.txtSenha.Location = new System.Drawing.Point(20, 160);
            this.txtSenha.Size = new System.Drawing.Size(300, 23);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.UseSystemPasswordChar = true;

            // btnSalvar
            this.btnSalvar.Location = new System.Drawing.Point(20, 200);
            this.btnSalvar.Size = new System.Drawing.Size(100, 30);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new EventHandler(this.btnSalvar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(130, 200);
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new EventHandler(this.btnCancelar_Click);

            // EmailConfigForm
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 260);
            this.Controls.Add(this.lblSmtp);
            this.Controls.Add(this.txtSmtp);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "EmailConfigForm";
            this.Text = "Configuração de E-mail";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
