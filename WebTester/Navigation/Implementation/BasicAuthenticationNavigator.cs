using System;
using System.Net;
using System.Text;
using WebTester.Authentication;

namespace WebTester.Navigation.Implementation
{
	public class BasicAuthenticationNavigator : Navigator
	{
		private readonly Credentials credentials;

		public BasicAuthenticationNavigator(Uri uri, Credentials credentials) : base(uri)
		{
			if(credentials == null)
				throw new ArgumentNullException("credentials");

			this.credentials = credentials;
		}

		protected override void OnGet()
		{
			webRequest = CreateWebRequest();

			SetCredentialsToBasic();

			AddAuthorizationHeaders();
		}

		private void AddAuthorizationHeaders()
		{
			var base64String = Convert.ToBase64String(new ASCIIEncoding().GetBytes(credentials.UsernamePassword));
			webRequest.Headers.Add("Authorization", "Basic " + base64String);
		}

		private void SetCredentialsToBasic()
		{
			webRequest.Credentials = new CredentialCache {{uri, "Basic", new NetworkCredential(credentials.Username, credentials.Password)}};
		}
	}
}