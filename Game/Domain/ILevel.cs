using System.Drawing;

namespace Game.Domain
{
    public interface ILevel
    {
        Color Color { get; }
        string ColorName { get; }
        bool Answer { get; }
    }
}