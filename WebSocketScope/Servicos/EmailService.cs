using System;
using System.IO;
using System.Net.Mail;
using System.Text.Json;
using System.Windows.Forms;
using WebSocketScope.Model;

namespace WebSocketScope.Servicos
{
    public static class EmailService
    {
        private const string ConfigPath = "email_config.json";

        public static void EnviarRelatorioComHtml(string destino, string caminhoCsv)
        {
            try
            {
                var config = CarregarConfiguracoes();
                if (config == null)
                {
                    MessageBox.Show("Configuração de e-mail não encontrada. Configure antes de enviar o relatório.",
                        "Configuração ausente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string corpoBruto = File.ReadAllText(caminhoCsv);
                string corpoHtml = $"<html><body><h3>Relatório WebSocketScope</h3><pre style='font-family: Consolas, monospace; font-size: 13px'>{corpoBruto}</pre></body></html>";

                using var mail = new MailMessage();
                using var smtp = new SmtpClient(config.smtp)
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential(config.usuario, config.senha),
                    EnableSsl = true
                };

                mail.From = new MailAddress(config.usuario);
                mail.To.Add(destino);
                mail.Subject = "Relatório WebSocketScope";
                mail.Body = corpoHtml;
                mail.IsBodyHtml = true;

                smtp.Send(mail);
                MessageBox.Show("Relatório HTML enviado com sucesso!", "E-mail enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar e-mail: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private static EmailConfigModel? CarregarConfiguracoes()
        {
            if (!File.Exists(ConfigPath))
                return null;

            try
            {
                var json = File.ReadAllText(ConfigPath);
                return JsonSerializer.Deserialize<EmailConfigModel>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
