using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsData.Strings
{
    internal class AsString
    {
        private readonly string _text;

        public string Key { get => _text; }

        public AsString(string text)
        {
            _text = text;
        }

        public static implicit operator AsString(string text)
        {
            return new AsString(text);
        }

        public static implicit operator string(AsString text)
        {
            return text.ToString();
        }

        public override string ToString()
        {
            return AsStringManager.Get(_text) ?? AsStrings.Test.Empty;
        }
    }
}
