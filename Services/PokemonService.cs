using System.Text.Json;

namespace maui_pokemon_list.Services;

public class PokemonService
{
    HttpClient httpClient;
    string baseUri = "https://pokeapi.co/api/v2/pokemon";

    public PokemonService()
    {
        httpClient = new HttpClient();
    }

    public async Task<List<Pokemon>> GetPokemons()
    {
        var json = await httpClient.GetStringAsync(baseUri);

        List<Pokemon> pokemonList = new();

        if (!String.IsNullOrEmpty(json))
        {
            Dictionary<string, object> rawData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            pokemonList = JsonSerializer.Deserialize<List<Pokemon>>(rawData["results"].ToString());
        }

        return pokemonList;
    }

    public async Task<PokemonDetailsModel> GetPokemonDetails(string name)
    {
        var json = await httpClient.GetStringAsync(baseUri + $"/{name}");

        Dictionary<string, object> rawData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

        string pokemonName = rawData["name"].ToString();
        int pokemonHeight = JsonSerializer.Deserialize<int>(rawData["height"].ToString());
        int pokemonWeight = JsonSerializer.Deserialize<int>(rawData["weight"].ToString());

        string pokemonImage = JsonSerializer.Deserialize<Dictionary<string, object>>(rawData["sprites"].ToString())["front_default"].ToString();

        List<string> types = new();
        List<Dictionary<string, object>> rawTypes = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(rawData["types"].ToString());
        foreach (var rawType in rawTypes)
        {
            string type = JsonSerializer.Deserialize<Dictionary<string, object>>(rawType["type"].ToString())["name"].ToString();
            types.Add(type);
        }

        PokemonDetailsModel pokemonDetails = new PokemonDetailsModel(pokemonName, pokemonImage, types, pokemonHeight * 10, pokemonWeight / 10);

        return pokemonDetails;
    }
}

