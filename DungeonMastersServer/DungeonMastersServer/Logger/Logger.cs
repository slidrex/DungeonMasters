namespace DungeonMastersServer.Logger;

public static class MessageLogger
{
    private static readonly string logFilePath = "../logs.txt";
    public static void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logMessage = $"[{timestamp}] [LOG]: {message}";
        
        Console.WriteLine(logMessage);

        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
            {
                writer.WriteLine(logMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{timestamp}] [ERROR]: Error writing logs to log file: {ex.Message}");
        }
    }
}