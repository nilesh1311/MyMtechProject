using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncryptedParameterAuthentication
{
    public class Constants
    {
        #region database column name
        public static string UserName = "UserName";
        public static string Status = "Status";
        public static string Name = "Name";
        public static string ContactNumber = "ContactNumber";
        public static string Address = "Address";
        public static string Password = "Password";
        public static string UserRegistrationDetailsID = "UserRegistrationDetailsID";
        public static string DB_CONNFAILED = "DB_CONNFAILED";
       
        #endregion

        #region stored procedures name
        public static string USP_CheckUserExistance = "USP_CheckUserExistance";
        public static string USP_SaveUserRegistrationDetails = "USP_SaveUserRegistrationDetails";
        public static string USP_GetUserRegistrationDetails = "USP_GetUserRegistrationDetails";
       
        #endregion

        #region UI Page Name
        
        public static string PageRegistration = "Registration.aspx";
        public static string PageDefault = "Default.aspx";
        public static string PageLoginPhaseFirst = "LoginPhaseFirst.aspx";
        public static string PageAbout = "About.aspx";
        #endregion

        public static string Path = "Path";

       
    }
}