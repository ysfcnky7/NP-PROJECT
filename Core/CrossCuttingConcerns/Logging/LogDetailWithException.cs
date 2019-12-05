namespace Core.CrossCuttingConcerns.Logging
{
    //Burda sıklıkla yapılan bir hata olmaması için LogDetailWithException adında bir class 
    //açtık aslında bu class yerıne LogDetail clasına gidip bir adet daha
    //parametre ekleyip kullanabilirdik
    //ama bu bizim için gereksiz propertiler getirecek boş yada kullanmayacağımız 
    //Inherit ettik bu gibi eklenecek olanı ekledik 

    public class LogDetailWithException : LogDetail
    {
        public string ExceptionMessage { get; set; }
    }
}
