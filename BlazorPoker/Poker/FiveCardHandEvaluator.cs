using System.Linq;

namespace BlazorPoker.Poker
{
    public class FiveCardHandEvaluator : IHandEvaluator
    {
        public HandEvaluation GetHandEvaluation(PlayerHand hand)
        {

            // royal flush - ace high straight flush
            if (IsAceHighStraight(hand) && IsFlush(hand))
            {
                return HandEvaluation.RoyalFlush;
            }

            //straight flush - 5 in a row, same suit
            if (IsStraight(hand) && IsFlush(hand))
            {
                return HandEvaluation.StraightFlush;
            }

            var faceGroupedHand = hand.GroupBy(x => x.Face).ToList();

            // Four of a Kind - four of same face
            if (IsFourOfAKind(faceGroupedHand))
            {
                return HandEvaluation.FourOfAKind;
            }

            // full house - three of a kind and a pair
            if (IsFullHouse(faceGroupedHand))
            {
                return HandEvaluation.FullHouse;
            }

            // flush - same suit
            if (IsFlush(hand))
            {
                return HandEvaluation.Flush;
            }

            // straight - 5 in a row
            if (IsStraight(hand))
            {
                return HandEvaluation.Straight;
            }

            // three of a kind
            if (IsThreeOfAKind(faceGroupedHand))
            {
                return HandEvaluation.ThreeOfAKind;
            }

            // two pair
            if (IsTwoPair(faceGroupedHand))
            {
                return HandEvaluation.TwoPair;
            }

            // pair
            if (IsPair(faceGroupedHand))
            {
                return HandEvaluation.Pair;
            }

            // high card is default if nothing better
            return HandEvaluation.HighCard;
        }

        private bool IsPair(List<IGrouping<CardFace, Card>> faceGroupedHand)
        {
            return faceGroupedHand.Any(x => x.Count() == 2);
        }

        private bool IsTwoPair(List<IGrouping<CardFace, Card>> faceGroupedHand)
        {
            return faceGroupedHand.Count(x => x.Count() == 2) == 2;
        }

        private bool IsThreeOfAKind(List<IGrouping<CardFace, Card>> faceGroupedHand)
        {
            return faceGroupedHand.Any(x => x.Count() == 3);
        }

        private bool IsFullHouse(List<IGrouping<CardFace, Card>> faceGroupedHand)
        {
            return IsThreeOfAKind(faceGroupedHand) && IsPair(faceGroupedHand);
        }

        private bool IsFourOfAKind(List<IGrouping<CardFace, Card>> faceGroupedHand)
        {
            return faceGroupedHand.Any(x => x.Count() == 4);
        }

        private bool IsAceHighStraight(PlayerHand hand)
        {
            if (hand.Any(x => x.Face == CardFace.ace) && 
                hand.Any(x => x.Face == CardFace.king) &&
                hand.Any(x => x.Face == CardFace.queen) &&
                hand.Any(x => x.Face == CardFace.jack) &&
                hand.Any(x => x.Face == CardFace.ten))
            {
                return true;
            }

            return false;
        }

        private bool IsStraight(PlayerHand hand)
        {
            if (IsAceHighStraight(hand))
                return true;

            var orderedHand = hand.OrderBy(x => x.Face).ToList();
            for (int i = 1; i < orderedHand.Count; i++)
            {
                if (orderedHand[i].Face - orderedHand[i - 1].Face != 1)
                    return false;
            }
            return true;
        }

        private bool IsFlush(PlayerHand hand)
        {
            return hand.All(x => x.Suit == hand.First().Suit);
        }

    }
}
