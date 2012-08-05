﻿using HttpGhost;
using NUnit.Framework;

namespace IntegrationTests
{
	[TestFixture]
    [Category("Integration")]
	public class NoAuthenticationTests : IntegrationTestsBase
	{
        protected readonly string BaseUrl = "http://127.0.0.1:8080";
	    private Session session;

	    [SetUp]
	    public void Setup()
	    {
            session = new Session();
	    }
        
		[Test]
		public void Session_Get_ReturnHtml()
		{
			var result = session.Get(BaseUrl);

			Assert.That(result.ResponseContent, Is.StringContaining("Getting"));
		}        

        [Test]
        public void Session_GetWithQuerystring_ReturnHtml()
        {
            var url = BaseUrl + "/get-querystring";
            var result = session.Get(url, new {q = "b"});

            Assert.That(result.ResponseContent, Is.StringContaining("b"));
        }

        [Test]
        public void Session_GetToUrlWhichRedirects_FollowRedirect()
        {
            var url = BaseUrl + "/redirect-to-home";
            var result = session.Get(url);

            Assert.That(result.ResponseContent, Is.StringContaining("Getting"));
        }

        [Test]
        public void Session_Post_ReturnHtml()
        {
            var result = session.Post(new { title = "jippi" }, BaseUrl);
            
            Assert.That(result.ResponseContent, Is.StringContaining("Posting"));
        }

        [Test]
        public void Session_Put_ReturnHtml()
        {
            var result = session.Put(new { Title = "jippi" }, BaseUrl);

            Assert.That(result.ResponseContent, Is.StringContaining("Putting"));
        }

        [Test]
        public void Session_Delete_ReturnHtml()
        {
            var result = session.Delete(new { id = 2 }, BaseUrl);

            Assert.That(result.ResponseContent, Is.StringContaining("Deleting"));
        }

	    [Test]
	    public void Session_GetAndFollow_ReturnsHtml()
	    {
            var url = BaseUrl + "/with-link";
            var firstResult = session.Get(url);
	        var secondResult = firstResult.Follow("#mylink");
            
	        Assert.That(secondResult.RequestUrl, Is.StringEnding("follow"));
            Assert.That(secondResult.ResponseContent, Is.EqualTo("Followed"));
	    }

        [Test]
        public void Session_GetAndFollow302_ReturnsHtml()
        {
            var url = BaseUrl + "/with-link";
            var firstResult = session.Get(url);
            var secondResult = firstResult.Follow("#mylink302");

            Assert.That(secondResult.RequestUrl, Is.StringEnding("follow"));
            Assert.That(secondResult.ResponseContent, Is.EqualTo("Followed"));
        }

        [Test]
        public void Session_SubmitForm_ReturnsResult()
        {
            var url = BaseUrl + "/page-with-form";
            var form = session.Get(url).FindForm("#form");
            var expectedRequestUrl = form.GetAttribute("action");
            form.SetValue("#input1", "value");
            session.ContentType = "application/x-www-form-urlencoded";
            var result = form.Submit();

            Assert.That(result.RequestUrl, Is.StringEnding(expectedRequestUrl));
            Assert.That(result.ResponseContent, Is.StringContaining("value"));
        }
	}
}
