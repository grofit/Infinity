using System;
using System.Windows.Forms;
using Infinity.Encryption;
using Infinity.Encryption.Xor;
using Infinity.Tools.Configuration;

namespace Infinity.Tools
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

            SetupDependencyInjection();

            Application.Run(new Main());
        }

        
        private static void SetupDependencyInjection()
        {
            DependencyInjectionConfiguration.Instance.Configure();
        }
    }
}
