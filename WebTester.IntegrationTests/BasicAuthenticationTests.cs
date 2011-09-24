﻿using System.Configuration;
using NUnit.Framework;

namespace WebTester.IntegrationTests
{
	[TestFixture]
	public class BasicAuthenticationTests
	{
		[Test]
		public void Session_GetHtml_ReturnHtml()
		{
			var url = ConfigurationManager.AppSettings["BasicAuthenticationUrl"];
			var username = ConfigurationManager.AppSettings["BasicAuthenticationUsername"];
			var password = ConfigurationManager.AppSettings["BasicAuthenticationPassword"];
			var session = new Session(url).As(username, password);

			var result = session.Get();

			Assert.That(result.Html, Is.StringContaining("<html>"));
		}
	}
}