using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors.Autofac
{
    //AttributeTargets.Class classlarda attribute olarak kullanılabılır 
    //AttributeTargets.Method metodlarda attribute olarak kullanılabılır 
    //AllowMultiple = true birden fazla kullanılabılr  
    //Inherited = true //inherit edildiği alt classlarda da kullanılabılsın 
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,
        AllowMultiple = true,
        Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {    //IInterceptor nereden gelıyor autofacın de kullandıgı dınamıc proxy dıye bır olay var 
         //bu yuzden gıdıp nugette custle.dinamyc proxy yı kurduk
        public int Priority { get; set; }
        //Priority koyma amacı aspectlerın sıralamasını belırlemek
        //normalde yukardan asağı kodu copy paste yapmayalım dıye__S2V5

        public virtual void Intercept(IInvocation invocation)
        {
            //virtual ile bu clasın override edilebilir özelliğini açmış olduk 

        }
    }
}
