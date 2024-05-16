using System.Collections.ObjectModel;
using System.ComponentModel;
namespace FlashCard;

public class CreateCardViewModel : INotifyPropertyChanged
{

    private static CreateCardViewModel Instance;
    public static CreateCardViewModel GetInstance(DatabaseConfig dbContext)
    {
        if (Instance == null)
        {
            Instance = new CreateCardViewModel(dbContext);
        }
        return Instance;
    }

    public ObservableCollection<Card?> Cards { get; set; } = new ObservableCollection<Card?>();


    private string _labelValue;

    public string LabelValue
    {
        get { return _labelValue; }
        set
        {
            if (_labelValue != value)
            {
                _labelValue = value;
                OnPropertyChanged(nameof(LabelValue));
            }
        }
    }
    private string _descriptionValue;

    public string DescriptionValue
    {
        get { return _descriptionValue; }
        set
        {
            if (_descriptionValue != value)
            {
                _descriptionValue = value;
                OnPropertyChanged(nameof(DescriptionValue));
            }
        }
    }

    private string _frontValue;

    public string Frontvalue
    {
        get { return _frontValue; }
        set
        {
            if (_frontValue != value)
            {
                _frontValue = value;
                OnPropertyChanged(nameof(Frontvalue));
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
    public CreateCardViewModel() { }
    public CreateCardViewModel(DatabaseConfig dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddCard()
    {
        var card = new Card()
        {
            Front = this._frontValue,
            Back = this._backValue,
        };
        Cards.Add(card);
    }
    public void SaveDeck()
    {
        if (this.Cards.Count == 0)
        {
            return;
        }

        _dbContext.Decks.Add(new Deck()
        {
            Label = this._labelValue,
            Description = this._descriptionValue,
            Cards = this.Cards
        });
        _dbContext.SaveChanges();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
