using Models.DTOs.Sessions;
using Models.Entities;

namespace Models.Mapping;

public static class SessionExtensions
{
    public static SessionInfo ToSessionInfo(this Session session)
    {
        return new SessionInfo
        {
            Id = session.Id,
            DeviceId = session.DeviceId,
            Name = session.Name,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Version = session.Version,
        };
    }
    
    public static Session ToSession(this SessionInfo sessionInfo)
    {
        return new Session
        {
            DeviceId = sessionInfo.DeviceId,
            Name = sessionInfo.Name,
            StartTime = sessionInfo.StartTime,
            EndTime = sessionInfo.EndTime,
            Version = sessionInfo.Version,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
        };
    }
}