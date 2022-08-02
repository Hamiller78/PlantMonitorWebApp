using PlantMonitorMobileApp.ViewModels;
using PlantMonitorWebApp.Shared.DataAccessor;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Models;
using System.Collections.ObjectModel;

namespace PlantMonitorMobileApp.Pages;

public partial class PlantOverviewPage : ContentPage
{
	private readonly IPlantAccessor _plantAccessor;
    private ObservableCollection<PlantViewModel> _plantVMs;
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
		_plantVMs = await GetPlantsDataAsync();
		listView.ItemsSource = _plantVMs;
	}

	async Task<ObservableCollection<PlantViewModel>> GetPlantsDataAsync()
	{
		var plants = await _plantAccessor.GetPlantsAsync();
        ObservableCollection<PlantViewModel> plantVMs = new(plants.Select(p => new PlantViewModel(p)));
        return plantVMs;
	}

}