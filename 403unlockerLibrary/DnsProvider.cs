using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlockerLibrary
{
    public class DnsProvider
    {
        private string name = "";
        private string dns = "";

        public string Name
        {
            get => name;
            set => name = value;
        }
        
        public string DNS
        {
            get => dns;
            set => dns = value;
        }

        public static bool IsIPv4(string dns)
        {
            var octets = dns.Split(new char[] { '.' });
            if (octets.Length == 4)
            {
                // converts octets string to int
                bool isOctetsValid = octets.Select(x => int.Parse(x))
                                     // checks are all between 0 to 255
                                     .All(x => 0 <= x && x <= 255); ;
                return isOctetsValid;
            }
            return false;
        }

        public async static Task<List<DnsProvider>> DnsScrapAsync()
        {
            //https://www.getflix.com.au/setup/dns-servers/
            try
            {
                var htmlDocument = await NetworkUtility.HttpRequest("https://publicdns.xyz");
                // get DNS table
                var table = htmlDocument.DocumentNode.SelectSingleNode("//table");

                // get rows of table
                var rows = table.SelectNodes(".//tr");

                // data preprocessing rows
                var customizedRows = rows.Select(row => row.ChildNodes.Where(cell => cell.Name != "#text"));

                // removes second row (IPv6)
                customizedRows = customizedRows.Where(x => x.Count() == 3);

                // removes table title
                customizedRows = customizedRows.Skip(1);

                // removes non-letter in cells e.g. \n \t
                var minedDns = customizedRows.Select(row =>
                                                     row.Select(
                                                         cell =>
                                                         string.Concat(
                                                                       cell.InnerText.Where(character => !char.IsControl(character))
                                                                       )
                                                               )
                                                     );

                // convert it to usable list for app
                var dnsList = minedDns.SelectMany(dnsConfig => new DnsProvider[]
                {
                    new DnsProvider()
                    {
                        Name = dnsConfig.ElementAt(0),
                        // ensures IPv6 is removed
                        DNS = IsIPv4(dnsConfig.ElementAt(1)) ? dnsConfig.ElementAt(1) : ""
                    },
                    new DnsProvider()
                    {
                        Name = dnsConfig.ElementAt(0),
                        // ensures IPv6 is removed
                        DNS = IsIPv4(dnsConfig.ElementAt(2)) ? dnsConfig.ElementAt(2) : ""
                    }
                })
                // removes empty DNS
                .Where(dnsConfig => !string.IsNullOrEmpty(dnsConfig.DNS)).ToList();

                return dnsList;
            }
            catch (HttpRequestException error)
            {
                string errorText = error.Message;
                string errorCaption = "Access Denied! - Server Blocked us";

                if (error.InnerException != null)
                {
                    errorText = error.InnerException.Message;
                    errorCaption = "Access Denied! - There is No Internet 🌐";
                }
                MessageBox.Show(errorText, errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TaskCanceledException error)
            {
                MessageBox.Show(error.Message, "Request Timeout!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Something Went Wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is DnsProvider)
            {
                return dns == (obj as DnsProvider).dns;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return dns.GetHashCode();
        }
    }
}
