using UnityEngine;

public class ClosedDrawPileClickTarget : MonoBehaviour
{
    public ClosedDrawPile closedDrawPile;

    private void OnMouseDown()
    {
        closedDrawPile.OnClick();
    }
}
