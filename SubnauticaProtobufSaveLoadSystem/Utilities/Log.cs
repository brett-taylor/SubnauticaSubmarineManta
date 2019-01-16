namespace SubnauticaProtobufSaveLoadSystem.Utilities
{
    public static class Log
    {
        public static void Print(string message, bool alertInGame = true)
        {
            if (alertInGame)
            {
                ErrorMessage.AddMessage("[SubnauticaProtobufSaveLoadSystem] " + message);
            }
            System.Console.WriteLine("[SubnauticaProtobufSaveLoadSystem] " + message);
        }

        public static void Error(string message, bool alertInGame = true)
        {
            Print("[ERROR] " + message, alertInGame);
        }
    }
}
