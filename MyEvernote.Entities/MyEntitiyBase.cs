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
    public class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Oluşturma Tarihi"),
         Required(ErrorMessage = "{0} alanı gereklidir.")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Değiştirme Tarihi"),
         Required(ErrorMessage = "{0} alanı gereklidir.")]
        public DateTime ModifiedOn { get; set; }

        [DisplayName("Değiştiren Kullanıcı"), 
         Required(ErrorMessage = "{0} alanı gereklidir."),
         StringLength(30, ErrorMessage = "{0} alanı maksimum {1} karakter olmalıdır.")]
        public string ModifiedUsername { get; set; }
    }
}
