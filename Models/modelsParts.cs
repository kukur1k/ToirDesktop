using System;
using System.Collections.Generic;

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

public record WorkOrder(
    string Id,
    string Eq,
    string Priority,
    string ColorCodePriority,
    string Status,
    string StatusColor,
    string Brigade,
    string DateStart,
    string Reason,
    string Master);

public record ItemHistoryTO(
    int Id,
    string ToTitle,
    string Date);

public record HistoryCardTO(
    int? EqId,
    string? EquipmentName,
    List<ItemHistoryTO>? History
);

 public record EqRequest(
        string? InvNumber,
        string? Name, string? Model,
        string? SerialNumber, DateOnly? ExpStart,
        DateOnly? WorkStart, string? Manifacture,
        DateOnly? WarrancyDate, string? Workshop,
        string? Status
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

public record TableOrderResponse(
    string Id,
    string Eq,
    string Priority,
    string ColorCodePriority,
    string Status,
    string StatusColor,
    string Brigade,
    string DateStart,
    string Reason,
    string Master
);

public record EqTableItem(
    string Id,
    string InventNumber,
    string Name,
    string Model,
    string Manufacture,
    string DateStartWork,
    string EqStatus,
    string StatusColor
);

    public record SheduleUpcomingTO(
        int EqId, string EqTitle, string TypeTo, string PlanDate, string Status, string Color
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