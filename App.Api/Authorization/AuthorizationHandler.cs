using SingleApp.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace SingleApp
{
    public class AuthorizationHandler : DelegatingHandler
    {

        private const string SessionError = "Unauthorized";
        private const string SessionHeader = "Session";

        public AuthorizationHandler(HttpConfiguration httpConfiguration)
        {
            InnerHandler = new HttpControllerDispatcher(httpConfiguration);
        }

        public AuthorizationHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {

        }
   
        public AuthorizationHandler()
        {

        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                //Unauthorized
                if (HttpContext.Current.Request.Headers[SessionHeader] == null)
                {
                    var errResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent(SessionError),
                        ReasonPhrase = SessionError
                    };
                    return errResponse;

                }

                string sessionHeader = HttpContext.Current.Request.Headers[SessionHeader];


                Session session = Session.Lookup(sessionHeader);

                if (session == null)
                {
                    throw new SecurityException("No session record exists");
                }
                else if (session.Expired)
                {
                    throw new SecurityException("Session Expired");
                }
                else
                {
                    session.LastAccess = DateTime.Now;
                    session.Save();
                    var identity = new SecurityPrincipal(session);
                    Thread.CurrentPrincipal = identity;
                    HttpContext.Current.User = identity;

                }

                // Call the inner handler.
                var response = await base.SendAsync(request, cancellationToken);
                return response;
            }
            catch (SecurityException secEx)
            {
                Debug.WriteLine(secEx);
                var errResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(SessionError),
                    ReasonPhrase = SessionError
                };

                return errResponse;
            }
            catch (Exception ex)
            {

                var errResponse = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = "Internal Server Error"
                };

                return errResponse;
            }
        }



    }
}