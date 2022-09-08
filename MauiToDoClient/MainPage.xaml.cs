using MauiToDoClient.DataServices;
using MauiToDoClient.Models;
using MauiToDoClient.Pages;

namespace MauiToDoClient;

public partial class MainPage : ContentPage
{
	private readonly IRestDataService _dataService;

	public MainPage(IRestDataService dataService)
	{
		InitializeComponent();

		_dataService = dataService;
    }

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		collectionView.ItemsSource = await _dataService.GetAllToDosAsync();
	}

	async void OnAddToDoClicked(object sender, EventArgs e) 
	{
        System.Diagnostics.Debug.WriteLine("---> Add button clicked...");

		var navigattionParamter = new Dictionary<string, object>()
		{
			{ nameof(ToDo), new ToDo() }
		};

		await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigattionParamter);
    }

	async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
        System.Diagnostics.Debug.WriteLine("---> Item changed click!...");

        var navigattionParamter = new Dictionary<string, object>()
        {
            { nameof(ToDo), e.CurrentSelection.FirstOrDefault() as ToDo }
        };

        await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigattionParamter);
    }
}

