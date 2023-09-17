using Microsoft.AspNetCore.Mvc;
using WebApiExam.Models;

namespace WebApiExam.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public static List<Account> Accounts = new();

        public static List<TokenResult> Tokens = new();

        [HttpPost("RegisterAccount")]
        public IActionResult RegisterAccount([FromBody] Account account)
        {
            account.Login = account.Login.ToLower();
            account.Password = account.Password.ToLower();

            if (account.Password.Length <= 6)
                return BadRequest("Пароль не подходит требованиям безопасности");

            var found = Accounts.FirstOrDefault(x => x.Login == account.Login);
            if (found != null)
                return BadRequest("Логин уже занят");

            Accounts.Add(account);

            return Ok();
        }

        [HttpPost("AuthorizateAccount")]
        public IActionResult AuthorizateAccount([FromBody] Account account)
        {
            account.Login = account.Login.ToLower();
            account.Password = account.Password.ToLower();

            var found = Accounts.FirstOrDefault(x => x.Login == account.Login && x.Password == account.Password);

            if(found == null)
                return BadRequest("Логин или пароль неправильный");

            TokenResult token = new TokenResult()
            {
                Token = Guid.NewGuid().ToString().Replace("-", string.Empty),
                ExpireDate = DateTime.Now.AddMinutes(45)
            };

            Tokens.Add(token);

            return Ok(token);
        }

    }
}
