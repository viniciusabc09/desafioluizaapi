using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Text;

namespace DesafioAPI.Utils
{
    public static class Email
    {
        private static void SendEmail(string email, string title, string msg)
        {
            var cfgEmail = new MimeMessage();
            cfgEmail.From.Add(MailboxAddress.Parse("desafiolabs@gmail.com"));
            cfgEmail.To.Add(MailboxAddress.Parse(email));
            cfgEmail.Subject = title;
            cfgEmail.Body = new TextPart(TextFormat.Html) { Text = msg };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("desafiolabs@gmail.com", "desafiolabs123");
            smtp.Send(cfgEmail);
            smtp.Disconnect(true);
        }

        public static void ConfirmRegistration(string email)
        {
            string title = "Cadastro Realizado com Sucesso";
            string msg = "<h5><b>Cadastro Realizado com Sucesso!!!<b><h5>";

            SendEmail(email, title, msg);
        }

        public static void RedefinePassword(string email) 
        {
            string title = "Redefinir Senha do Usuário";
            string link = "http://localhost/DesafioWeb/redefinirsenha/" + Convert.ToBase64String(Encoding.UTF8.GetBytes(email));
            string msg = "<h5><b>Redefinir Senha do Usuário<b><h5> " +
                         "<p>Clique no link abaixo para poder redefinir sua senha<p>" +
                         "<a href='"+link+"'>Clique aqui</a>";

            SendEmail(email, title, msg);
        }
    }
}
