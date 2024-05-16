using System.Text;

namespace FlashCard;
public partial class HelpCloningPage : ContentPage
{
	private Deck originalDeck;
	private Deck shallowClone;
	private Deck deepClone;

	public HelpCloningPage()
	{
		InitializeComponent();
	}

	private void OnCloneButtonClicked(object sender, EventArgs e)
	{
		originalDeck = new Deck();
		originalDeck.Label = labelEntry.Text;
		originalDeck.Description = descriptionEntry.Text;

		// Add some example cards
		originalDeck.Cards.Add(new Card { Front = "Front 1", Back = "Back 1" });
		originalDeck.Cards.Add(new Card { Front = "Front 2", Back = "Back 2" });

		// Create shallow and deep clones
		shallowClone = (Deck)originalDeck.Clone();
		deepClone = (Deck)originalDeck.DeepClone();

		// Update UI to show results
		originalLabel.Text = $"Original Label: {originalDeck.Label}\n{GetCardsInfo(originalDeck.Cards)}";
		shallowLabel.Text = $"Shallow Clone Label: {shallowClone.Label}\n{GetCardsInfo(shallowClone.Cards)}";
		deepLabel.Text = $"Deep Clone Label: {deepClone.Label}\n{GetCardsInfo(deepClone.Cards)}";
	}

	private string GetCardsInfo(System.Collections.Generic.ICollection<Card> cards)
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("Cards:");
		foreach (var card in cards)
		{
			sb.AppendLine($"- Front: {card.Front}, Back: {card.Back}");
		}
		return sb.ToString();
	}

	private void OnOriginalCardLabelChanged(object sender, TextChangedEventArgs e)
	{
		if (originalDeck != null && originalDeck.Cards.Any())
		{
			originalDeck.Cards.First().Front = originalCard1Label.Text;
			UpdateOriginalDeckLabel();

			shallowClone.Cards.First().Front = originalCard1Label.Text;
			UpdateShallowCloneLabel();
		}
	}

	private void OnShallowCardLabelChanged(object sender, TextChangedEventArgs e)
	{
		if (shallowClone != null && shallowClone.Cards.Any())
		{
			originalDeck.Cards.First().Front = shallowCard1Label.Text;
			UpdateOriginalDeckLabel();

			shallowClone.Cards.First().Front = shallowCard1Label.Text;
			UpdateShallowCloneLabel();
		}
	}

	private void OnDeepCardLabelChanged(object sender, TextChangedEventArgs e)
	{
		if (deepClone != null && deepClone.Cards.Any())
		{
			deepClone.Cards.First().Front = deepCard1Label.Text;
			UpdateDeepCloneLabel();
		}
	}

	private void UpdateOriginalDeckLabel()
	{
		originalLabel.Text = $"Original Label: {originalDeck.Label}\n{GetCardsInfo(originalDeck.Cards)}";
	}

	private void UpdateShallowCloneLabel()
	{
		shallowLabel.Text = $"Shallow Clone Label: {shallowClone.Label}\n{GetCardsInfo(shallowClone.Cards)}";
	}

	private void UpdateDeepCloneLabel()
	{
		deepLabel.Text = $"Deep Clone Label: {deepClone.Label}\n{GetCardsInfo(deepClone.Cards)}";
	}

	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("///CreateCardPage");
	}
}