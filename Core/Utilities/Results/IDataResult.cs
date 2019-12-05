namespace Core.Utilities.Results
{
    //Döndürmek istediğimiz datayı buradan döndürüyoruz.
    //
    public interface IDataResult<out T> : IResult  
    {
        T Data { get; }
    }
}
