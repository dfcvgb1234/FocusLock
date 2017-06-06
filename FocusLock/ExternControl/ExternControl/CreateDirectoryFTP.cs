using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExternControl
{
    class CreateDirectoryFTP
    {
        public CreateDirectoryFTP(string oldFolder, string newDirectory, bool isFirstDirectory)
        {

            if (!isFirstDirectory)
            {
                Console.WriteLine(string.Format("ftp://ftp.focuslock.dk/ftp/{0}/{1}", oldFolder, newDirectory));
                WebRequest request = WebRequest.Create(string.Format("ftp://ftp.focuslock.dk/ftp/{0}/{1}", oldFolder, newDirectory));
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
            }
            else
            {
                Console.WriteLine(string.Format("ftp://ftp.focuslock.dk/ftp/{0}", newDirectory));
                WebRequest request = WebRequest.Create(string.Format("ftp://ftp.focuslock.dk/ftp/{0}", newDirectory));
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
            }
        }
    }
}
