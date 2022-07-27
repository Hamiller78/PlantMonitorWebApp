using PlantMonitorWebApp.Shared.DataAccessor;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Models;
using System.Collections.ObjectModel;

namespace PlantMonitorMobileApp.Pages;

public partial class PlantOverviewPage : ContentPage
{
	readonly IPlantAccessor _plantAccessor;
	public IEnumerable<Plant> Plants { get; set; }

	public PlantOverviewPage()
	{
		InitializeComponent();
		HttpClient httpClient = new()
		{
			BaseAddress = new Uri("https://plantmonitorwebappserver20220208190443.azurewebsites.net/")
		};
		_plantAccessor = new PlantAccessor(httpClient);
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		Plants = await GetPlantsDataAsync();
		listView.ItemsSource = Plants;
	}

	async Task<IEnumerable<Plant>> GetPlantsDataAsync()
	{
		var plants = await _plantAccessor.GetPlantsAsync();
		return plants;
	}

}