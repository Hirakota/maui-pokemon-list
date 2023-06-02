using maui_pokemon_list.Services;
using System.Collections.ObjectModel;

namespace maui_pokemon_list.ViewModel;

public partial class PokemonListViewModel : BaseViewModel
{
    public ObservableCollection<PokemonModel> Pokemons { get; } = new();
    PokemonService pokemonService;

    public PokemonListViewModel(PokemonService pokemonService)
    {
        this.Title = "Pokemons";
        this.pokemonService = pokemonService;
        _ = GetPokemons();
    }

    async Task GetPokemons()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var pokemons = await pokemonService.GetPokemons();

            if (Pokemons.Count != 0)
                Pokemons.Clear();

            foreach (var pokemon in pokemons)
                Pokemons.Add(pokemon);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unable to get pokemons", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
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
