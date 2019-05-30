//using MyEvernote.DataAccessLayer.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyEvernote.DataAccessLayer.MySQL   
//    // IRepository oluşturmaktaki amaç, birden falza veritabanı ile çalışırsak bu interface ile kalıtım alabiliriz. Örnek için oluşturulan bir sınıf. Bu projede kullanılmamaktadır.
//{
//    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
//    {
//        public int Delete(T obj)
//        {
//            throw new NotImplementedException();
//        }

//        public T Find(Expression<Func<T, bool>> where)
//        {
//            throw new NotImplementedException();
//        }

//        public int Insert(T obj)
//        {
//            throw new NotImplementedException();
//        }

//        public List<T> List()
//        {
//            throw new NotImplementedException();
//        }

//        public List<T> List(Expression<Func<T, bool>> where)
//        {
//            throw new NotImplementedException();
//        }

//        public int Save()
//        {
//            throw new NotImplementedException();
//        }

//        public int Update(T obj)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
