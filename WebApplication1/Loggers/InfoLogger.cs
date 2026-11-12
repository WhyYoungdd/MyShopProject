namespace WebApplication1.Loggers
{
    public class InfoLogger : ILogger
    {
        private static InfoLogger _instance;

        private InfoLogger() { }

        public static InfoLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InfoLogger();
                }
                return _instance;
            }
        }

        public void Log(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
        }
    }
}