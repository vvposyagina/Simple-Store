using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Linq;

namespace BasketHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(BasketService.Basket)))
            {
                host.Open();
                Console.WriteLine("Host is started...");               
                Console.ReadLine();
            }
        }        
    }
}
