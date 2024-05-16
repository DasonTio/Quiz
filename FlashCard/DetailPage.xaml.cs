namespace FlashCard;

public partial class DetailPage : ContentPage
{

	public bool IsMAUIVisible { get; set; } = false;
	private DetailViewModel _detailViewModel { get; set; }
	// private readonly DatabaseConfig _dbContext;
	public DetailPage(DatabaseConfig dbContext)
	{
		InitializeComponent();
		_detailViewModel = DetailViewModel.GetInstance(dbContext);
		BindingContext = _detailViewModel;
	}
	private void OnFlipButtonClicked(object sender, EventArgs e)
	{
		_detailViewModel.IsCardFlipped = true;
	}

	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///MainPage");
	}
}

