using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci;
namespace SSHNetFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "hosturl";
            string username = "username";
            string password = "password";
            string pathtokey = @"Path to key";
            PrivateKeyFile file = new PrivateKeyFile(pathtokey, "WellsFargo");
            
            var keyfiles = new[] { file };
            var methods = new List<AuthenticationMethod>();
            methods.Add(new PasswordAuthenticationMethod(username, password));
            methods.Add(new PrivateKeyAuthenticationMethod(username, keyfiles));
            ConnectionInfo con = new ConnectionInfo(host,22, username, methods.ToArray());
            using (SftpClient client = new SftpClient(con))
            {
                client.Connect();
                var files = client.ListDirectory("/");
                foreach (var f in files)
                {
                    Console.WriteLine(f.Name);
                    Console.WriteLine(f.GetHashCode());
                }
                client.Disconnect();
            }
            Console.Read();
        }
    }
}
