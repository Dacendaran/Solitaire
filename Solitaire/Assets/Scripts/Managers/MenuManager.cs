using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button undoButton;
    public Button giveNewCardsButton;

    public void UndoLastStep()
    {
        if (!GameManager.Instance.IsAutoCompleting)
            History.UndoLastStep();
    }

    public void ToggleUndoButton() => undoButton.interactable = !undoButton.interactable;

    public void ToggleGiveNewCardsButton() => giveNewCardsButton.interactable = !giveNewCardsButton.interactable;
}
