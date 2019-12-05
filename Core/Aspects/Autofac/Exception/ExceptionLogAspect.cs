using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        //bir attribute idi bu bız bunu type olarak kabul edıyoruz 

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
                //Dil için database yada resource yada kod içinde geri dondurebiliriz dili değiştirip
                //ama resource dosyasını tavsıye etmıyoruz bu konuda. 
                //çunku resource dosyası arka planda aslında bir class desteği varsada aslında zordur 
                //yeni bir şey eklendiğinde zor olmasında dolayı 
                //tavsiye edilen yöntem ise veritabanında bu dil bilgilerinin alınması daha sonra cachelemek 
                //daha sonra da o şekilde kullanmak uzerıne bır yapı 
                //bu projeye gore değişebilir.
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        //S4v44 ==> Try catch işlemlerini gidip arayüz katmanında yapma dıyor 
        //backend tarafında oldugu için core tarafında yapıyoruz 
        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            //Exception olduğu zaman bunu loglamak hata falan olunca bunu kaydedicez o yuzden burayı yazdık 
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;
            _loggerServiceBase.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetailWithException;
        }
    }
}
