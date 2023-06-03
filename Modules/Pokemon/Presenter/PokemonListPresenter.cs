using maui_pokemon_list.Modules.Pokemon.Data;
using maui_pokemon_list.Modules.Pokemon.Entity;
using maui_pokemon_list.Modules.Pokemon.Iterator;
using maui_pokemon_list.Modules.Pokemon.View;
using System.Collections.ObjectModel;

namespace maui_pokemon_list.Modules.Pokemon.Presenter;

public partial class PokemonListPresenter : BasePokemonPresenter
{
    public ObservableCollection<PokemonModel> Pokemons { get; } = new();

    public PokemonListPresenter(PokemonIterator pokemonIterator) : base(pokemonIterator)
    { 
        _ = GetPokemons();
    }

    async Task GetPokemons()
    {
        if (IsBusy) return;

        IsBusy = true;

        var pokemons = await pokemonIterator.GetPokemons();

        if (Pokemons.Count != 0)
                Pokemons.Clear();

        foreach (var pokemon in pokemons)
            Pokemons.Add(pokemon);

        IsBusy = false;
    }

    [RelayCommand]
    async Task GoToDetails(PokemonModel pokemon)
    {

        if (pokemon == null)
            return;


        await Shell.Current.GoToAsync(nameof(DetailsPage), false, new Dictionary<string, object>
        {
            { "Pokemon", pokemon }
        });
    }
}
