using System.Text;
using CadastroDeClientes.Presentation.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class AccountController : Controller
{
    private readonly string _urlUsuario = "https://localhost:7226/api/usuario";
    private readonly HttpClient _httpClient;

    public AccountController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var requestBody = new
        {
            email = model.Email,
            senha = model.Senha
        };

        var json = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7226/Api/Authentication", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            HttpContext.Response.Cookies.Append("AuthToken", responseContent, new CookieOptions
            {
                Expires = DateTime.Now.AddHours(7),
                HttpOnly = true,  
                Secure = true,    
                SameSite = SameSiteMode.Strict  
            });

            TempData["Success"] = "Login realizado com sucesso!";

            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["Error"] = "Falha ao realizar login. Tente novamente!";
            return RedirectToAction("Login", "Account");
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Login", "Account");
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<ActionResult> Create(CadastroUsuarioViewModel usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        using (var client = new HttpClient())
        {
            var clienteContent = JsonConvert.SerializeObject(new
            {
                usuario.Nome,
                usuario.Email,
                usuario.Senha,
                usuario.Role
            });

            var content = new StringContent(clienteContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"{_urlUsuario}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Usuario criado com sucesso!";
            }
            else
            {
                TempData["ErrorMessage"] = "Erro ao criar usuario.";
            }
        }

        return RedirectToAction("Index", "Home");
    }

    private class ResponseToken
    {
        public string Token { get; set; }
    }
}
