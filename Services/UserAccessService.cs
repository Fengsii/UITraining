using UITraining.Interfaces;
using UITraining.Models;
using static UITraining.Models.GeneralStatus;
using UITraining.Models.DTO;
using Microsoft.EntityFrameworkCore;
using UITraining.Models.DB;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            if (dto.Password != dto.MatchPassword)
            {
                return false; // Menolak penyimpanan jika password tidak sama
            }

            var user = new UserAccess
            {
                Name = dto.Name,
                UserName = dto.UserName,
                Password = dto.Password,
                AccessDate = DateTime.Now,
                UserStatus = GeneralStatus.GeneralStatusData.Published,
            };

            _conteks.Add(user);
            _conteks.SaveChanges();

            return true;
        }


        // Method untuk validasi login
        public bool ValidateLogin(string username, string password)
        {
            var user = _conteks.UserAccesses
                .FirstOrDefault(x => x.UserName == username && x.Password == password && x.UserStatus != GeneralStatusData.delete);

            return user != null; // Return true jika user ditemukan
        }


        public List<UserAccessDTO> GetlistUser()
        {
            var data = _conteks.UserAccesses.Where(x => x.UserStatus != GeneralStatusData.delete).Select(x => new UserAccessDTO
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Password = x.Password,
                MatchPassword = x.Password,
                UserStatus = x.UserStatus

            }).ToList();
            return data;

        }

        public UserAccess GetUserById(int id)
        {
            var data = _conteks.UserAccesses.Where(x => x.Id == id && x.UserStatus != GeneralStatusData.delete).FirstOrDefault();
            if (data == null)
            {
                return new UserAccess();
            }

            return data;
        }

        public bool EditUser(UserAccessDTO userAccessDTO)
        {
            var data = _conteks.UserAccesses.FirstOrDefault(x => x.Id == userAccessDTO.Id);
            if (data == null)
            {
                return false;
            }
           
            data.AccessDate = DateTime.Now;
            data.UserStatus = userAccessDTO.UserStatus;


            _conteks.UserAccesses.Update(data);
            _conteks.SaveChanges();
            return true;
        }


       

        public bool DeleteUser(int id)
        {
            var data = _conteks.UserAccesses.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            data.UserStatus = GeneralStatusData.delete;
            //_conteks.Products.Update(data);
            _conteks.SaveChanges();
            return true;
        }



    }
}
