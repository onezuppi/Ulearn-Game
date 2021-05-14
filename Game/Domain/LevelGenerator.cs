using System;
using System.Drawing;

namespace Game.Domain
{
    public class LevelGenerator: ILevelGenerator
    {
        private readonly Random generator = new Random();
        private readonly (string name, Color color)[] colors = Colors.list;
        
        public Level Generate()
        {
            var index = generator.Next(0, colors.Length);
            if (generator.Next(0, 100) > 50)
                return new Level(colors[index].color, colors[index].name, true);

            var newIndex = -1;
            while (newIndex == -1 || newIndex == index)
                newIndex = generator.Next(0, colors.Length);
            
            return new Level(colors[index].color, colors[newIndex].name, false);
        }
    }
}