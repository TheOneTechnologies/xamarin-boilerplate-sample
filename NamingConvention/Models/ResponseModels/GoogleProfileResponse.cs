using System;
using System.Collections.Generic;

namespace NamingConvention.Models.ResponseModels
{
    public class Name
    {
        public string familyName { get; set; }
        public string givenName { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
    }

    public class Email
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class GoogleProfileResponse
    {
        public string id { get; set; }
        public Name name { get; set; }
        public string displayName { get; set; }
        public Image image { get; set; }
        public List<Email> emails { get; set; }
        public string nickname { get; set; }
        public string language { get; set; }
        public string kind { get; set; }
        public string etag { get; set; }
    }
}
