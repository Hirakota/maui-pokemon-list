using maui_pokemon_list.Modules.Pokemon.Entity;
using maui_pokemon_list.Modules.Pokemon.View;

namespace maui_pokemon_list;

public class Router
{
    public static async Task NavigateToPokemonDetails(PokemonModel pokemon) 
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), false, new Dictionary<string, object>
        {
            { "Pokemon", pokemon }
        });
    }
}
