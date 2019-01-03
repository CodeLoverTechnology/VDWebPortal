using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VDWebPortal.App_Code
{
    public class CommonFunctionVD
    {

        public static bool CheckUserAuthentication()
        {
            if (HttpContext.Current.Session["EmailID"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}