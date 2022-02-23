using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Helpers
{
    public class IpHelper
    {
        //Obtener la ip de donde se hagan ciertas peticiones que es necesario rastrear
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            
            foreach(var ip in host.AddressList)
            {

                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {

                    return ip.ToString();
                }
               


            }

            return string.Empty;

        }


    }
}
