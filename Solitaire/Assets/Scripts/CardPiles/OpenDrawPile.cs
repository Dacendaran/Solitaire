using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawPile : CardPile
{
    public ClosedDrawPile closedDrawPile;

    public void Add(List<Card> cards)
    {
        foreach(Card card in cards)
        {
            card.transform.parent = transform;
            card.ShowSide(CardSide.Front);
            base.Add(card, false);
        }

        SetCardPositions();
        SetOnlyTopCardDragable();
    }

    public override void Add(Card cardToAdd, bool addStepToHistory = true)
    {
        if (addStepToHistory)
            History.Add(new History.Step(cardToAdd.cardPile, cardToAdd));

        cardToAdd.transform.parent = transform;
        cardToAdd.ShowSide(CardSide.Front);
        base.Add(cardToAdd, addStepToHistory);

        SetCardPositions();
        SetOnlyTopCardDragable();

        cardToAdd.dragable = false;
        StartCoroutine(SetDragableAfterOneFrame(cardToAdd));        
    }

    private void SetCardPositions()
    {
        int offsetMultiplicator = Cards.Count > 2 ? 2 : Cards.Count - 1;
        for(int i = Cards.Count - 1; i >= 0; i--)
        {
            Card card = Cards[i];
            if(offsetMultiplicator > 0)
            {
                card.transform.position = transform.position + Vector3.right * offsetMultiplicator * GameManager.Instance.cardOffset;
                offsetMultiplicator--;
            }
            else
                card.transform.position = transform.position;
        }
    }

    public void AddAllBackToClosedDrawPile()
    {
        Cards.Reverse();
        closedDrawPile.AddCards(Cards, true);
        Cards.Clear();
    }

    public override void Remove(Card cardToRemove)
    {
        base.Remove(cardToRemove);
        SetCardPositions();
        SetOnlyTopCardDragable();
    }

    private void SetOnlyTopCardDragable()
    {
        for (int i = 0; i < Cards.Count; i++)
            Cards[i].dragable = i == Cards.Count - 1;
    }

    private IEnumerator SetDragableAfterOneFrame(Card card)
    {
        yield return null;
        card.dragable = true;
    }
}
