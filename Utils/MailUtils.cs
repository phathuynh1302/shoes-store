using System.Net.Mail;
using System.Net;
using System;

namespace PRN211_ShoesStore.Utils
{
	public class MailUtils
	{
		public static bool SendMail(string from, string to, string subject, string body)
		{
			using var message = new MailMessage();
			message.BodyEncoding = System.Text.Encoding.UTF8;
			message.SubjectEncoding = System.Text.Encoding.UTF8;

			message.From = new MailAddress(from);

			message.To.Add(to);

			message.Subject = subject;
			message.Body = body;

			using var smtp = new SmtpClient("smtp.gmail.com");
			smtp.Port = 587;
            
            smtp.EnableSsl = true;
			smtp.Timeout= 5000;
			smtp.Credentials = new NetworkCredential(from, "Nhaanhxinhdep@7812");
			try
			{
				smtp.Send(message);
				return true;
			}
			catch (SmtpFailedRecipientException ex)
			{
				return false;
			}
		}
	}
}
