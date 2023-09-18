namespace WebApiExam.Models
{
    public class WeatherResult
    {
        public DateOnly Date { get; set; }
        public int Temperature { get; set; }
        public string Description
        {
            get => GetRandomDescription();
        }


        public string GetRandomDescription()
        {
            List<string> WeatherSun = new List<string>() { "Солнечно", "Облачно", "Малооблачно", "Дождь", "Небольшой дождь", "Град" };
            List<string> WeatherWinter = new List<string>() { "Солнечно", "Облачно", "Малооблачно", "Снег", "Метель" };

            if (Temperature >= 0)
                return WeatherSun[new Random().Next(0, WeatherSun.Count)];
            else
                return WeatherWinter[new Random().Next(0, WeatherWinter.Count)];
        }
    }
}
