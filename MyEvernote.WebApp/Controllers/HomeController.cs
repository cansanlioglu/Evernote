using MyEvernote.Business;
using MyEvernote.Business.Result;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using MyEvernote.WebApp.Models;
using MyEvernote.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private EvernoteUserManager evernoteUserManager = new EvernoteUserManager();


        // GET: Home
        public ActionResult Index()
        {
            return View(noteManager.ListQueryable().OrderByDescending(x=> x.ModifiedOn).ToList());
        }


        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category cat = categoryManager.Find(x => x.Id == id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
            
        }


        public ActionResult MostLiked()
        {
            return View("Index", noteManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }


        public ActionResult About()
        {
            return View();
        }


        public ActionResult ShowProfile()
        {
            BusinessLayerResult<EvernoteUser> res =  evernoteUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count >0 )
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items =res.Errors,
                };
                return View("Error", errorNotifyObj);
            }
            return View(res.Result);
        }
        

        [HttpGet]
        public ActionResult EditProfile()
        {
            BusinessLayerResult<EvernoteUser> res =  evernoteUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors,
                };
                return View("Error", errorNotifyObj);
            }
            return View(res.Result);
        }


        [HttpPost]
        public ActionResult EditProfile(EvernoteUser model,HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                                        (ProfileImage.ContentType == "image/jpeg" ||
                                         ProfileImage.ContentType == "image/jpg" ||
                                         ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    model.ProfileImageFilename = filename;
                }

                BusinessLayerResult<EvernoteUser> res =  evernoteUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Title = "Profil Güncellenemedi",
                        Items = res.Errors,
                        RedirectingUrl = "/Home/EditProfile"
                    };
                    return View("Error", errorNotifyObj);
                }

                CurrentSession.Set<EvernoteUser>("login",res.Result); // Profil Güncellendiği için session güncellendi.

                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }


        public ActionResult DeleteProfile()
        {
            EvernoteUser currentUser = Session["login"] as EvernoteUser;
            BusinessLayerResult<EvernoteUser> res =  evernoteUserManager.RemoveUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Profil Silinemedi",
                    Items = res.Errors,
                    RedirectingUrl= "~/Home/ShowProfile"
                };
                return View("Error", errorNotifyObj);
            }

            Session.Clear();
            return RedirectToAction("Index");
        }


        public ActionResult TestNotify()
        {
            ErrorViewModel model = new ErrorViewModel()
            {
                Header = "Yönlendirme..",
                Title = "Error Test",
                RedirectingTimout = 3000,
                Items=new List<ErrorMessagesObj>() {
                    new ErrorMessagesObj(){Message="Hata1"},
                    new ErrorMessagesObj(){Message="Hata2"}
                }
            };

            return View("Error", model);
        }

        [HttpGet] // Get : Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost] // Post : Login
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<EvernoteUser> res =  evernoteUserManager.LoginUser(model);

                if (res.Errors.Count>0)
                {
                    if (res.Errors.Find(x=> x.Code==ErrorMessagesCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/123-4123-41232123";
                    }

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                CurrentSession.Set<EvernoteUser>("login", res.Result);    
                return RedirectToAction("Index");  
            }
           return View(model);
        }


        [HttpGet] 
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            // kullanıcı bilgilerinin kontrolü
            // Kayıt işlemi
            // Aktivasyon e-Postası gönderimi

            if (ModelState.IsValid)
            {
                BusinessLayerResult<EvernoteUser> res =  evernoteUserManager.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }


                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login"
                };

                notifyObj.Items.Add("Lütfen E-Poste adresinize gönderilen aktivasyon link'ine tıklayarak hesabınızı aktif hale getiriniz. Hesabınızı aktifleştirmeden not ekleyemez ve kullanamazsınız.");

                return View("Ok",notifyObj);
            }
            return View(model);
        }


        public ActionResult UserActivate(Guid id)
        {
            // kullanıcı aktivasyonunu sağlayacak
            BusinessLayerResult<EvernoteUser> res =  evernoteUserManager.AvtivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Heasp Aktifleştirildi",
                RedirectingUrl = "/Home/Login"
            };

            okNotifyObj.Items.Add("Hesabınız aktifleştirildi. Hoşgeldiniz...");

            return View("Ok", okNotifyObj);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}