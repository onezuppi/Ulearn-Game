using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Game.Domain;

namespace Game.Views
{
    public class Drawer
    {
        private protected Size Size;
        protected readonly GameModel Game;
        protected (int Large, int Medium, int Smal) FontSize;

        protected Drawer(Size size, GameModel game)
        {
            Game = game;
            SetSize(size);
        }

        public void SetSize(Size formSize)
        {
            Size = formSize;
            FontSize.Large = (formSize.Height + formSize.Width) / 20;
            FontSize.Medium = FontSize.Large / 2;
            FontSize.Smal = FontSize.Medium / 2;
        }

        protected void DrawText(PaintEventArgs e, Color color, string text, int fontSize, int shiftXFromCenter = 0,
            int shiftYFromCenter = 0,
            StringFormat stringFormat = null, FontStyle fontStyle = FontStyle.Regular)
        {
            var drawFont = new Font(Settings.FontFamilyName, fontSize, fontStyle);
            var brush = new SolidBrush(color);
            var widthCenter = Size.Width / 2 + shiftXFromCenter;
            var heightCenter = Size.Height / 2 + shiftYFromCenter;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.DrawString(text, drawFont, brush, widthCenter, heightCenter,
                stringFormat ?? new StringFormat
                    {LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center});
        }
    }
}