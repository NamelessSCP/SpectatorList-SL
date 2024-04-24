using Exiled.API.Interfaces;

namespace SpectatorList
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float RefreshRate { get; set; } = 2;
    }
}