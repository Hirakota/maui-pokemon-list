using maui_pokemon_list.Modules.Pokemon.Data;
using maui_pokemon_list.Modules.Pokemon.Entity;

namespace maui_pokemon_list.Modules.Pokemon.Iterator;

public class PokemonIterator
{
    private PokemonService pokemonService;

    public PokemonIterator(PokemonService pokemonService)
    {
        this.pokemonService = pokemonService;
    }

    public async Task<List<PokemonModel>> GetPokemons(int page = 0)
    {
        List<PokemonModel> pokemons = new();

        var offset = 20 * page;

        try
        {
            pokemons = await pokemonService.GetPokemons(offset);

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unable to get pokemons", ex.Message, "OK");
        }

        return pokemons;
    }

    public async Task<PokemonDetailsModel> GetPokemonDetails(string name)
    {
        PokemonDetailsModel pokemonDetails = new("", "", new(), 0, 0);

        try
        {
            pokemonDetails = await pokemonService.GetPokemonDetails(name);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unable to get details", ex.Message, "OK");
        }

        return pokemonDetails;
    }
}