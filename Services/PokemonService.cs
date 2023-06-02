using MonkeyCache.FileStore;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        List<Pokemon> pokemonList = new();

        Dictionary<string, object> rawData = await GetAsync<Dictionary<string, object>>(baseUri, "getpokemons");

        pokemonList = JsonSerializer.Deserialize<List<Pokemon>>(rawData["results"].ToString());

        return pokemonList;
    }

    public async Task<PokemonDetailsModel> GetPokemonDetails(string name)
    {
        Dictionary<string, object> rawData = await GetAsync<Dictionary<string, object>>($"{baseUri}/{name}", $"{name}/details");

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

    private async Task<T> GetAsync<T>(string url, string key, int mins = 1, bool forceRefresh = false)
    {
        var json = string.Empty;

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            json = Barrel.Current.Get<string>(key);
        else if (!forceRefresh && !Barrel.Current.IsExpired(key))
            json = Barrel.Current.Get<string>(key);

        try
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                json = await httpClient.GetStringAsync(url);

                Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
            }
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception ex) {
            throw ex;
        }
    }
}

