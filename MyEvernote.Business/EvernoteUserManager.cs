using MyEvernote.Business.Abstract;
using MyEvernote.Business.Result;
using MyEvernote.Common.Helpers;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Business
{
    public class EvernoteUserManager : ManagerBase<EvernoteUser>
    {
        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            EvernoteUser user = Find(x => x.Username == data.Username || x.Email == data.Email);

            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.AddError(ErrorMessagesCode.UsernameAlreadyExists,"Kullanıcı adı kayıtlı.");
                }

                if (user.Email == data.Email)
                {
                    layerResult.AddError(ErrorMessagesCode.EmailAlreadyExists,"E-Posta adresi kayıtlı.");
                }
            }

            else
            {
                int dbresult = base.Insert(new EvernoteUser()
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    Username = data.Username,
                    Password = data.Password,
                    Email = data.Email,
                    ProfileImageFilename="user.jpg",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false
                });

                if (dbresult > 0)
                {
                    layerResult.Result = Find(x => x.Username == data.Username && x.Email == data.Email);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string ActivateUri = $"{siteUri}/Home/UserActivate/{layerResult.Result.ActivateGuid}";
                    string body = $"Merhaba {layerResult.Result.Name} {layerResult.Result.Surname}; <br><br> Hesabınızı aktifleştirmek için <a href='{ActivateUri}' target='_blank'> tıklayınız..</a>.";

                    MailHelper.SendMail(body, layerResult.Result.Email, "MyEvernote Hesap Aktifleştirme");
                }
            }

            return layerResult;
        }

        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();

            layerResult.Result = Find(x => x.Username == data.Username && x.Password == data.Password);

            if (layerResult.Result!=null)
            {
                if (!layerResult.Result.IsActive)
                {
                    layerResult.AddError(ErrorMessagesCode.UserIsNotActive,"Aktifleştirilmemiş kullanıcı!!!");
                    layerResult.AddError(ErrorMessagesCode.CheckYourEmail, "Lütfen E-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                layerResult.AddError(ErrorMessagesCode.UsernameOrPassword,"Kullanıcı Adı ve Şifre uyuşmamaktadır.");
            }

            return layerResult;
        }

        public BusinessLayerResult<EvernoteUser> UpdateProfile(EvernoteUser data)
        {
            EvernoteUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }
                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Email adresi kayıtlı.");
                }
                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Username = data.Username;
            res.Result.Password = data.Password;

            if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }

            return res;

        }


        public BusinessLayerResult<EvernoteUser> AvtivateUser(Guid id)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = Find(x => x.ActivateGuid == id);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyActive, "Aktif edilmiş kullanıcı!");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessagesCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı..");
            }

            return res;
        }


        public BusinessLayerResult<EvernoteUser> GetUserById(int id)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = Find(x => x.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessagesCode.UserNotFound, "Kullanıcı bulunamadı.");
            }
            return res;
        }


        public BusinessLayerResult<EvernoteUser> RemoveUserById(int id)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            EvernoteUser user = Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) ==0)
                {
                    res.AddError(ErrorMessagesCode.UserCouldNotRemove, "Kullanıcı silinemedi..");
                    return res;
                }
            }

            else
            {
                res.AddError(ErrorMessagesCode.UserCouldNotFind, "Kullanıcı bulunamadı..");
            }

            return res;
        }


        public new BusinessLayerResult<EvernoteUser> Insert(EvernoteUser data)
        {
            EvernoteUser user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();

            layerResult.Result = data;

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (user.Email == data.Email)
                {
                    layerResult.AddError(ErrorMessagesCode.EmailAlreadyExists, "E-Posta adresi kayıtlı.");
                }
            }

            else
            {
                layerResult.Result.ProfileImageFilename = "user.jpg";
                layerResult.Result.ActivateGuid = Guid.NewGuid();

                if (base.Insert(layerResult.Result)==0)
                {
                    layerResult.AddError(ErrorMessagesCode.UserCouldNotInserted, "Kullanıcı eklenemedi");
                }
            }

            return layerResult;
        }


        public new BusinessLayerResult<EvernoteUser> Update(EvernoteUser data)
        {
            EvernoteUser db_user = Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            res.Result = data;

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }
                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessagesCode.UsernameAlreadyExists, "Email adresi kayıtlı.");
                }
                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Username = data.Username;
            res.Result.Password = data.Password;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;


            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessagesCode.ProfileCouldNotUpdated, "Profil güncellenemedi");
            }

            return res;

        }

    }
}

