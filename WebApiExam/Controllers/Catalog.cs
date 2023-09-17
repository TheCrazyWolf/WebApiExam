using Microsoft.AspNetCore.Mvc;
using WebApiExam.Models;

namespace WebApiExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Catalog : ControllerBase
    {
        public static List<Good> Goods = new List<Good>
        {
        };


        [HttpGet("GetGoods")]
        public IActionResult GetGoods(string token)
        {
            if (token != "a7b25d781e794003b3e4e1e43fa34ded")
                return Unauthorized("Токен недействительный, истек или не существует");

            return Ok(Goods);
        }

        [HttpPost("AddGood")]
        public IActionResult AddGood([FromHeader] string token, [FromBody] Good good)
        {
            if (token != "a7b25d781e794003b3e4e1e43fa34ded")
                return Unauthorized("Токен недействительный, истек или не существует");

            Goods.Add(good);

            return Ok();
        }

        [HttpDelete("DeleteGood")]
        public IActionResult DeleteGood([FromHeader] string token, [FromHeader] string guidGood)
        {
            if (token != "a7b25d781e794003b3e4e1e43fa34ded")
                return Unauthorized("Токен недействительный, истек или не существует");

            var found = Goods.FirstOrDefault(x => x.IdGood.ToString() == guidGood);
            if(found == null)
                return NotFound("Элемент не найден");

            Goods.Remove(found);
            return Ok();
        }

    }
}
