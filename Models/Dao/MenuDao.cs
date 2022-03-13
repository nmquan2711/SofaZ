using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class MenuDao
    {
        private shopnoithatDbContext db = null;

        public MenuDao()
        {
            db = new shopnoithatDbContext();
        }

        public IEnumerable<Menu> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menus;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString) || x.Text.Contains(searchString));
            }

            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public long Insert(Menu menu)
        {
            db.Menus.Add(menu);
            db.SaveChanges();
            return menu.ID;
        }

        public bool Delete(int id)
        {
            try
            {
                var menu = db.Menus.Find(id);
                db.Menus.Remove(menu);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Menu entity)
        {
            try
            {
                var menu = db.Menus.Find(entity.ID);
                menu.Link = entity.Link;
                menu.Target = entity.Target;
                menu.Text = entity.Text;
                menu.DisplayOrder = entity.DisplayOrder;
                menu.TypeID = entity.TypeID;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public List<Menu> ListAll()
        {
            return db.Menus.Where(x => x.Status == true).ToList();
        }

        public bool ChangeStatus(long id)
        {
            var Menu = db.Menus.Find(id);
            Menu.Status = !Menu.Status;
            db.SaveChanges();
            return Menu.Status;
        }
    }
}