using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:3000"));
            });
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,      //İSSUE BİLGİSİ VALİDATE EDEYIM MI bu adama token verınce 
                                                    //issue bilgisini validate edeyim mesela 
                                                    //www.yusuf.com bu bilgiyi kullanıcıya da 
                                                    //verıyoruz işlem yaparken bu bilgide bana gelsin mi 
                                                    //eğer bu bilgi geri gelmezse işlem yapamıyor sanırım.
                                                    //bunun kontrolunu yapıyoruz.
                        ValidateAudience = true,    // ?
                        ValidateLifetime = true,    // yani tokenın yaşam omrunu kontrol edeyım mı demek 
                                                    //sistemde her zaman login olsun mu yanı life time check 
                                                    //etmek ıcın yazılmıs bır olay bız ne kadar dakika verırsek 
                                                    //o kadar dakika sonraya kadar aktıf kalacak
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            services.AddDependencyResolvers(new ICoreModule[] {
                new CoreModule(), 
                //Bu yapı ile butun merkezı servis injection 
                //configurasyonlarını yapmış olduk
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureCustomExceptionMiddleware();
            //Bu middlewareyi kendim yazdım S4v11

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //Authentication yani kimlik doğrulama her zaman 
            //daha üstte olacak eğer UseAuthentication UseAuthorization'in altında olursa
            //uygulama duzgun çalışmayacaktır çunku burdaki middlewareler 
            //sıralı çalışmaktadır buda şu demek giriş yapmadan yetkileri 
            //ayarlamaya calışmak gibi
            //düşünebiliriz buda mumkun olmayacaktır.       
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
