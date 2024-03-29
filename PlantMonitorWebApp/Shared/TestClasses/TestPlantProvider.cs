﻿using PlantMonitorWebApp.Shared.ViewModels;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.TestData
{
    public static class TestPlantProvider
    {

        public static IEnumerable<PlantViewModel> GetTestPlantViewConfigurations()
        {
            var plantViewList = new List<PlantViewModel>
            {
                new PlantViewModel()
                {
                    Name = "Judith",
                    Description = "Weihnachtsstern",
                    ImageUrl = "/Images/CactusPic.png",
                    SensorId = 1
                },
                new PlantViewModel()
                {
                    Name = "Mister Nancy",
                    Description = "Palme",
                    ImageUrl = "/Images/CactusPic.png",
                    SensorId = 2
                },
                new PlantViewModel()
                {
                    Name = "Harry",
                    Description = "Philodendron",
                    ImageUrl = "/Images/CactusPic.png",
                    SensorId = 1
                }
            };

            return plantViewList;
        }

        public static List<Plant>  GetTestPlants()
        {
            var sensorList = GetTestSensors();

            var plantList = new List<Plant>
            {
                new Plant() { Name = "Judith", Description = "Weihnachtsstern", Sensor = sensorList[0]},
                new Plant() { Name = "Mister Nancy", Description = "Palme", Sensor = sensorList[1]},
                new Plant() { Name = "Harry", Description = "Philodendron", Sensor = sensorList[0]}
            };

            return plantList;
        }

        public static List<Sensor> GetTestSensors()
        {
            return new List<Sensor>()
            {
                new Sensor() {Id=1, ServiceUri=new Uri("https://mocksensorservice20220122160720.azurewebsites.net/api/GetSensorValue/1?code=vXOZ0KPHaey6xL944dS1sglf4qZMs9H73Wf3joInZE9gJlFKPqJnfg==")},
                new Sensor() {Id=2, ServiceUri=new Uri("https://mocksensorservice20220122160720.azurewebsites.net/api/GetSensorValue/2?code=vXOZ0KPHaey6xL944dS1sglf4qZMs9H73Wf3joInZE9gJlFKPqJnfg==")}
            };
        }
    }
}
