namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    //Bu class verıtabanına yapacak oldugumuz logu  anlatıyor
    public class DatabaseLogger : LoggerServiceBase
    {
        public DatabaseLogger() : base("DatabaseLogger")
        {
        }
    }
}
