#pragma warning disable CS8603 // Possible null reference return.
namespace DistribuTe.Mutators.Teams.Application.Associates.Mappings;

using DataContracts;
using Domain.Entities;
using Mapster;

public class AssociateMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssociateRequest, Associate>()
            .Ignore(dest => dest.Id)
            .Map(dest => dest.FirstName, src => src.First_Name)
            .Map(dest => dest.LastName, src => src.Last_Name)
            .Map(dest => dest.MiddleName, src => src.Middle_Name)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.EmailId, src => src.Email_Id)
            .Ignore(dest => dest.CreatedOn)
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.ModifiedOn)
            .Ignore(dest => dest.ModifiedBy);;

        config.NewConfig<Associate, AssociateResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.First_Name, src => src.FirstName)
            .Map(dest => dest.Last_Name, src => src.LastName)
            .Map(dest => dest.Middle_Name, src => src.MiddleName)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Email_Id, src => src.EmailId);
        
        config.NewConfig<Associate, Associate>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}