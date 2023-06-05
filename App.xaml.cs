using MonkeyCache.LiteDB;

namespace maui_pokemon_list;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		Barrel.ApplicationId = AppInfo.PackageName;

        MainPage = new AppShell();
	}
}
