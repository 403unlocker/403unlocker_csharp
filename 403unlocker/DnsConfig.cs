using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace _403unlocker
{
    internal class DnsConfig
    {
        private string provider = "", dns = "";
        public string Provider { get => provider; set => provider = value; }
        public string DNS { get => dns; set => dns = value; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Provider)) return "";
            return Provider;
        }

        public override bool Equals(object obj)
        {
            if (obj is DnsConfig)
            {
                return dns == (obj as DnsConfig).dns;
            }
            return false;
        }
    }
}
