using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CardColor { Red, Black }
public enum CardType { None, Spades, Hearts, Diamonds, Clubs }
public enum CardSide { Front, Back }

public class Card : MonoBehaviour, ICardCanBePutOn
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Sprite sprite;
    private Vector3 positionOnStartDrag;  

    public int value;
    public CardType type;
    public CardSide CurrentlyShowingSide { get; private set; } = CardSide.Front;
    public CardColor Color { get; private set; }
    public int SortingOrder { get { return spriteRenderer.sortingOrder; } set { spriteRenderer.sortingOrder = value; } }
    public CardPile CardPile { get; set; }

    private void Awake()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = spriteRenderer.sprite;
        Color = type == CardType.Hearts || type == CardType.Diamonds ? CardColor.Red : CardColor.Black;
    }

    public void ShowSide(CardSide side)
    {
        spriteRenderer.sprite = side == CardSide.Front ? sprite : GameManager.Instance.cardBack;
        CurrentlyShowingSide = side;
    }

    public bool CardCanBePutOnHere(Card cardToPutOn)
    {
        if(CardPile is InteractableCardPile)
        {
            InteractableCardPile interactableCardPile = CardPile as InteractableCardPile;
            return interactableCardPile.CardCanBePutOnCard(this, cardToPutOn);
        }
        return false;
    }

    public bool IsDragable => CardPile.CardIsDragable(this);

    public void OnBeginDrag()
    {
        positionOnStartDrag = transform.position;
        foreach (Card card in GetComponentsInChildren<Card>())
            card.SortingOrder += 100;
    }

    public void OnDrag(Vector2 delta)
    {
        transform.Translate(delta);
    }

    public void OnEndDrag()
    {
        List<Collider2D> overlappingColliders = new List<Collider2D>();
        boxCollider.OverlapCollider(new ContactFilter2D(), overlappingColliders);

        if (overlappingColliders.ContainsComponent(out Dictionary<Collider2D, ICardCanBePutOn> validTargets))
        {
            for (int i = validTargets.Count - 1; i >= 0; i--)
            {
                KeyValuePair<Collider2D, ICardCanBePutOn> pair = validTargets.ElementAt(i);
                if (!pair.Value.CardCanBePutOnHere(this))
                    validTargets.Remove(pair.Key);
            }
        }

        if (validTargets.Count == 0)
        {
            transform.position = positionOnStartDrag;
            foreach (Card card in GetComponentsInChildren<Card>())
                card.SortingOrder -= 100;
            return;
        }

        Move(validTargets.GetClosest(transform.position).Value.CardPile, true, true, true);
    }

    public void Move(CardPile newCardPile, bool checkForAutoComplete, bool addStepToHistory, bool increaseActionCounter)
    {        
        CardPile.Remove(this);
        newCardPile.Add(this, addStepToHistory);
        if (checkForAutoComplete)
            GameManager.Instance.CheckIfFinishedOrReadyForAutoComplete();
        if (increaseActionCounter)
            GameManager.Instance.ActionCounter++;
    }

    public void Add(Card cardToPutOn, bool addStepToHistory = true)
    {        
        CardPile.Add(cardToPutOn, true);
    }
}
