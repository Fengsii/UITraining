using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DB
{
    public class UserAccess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string Pwd_hash { get; set; }
        public DateTime AccessDate { get; set; }
        public GeneralStatusData UserStatus { get; set; }

    }
}
