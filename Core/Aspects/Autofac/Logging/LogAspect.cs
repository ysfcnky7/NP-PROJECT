using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace Core.Autofac.Aspects.Logging
{
    //Bilgi  anlamındaki loglama mesela metod çağrıldığı an gerçekleşir 
    //Bazen de yapılan işlemleri kontrol altına almak için metodun sonunda da bu işlem yapılabilir 
    //uygulama hata verdiğinde de yapılabilir. 
    public class LogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerService)
        {
            //Burası bir attribute olduğu için programcı yanlış gonderebilir
            //Product Menager de attribute olarak logu belırtmıstık
            //bu yuzden  if (loggerService.BaseType != typeof(LoggerServiceBase))
            //bu şekilde bir base kontrolu yapıyoruz
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            //burada çağrılan operasyonun ismini metod adını almak için getlogdetaili yazıyorum 
            _loggerServiceBase.Info(GetLogDetail(invocation));
            //GetLogDetail BUNU ayrı olarak yazdık cunku daha sonra onafter vesaire durumları
            //yazmamız gerekırse DO NOT REPEAT kuralı gereği yanı kendını tekrar etme aynı kodu bir daha yazma
            //kuralı gereği ayrı bır metodda oluşturuyoruz bu şekilde clean code 
            //tekniklerine detaylı bakmak lazım 
        }

        //Burayı dahada geliştirebiliriz örneğin gelen parametre bir obje ise 
        //içeriğini de okuyup json formatında yazabılırız 
        //yanı adamın eskı kaydını da tutabılırız.
        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    //invocation.GetConcreteMethod().GetParameters()[i].Name parametreyı bu şekilde alıyoruz 
                    //Cache dekı sorunda sankı benzer bır durum var gıbı 
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetail;
        }
    }
}
