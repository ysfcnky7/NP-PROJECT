namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {

        }
        public SuccessDataResult(T data) : base(data, true)
        {

        }
        //default parametresi kullanabilmek için C# 7 ve üstü bir sürüme sahip olmak gerekiyor.
        //Burda success ve data yanında bir de message göndermek için altta bulunan 
        //metodu yazdık
        public SuccessDataResult(string message) : base(default, true, message)
        {

        }
        //Burda ise sadece true şeklinde bir cevap dondurmuş olduk gerıye 
        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
