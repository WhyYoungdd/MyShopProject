namespace WebApplication1.Loggers
{
    public class ErrorLogger : ILogger
    {
        private static ErrorLogger _instance;

        private ErrorLogger() { }

        public static ErrorLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ErrorLogger();
                }
                return _instance;
            }
        }

        public void Log(string message)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now}: {message}");
        }
    }
}