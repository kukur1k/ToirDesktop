using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToirDesktop;

public class ApiService
{
    
    public static readonly ApiService Instance = new();

    private readonly HttpClient client;

     private static readonly JsonSerializerOptions JsonOpts = new()
        { PropertyNameCaseInsensitive = true };

    public string? AccessToken {get; private set;}
    public string? RefreshToken {get; private set;}
    public string? Role {get; private set;}

    private ApiService(string baseurl = "http://localhost:5333"){
        client = new HttpClient{ BaseAddress = new System.Uri(baseurl)};
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        try
        {
    
            var response = await client.PostAsJsonAsync("/auth/login", new loginRequest(email, password));

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<AuthData>>(JsonOpts);

            if (result is null) return "Не удалось разобрать ответ от сервера";

            if (!result.success || result.data is null)
                return result.message ?? "Неверный login или пароль";

            AccessToken = result.data.AccessToken;
            RefreshToken = result.data.RefreshToken;
            Role = result.data.Role;

            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            
            return null;
        }
        catch (HttpRequestException)
        {
            return "Сервер недоступен. Проверьте подключение";
        }
        catch (Exception ex)
        {
            return $"Ошибка - {ex.Message}";
        }
    }

    public async Task<User?> GetMe()
    {
        try
        {
            var res = await client.GetFromJsonAsync<ApiResponse<User>>("/users/me");
            return res?.data;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<DashboardKPI?> GetMainDashboard()
    {
        try
        {
            var response = await client.GetFromJsonAsync<ApiResponse<DashboardKPI>>("/dashboard/kpi");
            return response?.data;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<ApiResponse<List<TableOrderResponse?>>> GetTableOrders()
    {
        try
        {
            var res = await client.GetFromJsonAsync<ApiResponse<List<TableOrderResponse?>>>("/dashboard/active-orders-master");
            return res;

        } catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }



}
