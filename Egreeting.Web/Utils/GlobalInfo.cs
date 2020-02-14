using Egreeting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Egreeting.Web.Utils
{
    public class GlobalInfo
    {
        private static GlobalInfo _globalInfo = new GlobalInfo();


        public static GlobalInfo getInstance()
        {
            return _globalInfo;
        }

        public GlobalInfo()
        {
        }

        public bool IsAdmin
        {
            get
            {
                return SessionObject.GetBool(GlobalConstant.SESSION_IS_AMIN);
            }
            set
            {
                SessionObject.SetBool(GlobalConstant.SESSION_IS_AMIN, value);
            }
        }

        public string EgreetingUserName
        {
            get
            {
                return SessionObject.GetString(GlobalConstant.SESSION_EGREETING_USER_NAME);
            }
            set
            {
                SessionObject.SetString(GlobalConstant.SESSION_EGREETING_USER_NAME, value);
            }
        }




    }
}