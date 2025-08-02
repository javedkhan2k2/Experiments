using AutoMapper;
using ClampingDevice.DTOs;
using ClampingDevice.Entities;

namespace ClampingDevice.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Device mapping
        CreateMap<Device, DeviceDto>();
        CreateMap<DeviceDto, Device>();
        CreateMap<CreateDeviceDto, Device>();
        CreateMap<DeviceStatusDto, Device>();

        // ClampingData mapping
        CreateMap<ClampingData, ClampingDataDto>();
    }
}