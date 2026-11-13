using System;

namespace ToirDesktop;

public record loginRequest(string email, string password);

public record AuthData(string AccessToken, string RefreshToken, string Role);

public record ApiResponse<T>
(
    bool success,
    T data, 
    string message,
    string? errorCode = null
);


public record DashboardKPI(
    int totalEqCount,
    int brokeEqCount,
    string eqCountString,
    int countActiveOrders,
    int countActiveOrdersLost,
    int nowPlanOrders,
    int nowCreateOrders
);

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public DateTime RegisteredAt { get; set; }
    public string? Phone { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Role { get; set; } = null!;
}