using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public GeneralStatusData CategoryStatus { get; set; }
    }
}
