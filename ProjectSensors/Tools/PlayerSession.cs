namespace ProjectSensors.Tools
{
    public static class PlayerSession
    {
        public static string Username { get; private set; }

        public static void Login(string username)
        {
            Username = username;
        }
    }
}
