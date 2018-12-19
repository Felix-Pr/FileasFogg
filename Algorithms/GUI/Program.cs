using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algorithms;

namespace GUI
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            /*Context context = new Context() //Extracted context from Wagner and Whitin paper (1958)
            {
                horizon = 12,
                demand = new int[] { 69, 29, 36, 61, 61, 26, 34, 67, 45, 67, 79, 56 },
                inventoryCosts = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                setupCosts = new int[] { 85, 102, 102, 101, 98, 114, 105, 86, 119, 110, 98, 114 }
            };

            WagnerWithin w = new WagnerWithin(context);
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PhileasFogg());
        }
    }
}
