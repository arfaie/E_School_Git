using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_School.Models.DomainModels;
using System.Data;
using System.Data.Entity.Validation;
using static E_School.Helpers.Utitlies.Custom_Exception;

namespace E_School.Models.Repositories.api
{
    public class StudentRateRepository : IDisposable
    {
        private schoolEntities db = null;

        public StudentRateRepository()
        {
            db = new schoolEntities();
        }

        public bool Add(tbl_StudentsRates entity, bool autoSave = true)
        {
            try
            {
                db.tbl_StudentsRates.Add(entity);
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(tbl_StudentsRates entity, bool autoSave = true)
        {
            try
            {
                db.tbl_StudentsRates.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                return false;
            }
        }

        public bool Delete(tbl_StudentsRates entity, bool autoSave = true)
        {
            try
            {
                db.Entry(entity).State = EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id, bool autoSave = true)
        {
            try
            {
                var entity = db.tbl_StudentsRates.Find(id);
                db.Entry(entity).State = EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public tbl_StudentsRates Find(int id)
        {
            try
            {
                return db.tbl_StudentsRates.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<tbl_StudentsRates> Where(System.Linq.Expressions.Expression<Func<tbl_StudentsRates, bool>> predicate)
        {
            try
            {
                return db.tbl_StudentsRates.Where(predicate);
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<tbl_StudentsRates> Select()
        {
            try
            {
                return db.tbl_StudentsRates.AsQueryable();
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<tbl_StudentsRates, TResult>> selector)
        {
            try
            {
                return db.tbl_StudentsRates.Select(selector);
            }
            catch
            {
                return null;
            }
        }

        public int GetLastIdentity()
        {
            try
            {
                if (db.tbl_StudentsRates.Any())
                    return db.tbl_StudentsRates.OrderByDescending(p => p.id).First().id;
                else
                    return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int Save()
        {
            try
            {
                return db.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }

        ~StudentRateRepository()
        {
            Dispose(false);
        }
    }
}