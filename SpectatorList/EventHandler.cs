using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEC;
using Exiled.API.Features;
using PlayerRoles;

namespace SpectatorList
{
    public class EventHandler
    {
        private Config _config => SpectatorList.Instance.Config;
        
        public EventHandler() => Timing.RunCoroutine(DoList());

        private IEnumerator<float> DoList()
        {
            for (; ;)
            {
                if (Round.IsEnded)
                {
                    Timing.WaitForSeconds(5);
                    continue;
                }
                
                foreach (Player player in Player.List)
                {
                    if (player.IsDead || _config.HiddenFor.Contains(player.Role.Team)) continue;
                    
                    int count = player.CurrentSpectatingPlayers.Count(p => p.Role != RoleTypeId.Overwatch);
                    
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{count} players are spectating{(count == 0 ? string.Empty : ":")}");
                    
                    foreach (Player spectator in player.CurrentSpectatingPlayers.Where(p => p.Role != RoleTypeId.Overwatch))
                    {
                        sb.AppendLine(_config.PlayerDisplay.Replace("%name%", spectator.DisplayNickname));
                    }

                    player.ShowHint(_config.FullText.Replace("%display%", sb.ToString()), _config.RefreshRate + 0.15f);
                }
                
                yield return Timing.WaitForSeconds(_config.RefreshRate);
            }
        }
    }
}