using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class Card
    {
        public int number;
        public string shape;
        public bool used;
    }

    public enum Shape
    {
        club = 0, 
        diamond = 1,
        heart = 2,
        spade = 3
    }
}
