public interface ICardCanBePutOn
{
    CardPile CardPile { get; }
    bool CardCanBePutOnHere(Card cardToPutOn);
    void Add(Card cardToPutOn, bool addStepToHistory = true);
}
