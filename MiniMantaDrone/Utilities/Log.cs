namespace MiniMantaDrone.Utilities
{
    public static class Log
    {
        public static void Print(string message, bool alertInGame = true)
        {
            if (alertInGame)
            {
                ErrorMessage.AddMessage("[MiniMantaDrone] " + message);
            }
            System.Console.WriteLine("[MiniMantaDrone] " + message);
        }

        public static void Error(string message, bool alertInGame = true)
        {
            Print("[ERROR] " + message, alertInGame);
        }
    }
}
