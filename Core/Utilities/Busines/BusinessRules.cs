using Core.Utilities.Results;

namespace Core.Utilities.Busines
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var result in logics)
            {
                if (!result.Success)
                {
                    return result; //Herbir logic için logic çalışımazsa return result donduruyoruz 
                }
            }
            return null;  //hepsi success ise null donduruyoruz
        }
    }
}
