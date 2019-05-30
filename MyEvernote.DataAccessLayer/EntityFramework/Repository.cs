using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Common;
using MyEvernote.Core.DataAccess;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class Repository<T> :RepositoryBase , IDataAccess<T> where T : class
    {
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = db.Set<T>();
        }


        public List<T> List()
        {
            return _objectSet.ToList();
        }


        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }


        public List<T> List(Expression<Func<T,bool>>where)
        {
            return _objectSet.Where(where).ToList();
        }


        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = DateTime.Now;
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername =App.Common.GetUsername(); 
            }
            return Save();
        }


        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = "system"; 
            }

            return Save();
        }


        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }


        public int Save()
        {
            return db.SaveChanges();
        }


        public T Find(Expression<Func<T,bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

    }
}
