using maui_pokemon_list.Modules.Pokemon.Presenter;

namespace maui_pokemon_list.Modules.Pokemon.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(PokemonDetailsPresenter viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}


}