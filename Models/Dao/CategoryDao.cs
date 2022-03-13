using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CategoryDao
    {
        private shopnoithatDbContext db = null;
        public static string USER_SESSION = "USER_SESSION";

        public CategoryDao()
        {
            db = new shopnoithatDbContext();
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }

        public long Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);

                category.MetaDescription = entity.MetaDescription;
                category.MetaKeyword = entity.MetaKeyword;
                category.MetaTitle = entity.MetaTitle;
                category.Image = entity.Image;
                category.SeoTitle = entity.SeoTitle;
                category.UpdatedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public bool ChangeStatus(long id)
        {
            var category = db.Categories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }
    }
}