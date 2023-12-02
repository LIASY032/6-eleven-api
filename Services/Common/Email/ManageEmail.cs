using System;
using System.Net;
using System.Net.Mail;

namespace Service.Common.Email
{
	public class ManageEmail
	{
		public void sendEmail(string subject, string email, string body) {
            // TODO: change address
            var fromAddress = new MailAddress("from@gmail.com", "From Name");
            var toAddress = new MailAddress(email, "To Name");
            const string fromPassword = "fromPassword";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        public void sendConfirmationEmail(string email) {
            sendEmail("6-Eleven MFA code", email, "");
	
	}
	}
}

