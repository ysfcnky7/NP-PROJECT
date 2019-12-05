namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //Sadece Success gelebilir anlamı bu
        //Başka bir constructoru tetiklemek anlamına gelıyor bu 
        //this(success) ile o özelliği tetikliyoruz.bu bizi 
        //2 defa kod tekrarından kurtarıyor.
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
