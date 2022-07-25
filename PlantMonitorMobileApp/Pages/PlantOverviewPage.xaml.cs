using PlantMonitorWebApp.Client.DataAccessor;
using PlantMonitorWebApp.Client.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorMobileApp.Pages;

public partial class PlantOverviewPage : ContentPage
{
	const string PlantServiceUri = "";

	readonly IPlantAccessor _plantAccessor;
	public IEnumerable<Plant> Plants { get; set; }

	public PlantOverviewPage()
	{
		InitializeComponent();
		_plantAccessor = new PlantAccessor(new HttpClient());
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		Plants = await GetPlantsDataAsync();
	}

	async Task<IEnumerable<Plant>> GetPlantsDataAsync()
	{
		var plants = await _plantAccessor.GetPlantsAsync();
		return plants;
	}

}