using System.Drawing;

namespace Game.Domain
{
    public class Level: ILevel
    {
        public Color Color { get; }
        public string ColorName { get; }
        public bool Answer { get; }

        public Level(Color color, string colorName, bool answer)
        {
            Color = color;
            ColorName = colorName;
            Answer = answer;
        }
    }
}