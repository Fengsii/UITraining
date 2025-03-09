using UITraining.Interfaces;
using UITraining.Models;
using static UITraining.Models.GeneralStatus;
using UITraining.Models.DTO;
using Microsoft.EntityFrameworkCore;
using UITraining.Models.DB;

namespace UITraining.Services
{
    public class UserAccessService : IUserAccess
    {
        private readonly ApplicationContext _conteks;


        public UserAccessService(ApplicationContext conteks)
        {
            _conteks = conteks;
        }

        public bool InsertUserAccess(UserAccessDTO dto)
        {
            var user = new UserAccess
            {
                Name = dto.Name,
                UserName = dto.UserName,
                Password = dto.Password,
                AccessDate = DateTime.Now,
                UserStatus = GeneralStatus.GeneralStatusData.published,
            };

            _conteks.Add(user);
            _conteks.SaveChanges();

            return true;
        }

    }
}
