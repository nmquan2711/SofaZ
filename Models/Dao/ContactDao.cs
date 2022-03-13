using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        private shopnoithatDbContext db = null;

        public ContactDao()
        {
            db = new shopnoithatDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }

        public IEnumerable<Contact> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Contact> model = db.Contacts;

            return model.OrderByDescending(x => x.ID);
        }

        public Contact GetByID(long id)
        {
            return db.Contacts.Find(id);
        }

        public long Insert(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
            return contact.ID;
        }

        public bool Delete(int id)
        {
            try
            {
                var contact = db.Contacts.Find(id);
                db.Contacts.Remove(contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Contact entity)
        {
            try
            {
                var contact = db.Contacts.Find(entity.ID);

                contact.Content = entity.Content;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public List<Contact> ListAll()
        {
            return db.Contacts.Where(x => x.Status == true).ToList();
        }

        public bool ChangeStatus(long id)
        {
            var contact = db.Contacts.Find(id);
            contact.Status = !contact.Status;
            db.SaveChanges();
            return contact.Status;
        }
    }
}