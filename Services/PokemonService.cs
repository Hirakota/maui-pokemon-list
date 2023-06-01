namespace maui_pokemon_list.Services;

public class PokemonService
{
    HttpClient httpClient;

    public PokemonService() 
    {
        httpClient = new HttpClient();
    }
}


