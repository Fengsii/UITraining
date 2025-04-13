using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Models;
using Microsoft.EntityFrameworkCore;

namespace UITraining.Services
{
    public class ReviewService : IReview
    {
        private readonly ApplicationContext _conteks;

        public ReviewService (ApplicationContext conteks)
        {
            _conteks = conteks;
        }
        public List<ReviewDTO> GetlistReview()
        {
            var data = _conteks.Reviews.Include(y => y.User).Include(w => w.Product)
                .Select(x => new ReviewDTO
                {
                    Id = x.Id,
                    UserName = x.User.Username,
                    ProductName = x.Product.Name,
                    Image = x.Image,
                    Comment = x.Comment,
                    Rating = x.Rating,

                }).ToList();

            return data;
        }


        public Review GetOrderRewiewById(int id)
        {
            var data = _conteks.Reviews.FirstOrDefault();
            if (data == null)
            {
                return new Review();
            }

            return data;
        }

        public bool EditReview(ReviewDTO reviewDTO)
        {
            var data = _conteks.Reviews.FirstOrDefault(x => x.Id == reviewDTO.Id);
            if (data == null)
            {
                return false;
            }

            data.UserId = reviewDTO.UserId;
            data.ProductId = reviewDTO.ProductId;
            data.Image = reviewDTO.Image;
            data.Comment = reviewDTO.Comment;
            data.Rating = reviewDTO.Rating;

            _conteks.Reviews.Update(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool DeleteReview(int id)
        {
            var data = _conteks.Reviews.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            _conteks.Reviews.Remove(data);
            _conteks.SaveChanges();
            return true;
        }

        public bool AddReview(ReviewDTO reviewDTO)
        {
            var data = new Review();

            data.UserId = reviewDTO.UserId;
            data.ProductId = reviewDTO.ProductId;
            data.Image = reviewDTO.Image;
            data.Comment = reviewDTO.Comment;
            data.Rating = reviewDTO.Rating;
            data.CreatedAt = reviewDTO.CreatedAt;

            _conteks.Reviews.Add(data);
            _conteks.SaveChanges();
            return true;

        }
    }
}
