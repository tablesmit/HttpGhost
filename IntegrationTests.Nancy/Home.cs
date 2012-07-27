using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Nancy;
using Nancy.Responses;

namespace IntegrationTests.Nancy
{
    public class Home : NancyModule
    {
        public Home()
        {
            Get["/"] = _ => { return "Getting"; };
            Get["/get-querystring"] = parameters => Request.Query.q + "";
            Get["/redirect-to-home"] = _ => Response.AsRedirect("/", RedirectResponse.RedirectType.Permanent);
            Post["/"] = _ => "Posting";
            Put["/"] = _ => "Putting";
            Delete["/"] = _ => "Deleting";


            Get["/with-link"] = _ => "<a id='mylink' href='/follow'>follow</a><a id='mylink302' href='/with-link-302'>follow</a>";
            Get["/with-link-302"] = _ => Response.AsRedirect("/follow", RedirectResponse.RedirectType.Permanent); ;
            Get["/follow"] = _ => "Followed";
        }
    }
}