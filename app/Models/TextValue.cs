using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landmark.Models
{
    public class TextValue
    {
        public string text { get; set; }

        public string value { get; set; }

        public string DisplayName { get; set; }

        public List<TextValue> children { get; set; }

    }
}