using System.Collections.ObjectModel;

using PlantMonitorMobileApp.ViewModels;
using PlantMonitorWebApp.Shared.DataAccessor;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorMobileApp.Pages;

public partial class PlantDetailPage : ContentPage
{
    private readonly IPlantAccessor _plantAccessor;
    private ObservableCollection<PlantViewModel> _plantVMs;

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
        _plantVMs = await GetPlantsDataAsync();
        carouselView.ItemsSource = _plantVMs;
    }

    async Task<ObservableCollection<PlantViewModel>> GetPlantsDataAsync()
    {
        var plants = await _plantAccessor.GetPlantsAsync();
        ObservableCollection<PlantViewModel> plantVMs = new(plants.Select(p => new PlantViewModel(p)));
        return plantVMs;
    }
}