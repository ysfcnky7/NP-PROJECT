using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Utilities.Interceptors.Autofac
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            #region S04v08
            //Sistemde oluşan exceptionları file logger a yazıcam
            //classAttributes.Add(new ExceptionLogAspect(typeof(DatabaseLogger)));    // 2 ADET YAZABILIRIZ AMA 
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));      //PERFORMANS ACISINDAN MANTIKLI DEGIL
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));  sadece bu kodu 4.seride 
            //yazdık bunun da amacı exceptionların her yerde etkili olması yanı 
            //bunu yapmasa ıdım her motodun başına ayrı ayrı attribute olarak 
            //excception handler attributesını yazmam gerekecektı bu sorunu buraya yazarak aştık
            #endregion
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
