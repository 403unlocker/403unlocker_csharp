using System;
using System.Text.RegularExpressions;

namespace _403unlockerLibrary
{
    public class Website
    {
        private string name;
        private string url;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string URL
        {
            get => url;
            set
            {
                if (!IsValidUrl(value))
                {
                    throw new UriFormatException();
                }

                url = value;
            }
        }

        public static bool IsValidUrl(string hostname)
        {
            if (Regex.IsMatch(hostname, @"^(www.)?([^\W_]{1}[a-zA-Z\d\-]*){1}(\.[^\W_]{1}[a-zA-Z\d\-]*){0,60}(\.[a-z]+){1}(/[\w\W]*)*$")) return true;
            return false;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Website)
            {
                return url == (obj as Website).url;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return url.GetHashCode();
        }
    }
}
