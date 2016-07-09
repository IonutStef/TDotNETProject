using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ProjectClassLibrary;

namespace ConsoleClientLectorTest
{
    class Client
    {
        static void Main(string[] args)
        {
            //Step 1: Create an endpoint address and an instance of the WCF Client.
            ServerLectorClient client = new ServerLectorClient();

            //client.DeleteAll();

            //client.AddAll();

            //client.TestAddSubject();
            //client.PrintSubjects();
            int val = client.AddOpp(2, 5);
            Console.WriteLine(val);

            //List<Subject> subj = client.GetSubjects("Chapters").ToList();
            //foreach (Subject sub in subj)
            //{
            //    foreach (Chapter chap in sub.Chapters)
            //    {
            //        Console.WriteLine(chap.Title);
            //    }
            //}



            List<Question> questions = client.GetQuestions().ToList();
            foreach (Question quest in questions)
            {
                Console.WriteLine(quest.Requirement);
            }


            //Step 3: Closing the client gracefully closes the connection and cleans up resources.
            client.Close();

            Console.WriteLine((10e-6) == (1e-5));
            //Console.WriteLine((double)(1e-7));

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to terminate client.");
            Console.ReadLine();
        }
    }
}
