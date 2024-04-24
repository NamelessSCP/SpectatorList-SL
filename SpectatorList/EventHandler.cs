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
        public EventHandler() => Timing.RunCoroutine(DoList());

        private IEnumerator<float> DoList()
        {
            while (true)
            {
                if (Round.IsEnded)
                {
                    Timing.WaitForSeconds(5);
                    continue;
                }
                
                foreach (Player player in Player.List)
                {
                    if (player.IsDead) continue;
                    
                    int count = player.CurrentSpectatingPlayers.Count(p => p.Role != RoleTypeId.Overwatch);
                    
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{count} players are spectating{(count == 0 ? string.Empty : ":")}");
                    
                    foreach (Player spectator in player.CurrentSpectatingPlayers.Where(p => p.Role != RoleTypeId.Overwatch))
                    {
                        sb.AppendLine(spectator.DisplayNickname);
                    }

                    player.ShowHint($"<size=23><align=right><voffset=750>{sb}</size></voffset></align>", SpectatorList.Instance.Config.RefreshRate + 0.15f);
                }
                
                yield return Timing.WaitForSeconds(SpectatorList.Instance.Config.RefreshRate);
            }
        }
    }
}