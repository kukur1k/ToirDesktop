namespace ToirDesktop;

public record loginRequest(string Email, string Password);

public record AuthData(string Access, string Refresh, string Role);

public record ApiResponse<T>
(
    bool success,
    T data, 
    string message,
    string? errorCode = null
);


public record Dashboard(
    int totalEqCount,
    int brokeEqCount,
    string eqCountString,
    int countActiveOrders,
    int countActiveOrdersLost,
    int nowPlanOrders,
    int nowCreateOrders
);