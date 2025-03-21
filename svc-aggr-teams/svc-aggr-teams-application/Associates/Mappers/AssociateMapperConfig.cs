﻿namespace DistribuTe.Aggregates.Teams.Application.Associates.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;
using Shared;

public class AssociateMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssociateAggregate, AssociateModel>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.First_Name, src => src.FirstName)
            .Map(dest => dest.Last_Name, src => src.LastName)
            .Map(dest => dest.Middle_Name, src => src.MiddleName)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Email_Id, src => src.EmailId)
            .Map(dest => dest.Squad_Associates, src => src.SquadAssociates);
        
        config.NewConfig<AssociateAggregate, AssociateElement>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.First_Name, src => src.FirstName)
            .Map(dest => dest.Last_Name, src => src.LastName)
            .Map(dest => dest.Middle_Name, src => src.MiddleName)
            .Map(dest => dest.Gender, src => src.Gender)
            .Map(dest => dest.Email_Id, src => src.EmailId);
    }
}