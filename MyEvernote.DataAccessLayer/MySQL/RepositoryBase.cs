//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyEvernote.DataAccessLayer.MySQL
//{
//    // IRepository oluşturmaktaki amaç, birden falza veritabanı ile çalışırsak bu interface ile kalıtım alabiliriz. Örnek için oluşturulan bir sınıf. Bu projede kullanılmamaktadır.

//    public class RepositoryBase
//    {
//        protected static MySqlContext db;
//        private static object _lockSync = new object();


//        protected RepositoryBase()
//        {
//            CreateContext();
//        }


//        public static MySqlContext CreateContext()
//        {
//            if (db == null)
//            {
//                lock (_lockSync)
//                {
//                    if (db == null)
//                    {
//                        db = new MySqlContext();
//                    }
//                }
//            }
//            return db;
//        }
//    }
//}
