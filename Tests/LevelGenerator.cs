using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Game.Domain;
using NUnit.Framework;

namespace Tests
{
    public class LevelGenerator
    {
        private readonly Dictionary<Color, string> colorToName = Colors.list.ToDictionary(x => x.color, x => x.name);
        private readonly Dictionary<string, Color> nameToColor = Colors.list.ToDictionary(x => x.name, x => x.color);


        [Test]
        public void Generator_Test()
        {
            var generator = new Game.Domain.LevelGenerator();
            for (var i = 0; i < 1000000; i++)
                Assert.IsTrue(CheckLevel(generator.Generate()));
        }

        private bool CheckLevel(ILevel level)
        {
            return (colorToName[level.Color] == level.ColorName) == level.Answer
                   && (nameToColor[level.ColorName] == level.Color) == level.Answer;
        }
    }
}