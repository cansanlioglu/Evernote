using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("EvernoteUser")]
    public class EvernoteUser : MyEntityBase
    {
        [DisplayName("İsim"),
         Required,
         StringLength(25, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyisim"),
         Required,
         StringLength(25, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName(" Kullanıcı Adı"),
         Required,
         StringLength(25, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("Email"),
         Required,
         StringLength(70, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Şifre"),
         Required,
         StringLength(10, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [Required]
        public Guid ActivateGuid { get; set; }

        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }

    }
}
