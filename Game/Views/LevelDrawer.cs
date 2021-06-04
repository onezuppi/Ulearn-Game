using System.Drawing;
using System.Windows.Forms;
using Game.Domain;

namespace Game.Views
{
    public class LevelDrawer : Drawer
    {
        private LevelProperties levelProperties;

        public LevelDrawer(Size size, GameModel game) : base(size, game)
        {
            levelProperties = new LevelProperties(Game.CurrentLevel);
        }

        public void Draw(PaintEventArgs e)
        {
            e.Graphics.Clear(levelProperties.Background);
            if (levelProperties.Level != Game.CurrentLevel)
                levelProperties = new LevelProperties(Game.CurrentLevel);
            else
                levelProperties.CalculateDynamicLevels();
            DrawTimeBar(e);
            DrawPoints(e);
            DrawTask(e);
        }

        private void DrawTimeBar(PaintEventArgs e)
        {
            const int timeBarWidth = 20;
            const int shiftFromTop = 20;
            var timeBar = new Rectangle(0, timeBarWidth, (int) (Size.Width * Game.Time / 100), shiftFromTop);
            e.Graphics.FillRectangle(levelProperties.TimeBarBrush, timeBar);
        }

        private void DrawPoints(PaintEventArgs e)
        {
            const int leftShift = 30;
            var font = new Font(Settings.FontFamilyName, FontSize.Smal);
            var topShift = Size.Height - leftShift * 3;
            e.Graphics.DrawString($"{Game.Points}", font, levelProperties.PointsBrush, leftShift, topShift);
        }

        private void DrawTask(PaintEventArgs e)
        {
            var fontSize = (int) (FontSize.Large * levelProperties.SizeRatio);
            var shiftX = (int) (levelProperties.ShiftRatio.X * Size.Width);
            var shiftY = (int) (levelProperties.ShiftRatio.Y * Size.Height);
            DrawText(e, levelProperties.TaskColor, levelProperties.Task, fontSize, shiftX, shiftY,
                levelProperties.TaskStringFormat, levelProperties.TaskFontStyle);
        }
    }
}