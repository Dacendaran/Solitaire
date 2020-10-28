using System.Collections.Generic;
using System.Linq;

public static class History
{
    public class Step
    {        
        public CardPile oldCardPile;
        public Card card;
        public Card flippedCard;
        public List<Card> cards;

        public Step(CardPile _oldCardPile, Card _card)
        {
            oldCardPile = _oldCardPile;
            card = _card;
        }

        public Step(CardPile _oldCardPile, List<Card> _cards)
        {
            oldCardPile = _oldCardPile;
            cards = new List<Card>();
            cards.AddRange(_cards);
        }

        public void Undo()
        {
            if(card != null)
            {
                card.cardPile.Remove(card);
                oldCardPile.Add(card, false);
                if (flippedCard != null)
                    flippedCard.ShowSide(flippedCard.CurrentlyShowingSide == CardSide.Back ? CardSide.Front : CardSide.Back);
            }
            else
            {
                OpenDrawPile openDrawPile = oldCardPile as OpenDrawPile;
                foreach (Card card in cards)
                    card.cardPile.Remove(card);
                cards.Reverse();
                openDrawPile.Add(cards);
            }
        }
    }

    private static readonly Stack<Step> history = new Stack<Step>();

    public static Card flippedCard = null;

    public static void Add(Step step)
    {
        if(flippedCard != null)
        {
            step.flippedCard = flippedCard;
            flippedCard = null;
        }
        history.Push(step);
    }

    public static void Reset()
    {
        history.Clear();
        flippedCard = null;
    }

    public static void UndoLastStep()
    {
        if (history.Count == 0)
            return;

        GameManager.Instance.IncreaseActionCounter();
        Step stepToUndo = history.Pop();
        stepToUndo.Undo();
    }
}
