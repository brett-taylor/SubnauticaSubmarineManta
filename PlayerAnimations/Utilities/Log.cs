namespace PlayerAnimations.Utilities
{
    public static class Log
    {
        public static void Print(string message, bool alertInGame = true)
        {
            if (alertInGame)
            {
                ErrorMessage.AddMessage("[PlayerAnimations] " + message);
            }
            System.Console.WriteLine("[PlayerAnimations] " + message);
        }

        public static void Error(string message, bool alertInGame = true)
        {
            Print("[ERROR] " + message, alertInGame);
        }
    }
}
