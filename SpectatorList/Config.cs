using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace SpectatorList
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("A list of teams the hints should be hidden for")]
        public List<Team> HiddenFor { get; set; } = new List<Team>();
        [Description("How often in seconds to refresh the hud")]
        public float RefreshRate { get; set; } = 2;
        public string FullText { get; set; } = "<size=23><align=right><voffset=750>%display%</size></voffset></align>";
        public string PlayerDisplay { get; set; } = "%name%";
    }
}