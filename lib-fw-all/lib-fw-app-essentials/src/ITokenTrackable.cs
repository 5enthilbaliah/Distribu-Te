namespace DistribuTe.Framework.AppEssentials;

public interface ITokenTrackable : IUserTrackable
{
    string? Token { get; set; }
}