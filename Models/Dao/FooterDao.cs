using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class FooterDao
    {
        private shopnoithatDbContext db = null;

        public FooterDao()
        {
            db = new shopnoithatDbContext();
        }

        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }

        public IEnumerable<Footer> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Footer> model = db.Footers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Content.Contains(searchString));
            }

            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public string Insert(Footer footer)
        {
            db.Footers.Add(footer);
            db.SaveChanges();
            return footer.ID;
        }

        public bool Delete(int id)
        {
            try
            {
                var footer = db.Footers.Find(id);
                db.Footers.Remove(footer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Footer entity)
        {
            try
            {
                var footer = db.Footers.Find(entity.ID);

                footer.Content = entity.Content;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public List<Footer> ListAll()
        {
            return db.Footers.Where(x => x.Status == true).ToList();
        }

        public bool ChangeStatus(long id)
        {
            var footer = db.Contacts.Find(id);
            footer.Status = !footer.Status;
            db.SaveChanges();
            return footer.Status;
        }
    }
}