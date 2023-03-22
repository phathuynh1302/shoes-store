using System.Text.RegularExpressions;

namespace PRN211_ShoesStore.Utils
{
    public static class ValidateForm
    {
        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Match match = Regex.Match(email, emailPattern);
            return match.Success;
        }

		public static bool ContaintOnlyChar(string msg)
		{
			string pattern = @"^[a-zA-Z]+$";
			Match match = Regex.Match(msg, pattern);
			return match.Success;
		}
		

		public static bool IsValidPhone(string msg)
		{
			string pattern = @"^[0-9]{10}$";
			Match match = Regex.Match(msg, pattern);
			return match.Success;
		}


		public static bool StartWithANumber(string msg)
		{
			string pattern = @"^[0-9]$";
			Match match = Regex.Match(msg[0].ToString(), pattern);
			return match.Success;
		}

		public static bool IsValidFormatEmail(string email)
		{
			string emailPattern = @"^[0-9]+$";
			Match match = Regex.Match(email, emailPattern);
			return match.Success;
		}
	}
}
