﻿using BrokeProtocol.API;
using BrokeProtocol.Entities;
using BrokeProtocol.Utility;
using BrokeProtocol.Utility.Networking;
using UnityEngine;

namespace BrokeProtocol.GameSource.Types
{
    public class Player
    {
        [Target(GameSourceEvent.PlayerInitialize, ExecutionMode.Override)]
        public void OnInitialize(ShPlayer player)
        {
            /*
            if (player.svPlayer.CustomData.FetchCustomData<bool>(red) == true)
            {
                player.svPlayer.CustomData.AddOrUpdate(red, false);
                player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, "You can choose a team using /jointeam Red.");
                return;
            }
            if (player.svPlayer.CustomData.FetchCustomData<bool>(blue) == true)
            {
                player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, "You can choose a team using /jointeam Blue.");
                player.svPlayer.CustomData.AddOrUpdate(red, false);
                return;
            } I suppose this sets the player to neutral team so bottm answer is better*/
            player.svPlayer.CustomData.AddOrUpdate(Core.Instance.TeamKey,"Neutral");
            player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, $"Welcome {player.username} to {Core.Instance.Settings.nameserver}");
            player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, $"Discord: {Core.Instance.Settings.discordlink}");
            player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, "------------------------------------------------------------");
            player.svPlayer.Send(SvSendType.Self, Channel.Reliable, ClPacket.GameMessage, "You can choose a team using /jointeam Blue or /jointeam Red.");
        }

        [Target(GameSourceEvent.PlayerGlobalChatMessage, ExecutionMode.Override)] //GLOBAL CHAT
        public void OnGlobalChatMessage(ShPlayer player, string message)
        {
            if (player.manager.svManager.chatted.Limit(player))
            {
                return;
            }

            message = message.CleanMessage();

            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            Debug.LogError($"[CHAT] {player.username}:{message}");

            if (CommandHandler.OnEvent(player, message)) 
            {
                return;
            }
            var teamName = player.svPlayer.CustomData.FetchCustomData<string>(Core.Instance.TeamKey);
            if (teamName == "Red")
            {
                player.svPlayer.Send(SvSendType.All, Channel.Reliable, ClPacket.GlobalChatMessage, "(&4TEAM RED&f)" + player.ID, message);
            }
            else if (teamName == "Blue")
            {
                player.svPlayer.Send(SvSendType.All, Channel.Reliable, ClPacket.GlobalChatMessage, "(&1TEAM BLUE&f)" + player.ID, message);
            }
            else
            {
                player.svPlayer.Send(SvSendType.All, Channel.Reliable, ClPacket.GlobalChatMessage, "(&4LOBBY&f)" + player.ID, message);
            }
        }

        [Target(GameSourceEvent.PlayerLocalChatMessage, ExecutionMode.Override)] //QUESTIONS CHANNEL
        public void OnLocalChatMessage(ShPlayer player, string message)
        {
            if (player.manager.svManager.chatted.Limit(player))
            {
                return;
            }

            message = message.CleanMessage();

            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            Debug.LogWarning($"[QUESTION] {player.username}:{message}");

            if (CommandHandler.OnEvent(player, message))
            {
                return;
            }

            player.svPlayer.Send(SvSendType.All, Channel.Reliable, ClPacket.GlobalChatMessage, "(&gQUESTION&f)" + player.ID, message);
        }
    }
}
