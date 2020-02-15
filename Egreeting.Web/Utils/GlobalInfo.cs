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


    }
}