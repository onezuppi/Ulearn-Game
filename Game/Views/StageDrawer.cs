using System.Drawing;
using System.Windows.Forms;
using Game.Domain;

namespace Game.Views
{
    public class StageDrawer : Drawer
    {
        private readonly LevelDrawer levelDrawer;

        public StageDrawer(Size size, GameModel game) : base(size, game)
        {
            levelDrawer = new LevelDrawer(size, game);
        }

        public new void SetSize(Size formSize)
        {
            base.SetSize(formSize);
            levelDrawer.SetSize(formSize);
        }

        public void DrawStartScreen(PaintEventArgs e) =>
            DrawText(e, Colors.Information, GameInscriptions.GameRules, FontSize.Smal);

        public void DrawPlayingScreen(PaintEventArgs e) => levelDrawer.Draw(e);

        public void DrawFinishScreen(PaintEventArgs e)
        {
            const int shiftFromCenter = 120;
            var text = string.Format(GameInscriptions.YourPoints, Game.Points);
            DrawText(e, Colors.Information, GameInscriptions.GameOver, FontSize.Large,
                shiftYFromCenter: -shiftFromCenter);
            DrawText(e, Colors.Information, text, FontSize.Medium);
            DrawText(e, Colors.Information, GameInscriptions.Restart, FontSize.Smal, shiftYFromCenter: shiftFromCenter);
        }
    }
}