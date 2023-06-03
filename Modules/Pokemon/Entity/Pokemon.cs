namespace maui_pokemon_list.Modules.Pokemon.Entity;

public class PokemonModel
{
    public string name { get; set; }
    public string url { get; set; }

    public PokemonModel(string name, string url)
    {
        this.name = name;
        this.url = url;
    }
}
