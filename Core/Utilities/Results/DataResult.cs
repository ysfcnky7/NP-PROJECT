namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        //Hangi parametreleri alacağını constructorun belirliyoruz
        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }
        //Tek parametre alınca bu çalışacak usttekı calısmayacak
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
