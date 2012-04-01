using System;
using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;

namespace HttpGhost.Transport
{
	public interface IRequest
	{
		IResponse GetResponse();
		
		void SetAuthentication(AuthenticationInfo authentication);
		void SetMethod(string method);
		void SetContentType(string contentType);
		void WriteFormDataToRequestStream(string formData);

	    string Url { get; }
	    void WriteFormDataToRequestStream(object formData);
	    AuthenticationInfo GetAuthentication();
	    string GetContentType();
	    Uri Uri { get; }
	}
}