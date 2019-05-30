using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("İsim"),
         Required(ErrorMessage = "{0} alanı boş geçmeyiniz."),
         StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı")]
        public string Name { get; set; }

        [DisplayName("Soyisim"),
         Required(ErrorMessage = "{0} alanı boş geçmeyiniz."),
         StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), 
         Required(ErrorMessage = "{0} alanı boş geçmeyiniz."),
         StringLength(25,ErrorMessage ="{0} max. {1} karakter olmalı")]
        public string Username { get; set; }


        [DisplayName("Şifre"), 
         Required(ErrorMessage = "{0} alanı boş geçmeyiniz."), 
         StringLength(11, ErrorMessage = "{0} max. {1} karakter olmalı"),
         DataType(DataType.Password)]
        public string Password { get; set; }


        [DisplayName("Şifre Tekrar"),
         Required(ErrorMessage = "{0} alanı boş geçmeyiniz."),
         StringLength(11, ErrorMessage = "{0} max. {1} karakter olmalı"),
         DataType(DataType.Password),
         Compare("Password", ErrorMessage ="{0} ile {1} uyuşmuyor")]
        public string RePassword { get; set; }


        [DisplayName("E-Posta"),
         Required(ErrorMessage = "{0} alanı boş geçmeyiniz."),
         StringLength(70, ErrorMessage = "{0} max. {1} karakter olmalı"),
         EmailAddress(ErrorMessage = "{0} alanı için geçerli bir email adresi giriniz")]
        public string Email { get; set; }

    }
}