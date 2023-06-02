namespace maui_pokemon_list.Model;

public class Pokemon
{
    public string name { get; set; }
    public string url { get; set; }

    public Pokemon(string name, string url)
    {
        this.name = name;
        this.url = url;
    }
}

public class PokemonDetails
{
    public PokemonDetails(string name, string image, List<string> types, int height, int weight)
    {
        this.name = name;
        this.image = image;
        this.types = types;
        this.height = height;
        this.weight = weight;
    }

    public string name { get; set; }
    public string image { get; set; }
    public List<string> types { get; set; }
    public int height { get; set; }
    public int weight { get; set; }


}

/*
○ The Pokemon's name
○ A single image of the pokemon from the front(found under sprites )
○ The Pokemon's "types" e.g. grass
○ The Pokemon's weight in kg +
○ The Pokemon's height in cm +
*/