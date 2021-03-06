﻿using System.Collections.Generic;
using Nancy.Security;

namespace IntegrationTests.Nancy
{
    public class BasicUserIdentity : IUserIdentity
    {
        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}
