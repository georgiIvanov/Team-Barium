using System;

namespace Balloons_Pops_game
{
    public struct ScoreEntry : IComparable<ScoreEntry>
    {
        public int Score;
        public string Name;

        public ScoreEntry(int value, string name)
        {
            this.Score = value;
            this.Name = name;
        }

        public int CompareTo(ScoreEntry other)
        {
            int result = Score.CompareTo(other.Score);
            return result;
        }
    }
}
