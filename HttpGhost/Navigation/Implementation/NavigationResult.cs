using System.Linq;
using System.Collections.Generic;
using System.Net;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Implementation
{
	public class NavigationResult : INavigationResult
	{
	    private readonly IRequest request;
	    protected readonly IResponse response;

		public NavigationResult(IRequest request, IResponse response)
		{
		    this.request = request;
		    this.response = response;
		}

	    public HttpStatusCode Status { get { return response.StatusCode; } }

		public string ResponseContent { get { return response.Html; } }
		
		public WebHeaderCollection ResponseHeaders
		{
			get { return response.Headers; }
		}

	    public IEnumerable<string> Find(string pattern)
	    {
	        var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(ResponseContent);

	        var items = htmlDoc.DocumentNode.SelectNodes(pattern);

            return items == null ? new List<string>() : items.Select(i => i.OuterHtml);
	    }

	    public T ToJson<T>()
	    {
	        return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<T>(ResponseContent);
	    }

	    public string RequestUrl
	    {
            get { return request.Url; }
	    }
	}
}