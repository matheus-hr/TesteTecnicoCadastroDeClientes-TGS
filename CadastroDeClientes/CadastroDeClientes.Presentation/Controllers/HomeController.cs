using CadastroDeClientes.Presentation.Attribute;
using CadastroDeClientes.Presentation.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CadastroDeClientes.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _urlCliente = "https://localhost:7226/api/cliente";
        private readonly string _urlLogradouro = "https://localhost:7226/api/logradouro";

        public HomeController(HttpClient client)
        {
            _client = client;
        }

        [JwtAuth]
        public async Task<IActionResult> Index()
        {
            var clientes = new List<ClienteViewModel>();

            var authToken = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return RedirectToAction("Account", "Login");
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            HttpResponseMessage response = await _client.GetAsync(_urlCliente);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                clientes = JsonConvert.DeserializeObject<List<ClienteViewModel>>(json);
            }

            return View(clientes);
        }

        [JwtAuth]
        public IActionResult Create()
        {
            return View();
        }

        [JwtAuth]
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            ClienteViewModel cliente = null;

            var authToken = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return RedirectToAction("Account", "Login");
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var responseCliente = await _client.GetAsync($"{_urlCliente}/{id}");
            if (responseCliente.IsSuccessStatusCode)
            {
                cliente = await responseCliente.Content.ReadFromJsonAsync<ClienteViewModel>();
            }

            return View(cliente);
        }

        [JwtAuth]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cliente = new ClienteViewModel();

            var authToken = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return RedirectToAction("Account", "Login");
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var response = await _client.GetAsync($"{_urlCliente}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                cliente = JsonConvert.DeserializeObject<ClienteViewModel>(json);
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [JwtAuth]
        public async Task<ActionResult> Edit(ClienteViewModel cliente, [FromForm] IFormFile logotipo)
        {
            var authToken = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return RedirectToAction("Account", "Login");
            }

            using (var memoryStream = new MemoryStream())
            {
                string jsonClienteContent = string.Empty;

                if (logotipo == null || logotipo.Length == 0)
                {
                    jsonClienteContent = JsonConvert.SerializeObject(new
                    {
                        cliente.Nome,
                        cliente.Email,
                    });
                }
                else
                {
                    await logotipo.CopyToAsync(memoryStream);
                    var logotipoBytes = memoryStream.ToArray();

                    jsonClienteContent = JsonConvert.SerializeObject(new
                    {
                        cliente.Nome,
                        cliente.Email,
                        Logotipo = logotipoBytes
                    });
                }

                var clienteContent = new StringContent(jsonClienteContent, Encoding.UTF8, "application/json");

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                HttpResponseMessage clienteResponse = await _client.PutAsync($"{_urlCliente}/{cliente.Id}", clienteContent);

                var jsonLogradouroContent = JsonConvert.SerializeObject(new
                {
                    cliente.Logradouro.Endereco,
                    cliente.Logradouro.Cep,
                    cliente.Logradouro.Bairro,
                    cliente.Logradouro.Numero,
                    cliente.Logradouro.Complemento,
                    cliente.Logradouro.Cidade
                });
                var logradouroContent = new StringContent(jsonLogradouroContent, Encoding.UTF8, "application/json");

                HttpResponseMessage logradouroResponse = await _client.PutAsync($"{_urlLogradouro}/{cliente.Id}", logradouroContent);

                if (clienteResponse.IsSuccessStatusCode && logradouroResponse.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cliente atualizado com sucesso!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Erro ao atualizar cliente.";
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [JwtAuth]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            var authToken = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return RedirectToAction("Account", "Login");
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var response = await _client.DeleteAsync($"{_urlCliente}/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Cliente excluido com sucesso!";
            }
            else
            {
                TempData["ErrorMessage"] = "Erro ao excluir cliente.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [JwtAuth]
        public async Task<ActionResult> Create(ClienteViewModel cliente, [FromForm] IFormFile logotipo)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            var authToken = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(authToken))
            {
                return RedirectToAction("Account", "Login");
            }

            if (logotipo == null || logotipo.Length == 0)
            {
                ModelState.AddModelError("Logotipo", "O logotipo é obrigatório.");
                return View(cliente);
            }

            using (var memoryStream = new MemoryStream())
            {
                await logotipo.CopyToAsync(memoryStream);
                var logotipoBytes = memoryStream.ToArray();

                var clienteContent = JsonConvert.SerializeObject(new
                {
                    cliente.Nome,
                    cliente.Email,
                    Logotipo = logotipoBytes,
                    Logradouro = new
                    {
                        cliente.Logradouro.Endereco,
                        cliente.Logradouro.Cep,
                        cliente.Logradouro.Bairro,
                        cliente.Logradouro.Numero,
                        cliente.Logradouro.Complemento,
                        cliente.Logradouro.Cidade
                    }
                });

                var content = new StringContent(clienteContent, Encoding.UTF8, "application/json");

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                HttpResponseMessage response = await _client.PostAsync($"{_urlCliente}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cliente criado com sucesso!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Erro ao criar cliente.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
