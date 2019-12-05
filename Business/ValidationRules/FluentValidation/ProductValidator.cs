using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).Length(2, 50);
            //2 ile 50 karakter arasında olmalı 
            RuleFor(p => p.UnitPrice).NotEmpty();
            //Boş olamaz
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);
            //birden küçük olamaz 
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            // KATEGORy bir ise minimum degerı 10 dur dıyoruz 
            //burda boyle bır kural koyabiliyoruz 
            RuleFor(p => p.ProductName).Must(StartWithWithA);
        }
        private bool StartWithWithA(string arg)
        {
            return arg.StartsWith("A");
            // buyuk a ile başlamalı 
        }
    }
}
