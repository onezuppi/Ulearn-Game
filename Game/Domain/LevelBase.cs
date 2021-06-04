using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game.Domain
{
    public class LevelBase
    {
        public Color Color { get; }
        public string ColorName { get; }
        public bool Answer { get; }

        private readonly HashSet<Levels.LevelType> types;

        public LevelBase(Color color, string colorName, bool answer, IEnumerable<Levels.LevelType> types)
        {
            Color = color;
            ColorName = colorName;
            Answer = answer;
            this.types = types.ToHashSet();
        }

        public bool Is(Levels.LevelType type) => types.Contains(type);

        public int Reward => (int) (1.5 * types.Select(x => Levels.Rewards[x]).Average());
    }
}