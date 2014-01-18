using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

            using System;
using System.Drawing;
using System.IO;

namespace VisualME7Logger
{
    static class Program
    {
        public static string ME7LoggerDirectory;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ME7LoggerDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            if (args.Length > 0)
            {
                ME7LoggerDirectory = args[0];
            }


 /*
string fileName, newFileName;
fileName = @"C:\Users\steve\Desktop\VW\ECUTuning\ME7Logger\logs\graphCapture8999af62-98b5-46bf-b691-d6129826f178.jpg";
newFileName = Path.ChangeExtension(fileName, ".ico");
using (Bitmap bitmap = Image.FromFile(fileName, true) as Bitmap)
{
    using (Icon icon = Icon.FromHandle(bitmap.GetHicon()))
    {
        using (Stream imageFile = File.Create(newFileName))
        {
            icon.Save(imageFile);
            Console.WriteLine("Converted - {0}", newFileName);
        }
    }
}
            */


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SettingsForm());
            //Application.Run(new Form2());

        }
    }
}
