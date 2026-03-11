namespace InfotecsBackend.Errors.Exceptions;
#pragma warning disable CS1591

public static class ErrorMessages
{
    public static string DeviceNotFound(Guid deviceId) =>
        $"Device {deviceId} not found";
}