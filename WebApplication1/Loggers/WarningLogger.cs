namespace WebApplication1.Loggers
{
    public class WarningLogger : ILogger
    {
        private static WarningLogger _instance;

        private WarningLogger() { }

        public static WarningLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WarningLogger();
                }
                return _instance;
            }
        }

        public void Log(string message)
        {
            Console.WriteLine($"[WARNING] {DateTime.Now}: {message}");
        }
    }
}