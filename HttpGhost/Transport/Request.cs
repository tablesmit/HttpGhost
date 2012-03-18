using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text;
using HttpGhost.Authentication;
using HttpGhost.Serialization;

namespace HttpGhost.Transport
{
	public class Request : IRequest
	{
		private readonly HttpWebRequest webRequest;
	    private readonly ISerializer serializer;

	    public Request(HttpWebRequest webRequest, ISerializer serializer)
		{
			this.webRequest = webRequest;
		    this.serializer = serializer;
		    webRequest.Headers = new WebHeaderCollection();
		}

	    public string Url
	    {
	        get { return this.webRequest.RequestUri.PathAndQuery; }
	    }

	    public IResponse GetResponse()
		{
			return new Response((HttpWebResponse) webRequest.GetResponse());
		}

		public void SetAuthentication(AuthenticationInfo authentication)
		{
			if (authentication.Type == AuthenticationType.Anonymous)
				return;

			var base64String = Convert.ToBase64String(new ASCIIEncoding().GetBytes(authentication.Credentials.UsernamePassword));
			webRequest.Headers.Add("Authorization", "Basic " + base64String);
			var networkCredential = new NetworkCredential(authentication.Credentials.Username, authentication.Credentials.Password);
			webRequest.Credentials = new CredentialCache{{ webRequest.RequestUri , "Basic", networkCredential }} ;
			webRequest.AllowAutoRedirect = false;

		}

		public void SetMethod(string method)
		{
			webRequest.Method = method;
		}

		public void SetContentType(string contentType)
		{
			webRequest.ContentType = contentType;
		}

        public void WriteFormDataToRequestStream(object formData)
        {
            WriteFormDataToRequestStream(serializer.Serialize(formData));
        }

		public void WriteFormDataToRequestStream(string formData)
		{
			var bytes = Encoding.UTF8.GetBytes(formData);
			webRequest.ContentLength = bytes.Length;

			var dataStream = webRequest.GetRequestStream();
			dataStream.Write(bytes, 0, bytes.Length);
			dataStream.Close();
		}
	}
}