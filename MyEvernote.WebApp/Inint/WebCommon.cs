using MyEvernote.Common;
using MyEvernote.Entities;
using MyEvernote.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.WebApp.Inint
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            EvernoteUser user = CurrentSession.User;

            if (user != null)
            {
                return user.Username;
            }

            return "system";
        }
    }
}