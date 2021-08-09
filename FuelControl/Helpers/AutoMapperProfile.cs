using AutoMapper;
using FuelControl.Entities;
using FuelControl.Models.Accounts;
using FuelControl.Models.Drivers;
using FuelControl.Models.Vehicles;
using FuelControl.Models.FuelPrices;
using FuelControl.Models.FuelSupplies;
using FuelControl.Dtos.FuelSupplies;

namespace FuelControl.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();

            CreateMap<Account, AuthenticateResponse>();

            CreateMap<RegisterRequest, Account>();

            CreateMap<CreateRequest, Account>();

            CreateMap<UpdateRequest, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));

            CreateMap<Driver, DriverResponse>();

            CreateMap<CreateDriverRequest, Driver>();

            CreateMap<UpdateDriverRequest, Driver>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

            CreateMap<Vehicle, VehicleResponse>();

            CreateMap<CreateVehicleRequest, Vehicle>();

            CreateMap<UpdateVehicleRequest, Vehicle>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

            CreateMap<FuelPrice, FuelPriceResponse>();

            CreateMap<CreateFuelPriceRequest, FuelPrice>();

            CreateMap<UpdateFuelPriceRequest, FuelPrice>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

            CreateMap<AddFuelSupplyDtoRequest, FuelSupply>();
            CreateMap<FuelSupply, FuelSupplyResponse>();
            CreateMap<FuelSupply, FuelSupplyDetailResponse>();

            CreateMap<UpdateFuelSupplyRequest, FuelSupply>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}