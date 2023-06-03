namespace maui_pokemon_list.Modules.Pokemon.Entity;
public class PokemonDetailsModel
{
    public PokemonDetailsModel(string name, string image, List<string> types, int height, int weight)
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
    public double height { get; set; }
    public double weight { get; set; }
}
