using maui_pokemon_list.Modules.Pokemon.Entity;
using maui_pokemon_list.Modules.Pokemon.Iterator;
using maui_pokemon_list.Modules.Pokemon.View;
using System.Collections.ObjectModel;

namespace maui_pokemon_list.Modules.Pokemon.Presenter;

public partial class PokemonListPresenter : BasePokemonPresenter
{
    private int page = 0;
    public ObservableCollection<PokemonModel> Pokemons { get; set; } = new();

    [ObservableProperty]
    private bool isLoading = false;


    public PokemonListPresenter(PokemonIterator pokemonIterator) : base(pokemonIterator)
    { 
        _ = GetPokemons();
    }

    async Task GetPokemons()
    {
        if (IsBusy) return;

        IsBusy = true;

        var pokemons = await pokemonIterator.GetPokemons(page);

        foreach (var pokemon in pokemons)
            Pokemons.Add(pokemon);

        IsBusy = false;
    }

    [RelayCommand]
    public async void LoadMorePokemons()
    {
        if(IsLoading) return;

        if(Pokemons.Count > 0)
        {
            IsLoading = true;
            var pokemonsToBeAdded = await pokemonIterator.GetPokemons(page + 1);

            if(pokemonsToBeAdded?.Count > 0) 
            {
                page++;

                foreach(var pokemon in pokemonsToBeAdded)
                {
                    Pokemons.Add(pokemon);
                }
            }
        }

        IsLoading = false;
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
