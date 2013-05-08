using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balloons_Pops_game
{
    public struct ScoreEntry : IComparable<ScoreEntry>
    {
        public int Score;
        public string Name;

        public ScoreEntry(int value, string name)
        {
            Score = value;
            Name = name;
        }

        public int CompareTo(ScoreEntry other)
        {
            return Score.CompareTo(other.Score);
        }
    }
}
