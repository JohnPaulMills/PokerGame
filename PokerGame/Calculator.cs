using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class CalculateHand2
    {
        public Player total = new Player();

        public CalculateHand2(Player theflop, Player player)
        {
            // Put all hands into total
            for (int i = 0; i < 5; i++)
                total.Hands.Add(theflop.Hands[i]);
            total.Hands.Add(player.Hands[0]);
            total.Hands.Add(player.Hands[1]);

            total.Hands = total.Hands.OrderByDescending(x => x.number).ThenBy(x => x.shape).ToList();

            Predicate<int> four = delegate (int a) { return a == 4; };
            Predicate<int> three = delegate (int a) { return a == 3; };
            Predicate<int> pair = delegate (int a) { return a == 2; };
            Predicate<int> single = delegate (int a) { return a == 1; };
            Predicate<Card> diamond = delegate (Card a) { return a.shape == "diamond"; };
            Predicate<Card> heart = delegate (Card a) { return a.shape == "heart"; };
            Predicate<Card> spade = delegate (Card a) { return a.shape == "spade"; };
            Predicate<Card> club = delegate (Card a) { return a.shape == "club"; };

            var diamonds = total.Hands.FindAll(diamond);
            var hearts = total.Hands.FindAll(heart);
            var spades = total.Hands.FindAll(spade);
            var clubs = total.Hands.FindAll(club);
            var straight = CountSequence(total.Hands, 5, 1);
            var fours = CountSequence(total.Hands, 4, 0);
            var threes = CountSequence(total.Hands, 3, 0);
            var pairs = CountSequence(total.Hands, 2, 0);
            var secondpairs = CountSequence(total.Hands, 2, 0);

            var rank = "";
            if (diamonds.Count != 5 || hearts.Count != 5 || spades.Count != 5 || clubs.Count != 5)
                if (straight.Count != 0)
                    if (straight[0].number == 13)
                        rank = "Kings Flush";
                    else
                        rank = "Straight Flush";
                else
                    rank = "Flush";
            if (straight.Count != 0)
                rank = "Straight";
            if (fours.Count != 0)
                rank = "Four of a kind";
            if (threes.Count != 0)
                if (pairs.Count != 0)
                    rank = "Full House";
                else
                    rank = "Three of a kind";
            if (pairs.Count != 0)
                if (secondpairs.Count != 0)
                    rank = "Two pairs";
                else
                    rank = "A pair";

            Console.WriteLine(rank);

        }

        public List<Card> CountSequence(List<Card> cards, int sequence, int delta)
        {
            var hand = new List<Card>();

            for (int i = 1; i < cards.Count; i++)
            {
                if (cards[i - 1].number - cards[i].number == delta && cards[i].used == false)
                    hand.Add(cards[i]);
                else if (cards[i - 1].number - cards[i].number > delta && hand.Count < sequence)
                    hand.Clear();
            }

            if (hand.Count == sequence-1)
            {
                for (int i = 0; i < sequence; i++)
                    total.Hands[i].used = true;
                hand.Add(cards[cards.Count -1 - sequence]);
            }

            return hand;
        }
    }

}


/* 
 * 0 - Royal Flush
 * 1 - Straight Flush (5 cards in sequence, same suits)
 * 2 ------ Four of a kind
 * 3 ------ Full House (Three kind & pair)
 * 4 ------ Flush (5 cards same suits)
 * 5 ------ Straight (5 cards in sequence)
 * 6 ------ Three of a kind
 * 7 ------ 2 pairs
 * 8 ------ Pair
 * 9 - High card
 */
