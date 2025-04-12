namespace UITraining.Models
{
    public class GeneralOrderStatus
    {
        public enum GeneralOrderStatusData
        {
            Unpaid,// Belum Bayar
            Processing,// Dikemas
            Shipped,// Dikirim
            Delivered // Selesai
        }
    }
}
