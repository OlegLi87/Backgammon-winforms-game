using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgammonEngine
{
    public class GameState
    {
        public List<Stack<char>> Triangles { get; internal set; }
        public Stack<char> EatenWhiteCheckers { get; internal set; }
        public Stack<char> EatenBlackCheckers { get; internal set; }
        public char PlayingColor { get; internal set; }
        public char RollingColor { get; internal set; }
        public char WinnerColor { get; internal set; }
        public int[] RolledDices { get; internal set; }
        public string InfoMessage { get; internal set; }

        internal GameState() { }
    }
}
