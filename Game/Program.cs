using System;
using System.Windows.Forms;
using Game.Views;

namespace Game
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var controller = new Controller();
            Application.Run(controller.MainForm);
        }
    }
}
