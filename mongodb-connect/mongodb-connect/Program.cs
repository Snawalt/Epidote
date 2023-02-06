using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mongodb_connect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MongoDBSettings.Connect();
                Console.WriteLine("Connected to MongoDB Database");

                var version = MongoDBSettings.GetVersion();
                Console.WriteLine(mongodb_connect.MongoDBSettings.GetVersion());
            }
            catch(Exception ex)
            {
                Console.WriteLine(  ex.ToString());
            }
            Console.ReadKey();
        }
    }
}
