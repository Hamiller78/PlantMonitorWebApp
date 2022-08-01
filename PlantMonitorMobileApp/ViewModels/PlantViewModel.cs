using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using PlantMonitorWebApp.Shared.DataAccessor;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorMobileApp.ViewModels
{
    public class PlantViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Plant _plant;
        private readonly int _sensorId;
        private double _sensorValue;

        private readonly HubConnection _hubConnection;

        public string Name => _plant?.Name;
        public string Description => _plant?.Description;
        public string ImageUrl => _plant?.ImageUrl;

        public double SensorValue
        {
            get { return _sensorValue; }
            set
            {
                if (_sensorValue != value)
                {
                    _sensorValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public PlantViewModel(Plant plantModel)
        {
            _plant = plantModel;
            _sensorId = plantModel.SensorId ?? -1;

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://plantmonitorwebappserver20220208190443.azurewebsites.net/sensorvaluehub")
                .Build();

            _hubConnection.On<string, double>("SensorValueChanged", (sensorId, value) =>
            {
                if (int.Parse(sensorId) == _sensorId)
                {
                    SensorValue = value;
                }
            });

            _hubConnection.StartAsync();
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
