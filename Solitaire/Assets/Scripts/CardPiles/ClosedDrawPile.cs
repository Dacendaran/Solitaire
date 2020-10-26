using System.Collections.Generic;
using UnityEngine;

public class ClosedDrawPile : CardPile
{
    public Transform discardArea;
    public OpenDrawPile openDrawPile;

    public void AddCards(List<Card> cardsToAdd, bool addStepToHistory)
    {
        if (addStepToHistory)
            History.Add(new History.Step(cardsToAdd[0].cardPile, cardsToAdd));

        foreach(Card card in cardsToAdd)
            Add(card, false);
    }

    public override void Add(Card cardToAdd, bool addStepToHistory = true)
    {
        if (addStepToHistory)
            History.Add(new History.Step(cardToAdd.cardPile, cardToAdd));

        cardToAdd.ShowSide(CardSide.Back);
        cardToAdd.transform.position = transform.position;
        cardToAdd.transform.parent = transform;
        base.Add(cardToAdd, addStepToHistory);
    }

    public void OnClick()
    {
        GameManager.Instance.IncreaseActionCounter();

        if (Cards.Count > 0)
        {
            Card cardToDiscard = Cards[Cards.Count - 1];
            Remove(cardToDiscard);
            openDrawPile.Add(cardToDiscard);
        }
        else
        {
            openDrawPile.AddAllBackToClosedDrawPile();
        }
    }
}
