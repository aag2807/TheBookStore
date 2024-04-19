namespace Boundaries.Logger;

public interface IBookLogger
{
    /// <summary>
    /// Logs a message of type T with the specified log type.
    /// </summary>
    /// <param name="logType"> A value from <see cref="LogType"/></param>
    /// <param name="message"> A primitive or object of <see cref="T"/></param>
    /// <typeparam name="T"></typeparam>
    void Log<T>(LogType logType, T message = default(T));
    
    /// <summary>
    /// Writes a message of type T with the specified log type to a log file.
    /// </summary>
    /// <param name="logType"> A value from <see cref="LogType"/></param>
    /// <param name="message"> A primitive or object of <see cref="T"/></param>
    /// <typeparam name="T"></typeparam>
    void WriteToLogFile<T>(LogType logType, T message = default(T));
}