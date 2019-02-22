namespace MiniMantaVehicle.Utilities
{
    public static class Log
    {
        public static void Print(string message, bool alertInGame = true)
        {
            if (alertInGame)
            {
                ErrorMessage.AddMessage("[MiniMantaVehicle] " + message);
            }
            System.Console.WriteLine("[MiniMantaVehicle] " + message);
        }

        public static void Error(string message, bool alertInGame = true)
        {
            Print("[ERROR] " + message, alertInGame);
        }
    }
}
