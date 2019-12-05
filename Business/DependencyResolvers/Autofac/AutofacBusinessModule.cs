using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        //Configurasyonun yapıldıgı yer
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductMenager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryMenager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserMenager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthMenager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //Aspectin devreye girebilmesi için şimdi bizim tüm operasyonlarımız ioc container 
            //üzerinden çalışıyor. yanı ordaki dependency cozuluyor ve ona göre uygun aspect cagrılıyor 
            //tüm işlemlerin çoğunu aspectlerle yurutuyoruz.
            //bunun için configurasyon yapıyoruz burada 
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //using Autofac.Extras.DynamicProxy; burda dinamik proxy kutuphanesı core da yuklu idi 
            //onu kaldırdık ve Autofac.Extras.DynamicProxy Dinamic proxy extras yukledık 

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()  //araya gırecek olan nesne 
                    //ismi AspectInterceptorSelector bu 
                }).SingleInstance(); // her seferınde tek bır ınstance oluştursun çok bir instance oluşturmasın anlamında 


        }
    }
}
