using System.Collections.Generic;
using UnityEngine;
using Dacen.ExtensionMethods.Generic;
using Dacen.ExtensionMethods.General;
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
    private int sortingOrderOnStartDrag;
    public bool dragable = true;

    public int value;
    public CardType type;
    [HideInInspector] public CardPile cardPile;
    public CardSide CurrentlyShowingSide { get; private set; } = CardSide.Front;
    public CardColor Color { get; private set; }
    public int SortingOrder { get { return spriteRenderer.sortingOrder; } set { spriteRenderer.sortingOrder = value; } }   

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
        if(cardPile is InteractableCardPile)
        {
            InteractableCardPile interactableCardPile = cardPile as InteractableCardPile;
            return interactableCardPile.CardCanBePutOnCard(this, cardToPutOn);
        }
        return false;
    }

    public bool IsDragable => dragable && CurrentlyShowingSide == CardSide.Front;

    public void OnStartDrag(int sortingOrder = 100)
    {
        positionOnStartDrag = transform.position;
        sortingOrderOnStartDrag = SortingOrder;
        SortingOrder = sortingOrder;

        if (transform.TryGetComponentsInChildrenExcludingSelf(out Card[] childCards))
            for(int i = 0; i < childCards.Length; i++)
                childCards[i].OnStartDrag(SortingOrder + 1 + i);
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

        if(validTargets.Count == 0)
        {
            ResetPosition();
            return;
        }

        Move(validTargets.GetClosest((Vector2)transform.position).Value, true);
    }

    public void Move(ICardCanBePutOn newContainer, bool checkForAutoComplete)
    {
        GameManager.Instance.IncreaseActionCounter();
        cardPile.Remove(this);
        newContainer.Add(this);
        if (checkForAutoComplete)
            GameManager.Instance.CheckIfFinishedOrReadyForAutoComplete();
    }

    public void ResetPosition()
    {
        transform.position = positionOnStartDrag;
        SortingOrder = sortingOrderOnStartDrag;

        if (transform.TryGetComponentsInChildrenExcludingSelf(out Card[] childCards))
            foreach(Card childCard in childCards)
                childCard.ResetPosition();
    }

    public void Add(Card cardToPutOn, bool addStepToHistory = true)
    {        
        cardPile.Add(cardToPutOn, true);
    }
}
