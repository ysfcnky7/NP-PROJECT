using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors.Autofac
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        //Metod çalışmadan çalışacak kod 
        protected virtual void OnAfter(IInvocation invocation) { }
        //Metod bitiminde çalışacak kod 
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        //Metod hata verdiğinde çalışacak kod ve hatayı gonderecek 
        protected virtual void OnSuccess(IInvocation invocation) { } //Metod başarılıysa çalışacak kod 
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed(); //Metodu çalıştır anlamında 
            }
            catch (System.Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
