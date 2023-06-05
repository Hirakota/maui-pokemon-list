using maui_pokemon_list.Modules.Pokemon.Entity;
using MonkeyCache.LiteDB;
using System.Text.Json;

namespace maui_pokemon_list.Modules.Pokemon.Data;

public class PokemonService
{
    HttpClient httpClient;
    string baseUri = "https://pokeapi.co/api/v2/pokemon";

    public PokemonService()
    {
        httpClient = new HttpClient();
    }

    public async Task<List<PokemonModel>> GetPokemons(int offset)
    {
        string uri = $"{baseUri}?offset={offset}";

        Dictionary<string, object> rawData = await GetAsync<Dictionary<string, object>>(uri);

        return DataAdapter.ConvertDictionaryIntoPokemonList(rawData);
    }

    public async Task<PokemonDetailsModel> GetPokemonDetails(string name)
    {
        Dictionary<string, object> rawData = await GetAsync<Dictionary<string, object>>($"{baseUri}/{name}");

        return DataAdapter.ConvertDictionaryIntoPokemonDetails(rawData);
    }

    private async Task<T> GetAsync<T>(string url, bool forceRefresh = true)
    {
        var json = string.Empty;

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            return Barrel.Current.Get<T>(url);

        if (!forceRefresh && !Barrel.Current.IsExpired(url))
            return Barrel.Current.Get<T>(url);

        try
        {
            json = await httpClient.GetStringAsync(url);

            Barrel.Current.Add(key: url, data: json, expireIn: TimeSpan.FromHours(2));

            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

class DataAdapter
{
    public static List<PokemonModel> ConvertDictionaryIntoPokemonList(Dictionary<string, object> rawData)
    {
        return JsonSerializer.Deserialize<List<PokemonModel>>(rawData["results"].ToString()) ?? new List<PokemonModel>();
    }

    public static PokemonDetailsModel ConvertDictionaryIntoPokemonDetails(Dictionary<string, object> rawData)
    {
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

        return new PokemonDetailsModel(pokemonName, pokemonImage, types, pokemonHeight * 10, pokemonWeight / 10);
    }
}

