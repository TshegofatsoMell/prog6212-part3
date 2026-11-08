using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace RaceDay.Mvc.Controllers;

public class AccountController : Controller
{
    private const string Api = "https://localhost:7051";

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        using var http = new HttpClient();
        var json = JsonSerializer.Serialize(new { Id = 0, FullName = "Test User", Email = email, PasswordHash = password, Role = "Athlete" });
        var res = await http.PostAsync(Api + "/api/Auth/login",
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (!res.IsSuccessStatusCode)
        {
            ViewBag.Error = "Invalid login";
            return View();
        }

        var data = JsonSerializer.Deserialize<JsonElement>(await res.Content.ReadAsStringAsync());
        var user = data.GetProperty("user");
        HttpContext.Session.SetString("Email", email);
        HttpContext.Session.SetString("Role", user.GetProperty("role").GetString() ?? "Athlete");
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string fullName, string email, string password, string role)
    {
        using var http = new HttpClient();
        var json = JsonSerializer.Serialize(new { FullName = fullName, Email = email, PasswordHash = password, Role = role });
        var res = await http.PostAsync(Api + "/api/Auth/register",
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (!res.IsSuccessStatusCode)
        {
            ViewBag.Error = "Email exists";
            return View();
        }
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}