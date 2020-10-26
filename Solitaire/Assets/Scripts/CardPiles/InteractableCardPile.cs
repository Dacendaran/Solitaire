using UnityEngine;

public abstract class InteractableCardPile : CardPile, ICardCanBePutOn
{
    protected BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public abstract bool CardCanBePutOnCard(Card bottomCard, Card topCard);

    public abstract bool CardCanBePutOnHere(Card cardToPutOn);

    public override void Remove(Card cardToRemove)
    {
        base.Remove(cardToRemove);
        if (Cards.Count == 0)
            boxCollider.enabled = true;
    }
}
