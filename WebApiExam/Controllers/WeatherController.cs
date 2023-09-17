using Microsoft.AspNetCore.Mvc;
using WebApiExam.Models;

namespace WebApiExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        public static List<City> Cityies = new List<City>
        {
            new City() { IdCity = 0, Name = "Москва"},
            new City() { IdCity = 1, Name = "Самара"},
            new City() { IdCity = 2, Name = "Сочи"},
            new City() { IdCity = 3, Name = "Владивосток"},
            new City() { IdCity = 4, Name = "Новокуйбышевск"},
            new City() { IdCity = 5, Name = "Южно-Сахалинск"},
            new City() { IdCity = 6, Name = "Сочи"},
        };

        public static List<string> WeatherSun = new List<string>()
        {
            "Солнечно", "Облачно", "Малооблачно", "Дождь", "Небольшой дождь", "Снег"
        };


        [HttpGet("GetCity")]
        public IActionResult GetCity(string token)
        {
            var found = AccountController.Tokens.FirstOrDefault(x => x.Token == token);
            if (found == null)
                return Unauthorized("Токен недействительный, истек или не существует");

            return Ok(Cityies);
        }

        [HttpGet("GetWeatherByCity")]
        public IActionResult GetWeather(string token, int idCity)
        {
            var found = AccountController.Tokens.FirstOrDefault(x => x.Token == token && x.ExpireDate.Ticks <= DateTime.Now.Ticks);
            if (found == null)
                return Unauthorized("Токен недействительный, истек или не существует");

            List<WeatherResult> result = new();

            for (int i = 0; i < 7; i++)
            {
                WeatherResult weather = new()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(i)),
                    Temperature = new Random().Next(-30, 30),
                    Description = WeatherSun[new Random().Next(0, WeatherSun.Count)]
                };
                result.Add(weather);
            }
            return Ok(result);
        }
    }
}
