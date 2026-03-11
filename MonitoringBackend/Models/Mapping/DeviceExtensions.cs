using Models.DTOs.Devices;
using Models.Entities;

namespace Models.Mapping;

public static class DeviceExtensions
{
    public static DeviceInfo ToDeviceInfo(this Device device)
    {
        return new DeviceInfo
        {
            CreatedAt = device.CreatedAt,
            Id = device.Id,
            LastSeenAt = device.LastSeenAt,
        };
    }
}