using System;
using System.IO;
using System.Windows.Forms;

namespace _3ds_Rom_Tools
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!File.Exists("ctrtool.exe"))
            {
                MsgBox.Show("ctrtool.exe not found! Please place it in this folder and restart the program.", "ERROR File Missing!", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                Environment.Exit(0);
            }
            if (!File.Exists("padxorer.exe"))
            {
                MsgBox.Show("padxorer.exe not found! Please place it in this folder and restart the program.", "ERROR File Missing!", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                Environment.Exit(0);
            }
            if (!File.Exists("MEX.py"))
            {
                MsgBox.Show("MEX.py not found! Please place it in this folder and restart the program.", "ERROR File Missing!", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                Environment.Exit(0);
            }
            Application.Run(new MainForm());
        }
    }
}