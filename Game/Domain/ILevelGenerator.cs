namespace Game.Domain
{
    public interface ILevelGenerator
    {
        LevelBase Generate(int complexity);
    }
}