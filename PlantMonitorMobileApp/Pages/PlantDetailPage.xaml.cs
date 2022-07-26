using PlantMonitorWebApp.Shared.DataAccessor;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorMobileApp.Pages;

public partial class PlantDetailPage : ContentPage
{
    readonly IPlantAccessor _plantAccessor;
    public IEnumerable<Plant> Plants { get; set; }

    public PlantDetailPage()
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
    }

    async Task<IEnumerable<Plant>> GetPlantsDataAsync()
    {
        var plants = await _plantAccessor.GetPlantsAsync();
        return plants;
    }
}