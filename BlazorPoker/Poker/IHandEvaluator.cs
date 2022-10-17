namespace BlazorPoker.Poker
{
    public interface IHandEvaluator
    {
        public HandEvaluation GetHandEvaluation(PlayerHand hand);

    }
}
