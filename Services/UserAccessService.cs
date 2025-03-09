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


        public List<UserAccessDTO> GetlistUser()
        {
            var data = _conteks.UserAccesses.Where(x => x.UserStatus != GeneralStatusData.delete).Select(x => new UserAccessDTO
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Password = x.Password,

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

            //// Ambil semua produk yang terkait dengan supplier ini dan tandai sebagai dihapus
            //var relatedProducts = _conteks.Products.Where(p => p.IdSupplier == id).ToList();
            //foreach (var product in relatedProducts)
            //{
            //    product.ProductStatus = GeneralStatusData.delete;
            //}

            //// Tandai supplier sebagai dihapus
            //data.SupplierStatus = GeneralStatusData.delete;


            data.UserStatus = GeneralStatusData.delete;
            //_conteks.Products.Update(data);
            _conteks.SaveChanges();
            return true;
        }

    }
}
