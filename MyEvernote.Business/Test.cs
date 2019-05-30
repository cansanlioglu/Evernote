//using MyEvernote.DataAccessLayer.EntityFramework;
//using MyEvernote.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyEvernote.Business
//{
//   public class Test
//    {
//        Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
//        Repository<Category> repo_category = new Repository<Category>();
//        Repository<Comment> repo_comment = new Repository<Comment>();
//        Repository<Note> repo_note = new Repository<Note>();



//        public Test()
//        {
//            List<Category> categories = repo_category.List(x => x.Id > 5);
//            //List<Category> categories = repo.List();
//        }


//        public void InsertTest()
//        {
//            int result= repo_user.Insert(new EvernoteUser()
//            {
//                Name="aaa",
//                Surname="bbb",
//                Email="name@gmail.com",
//                ActivateGuid=Guid.NewGuid(),
//                IsActive=true,
//                IsAdmin=true,
//                Username="aabb",
//                Password="123",
//                CreatedOn=DateTime.Now,
//                ModifiedOn=DateTime.Now,
//                ModifiedUsername="aabb"

//            });  
//        }


//        public void UpdateTest()
//        {
//            EvernoteUser user = repo_user.Find(x => x.Username == "aabb");
//            if (user!=null)
//            {
//                user.Username = "xxx";
//                int result = repo_user.Update(user);
//            }
//        }


//        public void DeleteTest()
//        {
//            EvernoteUser user = repo_user.Find(x => x.Username == "xxx");
//            if (user != null)
//            {
//                int result = repo_user.Delete(user);
//            }
//        }


//        public void CommentTest()
//        {
//            EvernoteUser user = repo_user.Find(x => x.Id == 1);
//            Note note = repo_note.Find(x => x.Id == 3);
//            Comment comment = new Comment()
//            {
//                Text = "Bu bir test'dir..",
//                CreatedOn = DateTime.Now,
//                ModifiedOn = DateTime.Now,
//                ModifiedUsername = "cnsnlgl",
//                Note = note,
//                Owner = user
//            };

//            repo_comment.Insert(comment);
//        }

//    }
//}
