using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FlashCard;

public partial class MainPage : ContentPage
{
	private DeckViewModel _deckViewModel { get; set; }
	private DetailViewModel _detailViewModel { get; set; }
	public MainPage(DatabaseConfig dbContext)
	{
		InitializeComponent();
		_deckViewModel = DeckViewModel.GetInstance(dbContext);
		_detailViewModel = DetailViewModel.GetInstance(dbContext);

		BindingContext = _deckViewModel;

		dateLabel.Text = GetCurrentDateString();
		deckLabel.Text = GetTotalDeckString();
	}

	private string GetCurrentDateString()
	{
		return DateTime.Now.ToString("dd MMMM yyyy");
	}

	private string GetTotalDeckString()
	{
		return "Decks: " + _deckViewModel.Decks.Count.ToString();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_deckViewModel.LoadDecks();
	}

	private async void OnAdditionButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///CreateCardPage");
	}

	private async void OnStudyButtonClicked(object sender, EventArgs e)
	{
		if (_deckViewModel.PrioritizedDeck == null)
		{
			return;
		}
		// _detailViewModel.UseDeck(_deckViewModel.GetPrioritizedDeck());
		await Shell.Current.GoToAsync("///DetailPage");
	}
}
