using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface IUserAccess
    {
        public bool InsertUserAccess(UserAccessDTO dto);
    }
}
