namespace maui_pokemon_list.View;

public partial class MainPage : ContentPage
{
	public MainPage(PokemonListViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}

