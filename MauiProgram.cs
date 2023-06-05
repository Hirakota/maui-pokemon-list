using maui_pokemon_list.Modules.Pokemon.Data;
using maui_pokemon_list.Modules.Pokemon.Iterator;
using maui_pokemon_list.Modules.Pokemon.Presenter;
using maui_pokemon_list.Modules.Pokemon.View;
using Microsoft.Extensions.Logging;

namespace maui_pokemon_list;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<PokemonService>();
		builder.Services.AddSingleton<PokemonIterator>();
		builder.Services.AddSingleton<Router>();

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<PokemonListPresenter>();
		builder.Services.AddTransient<DetailsPage>();
		builder.Services.AddTransient<PokemonDetailsPresenter>();

		return builder.Build();
	}
}
