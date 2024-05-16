using System.Collections.ObjectModel;
using System.ComponentModel;
namespace FlashCard;

public class DetailViewModel
{


    private static DetailViewModel Instance;
    public static DetailViewModel GetInstance(DatabaseConfig dbContext)
    {
        if (Instance == null)
        {
            Instance = new DetailViewModel(dbContext);
        }
        return Instance;
    }

    private int index = 0;
    public Deck? _currentDeck;
    public Deck? CurrentDeck
    {
        get { return _currentDeck; }
        set
        {
            if (_currentDeck != value)
            {
                _currentDeck = value;
                OnPropertyChanged(nameof(CurrentDeck));
            }
        }
    }
    public ObservableCollection<Card?> Cards { get; set; } = new ObservableCollection<Card?>();

    private bool _isCardFlipped = false;
    public bool IsCardFlipped
    {
        get { return _isCardFlipped; }
        set
        {
            if (_isCardFlipped != value)
            {
                _isCardFlipped = value;
                OnPropertyChanged(nameof(IsCardFlipped));
            }
        }
    }

    private string _frontValue;

    public string FrontValue
    {
        get { return _frontValue; }
        set
        {
            if (_frontValue != value)
            {
                _frontValue = value;
                OnPropertyChanged(nameof(FrontValue));
            }
        }
    }
    private string _backValue;

    public string BackValue
    {
        get { return _backValue; }
        set
        {
            if (_backValue != value)
            {
                _backValue = value;
                OnPropertyChanged(nameof(BackValue));
            }
        }
    }
    private readonly DatabaseConfig _dbContext;
    public DetailViewModel() { }
    public DetailViewModel(DatabaseConfig dbContext)
    {
        _dbContext = dbContext;
        LoadDecks();

        Console.WriteLine(Cards[0].Front);
        Console.WriteLine(Cards[0].Back);

        FrontValue = Cards[index].Front!;
        BackValue = Cards[index].Back!;

    }
    public void UseDeck(Deck deck)
    {
        CurrentDeck = deck;

        Cards.Clear();
        foreach (var card in deck.Cards)
        {
            Cards.Add(card);
        }

        Console.WriteLine("MASUK");
        Console.WriteLine(Cards.Count);
        if (Cards == null || Cards.Count == 0)
        {
            Console.WriteLine("CARD NULL");
            return;
        }
    }
    public void LoadDecks()
    {
        Cards.Add(new Card { Front = "Card 1 Front", Back = "Card 1 Back" });
        Cards.Add(new Card { Front = "Card 2 Front", Back = "Card 2 Back" });

        // var dbDecks = _dbContext.Decks.ToList();
        // CurrentDeck = dbDecks.FirstOrDefault();
        // Console.WriteLine(CurrentDeck);
        // Console.WriteLine(CurrentDeck.Cards.Count);
        // Console.WriteLine(CurrentDeck.Cards.Count);
        // _frontValue = CurrentDeck.Cards[index].Front;
        // _backValue = CurrentDeck.Cards[index].Back;
    }
    public void FlipCard()
    {
        Console.WriteLine("Update");
        if (IsCardFlipped == false)
        {
            IsCardFlipped = true;
        }
        else
        {
            IsCardFlipped = false;
        }
    }

    public void NextCard()
    {
        index++;

        _frontValue = Cards[index].Front!;
        _backValue = Cards[index].Back!;
    }

    public void PreviousCard()
    {
        index--;

        _frontValue = Cards[index].Front!;
        _backValue = Cards[index].Back!;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
