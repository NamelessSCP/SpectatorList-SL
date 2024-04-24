using Exiled.API.Features;

namespace SpectatorList
{
    public class SpectatorList : Plugin<Config>
    {
        public static SpectatorList Instance { get; private set; }
        
        public override string Name { get; } = "Spectator List";
        public override string Author { get; } = "@misfiy";

        public override void OnEnabled()
        {
            Instance = this;
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            base.OnDisabled();
        }
    }
}