using MauiToDoClient.Pages;

namespace MauiToDoClient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ManageToDoPage), typeof(ManageToDoPage));
	}
}
