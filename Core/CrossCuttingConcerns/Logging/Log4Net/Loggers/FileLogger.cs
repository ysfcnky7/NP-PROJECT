namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        //FileLogger bu ismi gidip business concrete de yazıcaksın eğer bır
        //json file olarak kaydetmesını ıstıyorsan
        public FileLogger() : base("JsonFileLogger")
        {
        }
    }
}
