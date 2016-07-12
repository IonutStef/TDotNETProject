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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LectorWF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
