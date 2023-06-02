using maui_pokemon_list.Services;
using maui_pokemon_list.View;
using Microsoft.Extensions.DependencyInjection;
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
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<PokemonListViewModel>();
		builder.Services.AddTransient<DetailsPage>();
		builder.Services.AddTransient<PokemonDetailsViewModel>();

		return builder.Build();
	}
}
