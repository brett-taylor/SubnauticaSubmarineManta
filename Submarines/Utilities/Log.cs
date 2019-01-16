namespace Submarines.Utilities
{
    public static class Log
    {
        public static void Print(string message, bool alertInGame = true)
        {
            if (alertInGame)
            {
                ErrorMessage.AddMessage("[SubmarinesFramework] " + message);
            }
            System.Console.WriteLine("[SubmarinesFramework] " + message);
        }

        public static void Error(string message, bool alertInGame = true)
        {
            Print("[ERROR] " + message, alertInGame);
        }
    }
}
