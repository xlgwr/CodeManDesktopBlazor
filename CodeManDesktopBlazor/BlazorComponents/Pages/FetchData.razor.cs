using CodeManDesktopBlazor.BlzsorComponents.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeManDesktopBlazor.BlzsorComponents.Pages
{
    public partial class FetchData : ComponentBase
    { 
        protected override async Task OnInitializedAsync()
        {
            try
            {
                //HttpClient Http = new HttpClient();
                //forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");

                var lists = new List<WeatherForecast>();
                for (int i = 0; i < 20; i++)
                {
                    lists.Add(new WeatherForecast()
                    {
                        Date = DateTime.Now.AddHours(i),
                        Summary = $"{i}Summary",
                        TemperatureC = i
                    });
                }
                this.forecasts = lists.ToArray();
                await Task.Delay(1);
            }
            catch (Exception ex)
            {
                this.errormsg = ex.Message;
            }
        }

        
    }
}
