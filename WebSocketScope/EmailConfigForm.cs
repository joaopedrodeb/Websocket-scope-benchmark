using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketScope.Model;

namespace WebSocketScope
{
    public partial class EmailConfigForm : Form
    {
        private const string ConfigPath = "email_config.json";

        public EmailConfigForm()
        {
            InitializeComponent();
            CarregarConfiguracoes();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var config = new
            {
                smtp = txtSmtp.Text,
                usuario = txtUsuario.Text,
                senha = txtSenha.Text
            };

            var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(ConfigPath, json);

            MessageBox.Show("Configurações salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CarregarConfiguracoes()
        {
            if (File.Exists(ConfigPath))
            {
                try
                {
                    var json = File.ReadAllText(ConfigPath);
                    var config = JsonSerializer.Deserialize<EmailConfigModel>(json);

                    txtSmtp.Text = config?.smtp ?? "";
                    txtUsuario.Text = config?.usuario ?? "";
                    txtSenha.Text = config?.senha ?? "";
                }
                catch
                {
                    MessageBox.Show("Falha ao carregar configurações de e-mail.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

