using System;
using Exiled.API.Features;

namespace SpectatorList
{
    public class SpectatorList : Plugin<Config>
    {
        public static SpectatorList Instance { get; private set; }

        public override string Name { get; } = "Spectator List";
        public override string Author { get; } = "@misfiy";
        public override Version Version { get; } = new Version(1,1,0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 8, 1);

        private EventHandler _handler;

        public override void OnEnabled()
        {
            Instance = this;
            _handler = new EventHandler();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            _handler = null;
            Instance = null;

            base.OnDisabled();
        }
    }
}