using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Game.Domain;

namespace Game.Views
{
    public class MainForm : Form
    {
        private readonly Controller controller;
        private int MainFontSize => (Size.Height + Size.Width) / 20;
        private int MediumFontSize => MainFontSize / 2;
        private int LowFontSize => MediumFontSize / 2;

        public MainForm(Controller controller)
        {
            this.controller = controller;
            SetCommonSettings();
        }

        protected override void OnKeyDown(KeyEventArgs e) => controller.OnKeyDown(e);

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            switch (controller.Game.Stage)
            {
                case GameStage.Main:
                    ShowStartScreen(e);
                    break;
                case GameStage.Playing:
                    ShowPlayingScreen(e);
                    break;
                case GameStage.Finished:
                    ShowFinishedScreen(e);
                    break;
            }
        }

        private void ShowStartScreen(PaintEventArgs e)
        {
            DrawText(e, Color.Black, GameInscriptions.GameRules, LowFontSize);
        }

        private void ShowPlayingScreen(PaintEventArgs e)
        {
            DrawText(e, controller.Game.CurrentLevel.Color, controller.Game.CurrentLevel.ColorName, MainFontSize);
            DrawTimeBar(e, controller.Game.Time);
            DrawPoints(e);
        }

        private void ShowFinishedScreen(PaintEventArgs e)
        {
            DrawText(e, Color.Black, GameInscriptions.GameOver, MainFontSize, -120);
            DrawText(e, Color.Black, string.Format(GameInscriptions.YourPoints, controller.Game.Points),
                MediumFontSize);
            DrawText(e, Color.Black, GameInscriptions.Restart, LowFontSize, 120);
        }

        private void DrawText(PaintEventArgs e, Color color, string text, int fontSize, int shiftFromCenter = 0)
        {
            var drawFont = new Font("Arial", fontSize);
            var drawBrush = new SolidBrush(color);
            var drawFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center
            };
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.DrawString(text, drawFont, drawBrush, Width / 2, Height / 2 + shiftFromCenter, drawFormat);
        }

        private void DrawTimeBar(PaintEventArgs e, double percent)
        {
            var timeBar = new Rectangle(0, 20, (int) (Size.Width * percent / 100), 20);
            e.Graphics.FillRectangle(Domain.Brushes.TimeBarBrush, timeBar);
        }

        private void DrawPoints(PaintEventArgs e)
        {
            var font = new Font("Arial", LowFontSize);
            e.Graphics.DrawString($"{controller.Game.Points}", font, Domain.Brushes.PointsBrush, 50,
                Size.Height - 100);
        }

        private void SetCommonSettings()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(888, 500);
        }
    }
}