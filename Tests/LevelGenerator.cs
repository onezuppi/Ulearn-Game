using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Game.Domain;
using NUnit.Framework;

namespace Tests
{
    public class LevelGenerator
    {
        private const int IterationCount = 1000;

        private static readonly Type type = typeof(Levels.LevelType);

        private readonly Dictionary<Color, string> colorToName = Colors.list.ToDictionary(x => x.color, x => x.name);

        private readonly Dictionary<string, Color> nameToColor = Colors.list.ToDictionary(x => x.name, x => x.color);

        private readonly Game.Domain.LevelGenerator generator = new Game.Domain.LevelGenerator();

        private readonly Levels.LevelType[] levelTypes = Enum.GetNames(typeof(Levels.LevelType))
            .Select(x => (Levels.LevelType) Enum.Parse(type, x)).ToArray();


        [Test]
        public void Generator_Test()
        {
            for (var i = 0; i < IterationCount * IterationCount; i++)
                Assert.IsTrue(CheckLevel(generator.Generate()));
        }

        [Test]
        public void GeneratorType_Test()
        {
            for (var maxComplexity = 1; maxComplexity < levelTypes.Length; maxComplexity++)
            for (var i = 0; i < IterationCount; i++)
            {
                var level = generator.Generate(maxComplexity);
                var contains = levelTypes.Count(x => level.Is(x));
                Assert.IsTrue(maxComplexity >= contains);
            }
        }

        private bool CheckLevel(LevelBase level)
        {
            return (colorToName[level.Color] == level.ColorName) == level.Answer
                   && (nameToColor[level.ColorName] == level.Color) == level.Answer;
        }
    }
}