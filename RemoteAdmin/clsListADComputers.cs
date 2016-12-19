using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Collections;
namespace RemoteAdmin
{
    class ListADComputers
    {
        public static List<string> GetComputers()
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + Conf.Default.DomainName + "" );
                DirectorySearcher mySearcher = new DirectorySearcher(entry);
                mySearcher.Filter = ("(objectClass=computer)");

                List<string> CompName = new List<string>();
                string xx = "";
                foreach (SearchResult resEnt in mySearcher.FindAll())
                {
                    xx = resEnt.GetDirectoryEntry().Name.ToString().Substring(3);
                    
                   CompName.Add(xx);

                }
                entry.Close(); mySearcher.Dispose();
                return CompName;
            }
            catch(Exception er)
            {

                throw er;
                //return null;
            }
        }
    }
    
}
