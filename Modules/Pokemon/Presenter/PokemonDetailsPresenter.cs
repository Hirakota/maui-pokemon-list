using maui_pokemon_list.Modules.Pokemon.Data;
using maui_pokemon_list.Modules.Pokemon.Entity;
using maui_pokemon_list.Modules.Pokemon.Iterator;

namespace maui_pokemon_list.Modules.Pokemon.Presenter;

public partial class PokemonDetailsPresenter : BasePokemonPresenter, IQueryAttributable
{
    [ObservableProperty]
    public PokemonDetailsModel pokemonDetails;

    private string name;

    public PokemonDetailsPresenter(PokemonIterator pokemonIterator) : base(pokemonIterator)
    {
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        name = (query["Pokemon"] as PokemonModel).name;
        _ = GetDetails(this.name);
    }

    async Task GetDetails(string name)
    {
        IsBusy = true;

        PokemonDetails = await pokemonIterator.GetPokemonDetails(name);

        IsBusy = false;
    }
}
