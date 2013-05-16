// ********************************
// <copyright file="ScoreEntry.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
using System;

namespace Balloons_Pops_game
{
    /// <summary>
    /// Keeps the score of a player.
    /// </summary>
    public struct ScoreEntry : IComparable<ScoreEntry>
    {
        /// <summary>
        /// Represents the score of the player.
        /// </summary>
        public int Score;

        /// <summary>
        /// Represents the name of the player.
        /// </summary>
        public string Name;

        /// <summary>
        /// Initializes a new instance of the ScoreEntry struct.
        /// </summary>
        /// <param name="value">The score of the player.</param>
        /// <param name="name">The name of the player.</param>
        public ScoreEntry(int value, string name)
        {
            this.Score = value;
            this.Name = name;
        }

        /// <summary>
        /// Compares two players by their score.
        /// </summary>
        /// <param name="other">The second player to compare.</param>
        /// <returns>Returns 0 if the scores of two players are equal, -1 if the score of this player is lower or 1 if it is higher than the score of the other player.</returns>
        public int CompareTo(ScoreEntry other)
        {
            int result = Score.CompareTo(other.Score);
            return result;
        }
    }
}
