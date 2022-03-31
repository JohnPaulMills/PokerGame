using System;
using System.Collections.Generic;

namespace PokerGame
{
    public class Deck
    {
        private List<Card> deck;
        private static Random rng = new Random();

        public Deck()
        {
            deck = new List<Card>();
            var shapes = new string[] {"club", "diamond", "heart", "spade"};
            for (int i = 1; i <= 13; i++)
            {
                foreach(string shape in shapes)
                {
                    deck.Add(new Card() { number = i, shape = shape});
                }
            }
        }

        public void Shuffle()
        {
            var n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }

        public Card TakeACard()
        {
            var card = deck[0];
            deck.RemoveAt(0);
            return card;
        }



    }
}
