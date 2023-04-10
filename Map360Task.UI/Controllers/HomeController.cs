using ClosedXML.Excel;
using Map360Task.Domain.Entities;
using Map360Task.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Map360Task.UI.Controllers
{
    public class HomeController : Controller
    {
        HttpClient _client;
        HttpResponseMessage _response;
        public HomeController()
        {
            _client = new HttpClient();

        }

        private async Task<string> GetApiResponseAsync(string apiUrl)
        {
            _response = await _client.GetAsync("https://localhost:7229/api/" + apiUrl);
            return await _response.Content.ReadAsStringAsync();
        }

        private async Task<T> GetApiResponseAsync<T>(string apiUrl)
        {
            _response = await _client.GetAsync("https://localhost:7229/api/" + apiUrl);
            var jsonString = await _response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        private async Task<string> PostApiResponseAsync(string apiUrl, object payload)
        {
            var jsonObject = JsonConvert.SerializeObject(payload);
            var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            _response = await _client.PostAsync("https://localhost:7229/api/" + apiUrl, stringContent);
            return await _response.Content.ReadAsStringAsync();
        }

        private async Task<string> PutApiResponseAsync(string apiUrl, object payload)
        {
            var jsonObject = JsonConvert.SerializeObject(payload);
            var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            _response = await _client.PutAsync("https://localhost:7229/api/" + apiUrl, stringContent);
            return await _response.Content.ReadAsStringAsync();
        }
        private async Task<string> DeleteApiResponseAsync(string apiUrl, int id)
        {
            _response = await _client.DeleteAsync("https://localhost:7229/api/" + apiUrl + "?id=" + id);
            return await _response.Content.ReadAsStringAsync();
        }
        public IActionResult Users()
        {
            return View();
        }

        public async Task<IActionResult> ListUser()
        {
            var users = await GetApiResponseAsync("Users/WithCompanyAndRole");
            return Json(users);
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await DeleteApiResponseAsync("Users", id);
            if (_response.IsSuccessStatusCode)
                return Json(user);
            return BadRequest("Bir hata oluştu. Kullanıcı silinemedi.");
        }
        
        public async Task<IActionResult> AddUser(User model)
        {
            var user = await PostApiResponseAsync("Users", model);
            if (_response.IsSuccessStatusCode)
                return Json(user);
            return BadRequest("Bir hata oluştu. Kullanıcı eklenemedi.");
        }
        
        public async Task<IActionResult> UpdateUser(User model)
        {
            var user = await PutApiResponseAsync("Users", model);
            if (_response.IsSuccessStatusCode)
                return Json(user);
            return BadRequest("Bir hata oluştu. Kullanıcı güncellenemedi.");
        }

        public IActionResult Companies()
        {
            return View();
        }

        public async Task<IActionResult> ListCompany()
        {
            var companies = await GetApiResponseAsync("Companies");
            return Json(companies);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(Company model)
        {
            var company = await PostApiResponseAsync("Companies", model);
            if (_response.IsSuccessStatusCode)
                return Json(company);
            return BadRequest("Bir hata oluştu. Şirket eklenemedi.");
        }

        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await DeleteApiResponseAsync("Companies", id);
            if (_response.IsSuccessStatusCode)
                return Json(company);
            return BadRequest("Bir hata oluştu. Şirket silinemedi.");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCompany(Company model)
        {
            var company = await PutApiResponseAsync("Companies", model);
            if (_response.IsSuccessStatusCode)
                return Json(company);
            return BadRequest("Bir hata oluştu. Şirket güncellenemedi.");
        }

        public async Task<IActionResult> ListRole()
        {
            var roles = await GetApiResponseAsync("Roles");
            return Json(roles);
        }
    }
}