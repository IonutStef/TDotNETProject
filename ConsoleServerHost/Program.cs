/// <summary> 
/// Nume si prenume: Dinu Ionut Stefan
/// Laborator: Vineri 16-18
/// Tema laborator: Proiect 2
/// Data predare proiect: 15.04.2016 
/// Declaratie: Declar pe propria raspundere ca acest proiect nu a fost copiat din alte surse 
/// Bibliografie, surse de inspiratie: MSDN, http://..., http://blog.staticvoid.co.nz/2012/7/17/entity_framework-navigation_property_basics_with_code_first  http://www.codeproject.com/Articles/491844/A-Beginners-Guide-to-Duplex-WCF  stackoverflow /// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using ProjectClassLibrary;

namespace ConsoleServerHost
{
    class WCFServer
    {
        static void Main(string[] args)
        {
            HelloWorld hw = new HelloWorld();
            hw.sayHelloWorld();
            //ServerLector srv = new ServerLector();
            //srv.TestAddSubject();
            //srv.PrintSubjects();
            ServiceHost myService =
            new ServiceHost(typeof(ProjectClassLibrary.ServerLector),
            new
            Uri("http://localhost:9191/ProjectClassLibrary/Lector/"));
            ServiceDescription serviceDesciption = myService.Description;
            // enumerare endpoint-uir din serviciu - informativ
            foreach (ServiceEndpoint endpoint in serviceDesciption.Endpoints)
            {
                ConsoleColor oldColour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Endpoint - address: {0}",
                endpoint.Address);
                Console.WriteLine(" - binding name:\t\t{0}",
                endpoint.Binding.Name);
                Console.WriteLine(" - contract name:\t\t{0}",
                endpoint.Contract.Name);
                Console.WriteLine();
                Console.ForegroundColor = oldColour;
            }
            myService.Open();
            Console.WriteLine("Press enter to stop.");
            Console.ReadKey();
            if (myService.State != CommunicationState.Closed)
                myService.Close();
        }
    }
}
