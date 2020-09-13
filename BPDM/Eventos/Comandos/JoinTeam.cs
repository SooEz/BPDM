using BrokeProtocol.API;
using BrokeProtocol.Entities;
using BrokeProtocol.Utility.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BrokeProtocol.CustomEvents
{
    public class JoinTeam : IScript
    {
        public Jointeam()
        {
            CommandHandler.RegisterCommand("jointeam", new Action<ShPlayer, string>(Invoke));
        }

        public void Invoke(ShPlayer player, string team)
        {
            if (team == "Blue")
            {
                Vector3 BlueSpawn = new Vector3(1, 2, 3); //COORDENADAS PARA TELETRASPORTARLOS A LA BASE AZUL.
                player.SetPositionSafe(BlueSpawn);
                player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, "You just joined &1Blue&f team.");
                player.svPlayer.CustomData.AddOrUpdate(Core.Instance.TeamKey, "Blue");
                return;
            }
            else if (team == "Red")
            {
                Vector3 RedSpawn = new Vector3(1, 2, 3); //COORDENADAS PARA TELETRASPORTARLOS A LA BASE ROJA.
                player.SetPositionSafe(RedSpawn);
                player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, "You just joined &1Red&f team.");
                player.svPlayer.CustomData.AddOrUpdate(Core.Instance.TeamKey, "Red");
                return;
            }
            else
            {
                player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, "Error: You can only join team &4Red&f or &1Blue&f.");
            }
        }
    }
}
