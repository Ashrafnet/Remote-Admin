using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace RemoteAdmin
{
    class clsPing
    {
       static public   bool Ping(string IPs)
        {
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1;
                PingReply reply = pingSender.Send(IPs, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {                    
                    return true;
                }                
                return false;
            }
            catch (Exception er)
            {                
                return false;
            }
        }
    }
}
