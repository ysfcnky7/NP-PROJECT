namespace Core.Utilities.Results
{
    //Burda Interface olmayan bir clası implemente ettik
    public class SuccessResult : Result
    {
        //İşlemin başarılı olmasında dolayı default değeri true olarak ayarladık
        public SuccessResult(string message) : base(true, message)
        {
        }
        //Yine aynı şekilde true olarak ayarladık
        public SuccessResult() : base(true)
        {
        }
    }
}
