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


        [HttpGet("GetCities")]
        public IActionResult GetCities(string token)
        {
            var found = AccountController.Tokens.FirstOrDefault(x => x.Token == token);
            
            if (found == null)
            {
                Console.WriteLine($"[{DateTime.Now}] Неудачная попытка получить список городов. Недействительный токен: {token} ");
                return Unauthorized("Токен недействительный, истек или не существует");
            }

            Console.WriteLine($"[{DateTime.Now}] Успешно получил список городов: {found.Account.Fio}");

            return Ok(Cityies);
        }

        [HttpGet("GetWeatherByCity")]
        public IActionResult GetWeather(string token, int idCity)
        {
            var found = AccountController
                .Tokens.FirstOrDefault(x => x.Token == token && x.ExpireDate.Ticks >= DateTime.Now.Ticks);

            if (found == null)
            {
                Console.WriteLine($"[{DateTime.Now}] Неудачная попытка получить список городов. Недействительный токен: {token} ");
                return Unauthorized("Токен недействительный, истек или не существует");
            }    

            List<WeatherResult> result = new();

            for (int i = 0; i < 7; i++)
            {
                WeatherResult weather = new()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(i)),
                    Temperature = new Random().Next(-30, 30),
                };
                result.Add(weather);
            }

            Console.WriteLine($"[{DateTime.Now}] Успешно получил погоду: {found.Account.Fio}");

            return Ok(result);
        }
    }
}
