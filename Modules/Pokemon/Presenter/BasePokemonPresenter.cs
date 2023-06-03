using maui_pokemon_list.Modules.Pokemon.Iterator;

namespace maui_pokemon_list.Modules.Pokemon.Presenter;

public partial class BasePokemonPresenter : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !IsBusy;

    protected PokemonIterator pokemonIterator;

    public BasePokemonPresenter(PokemonIterator pokemonIterator)
    {
        this.pokemonIterator = pokemonIterator;
    }
}
