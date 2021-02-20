namespace Networking
{
	public static class DBManager
	{
		public static string UserName;
		public static int Score;

		public static bool LoggedIn
		{
			get { return UserName != null;  }
		}

		public static void LogOut()
		{
			UserName = null;
		}
	}
}