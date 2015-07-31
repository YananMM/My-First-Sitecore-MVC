using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landmark.Models
{
    public class TextValue
    {
        public string text;
        public string value;
        public TextValue(string _text, string _value)
        {
            text = _text;
            value = _value;
        }
        public List<TextValue> children { get; set; }
    }
}