using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace SpectatorList
{
    public class EventHandler
    {
        private const string CoroutineTag = "Spectator-List"; 

        private Config _config => SpectatorList.Instance.Config;

        public EventHandler()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
        }

        ~EventHandler()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Timing.KillCoroutines(CoroutineTag);
        }

        private void OnRoundStarted() => Timing.RunCoroutine(DoList().CancelWith(Server.Host.GameObject), CoroutineTag);

        private IEnumerator<float> DoList()
        {
            Log.Debug("haii");
            while (true)
            {
                Log.Debug("Running foreach loop.");

                foreach (Player player in Player.List)
                {
                    Log.Debug("Checking condition for Spec List");
                    if (player.IsDead || _config.HiddenFor.Contains(player.Role.Team)) continue;
                    Log.Debug("Player is able to see Spectator List");

                    int count = player.CurrentSpectatingPlayers.Count(p => p.Role != RoleTypeId.Overwatch);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{count} players are spectating{(count == 0 ? string.Empty : ":")}");

                    foreach (Player spectator in player.CurrentSpectatingPlayers.Where(p => p.Role != RoleTypeId.Overwatch))
                        sb.AppendLine(_config.PlayerDisplay.Replace("%name%", spectator.CustomName));

                    player.ShowHint(_config.FullText.Replace("%display%", sb.ToString()), _config.RefreshRate + 0.15f);
                }

                yield return Timing.WaitForSeconds(_config.RefreshRate);
            }
        }
    }
}