namespace APICatalogo.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;  
    }



    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= loggerConfig.LogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()} - {loggerName} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArquivo = @"C:\Users\Joao\Estudos\log\log.txt";

        using (StreamWriter sw = new StreamWriter(caminhoArquivo, true))
        {
            try
            {
                sw.WriteLine(mensagem);
                sw.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
