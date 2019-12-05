namespace Core.CrossCuttingConcerns.Caching
{
    //Bunu yanı cashe işlemini neden crosscuttingconsern de açtık 
    //normalde aspect olarak bakamaz mıydık gıdıp orda açamazmı ıdık bu cash ıcın bır klasor ? 
    //Burda işte CrossCuttingConcern ile Aspect yazımının karışmaması lazım 
    //CrossCuttingConcern bizim için genel kodları içerir Aspect ise araya girişleri içerir 
    //bunları kontrol ederız.
    //Boyle bır ınterface oluşturduk neler olabilir diye düşününce direk 
    //interface ile ne yapılabilirse bunu burda yapabılırız.

    public interface ICacheManager
    {
        T Get<T>(string key);
        //Get ile belli bir tipteki cashe değerini okumaya çalışacam 
        //key değerini vereceğim bana hangi tipte istiyorsam gerıye verecek
        object Get(string key);
        //obje olarak da okuyup kullanabılırım.mesela burdaki gibi 
        void Add(string key, object data, int duration);
        //şimdi cashe ye bir key değeri ile ekleriz datasını veririz
        //ve ne kadar süre duracak onu ekliyoruz 
        bool IsAdd(string key);
        //cashe ye eklenmıs mı bunun kontrolunu yapan metod olacak 
        void Remove(string key);
        //Belli bir keydeki ortadan kaldırılmasını sağlar 
        void RemoveByPattern(string key);
        //string bir pattern verip o patterne uyanları silmesini sağlayabiliriz.
    }
}
