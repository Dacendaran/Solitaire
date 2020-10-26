public interface ICardCanBePutOn
{
    bool CardCanBePutOnHere(Card cardToPutOn);
    void Add(Card cardToPutOn, bool addStepToHistory = true);
}
