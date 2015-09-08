using MetroFramework;
using MetroFramework.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace _3ds_Rom_Tools
{
    public partial class MainForm : MetroForm
    {
        //==========================================================================================
        
        [BrowsableAttribute(false)]

        string space = " ";
        public string s { get; set; }
        public string SafeFileName { get; set; }
        
        string curDir = Directory.GetCurrentDirectory();
        
        OpenFileDialog ofd = new OpenFileDialog();
        OpenFileDialog ofd2 = new OpenFileDialog();
        OpenFileDialog ofd3 = new OpenFileDialog();
        OpenFileDialog ofd4 = new OpenFileDialog();
        OpenFileDialog ofd5 = new OpenFileDialog();
        OpenFileDialog ofd6 = new OpenFileDialog();

        //==========================================================================================

        string exheader = "exheader.bin";
        string exefs = "exefs.bin";
        string romfs = "romfs.bin";

        string exheadert = "exheader.txt";
        string exefsd = "exefs";
        string romfsd = "romfs";
        
        //==========================================================================================

        public MainForm()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            metroStyleManager1.Style = MetroColorStyle.Teal;

            //Disable extract and unpack buttons until game is opened.
            metroButton2.Enabled = false;
            metroButton3.Enabled = false;
            metroButton4.Enabled = false;
            metroButton5.Enabled = false;
            metroButton6.Enabled = false;
            metroButton7.Enabled = false;

            //Disable Xor buttons until xorpads are opened.
            metroButton15.Enabled = false;
            metroButton16.Enabled = false;
            metroButton17.Enabled = false;
            metroButton18.Enabled = false;
        }

        //==========================================================================================
        
        //Switch Theme Checkbox
        private void metroCheckBox1_CheckStateChanged(object sender, EventArgs e)
        {
            this.metroStyleManager1.Theme = metroStyleManager1.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
            if (metroStyleManager1.Theme == MetroThemeStyle.Dark)
            {
                Properties.Settings.Default.Theme = "Dark";
                Properties.Settings.Default.Save();
                richTextBox1.Text = "Theme changed to Dark";
            }
            else if (metroStyleManager1.Theme == MetroThemeStyle.Light)
            {
                Properties.Settings.Default.Theme = "Light";
                Properties.Settings.Default.Save();
                richTextBox1.Text = "Theme changed to Light";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            switch (Properties.Settings.Default.Theme)
            {
                case "Dark":
                    this.metroStyleManager1.Theme = Properties.Settings.Default.ThemeDark;
                    break;

                case "Light":
                    this.metroStyleManager1.Theme = Properties.Settings.Default.ThemeLight;
                    break;

                default:
                    break;
            }
        }

        //==========================================================================================

        //Exit Button
        private void metroButton10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Help Button
        private void metroButton8_Click(object sender, EventArgs e)
        {
            HelpBox help = new HelpBox();
            help.StyleManager = this.StyleManager;
            help.ShowDialog();
            help.Dispose();
        }

        //==========================================================================================

        //Console output text scroll to bottom
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        //==========================================================================================

        //Rom command
        public void runCommand()
        {
            Process proc;
            proc = new Process();
            proc.StartInfo.FileName = @"cmd";
            
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.Arguments = "/C" + richTextBox1.Text;

            proc.Start();
            
            string s = proc.StandardOutput.ReadToEnd();
            richTextBox1.Text = s;
            
            proc.WaitForExit();
            proc.Close();
        }

        //Xorpad command
        public void runXor()
        {
            Process proc;
            proc = new Process();
            proc.StartInfo.FileName = @"cmd";

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;

            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.Arguments = "/C" + richTextBox2.Text;

            proc.StartInfo.RedirectStandardInput = true;

            proc.Start();
            string s = proc.StandardOutput.ReadToEnd();
            richTextBox2.Text = s;

            proc.WaitForExit();
            proc.Close();
        }

        //==========================================================================================

        //EXTRACT BUTTONS
        //=======================

        //Extract Exheader Button
        private void metroButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "ctrtool -p --exheader=exheader.bin" + space + "\"" + ofd.FileName + "\"";
            runCommand();
            MsgBox.Show("Exheader extraction complete!", "Exheader Extraction", MsgBox.Buttons.OK);
            if (File.Exists(exheader))
            {
                //Enable unpack button only if exheader.bin exists
                metroButton5.Enabled = true;
            }
        }

        //Extract ExeFS Button
        private void metroButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "ctrtool -p --exefs=exefs.bin" + space + "\"" + ofd.FileName + "\"";
            runCommand();
            MsgBox.Show("ExeFS extraction complete!", "ExeFS Extraction", MsgBox.Buttons.OK);
            if (File.Exists(exefs))
            {
                //Enable unpack button only if exefs.bin exists
                metroButton6.Enabled = true;
            }
        }

        //Extract RomFS Button
        private void metroButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "ctrtool -p --romfs=romfs.bin" + space + "\"" + ofd.FileName + "\"";
            runCommand();
            MsgBox.Show("RomFS extraction complete!", "RomFS Extraction", MsgBox.Buttons.OK);
            if (File.Exists(romfs))
            {
                //Enable unpack button only if romfs.bin exists
                metroButton7.Enabled = true;
            }
        }

        //==========================================================================================

        //UNPACK BUTTONS
        //=======================

        //Unpack Exheader Button
        private void metroButton5_Click(object sender, EventArgs e)
        {
            string path = Path.GetFileNameWithoutExtension(metroTextBox1.Text);
            richTextBox1.Text = "ctrtool -t exheader exheader.bin >exheader.txt";
            runCommand();
            if (File.Exists(exheader) && File.Exists(exheadert))
            {
                File.Move(curDir + "\\exheader.bin", path + "\\decrypted" + "\\exheader.bin");
                File.Move(curDir + "\\exheader.txt", path + "\\unpacked" + "\\exheader.txt");
                richTextBox1.AppendText("\nMoved" + space + exheader + space + "from" + space + curDir + "\nto" + space + path + space + "\\decrypted" + space + exheader);
                richTextBox1.AppendText("\nMoved" + space + exheadert + space + "from" + space + curDir + "\nto" + space + path + space + "\\unpacked" + space + exheadert);
            }
            MsgBox.Show("Exheader unpack complete! Check the `exheader.txt`.", "Exheader Unpack", MsgBox.Buttons.OK);
        }

        //Unpack ExeFS Button
        private void metroButton6_Click(object sender, EventArgs e)
        {
            string path = Path.GetFileNameWithoutExtension(metroTextBox1.Text);
            richTextBox1.Text = "ctrtool -t exefs --exefsdir exefs exefs.bin";
            runCommand();
            if (File.Exists(exefs) && Directory.Exists(exefsd))
            {
                File.Move(curDir + "\\exefs.bin", path + "\\decrypted" + "\\exefs.bin");
                Directory.Move(curDir + "\\exefs", path + "\\unpacked" + "\\exefs");
                richTextBox1.AppendText("\nMoved" + space + exefs + space + "from" + space + curDir + "\nto" + space + path + space + "\\decrypted" + space + exefs);
                richTextBox1.AppendText("\nMoved" + space + exefsd + space + "from" + space + curDir + "\nto" + space + path + space + "\\unpacked" + space + exefsd);
            }
            MsgBox.Show("ExeFS unpack complete! Check the `exefs` folder.", "ExeFS Unpack", MsgBox.Buttons.OK);
        }

        //Unpack RomFS Button
        private void metroButton7_Click(object sender, EventArgs e)
        {
            string path = Path.GetFileNameWithoutExtension(metroTextBox1.Text);
            richTextBox1.Text = "ctrtool -t romfs --romfsdir romfs romfs.bin";
            runCommand();
            if (File.Exists(romfs) && Directory.Exists(romfsd))
            {

                File.Move(curDir + "\\romfs.bin", path + "\\decrypted" + "\\romfs.bin");
                Directory.Move(curDir + "\\romfs", path + "\\unpacked" + "\\romfs");
                richTextBox1.AppendText("\nMoved" + space + romfs + space + "from" + space + curDir + "\nto" + space + path + space + "\\decrypted" + space + romfs);
                richTextBox1.AppendText("\nMoved" + space + romfsd + space + "from" + space + curDir + "\nto" + space + path + space + "\\unpacked" + space + romfsd);
            }
            MsgBox.Show("RomFS unpack complete! Check the `romfs` folder.", "RomFS Unpack", MsgBox.Buttons.OK);
        }

        //==========================================================================================

        //OPEN FILE DIALOGS
        //=================

        //Browse Game Button
        private void OpenFile(object sender, EventArgs e)
        {
            ofd.Filter = "Rom Files|*.3DS;*.3DZ;*.3ds;*.3dz;*.cci;*.csu|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                metroTextBox1.Text = ofd.SafeFileName;
                richTextBox1.AppendText(space + ofd.FileName);

                //Enable all the extract buttons once a game has been opened.
                metroButton2.Enabled = true;
                metroButton3.Enabled = true;
                metroButton4.Enabled = true;
            }
            //Create the output directory based on the game file name (minus extension).
            //Then create the encrypted, decrypted and unpacked sub-folders.
            string path = Path.GetFileNameWithoutExtension(metroTextBox1.Text);
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(Path.Combine(path, "unpacked"));
            Directory.CreateDirectory(Path.Combine(path, "encrypted"));
            Directory.CreateDirectory(Path.Combine(path, "decrypted"));
        }

        //exefs norm xorpad
        private void OpenFile2(object sender, EventArgs e)
        {
            ofd2.Filter = "ExeFS|*Main.exefs_norm.xorpad;|All files (*.*)|*.*";

            if (ofd2.ShowDialog() == DialogResult.OK)
            {
                metroTextBox2.Text = ofd2.SafeFileName;
                richTextBox2.AppendText(space + ofd2.FileName);
            }
        }

        //exefs 7x xorpad
        private void OpenFile3(object sender, EventArgs e)
        {
            ofd3.Filter = "ExeFS7x|*Main.exefs_7x.xorpad;|All files (*.*)|*.*";

            if (ofd3.ShowDialog() == DialogResult.OK)
            {
                metroTextBox3.Text = ofd3.SafeFileName;
                richTextBox2.AppendText(space + ofd3.FileName);

                //enable Merge ExeFS Button once exefs_norm and exefs_7x xorpads have been opened.
                metroButton15.Enabled = true;
            }
        }
        
        //exheader xorpad
        private void OpenFile4(object sender, EventArgs e)
        {
            ofd4.Filter = "Exheader|*Main.exheader.xorpad;|All files (*.*)|*.*";

            if (ofd4.ShowDialog() == DialogResult.OK)
            {
                metroTextBox4.Text = ofd4.SafeFileName;
                richTextBox2.AppendText(space + ofd4.FileName);

                //enable Xor Exheader Button once exheader xorpad has been opened.
                metroButton16.Enabled = true;
            }
        }

        //romfs xorpad
        private void OpenFile5(object sender, EventArgs e)
        {
            ofd5.Filter = "RomFS|*Main.romfs.xorpad;|All files (*.*)|*.*";

            if (ofd5.ShowDialog() == DialogResult.OK)
            {
                metroTextBox5.Text = ofd5.SafeFileName;
                richTextBox2.AppendText(space + ofd5.FileName);

                //enable Xor RomFS Button once romfs xorpad has been opened.
                metroButton17.Enabled = true;
            }
        }

        //exefs normal (non 7x game) or the combined norm and 7x xorpad
        private void OpenFile6(object sender, EventArgs e)
        {
            ofd6.Filter = "ExeFS|*Main.exefs.xorpad;|All files (*.*)|*.*";

            if (ofd6.ShowDialog() == DialogResult.OK)
            {
                metroTextBox6.Text = ofd6.SafeFileName;
                richTextBox2.AppendText(space + ofd6.FileName);

                //enable Xor ExeFS Button once exefs xorpad has been opened.
                metroButton18.Enabled = true;
            }
        }

        //==========================================================================================

        //XOR BUTTONS
        //============

        //Merge ExeFS Xorpads (norm + 7x into one exefs.xorpad) Button
        private void metroButton15_Click(object sender, EventArgs e)
        {
            string exefsna = ofd2.SafeFileName;
            richTextBox2.Text = "MEX" + space + exefs + space + "\"" + ofd2.SafeFileName + "\"" + space + "\"" + ofd3.SafeFileName + "\"" + space + exefsna.Substring(0, 16) + ".Main." + "exefs.xorpad";
            runXor();
            MsgBox.Show("exefs_norm.xorpad and exefs_7x.xorpad\nhave been merged into exefs.xorpad", "ExeFS Xorpad Merge Complete!", MsgBox.Buttons.OK);
        }

        //Xor Exheader
        private void metroButton16_Click(object sender, EventArgs e)
        {
            string path = Path.GetFileNameWithoutExtension(metroTextBox1.Text);
            richTextBox2.Text = "padxorer" + space + exheader + space + "\"" + ofd4.FileName + "\"";
            runXor();
            if (File.Exists (exheader) && File.Exists ("exheader.bin.out"))
            {
                //Move encrypted exheader.bin to "Game Folder"/encrypted.
                File.Move(curDir + "\\exheader.bin", path + "\\encrypted" + "\\exheader.bin");

                //Move decrypted exheader.bin to "Game Folder"/decrypted and delete the leftover file in main folder.
                File.Copy("exheader.bin.out", exheader);
                File.Move(curDir + "\\exheader.bin", path + "\\decrypted" + "\\exheader.bin");
                File.Delete("exheader.bin.out");
            }
            MsgBox.Show("Exheader.bin decrypted and moved to 'Game Folder'/decrypted", "Exheader Decryption Complete!", MsgBox.Buttons.OK);
        }

        //Xor RomFS
        private void metroButton17_Click(object sender, EventArgs e)
        {
            string path = Path.GetFileNameWithoutExtension(metroTextBox1.Text);
            richTextBox2.Text = "padxorer" + space + romfs + space + "\"" + ofd5.FileName + "\"";
            runXor();
            if (File.Exists(romfs) && File.Exists("romfs.bin.out"))
            {
                //Move encrypted romfs.bin to "Game Folder"/encrypted.
                File.Move(curDir + "\\romfs.bin", path + "\\encrypted" + "\\romfs.bin");

                //Move decrypted romfs.bin to "Game Folder"/decrypted and delete the leftover file in main folder.
                File.Copy("romfs.bin.out", romfs);
                File.Move(curDir + "\\romfs.bin", path + "\\decrypted" + "\\romfs.bin");
                File.Delete("romfs.bin.out");
            }
            MsgBox.Show("RomFS.bin decrypted and moved to 'Game Folder'/decrypted", "RomFS Decryption Complete!", MsgBox.Buttons.OK);
        }

        //Xor ExeFS
        private void metroButton18_Click(object sender, EventArgs e)
        {
            string path = Path.GetFileNameWithoutExtension(metroTextBox1.Text);
            richTextBox2.Text = "padxorer" + space + exefs + space + "\"" + ofd6.FileName + "\"";
            runXor();
            if (File.Exists(exefs) && File.Exists("exefs.bin.out"))
            {
                //Move encrypted exefs.bin to "Game Folder"/encrypted.
                File.Move(curDir + "\\exefs.bin", path + "\\encrypted" + "\\exefs.bin");

                //Move decrypted exefs.bin to "Game Folder"/decrypted and delete the leftover file in main folder.
                File.Copy("exefs.bin.out", exefs);
                File.Move(curDir + "\\exefs.bin", path + "\\decrypted" + "\\exefs.bin");
                File.Delete("exefs.bin.out");
            }
            MsgBox.Show("ExeFS.bin decrypted and moved to 'Game Folder'/decrypted", "ExeFS Decryption Complete!", MsgBox.Buttons.OK);
        }

        //==========================================================================================
    }
}