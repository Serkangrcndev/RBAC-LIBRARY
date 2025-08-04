using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seker_kutuphane
{
    public class IconTextBox : TextBox
    {
        private PictureBox iconPictureBox;

        public IconTextBox()
        {
            iconPictureBox = new PictureBox();
            iconPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            iconPictureBox.BackColor = Color.Transparent;
            iconPictureBox.Size = new Size(20, 20);
            this.Controls.Add(iconPictureBox);
        }

        public Image Icon
        {
            get { return iconPictureBox.Image; }
            set { iconPictureBox.Image = value; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            iconPictureBox.Location = new Point(5, (this.ClientSize.Height - iconPictureBox.Height) / 2);
            this.Padding = new Padding(iconPictureBox.Width + 10, this.Padding.Top, this.Padding.Right, this.Padding.Bottom);
        }
    }
} 