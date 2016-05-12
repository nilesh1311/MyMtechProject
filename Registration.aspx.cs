using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoJobber.Utilities;
using EncryptedParameterAuthentication.BL;
using EncryptedParameterAuthentication.Entities;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EncryptedParameterAuthentication
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            if (!IsPostBack)
            {
                if (Request.QueryString["MSG"] != null && Request.QueryString["MSG"] != "")
                {
                    string MSG = Request.QueryString["MSG"];
                    string script = "<script>alert('" + MSG + "');</script>";
                    if (!Page.ClientScript.IsStartupScriptRegistered("myErrorScript"))
                    {
                        Page.RegisterStartupScript("myErrorScript", script);
                    }
                }
                Page.SetFocus(Name);
            }
        }
        protected void UserName_TextChanged(object sender, EventArgs e)
        {
            //Writing Entry log in the log file
            Logger.WriteToLogFile(Logger.LogMessageTypes.DEBUG, Logger.LogMessageTypes.ENTRY);
            //Declaring a boolen variable to Signify error 
            bool errFlag = false;

            try
            {
                BLUserRegistrationDetails objBLUserRegistrationDetails = new BLUserRegistrationDetails();
                if (objBLUserRegistrationDetails.CheckUserExistence(UserName.Text.Trim()))
                {
                    UserName.Focus();
                    throw new JustifierCustomException("User Name already registered.");
                }
                else
                {
                    Password.Focus();
                }
            }
            catch (Exception ex)
            {

                string script = "<script>alert('" + ex.Message + "');</script>";
                if (!Page.ClientScript.IsStartupScriptRegistered("myErrorScript"))
                {
                    Page.RegisterStartupScript("myErrorScript", script);
                }

            }
        }
        protected void BTSubmit_Click(object sender, EventArgs e)
        {
            //Writing Entry log in the log file
            Logger.WriteToLogFile(Logger.LogMessageTypes.DEBUG, Logger.LogMessageTypes.ENTRY);
            //Declaring a boolen variable to Signify error 
            bool errFlag = false;

            try
            {
                Name.Text = Name.Text.Trim();
                UserName.Text = UserName.Text.Trim();
                Password.Text = Password.Text.Trim();
                ConfirmPassword.Text = ConfirmPassword.Text.Trim();
                ContactNumber.Text = ContactNumber.Text.Trim();
                Address.Text = Address.Text.Trim();
                if (Name.Text == "")
                {
                    Name.Focus();
                    throw new JustifierCustomException("Name can not be empty.");
                }
                if (UserName.Text == "")
                {
                    UserName.Focus();
                    throw new JustifierCustomException("User Name can not be empty.");
                }
                if (Password.Text == "")
                {
                    Password.Focus();
                    throw new JustifierCustomException("Password can not be empty.");
                }
                if (Password.Text.Length < 6)
                {
                    Password.Focus();
                    throw new JustifierCustomException("Password should be minimum six character.");
                }
                if (Password.Text != ConfirmPassword.Text)
                {
                    ConfirmPassword.Focus();
                    throw new JustifierCustomException("Password and ConfirmPassword should be same.");
                }

                UserRegistrationDetails objUserRegistrationDetails = new UserRegistrationDetails();
                objUserRegistrationDetails.Name = Name.Text;
                objUserRegistrationDetails.UserName = UserName.Text;
                objUserRegistrationDetails.Password = Password.Text;
                objUserRegistrationDetails.ContactNumber = ContactNumber.Text;
                objUserRegistrationDetails.Address = Address.Text;

                BLUserRegistrationDetails objBLUserRegistrationDetails = new BLUserRegistrationDetails();
                int UserRegistraionID = 0;
                UserRegistraionID = objBLUserRegistrationDetails.SaveUserRegistrationDetails(objUserRegistrationDetails);
                objUserRegistrationDetails.UserRegistrationDetailsID = UserRegistraionID;
                if (UserRegistraionID != 0)
                {
                    try
                    {
                        GenerateFile(objUserRegistrationDetails);
                    }
                    catch (Exception ex)
                    {
                        throw new JustifierCustomException(ex);
                    }
                    string msg = "User Registration Successfully.";
                    Response.Redirect(Constants.PageRegistration + "?MSG=" + msg);
                }
                else
                {
                    throw new JustifierCustomException("User Registration can not be submited.");
                }

            }
            catch (Exception ex)
            {

                string script = "<script>alert('" + ex.Message + "');</script>";
                if (!Page.ClientScript.IsStartupScriptRegistered("myErrorScript"))
                {
                    Page.RegisterStartupScript("myErrorScript", script);
                }

            }
        }
        public void GenerateFile(UserRegistrationDetails objUserRegistrationDetails)
        {
            try
            {
                if (!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings[Constants.Path]))
                {
                    Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings[Constants.Path]);
                }
                var pdfDoc = new iTextSharp.text.Document(PageSize.A4);
                PdfWriter.GetInstance(pdfDoc, new FileStream(System.Configuration.ConfigurationManager.AppSettings[Constants.Path] + objUserRegistrationDetails.UserRegistrationDetailsID + objUserRegistrationDetails.Name + ".txt", FileMode.Create));
                pdfDoc.Open();
                Paragraph paragraph = new Paragraph(objUserRegistrationDetails.UserName + "-" + objUserRegistrationDetails.Name + "-" + objUserRegistrationDetails.ContactNumber);
                pdfDoc.Add(paragraph);

                pdfDoc.Close();
                FileAttributes attributes = File.GetAttributes(System.Configuration.ConfigurationManager.AppSettings[Constants.Path] + objUserRegistrationDetails.UserRegistrationDetailsID + objUserRegistrationDetails.Name + ".txt");
                File.SetAttributes(System.Configuration.ConfigurationManager.AppSettings[Constants.Path] + objUserRegistrationDetails.UserRegistrationDetailsID + objUserRegistrationDetails.Name + ".txt", File.GetAttributes(System.Configuration.ConfigurationManager.AppSettings[Constants.Path] + objUserRegistrationDetails.UserRegistrationDetailsID + objUserRegistrationDetails.Name + ".txt") | FileAttributes.Hidden);
              
            }
            catch (Exception ex)
            {

                throw new JustifierCustomException(ex);

            }
        }
    }
}