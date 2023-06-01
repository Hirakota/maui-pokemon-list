using System.Text.Json;

namespace maui_pokemon_list.Services;

public class PokemonService
{
    HttpClient httpClient;
    string baseUri = "https://pokeapi.co/api/v2/";

    public PokemonService() 
    {
        httpClient = new HttpClient();
    }

    public async Task<List<Pokemon>> GetPokemons() {
        var json = await httpClient.GetStringAsync(baseUri + "pokemon");

        List<Pokemon> pokemonList = new();

        if (!String.IsNullOrEmpty(json))
        {
            PokemonResponse response = JsonSerializer.Deserialize<PokemonResponse>(json);
            if (response != null)
            {
                pokemonList = response.results;
            }
        }

        return pokemonList;
    }
}

class PokemonResponse
{
    public string next { get; set; }
    public List<Pokemon> results { get; set; }
}

