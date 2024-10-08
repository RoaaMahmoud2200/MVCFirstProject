using System.Net.Mail;
using System.Net;
using Company.Route2.DAL.Models;

namespace Company.Route2.PL.Helper.EmailSetting
{
	public class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			// to send email we use SMTp protocole

			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;	
			//crwpvasdbjkdwaod
			client.Credentials = new NetworkCredential("roaamahmoud2200@gmail.com", "crwpvasdbjkdwaod");

			client.Send("roaamahmoud2200@gmail.com", email.to, email.subject, email.body);

		}
	}
}
