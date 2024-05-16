using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace FlashCard;

public class DeckViewModel : INotifyPropertyChanged
{
    private static DeckViewModel Instance;
    public static DeckViewModel GetInstance(DatabaseConfig dbContext)
    {
        if (Instance == null)
        {
            Instance = new DeckViewModel(dbContext);
        }
        return Instance;
    }
    public ICommand ItemClickedCommand { get; }

    public ObservableCollection<Deck?> Decks { get; set; } = new ObservableCollection<Deck?>();
    public Deck? _prioritizedDeck;
    public Deck? PrioritizedDeck
    {
        get { return _prioritizedDeck; }
        set
        {
            _prioritizedDeck = value;
            OnPropertyChanged(nameof(PrioritizedDeck));
        }
    }

    public bool? _hasPrioritizedDeck;
    public bool? HasPrioritizedDeck
    {
        get { return PrioritizedDeck != null; }
        set
        {
            _hasPrioritizedDeck = value;
            OnPropertyChanged(nameof(HasPrioritizedDeck));
        }
    }
    private readonly DatabaseConfig _dbContext;
    public DeckViewModel()
    {
        ItemClickedCommand = new Command<Deck>(OnItemClicked);
    }
    public DeckViewModel(DatabaseConfig dbContext)
    {
        ItemClickedCommand = new Command<Deck>(OnItemClicked);
        _dbContext = dbContext;
        LoadDecks();
        _prioritizedDeck = GetPrioritizedDeck();
    }

    public Deck? GetPrioritizedDeck()
    {
        return Decks.OrderBy(deck => deck?.OpenedAt).FirstOrDefault();
    }

    public void LoadDecks()
    {
        Console.WriteLine("UPDATE DECK UI");
        Decks.Clear();
        var dbDecks = _dbContext.Decks.ToList();
        foreach (var deck in dbDecks)
        {
            Decks.Add(deck);
        }
        PrioritizedDeck = GetPrioritizedDeck();
        Console.WriteLine("PRIORITIZE");
        Console.WriteLine(_prioritizedDeck);
    }

    public void AddDeck(Deck deck)
    {
        _dbContext.Decks.Add(deck);
        _dbContext.SaveChanges();
    }

    public async void OnItemClicked(Deck deck)
    {
        Console.WriteLine("Click");
        Console.WriteLine(deck);
        // DetailViewModel.GetInstance(_dbContext).UseDeck(deck);
        // await Shell.Current.GoToAsync("///DetailPage");
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}
