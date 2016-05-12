using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoJobber.Utilities;
using EncryptedParameterAuthentication.Entities;
using EncryptedParameterAuthentication.BL;
using System.IO;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.text.pdf.parser;

namespace EncryptedParameterAuthentication
{
    public partial class _Default : System.Web.UI.Page
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
                Page.SetFocus(UserName);
            }
        }
        protected void BTLogin_Click(object sender, EventArgs e)
        {
            //Writing Entry log in the log file
            Logger.WriteToLogFile(Logger.LogMessageTypes.DEBUG, Logger.LogMessageTypes.ENTRY);
            //Declaring a boolen variable to Signify error 
            bool errFlag = false;

            try
            {
                UserName.Text = UserName.Text.Trim();
                Password.Text = Password.Text.Trim();
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
                UserRegistrationDetails objUserRegistrationDetails = new UserRegistrationDetails();
                objUserRegistrationDetails.UserName = UserName.Text;
                objUserRegistrationDetails.Password = Password.Text;

                BLUserRegistrationDetails objBLUserRegistrationDetails = new BLUserRegistrationDetails();
                objUserRegistrationDetails = objBLUserRegistrationDetails.GetUserRegistrationDetails(objUserRegistrationDetails);

                if (objUserRegistrationDetails.UserRegistrationDetailsID != 0)
                {
                    try
                    {
                        ReadFile(objUserRegistrationDetails);
                        Session["UserRegistrationID"] = objUserRegistrationDetails.UserRegistrationDetailsID;
                        Response.Redirect(Constants.PageAbout);
                    }
                    catch (Exception ex)
                    {
                        throw new JustifierCustomException(ex);
                    }
                    

                }
                else
                {
                    throw new JustifierCustomException("Invalid credentials......");
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
        public void ReadFile(UserRegistrationDetails objUserRegistrationDetails)
        {
            try
            {
                //string [] files= Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings[Constants.Path]);
                string fileName = System.Configuration.ConfigurationManager.AppSettings[Constants.Path] + objUserRegistrationDetails.UserRegistrationDetailsID + objUserRegistrationDetails.Name.Trim() + ".txt";
                if (File.Exists(fileName))
                {
                    FileAttributes attributes = File.GetAttributes(fileName);

                    if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        // Show the file.
                        attributes = RemoveAttribute(attributes, FileAttributes.Hidden);
                        File.SetAttributes(fileName, attributes);
                        
                    }
                    //string newfile = Path.ChangeExtension(fileName, ".pdf");
                    PDFParser pdfParser = new PDFParser();
                    string result = string.Empty;

                    StringBuilder text = new StringBuilder();

                    PdfReader pdfReader = new PdfReader(fileName);

                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = string.Empty;
                        try
                        {
                             currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                        }
                        catch
                        {
                            throw new JustifierCustomException("Invalid file.");
                        }

                        currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                        text.Append(currentText);
                        pdfReader.Close();
                    }
                    FileAttributes attribute1 = File.GetAttributes(fileName);
                    File.SetAttributes(fileName, File.GetAttributes(fileName) | FileAttributes.Hidden);

                    result = text.ToString();
                    string[] temp = result.Split('-');
                    if (temp[0] != objUserRegistrationDetails.UserName || temp[1] != objUserRegistrationDetails.Name || temp[2] != objUserRegistrationDetails.ContactNumber)
                    {
                        throw new JustifierCustomException("Invalid file.");
                    }

                }
                else
                {
                    throw new JustifierCustomException("Associated file does not exists.");
                }
                     
            }
            catch (Exception ex)
            {

                throw new JustifierCustomException(ex);

            }
        }
        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }
    }
}
