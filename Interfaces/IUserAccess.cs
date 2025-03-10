using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface IUserAccess
    {
        public bool InsertUserAccess(UserAccessDTO dto);
        public List<UserAccessDTO> GetlistUser();
        //public UserAccessDTO GetUserById(int id);
        public UserAccess GetUserById(int id);
        public bool EditUser(UserAccessDTO userAccessDTO);
        public bool DeleteUser(int id);
    }
}
