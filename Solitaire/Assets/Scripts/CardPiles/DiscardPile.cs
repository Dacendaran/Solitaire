using System.Collections.Generic;

public class DiscardPile : InteractableCardPile
{
    private static readonly List<DiscardPile> allDiscardPiles = new List<DiscardPile>();

    public CardType MyCardType { get; private set; } = CardType.None;

    private void Awake()
    {
        allDiscardPiles.Add(this);
    }

    public static bool AllAreFull()
    {
        foreach (DiscardPile discardPile in allDiscardPiles)
            if (discardPile.Cards.Count != 13)
                return false;
        return true;
    }

    public static DiscardPile GetSmallest(out Card missingCard)
    {
        missingCard = null;
        DiscardPile smallestDiscardPile = null;

        foreach (DiscardPile discardPile in allDiscardPiles) {
            Card missingCardOnThisPile = discardPile.GetMissingCard();
            if (missingCard == null || missingCard.value > missingCardOnThisPile.value)
            {
                missingCard = missingCardOnThisPile;
                smallestDiscardPile = discardPile;
            }
        }
        return smallestDiscardPile;
    }

    public static List<CardType> GetMissingCardTypes()
    {
        List<CardType> missingCardTypes = new List<CardType>() { CardType.Clubs, CardType.Diamonds, CardType.Hearts, CardType.Spades };
        foreach (DiscardPile discardPile in allDiscardPiles)
            if (discardPile.MyCardType != CardType.None)
                missingCardTypes.Remove(discardPile.MyCardType);
        return missingCardTypes;
    }

    public Card GetMissingCard()
    {
        if (MyCardType == CardType.None)
            MyCardType = GetMissingCardTypes()[0];

        return GameManager.Instance.FindCard(MyCardType, Cards.Count == 0 ? 1 : TopCard.value + 1);
    }

    public override bool CardCanBePutOnHere(Card cardToPutOn) => Cards.Count == 0 && cardToPutOn.value == 1;

    public override void Add(Card cardToPutOn, bool addStepToHistory = true)
    {
        if (addStepToHistory)
            History.Add(new History.Step(cardToPutOn.cardPile, cardToPutOn));

        MyCardType = cardToPutOn.type;
        boxCollider.enabled = false;
        cardToPutOn.transform.position = transform.position;
        cardToPutOn.transform.parent = transform;

        base.Add(cardToPutOn, addStepToHistory);        
    }

    public override bool CardCanBePutOnCard(Card bottomCard, Card topCard)
    {
        return topCard.type == MyCardType && topCard.value == bottomCard.value + 1 && topCard.transform.childCount == 0;
    }

    public override void Clear()
    {
        base.Clear();
        MyCardType = CardType.None;
        boxCollider.enabled = true;
    }
}
