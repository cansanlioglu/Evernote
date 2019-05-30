using MyEvernote.Business.Abstract;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Business
{
    public class CategoryManager : ManagerBase<Category>
    {
        public override int Delete(Category category)
        {
            NoteManager noteManager = new NoteManager();
            LikedManager likeManager = new LikedManager();
            CommentManager commentManager = new CommentManager();

            foreach (Note note in category.Notes.ToList())
            {
                foreach (Liked like in note.Likes.ToList())
                {
                    likeManager.Delete(like);
                }

                foreach (Comment comment in note.Comments.ToList())
                {
                    commentManager.Delete(comment);
                }

                noteManager.Delete(note);
            }

            return base.Delete(category);
        }
    }
}

