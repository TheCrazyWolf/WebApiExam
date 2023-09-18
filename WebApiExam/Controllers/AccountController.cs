using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using WebApiExam.Models;

namespace WebApiExam.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public static List<Account> Accounts = new();

        public static List<TokenAdmin> Tokens = new();

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            account.Login = account.Login.ToLower();
            account.Password = account.Password.ToLower();

            if (account.Password.Length <= 6)
            {
                Console.WriteLine($"[{DateTime.Now}] Неудачная попытка регистрации: {account.Fio} ({account.Login} - {account.Password}). Пароль не прошел политику безопасности");
                return BadRequest("Пароль не подходит требованиям безопасности");
            }

            var found = Accounts.FirstOrDefault(x => x.Login == account.Login);
            if (found != null)
            {
                Console.WriteLine($"[{DateTime.Now}] Неудачная попытка регистрации: {account.Fio} ({account.Login} - {account.Password}). Логинг занят");
                return BadRequest("Логин уже занят");
            }

            Accounts.Add(account);

            Console.WriteLine($"[{DateTime.Now}] Зарегистрирован аккаунт: {account.Fio}");

            return Ok();
        }

        [HttpGet("GetToken")]
        public IActionResult GetToken(string login, string password)
        {
            password = password.ToLower();
            login = login.ToLower();

            var found = Accounts
                .FirstOrDefault(x => x.Login == login && x.Password == password);

            if(found == null)
            {
                Console.WriteLine($"[{DateTime.Now}] Неудачная попытка авторизации ({login} - {password})");
                return BadRequest("Логин или пароль неправильный");
            }

            //TokenResult token = new TokenResult()
            //{
            //    Token = Guid.NewGuid().ToString().Replace("-", string.Empty),
            //    ExpireDate = DateTime.Now.AddMinutes(45)
            //};

            TokenAdmin tokenAccount = new() { Token = Guid.NewGuid().ToString().Replace("-", string.Empty), ExpireDate = DateTime.Now.AddMinutes(45), Account = found };

            Tokens.Add(tokenAccount);

            TokenResult token = (TokenResult)tokenAccount;

            Console.WriteLine($"[{DateTime.Now}] Успешная авторизация: {found.Fio}. Выдан токен: {token.Token}");

            return Ok(token);
        }

    }
}
