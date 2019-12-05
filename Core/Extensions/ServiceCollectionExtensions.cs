using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(
            this IServiceCollection services,
           ICoreModule[] modules)
        {
            //IServiceCollection'ı extend eden bır yapı 
            //bunu kullanarak api tarafında butun merkezı operasyonları eklıyor olacagım 
            //parametrelerde goruldugu gıbı merkezı modullerı burda array olarak vermıs oluyoruz 
            foreach (var module in modules)
            {
                module.Load(services);  //butun modullerımı bu şekilde yukluyorum 
            }
            return ServiceTool.Create(services);
            //bu kısımda da ServiceTool vasıtasıyla servislerı yapılandırıyoruz.
            //Son olarak api tarafına ekleyıp bundan sonra işi bitircez.starup.cs de yanı s2v16
        }
    }
}
