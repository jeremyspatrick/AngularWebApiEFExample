using SingleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace SingleApp.Controllers
{
    public class AuthenticationController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Login(User user)
        {
           // var logger = log4net.LogManager.GetLogger("myLog"); logger.Debug("ttttest");

            //logger.Error("vat");
            //FileStream logFileStream = new FileStream(@"D:\home\savinglivestech.com\logs\W3SVC19\u_ex160502.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //StreamReader logFileReader = new StreamReader(logFileStream);
            //string lines = "";
            //while (!logFileReader.EndOfStream)
            //{
            //    string line = logFileReader.ReadLine();
            //    // Your code here
            //    lines += line +"       ";
                
            //}

            //// Clean up
            //logFileReader.Close();
            //logFileStream.Close();

            //var errResponse2 = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
            //{
            //    Content = new StringContent(lines),
            //    ReasonPhrase = "umk"
            //};
            // throw new HttpResponseException(errResponse2);

            //var json = new JavaScriptSerializer().Serialize(user);

            //var errResponse2 = new HttpResponseMessage(System.Net.HttpStatusCode.BadGateway)
            //{
            //    Content = new StringContent(json),
            //    ReasonPhrase = ""
            //};
            //throw new HttpResponseException(errResponse2);


            Session session = null;
            try
            {
                session = Models.User.Login(user);
            }
            catch (Exception ex)
            {
                var errResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Invalid Username or Password"),
                    ReasonPhrase = ex.InnerException.ToString()
                };
                throw new HttpResponseException(errResponse);
            }
            if (session == null)
            {
                var errResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Invalid Username or Password"),
                    ReasonPhrase = "Invalid Username or Password"
                };
                throw new HttpResponseException(errResponse);
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.OK, session.User.Roles);
                response.Headers.Add("session", session.SessionId);
                return response;
            }
            
        }

    }
}
