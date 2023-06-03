using maui_pokemon_list.Modules.Pokemon.Presenter;

namespace maui_pokemon_list.Modules.Pokemon.View;

public partial class MainPage : ContentPage
{
	public MainPage(PokemonListPresenter presenter)
	{
        InitializeComponent();
        BindingContext = presenter;
    }
}

