using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class Calculator
    {
        public Player total = new Player();

        public string CalculateHand(Player theflop, Player player)
        {
            var Rank = "";
            
            // Put all hands into total
            for (int i = 0; i < 5; i++)
                total.Hands.Add(theflop.Hands[i]);
            total.Hands.Add(player.Hands[0]);
            total.Hands.Add(player.Hands[1]);

            total.Hands[0].number = 13;
            total.Hands[1].number = 10;
            total.Hands[2].number = 10;
            total.Hands[3].number = 3;
            total.Hands[4].number = 9;
            total.Hands[5].number = 11;
            total.Hands[6].number = 12;
            total.Hands[0].shape = "diamond";
            total.Hands[1].shape = "diamond";
            total.Hands[2].shape = "club";
            total.Hands[3].shape = "diamond";
            total.Hands[4].shape = "diamond";
            total.Hands[5].shape = "diamond";
            total.Hands[6].shape = "diamond";
            
            var straight = CountSequenceofNumbers(total.Hands, 1, 5);
            var straightflush = CountSequenceofShapes(straight.UsedHand, 0, 5);
            var fours = CountSequenceofNumbers(total.Hands, 0, 4);
            var threes = CountSequenceofNumbers(total.Hands, 0, 3);
            var pair = CountSequenceofNumbers(total.Hands, 0, 2);
            var flush = CountSequenceofShapes(total.Hands, 0, 5);
            var twopairs = MergeHands(pair.UsedHand, CountSequenceofNumbers(pair.UnusedHand, 0, 2).UsedHand);
            var fullhouse = MergeHands(threes.UsedHand, CountSequenceofNumbers(threes.UnusedHand, 0, 2).UsedHand);

            var RankList = new List<Player> { straightflush, fours, fullhouse, flush, straight, threes, twopairs, pair };
            var RankName = new List<String> { "Straight Flush", "Four of  Kind", "Full House", "Flush", "Straight", "Threes", "Two Pairs", "A Pair" };


            for (var i = 0; i < RankList.Count; i++)
            {
                if (RankList[i].UsedHand.Count > 0)
                {
                    Rank = RankName[i];
                    break;
                }

            }
            


            return Rank;    
        }
        

        public int[] CardListToNumberArray(List<Card> Hand)
        {
            var array = new int[Hand.Count];

            for (var i = 0; i < Hand.Count(); i++)
                array[i] = Hand[i].number;

            return array;
        }

        public int[] CardListToShapeArray(List<Card> Hand)
        {
            var array = new int[Hand.Count];

            for (var i = 0; i < Hand.Count(); i++)
                array[i] = (int) Enum.Parse(typeof(Shape), Hand[i].shape);

            return array;
        }


        public Player CountSequenceofNumbers(List<Card> Hand, int delta, int sequence)
        {
            var player = new Player();
            
            Hand = Hand.OrderBy(x => x.number).ToList();

            int[] array = CardListToNumberArray(Hand);

            Hand = CountSequence(Hand, array, delta, sequence);

            if (Hand.FindAll(x => x.used == true).Count >= sequence)
            {
                for (var i = 0; i < Hand.Count(); i++)
                {
                    if (Hand[i].used == true)
                    {
                        player.UsedHand.Add(Hand[i]);
                    }
                    else
                        player.UnusedHand.Add(Hand[i]);

                }
            }
            return player;
            
        }

        public Player CountSequenceofShapes(List<Card> Hand, int delta, int sequence)
        {
            var player = new Player();

            Hand = Hand.OrderBy(x => x.shape).ToList();

            int[] array = CardListToShapeArray(Hand);

            Hand = CountSequence(Hand, array, delta, sequence);

            if (Hand.FindAll(x => x.used == true).Count >= sequence)
            {
                for (var i = 0; i < Hand.Count(); i++)
                {
                    if (Hand[i].used == true)
                    {
                        player.UsedHand.Add(Hand[i]);
                    }
                    else
                        player.UnusedHand.Add(Hand[i]);

                }
            }
            return player;

        }

        public List<Card> CountSequence(List<Card> Hand, int[] array, int delta, int sequence)
        {

            for (var i = 0; i < Hand.Count(); i++)
                Hand[i].used = false; //do a reset
            
            for (var i = 0; i < array.Count() - 1; i++)
            {
                if (array[i+1] - array[i] <= delta)
                {
                    Hand[i].used = true;
                }
            }

            for (var i = Hand.Count() - 1; i >= 0; i--)
            {
                if (Hand[i].used)
                {
                    Hand[i + 1].used = true;
                    break;
                }
            }

            return Hand;

        }

        public Player MergeHands(List<Card> Hand1, List<Card> Hand2)
        {
            var player = new Player();
            
            for (var i = 0; i < Hand1.Count(); i++)
            {
                player.UsedHand.Add(Hand1[i]);
            }
            
            for (var i = 0; i < Hand2.Count(); i++)
            {
                player.UsedHand.Add(Hand2[i]);
            }
            return player;
        }
    
    
    
    }

}
