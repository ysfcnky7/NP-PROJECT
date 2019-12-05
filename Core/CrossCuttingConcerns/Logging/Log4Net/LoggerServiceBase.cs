using log4net;
using log4net.Repository;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    //BURAYI BIR BASE SINIF OLARAK TASARLIYORUZ INHERITENCE OZELLIGI GORECEK BIR SINIF 
    //xmlDocument.Load(File.OpenRead("log4Net.config")); bu kısımda o dosyadan okuyor
    //BURDA ŞU DUŞUNULEBİLİR BİZ CORE DAYIZ AMA GIDIP APIDEKI PROJEDE BU ISLEMI YAPTIK 
    //YANI ORDAKI BIR DOSYAYI REFERANS GOSTERDIK ASLINDA BURDA MANTIK GENEL OLARAK BIZIM 
    //PROJEMIZ CALISAN PROJEDIR DIYE BAKIYOR YANI API CALISIYOR OALCAGI ICIN BUNU 
    //YANI O DOSYAYI OKUYABILIYOR.

    public class LoggerServiceBase
    {
        private ILog _log;
        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4Net.config"));
            //LogManager.CreateRepository log4netten gelıyor bızım ıcın bır repository olusturacak 
            //Assembly.GetExecutingAssembly() mevcut assembly yı verıyor bıze 
            //
            ILoggerRepository loggerRepository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
            typeof(log4net.Repository.Hierarchy.Hierarchy));
            //loggerRepository.Name burası ilgili configurasyondaki yerı al ya DatabaseLogger yada 
            //JsonFıleLogger olacak 
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);
            _log = LogManager.GetLogger(loggerRepository.Name, name);
        }


        //C# 7 ile gelen daha kısa metod yazmaya yarayan 
        //bir syntax ınfo acık mı dıye kontrol edeecek 
        public bool IsInfoEnabled => _log.IsInfoEnabled;
        public bool IsDebugEnabled => _log.IsDebugEnabled;
        //Mesela performans için warn kullanılabılır 
        public bool IsWarnEnabled => _log.IsWarnEnabled;
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsErrorEnabled => _log.IsErrorEnabled;

        //Kendımıze özel yapıyoruz 
        //istersek info logu alma fatal logu alma gıbı durumlar oluyor
        //bazı logları azaltmak ıstıyor
        public void Info(object logMessage)
        {
            if (IsInfoEnabled)
                _log.Info(logMessage);
        }

        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
                _log.Debug(logMessage);
        }

        public void Warn(object logMessage)
        {
            if (IsWarnEnabled)
                _log.Warn(logMessage);
        }

        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
                _log.Fatal(logMessage);
        }

        public void Error(object logMessage)
        {
            if (IsErrorEnabled)
                _log.Error(logMessage);
        }
    }
}
