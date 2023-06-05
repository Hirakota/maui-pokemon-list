using maui_pokemon_list.Modules.Pokemon.View;

namespace maui_pokemon_list;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
    }
}
