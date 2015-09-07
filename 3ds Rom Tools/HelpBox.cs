using MetroFramework.Forms;
using System;

namespace _3ds_Rom_Tools
{
    public partial class HelpBox : MetroForm
    {
        public HelpBox()
        {
            InitializeComponent();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //metroLabel1 help text
/*
        For decrypted games decrypted with Decrypt 9's 'Rom Decryption'
        option, simply press 'browse' and select your game file.
        Then press the extract buttons for exheader, exefs and romfs.

        Then press the 'Unpack' button for each once to unpack the contents
        of each file which will be moved to the 'Game Folder'/unpacked.

        ========================================

        For encrypted games, press 'browse' and select your game file.
        Then press the extract buttons for exheader, exefs and romfs.

        Then go to the 'Xorpad options' tab, press the button next to each
        text box and open the xorpad for each one.

        Then press the 'Xor' button for each one. If the game has both _norm
        and _7x exefs xorpads then they will need to be merged first.

        With all the xorpads already open, just press the 'Merge exefs' button
        then open that in the last xorpad slot.

        Then you'll be able to xor the exefs.bin for games with both xorpads.

        After that go back to the 'Rom Options' tab and press the 'unpack' buttons.

        And done! Everything extract both ways. :)
*/
    }
}
