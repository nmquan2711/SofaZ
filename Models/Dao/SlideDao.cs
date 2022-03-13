using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class SlideDao
    {
        private shopnoithatDbContext db = null;

        public SlideDao()
        {
            db = new shopnoithatDbContext();
        }

        public Slide GetFooter()
        {
            return db.Slides.SingleOrDefault(x => x.Status == true);
        }

        public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public long Insert(Slide slide)
        {
            db.Slides.Add(slide);
            db.SaveChanges();
            return slide.ID;
        }

        public bool Delete(int id)
        {
            try
            {
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Slide entity)
        {
            try
            {
                var slide = db.Slides.Find(entity.ID);
                slide.Name = entity.Name;
                slide.Image = entity.Image;
                slide.Link = entity.Link;
                slide.UpdatedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).ToList();
        }

        public bool ChangeStatus(long id)
        {
            var slide = db.Slides.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }
    }
}