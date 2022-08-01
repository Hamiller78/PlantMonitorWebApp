using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PlantMonitorMobileApp.ViewModels
{
    public class SensorValueViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly int _sensorId;
        private double _sensorValue;

        private readonly HubConnection _hubConnection;

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

        public SensorValueViewModel(int sensorId)
        {
            _sensorId = sensorId;

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://plantmonitorwebappserver20220208190443.azurewebsites.net/sensorvaluehub")
                .Build();

            _hubConnection.On<string, double>("SensorValueChanged", (sensorId, value) =>
            {
                SensorValue = value;
            });

            _hubConnection.StartAsync();
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
