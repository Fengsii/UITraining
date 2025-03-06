namespace UITraining.Models
{
    public class GeneralStatus
    {
        public enum GeneralStatusData
        {
            published,// semua bisa lihat
            unpublished,//admin aja yang lihat
            Active,// semua bisa lihat
            Inactive,//admin aja yang lihat
            delete//cuman ad didata base
        }

       
    }
}
