namespace maui_pokemon_list.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(PokemonDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}


}