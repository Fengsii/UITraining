using UITraining.Interfaces;
using UITraining.Models;
using static UITraining.Models.GeneralStatus;
using UITraining.Models.DTO;
using Microsoft.EntityFrameworkCore;
using UITraining.Models.DB;
using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UITraining.Services
{
    public class UserAccessService : IUserAccess
    {
        private readonly ApplicationContext _conteks;
        private readonly string _paper;
        private readonly string _iteration;


        public UserAccessService(ApplicationContext conteks, IConfiguration configuration)
        {
            _paper = configuration.GetSection("Security:Papper").Value ?? "";
            _iteration = configuration.GetSection("Security:Iteration").Value ?? "";
            _conteks = conteks;
        }

        public bool InsertUserAccess(UserAccessDTO dto)
        {
            var GenerateSalt = Helper.Hasher.GenerateSalt();
            var user = new UserAccess
            {
                Name = dto.Name,
                UserName = dto.UserName,
                AccessDate = DateTime.Now,
                Salt = GenerateSalt,
                UserStatus = GeneralStatus.GeneralStatusData.Published,
                Pwd_hash = Hasher.ComputeHash(dto.Password, GenerateSalt, _paper, Convert.ToInt32(_iteration))

            };

            _conteks.Add(user);
            _conteks.SaveChanges();

            return true;
        }
        
        public bool ValidateLogin(string username, string password)
        {

            var user = _conteks.UserAccesses.FirstOrDefault(x =>
                x.UserName == username &&
                x.UserStatus == GeneralStatus.GeneralStatusData.Published);

            if (user == null)
            {
                return false;
            }

            var hashResult = Hasher.ComputeHash(password, user.Salt, _paper, Convert.ToInt32(_iteration));

            if (hashResult == user.Pwd_hash && username == user.UserName  )
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<UserAccessDTO> GetlistUser()
        {
            var data = _conteks.UserAccesses.Where(x => x.UserStatus != GeneralStatusData.delete).Select(x => new UserAccessDTO
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Password = "*******",
                MatchPassword = "*******",
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
