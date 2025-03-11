// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.UnitTests.Apis.Odata;

using FluentAssertions;
using Framework.OData;
using Microsoft.OData.ModelBuilder;
using Teams.Apis.Odata;
using Teams.Application.Associates.DataContracts;
using Teams.Application.SquadAssociates.DataContracts;
using Teams.Application.Squads.DataContracts;

public class ResponseConfigurationTests
{
    [Fact]
    public void AssociateResponseConfiguration_Edm_Succeeds()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<AssociateResponse>("associates");
        odataBuilder.AddOdataConfigurations<AssociateResponseConfiguration, AssociateResponse>();
        
        var edmModel = odataBuilder.GetEdmModel();
        edmModel.EntityContainer.Elements.Count().Should().Be(1);
        edmModel.EntityContainer.Elements.First().Name.Should().Be("associates");
    }
    
    [Fact]
    public void SquadResponseConfiguration_Edm_Succeeds()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<SquadResponse>("squads");
        odataBuilder.AddOdataConfigurations<SquadResponseConfiguration, SquadResponse>();
        
        var edmModel = odataBuilder.GetEdmModel();
        edmModel.EntityContainer.Elements.Count().Should().Be(1);
        edmModel.EntityContainer.Elements.First().Name.Should().Be("squads");
    }
    
    [Fact]
    public void SquadAssociateResponseConfiguration_Edm_Succeeds()
    {
        var odataBuilder = new ODataConventionModelBuilder();
        odataBuilder.EntitySet<SquadAssociateResponse>("squad-associates");
        odataBuilder.AddOdataConfigurations<SquadAssociateResponseConfiguration, SquadAssociateResponse>();
        
        var edmModel = odataBuilder.GetEdmModel();
        edmModel.EntityContainer.Elements.Count().Should().Be(1);
        edmModel.EntityContainer.Elements.First().Name.Should().Be("squad-associates");
    }
}