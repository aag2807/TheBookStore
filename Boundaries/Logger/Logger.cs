namespace Boundaries.Logger;

public sealed class Logger : ILogger
{
    /// <inheritdoc />
    void ILogger.Log<T>(LogType logType, T message)
    {
        switch (logType)
        {
            case LogType.Info:
                Console.WriteLine($"INFO: {message}");
                break;
            case LogType.Warning:
                Console.WriteLine($"WARNING: {message}");
                break;
            case LogType.Error:
                Console.WriteLine($"ERROR: {message}");
                break;
            default:
                break;
        }
    }
    
    /// <inheritdoc />
    void ILogger.WriteToLogFile<T>(LogType logType, T message)
    {
        string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        string logFileName = $"{logType}_{DateTime.Now:yyyy-MM-dd}.txt";
        string logFilePath = Path.Combine(logDirectory, logFileName);

        Directory.CreateDirectory(logDirectory);

        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{DateTime.Now:HH:mm:ss} [{logType}] {message}");
        }
    }
}