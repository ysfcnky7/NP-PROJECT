using log4net.Core;
using System;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    //Serializable autofacde de ve sıstem using içerisinde de var 
    //BU YUZDEN core da autofac dependency ınjection u yukluyor olacagız.
    [Serializable]
    public class SerializableLogEvent
    {
        private LoggingEvent _loggingEvent;

        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        public object Message => _loggingEvent.MessageObject;
        //MessageObject LoggerServiceBase deki message burda farklı işlemlerde koymamız lazım 
        //username gibi bu işlemi kim yapmış onu burda koymamız gerekıyor.
        //O kısım hakkında bılgı yok
    }
}
