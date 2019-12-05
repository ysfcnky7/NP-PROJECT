using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System.IO;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    //Core.CrossCuttingConcerns.Logging.Log4Net.Layouts bu kısmın yolu çok onemlı çunku 
    //benım config log4net config fileım buraya bakacak buna gore layoutu alaca o yuzden burdaki
    //klasör hiyerarşisi çok önemli 
    //bu class ıle artık elımızde hangı formatta yazılacak olduğu da belırlemıs oluyoruz
    public class JsonLayout : LayoutSkeleton //LayoutSkeleton layout olması ıcın log4net de ki base sınıf 
    {
        public override void ActivateOptions()
        {
            //Opsiyon aktıvasyonu dıye soyledı bos olabılır burası dıyor 
            //ne işe yarıyor bılmıyorum.
        }
        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logEvent = new SerializableLogEvent(loggingEvent);
            var json = JsonConvert.SerializeObject(logEvent, Formatting.Indented);
            //Formatting.Indented logu açınca json formatında tablı gırıntılı olarak gosterıcek 
            writer.WriteLine(json);
        }
    }
}
