namespace FlashCard;

public partial class CreateCardPage : ContentPage
{
	private CreateCardViewModel _createCardViewModel { get; set; }
	private readonly DatabaseConfig _dbContext;
	private Deck _templateDeck { get; set; }
	public CreateCardPage(DatabaseConfig dbContext)
	{
		InitializeComponent();
		_dbContext = dbContext;
		_createCardViewModel = CreateCardViewModel.GetInstance(dbContext);
		BindingContext = _createCardViewModel;

		// Cloning 
		Deck _templateDeck = new Deck();
		_templateDeck.Label = "My Deck";
		_templateDeck.Description = "Description of my deck";
		_templateDeck.Cards.Add(new Card { Front = "Front 1", Back = "Back 1" });
		_templateDeck.Cards.Add(new Card { Front = "Front 2", Back = "Back 2" });
	}

	private void OnFrontTextChanged(object sender, TextChangedEventArgs e)
	{
		if (BindingContext is CreateCardViewModel viewModel)
		{
			viewModel.Frontvalue = e.NewTextValue;
		}
	}
	private void OnBackTextChanged(object sender, TextChangedEventArgs e)
	{
		if (BindingContext is CreateCardViewModel viewModel)
		{
			viewModel.BackValue = e.NewTextValue;
		}
	}
	private void OnLabelTextChanged(object sender, TextChangedEventArgs e)
	{
		if (BindingContext is CreateCardViewModel viewModel)
		{
			viewModel.LabelValue = e.NewTextValue;
		}
	}
	private void OnDescriptionTextChanged(object sender, TextChangedEventArgs e)
	{
		if (BindingContext is CreateCardViewModel viewModel)
		{
			viewModel.DescriptionValue = e.NewTextValue;
		}
	}
	private void OnAddButtonClicked(object sender, EventArgs e)
	{
		_createCardViewModel.AddCard();
	}

	private async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		_createCardViewModel.SaveDeck();
		await Shell.Current.GoToAsync("///MainPage");
	}

	private void OnDeepCloneClicked(object sender, EventArgs e)
	{
		Deck deepClone = (Deck)_templateDeck.DeepClone();

		_createCardViewModel.LabelValue = deepClone.Label;
		foreach (var card in _templateDeck.Cards)
		{
			_createCardViewModel.Cards.Add(card);
		}
		_templateDeck.Label = "Modified Label";
		_templateDeck.Cards.First().Front = "Modified Front";
	}

	private void OnShallowCloneClicked(object sender, EventArgs e)
	{
		Deck shallowClone = (Deck)_templateDeck.Clone();

	}

	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///MainPage");
	}
	private async void OnHelpButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///HelpCloningPage");
	}
}
