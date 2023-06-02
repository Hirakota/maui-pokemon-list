using maui_pokemon_list.Services;

namespace maui_pokemon_list.ViewModel;

public partial class PokemonDetailsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    public PokemonDetails pokemonDetails;

    private PokemonService pokemonService;

    public PokemonDetailsViewModel(PokemonService service)
    {
        pokemonService = service;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Title = (query["Pokemon"] as Pokemon).name;
    }

    [RelayCommand]
    async Task GetDetails()
    {
        try
        {
            IsBusy = true;

            pokemonDetails = await pokemonService.GetPokemonDetails("bulbasaur");
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
