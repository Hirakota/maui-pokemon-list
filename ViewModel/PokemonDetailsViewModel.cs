using CommunityToolkit.Mvvm.ComponentModel;
using maui_pokemon_list.Services;

namespace maui_pokemon_list.ViewModel;

public partial class PokemonDetailsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    public PokemonDetailsModel pokemonDetails;

    private string name;

    private PokemonService pokemonService;

    public PokemonDetailsViewModel(PokemonService service)
    {
        pokemonService = service;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        name = (query["Pokemon"] as PokemonModel).name;
        _ = GetDetails(this.name);
    }

    async Task GetDetails(string name)
    {
        try
        {
            IsBusy = true;

            PokemonDetails = await pokemonService.GetPokemonDetails(name);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unable to get details", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
