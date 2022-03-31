using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();
            var player = new Player();
            var theflop = new Player();
   
            deck.Shuffle();

            player.Hands.Add(deck.TakeACard());
            player.Hands.Add(deck.TakeACard());
            for (int i = 0; i < 5; i++)
                theflop.Hands.Add(deck.TakeACard());

            var calculator = new Calculator();
            var rank = calculator.CalculateHand(theflop, player);

            Console.WriteLine(rank);

        }


    }
}
