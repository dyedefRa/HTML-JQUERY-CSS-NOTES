using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;

namespace TBBDokuman.Core.Attributes
{
   public class PreventSpamAttribute: ActionFilterAttribute
    {
        public int DelayRequest = 10;
        public string ErrorMessage = "Excessive Request Attempts Detected.";
        public string RedirectURL;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var cache = filterContext.HttpContext.Cache;

            //Grab the IP Address from the originating Request (very simple implementation for example purposes)
            var originationInfo = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;

            //Append the User Agent
            originationInfo += request.UserAgent;

            //Now we just need the target URL Information
            var targetInfo = request.RawUrl + request.QueryString;

            //Generate a hash for your strings (this appends each of the bytes of the value into a single hashed string
            var hashValue = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(originationInfo + targetInfo)).Select(s => s.ToString("x2")));

            //Checks if the hashed value is contained in the Cache (indicating a repeat request)
            if (cache[hashValue] != null)
            {
                //Adds the Error Message to the Model and Redirect
                filterContext.Controller.ViewData.ModelState.AddModelError("ExcessiveRequests", ErrorMessage);
            }
            else
            {
                cache.Add(hashValue, hashValue, null, DateTime.Now.AddSeconds(DelayRequest), Cache.NoSlidingExpiration, CacheItemPriority.Default,null);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
