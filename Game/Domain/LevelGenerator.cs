using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game.Domain
{
    public class LevelGenerator : ILevelGenerator
    {
        private static readonly Type Type = typeof(Levels.LevelType);
        private readonly Random generator = new Random();
        private readonly (string name, Color color)[] colors = Colors.list;
        private readonly string[] allLevelTypes = Enum.GetNames(Type);

        public LevelBase Generate(int maxComplexity = 1)
        {
            maxComplexity = generator.Next(1, maxComplexity);
            var types = allLevelTypes
                .OrderBy(x => generator.Next())
                .Take(maxComplexity)
                .Select(x => (Levels.LevelType)Enum.Parse(Type, x));
            return Generate(types);
        }

        private LevelBase Generate(IEnumerable<Levels.LevelType> levelTypes)
        {
            var index = generator.Next(0, colors.Length);
            if (generator.Next(0, 100) > 50)
                return new LevelBase(colors[index].color, colors[index].name, true, levelTypes);

            var newIndex = -1;
            while (newIndex == -1 || newIndex == index)
                newIndex = generator.Next(0, colors.Length);

            return new LevelBase(colors[index].color, colors[newIndex].name, false, levelTypes);
        }
    }
}