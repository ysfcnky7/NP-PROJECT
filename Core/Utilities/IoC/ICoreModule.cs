using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    //ICoreModule böyle bir isim koymamızın sebebı .net core ile ilgili değil 
    //bu tum projelerde bunu kullanacağımız bir yapı dıye ismini core olarak koyuyoruz
    public interface ICoreModule
    {
        //Burdaki Load benzer şekilde  Business.DependencyResolvers.Autofac içindeki 
        //AutofacBusinessModule gibi ama orda ContainerBuilder yanı autofacı dahıl etmıstım 
        //burda ise IServiceCollection dahıl edıyorum 

        void Load(IServiceCollection collection);
    }
}
