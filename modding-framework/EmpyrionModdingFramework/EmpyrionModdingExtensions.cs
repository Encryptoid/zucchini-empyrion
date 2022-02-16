﻿using Eleon.Modding;
using EmpyrionModdingFramework.Database;
using EmpyrionModdingFramework.Teleport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionModdingFramework
{
    public enum MessagerPriority
    {
        Red = 0, // Normal
        Yellow = 1, // Alert
        Blue = 2 // Attention
    }

    partial class EmpyrionModdingFrameworkBase
    {
        public async Task MessagePlayer(int entityId, string msg, float time, MessagerPriority prio = MessagerPriority.Blue)
        {
            Log($"Messaging entity {entityId}, message: {msg}");
            await RequestManager.SendGameRequest(CmdId.Request_InGameMessage_SinglePlayer, new IdMsgPrio()
            {
                id = entityId,
                msg = msg,
                prio = (byte)prio,
                time = time
            });
        }

        public async Task ShowDialog(int entityId, string msg)
        {
            await RequestManager.SendGameRequest(CmdId.Request_ShowDialog_SinglePlayer, new DialogBoxData()
            {
                Id = entityId,
                MsgText = msg
            });
        }

        public void ShowLinkedDialog(int entityId, string message, string title, Action<int, string, string, int, int> linkCallback)
        {
            var dialogConfig = new DialogConfig()
            {
                ButtonTexts = new string[] { null, null, "Cancel" },
                ButtonIdxForEnter = -1,
                ButtonIdxForEsc = -1,
                BodyText = message,
                TitleText = title
            };

            ModAPI.Application.ShowDialogBox(entityId, dialogConfig, new DialogActionHandler(linkCallback), 99); //TODO Get Valid ID
        }

        public async Task TeleportPlayerToPlayer(int playerId, int targetId)
        {
            var target = await QueryPlayerInfo(targetId);

            await TeleportPlayer(playerId, target.playfield, target.pos.x, target.pos.y, target.pos.z, target.rot.x, target.rot.y, target.rot.z);
        }

        public async Task TeleportPlayer(int entityId, string playfield, float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
        {
            Log($"Teleporting entity: {entityId} to playfield: {playfield}");
            await RequestManager.SendGameRequest(CmdId.Request_Player_ChangePlayerfield, new IdPlayfieldPositionRotation
            {
                id = entityId,
                playfield = playfield,
                pos = new PVector3 { x = posX, y = posY, z = posZ},
                rot = new PVector3 { x = rotX, y = rotY, z = rotZ }
            });
        }

        public async Task<PlayerInfo> QueryPlayerInfo(int entityId)
        {
            PlayerInfo player = null;

            try
            {
                player = (PlayerInfo)await RequestManager.SendGameRequest(
                   CmdId.Request_Player_Info, new Id() { id = entityId });
            }
            catch
            {
                await MessagePlayer(entityId, $"Could not retrieve info for EntityId: {entityId}", 5, MessagerPriority.Red);
            }

            return player;
        }

        public bool IsAdmin(int playerPermission)
        {
            return playerPermission >= (int)PlayerPermission.Admin;
        }

        public async Task<List<int>> QueryPlayerList()
        {
            List<int> playerIds = new List<int>();

            try
            {
                var idList = (IdList)await RequestManager.SendGameRequest(CmdId.Request_Player_List, null);
                playerIds = idList.list;
            }
            catch(Exception e)
            {
                Log($"Could not retrieve player list. Exception: {e}");
            }

            return playerIds;
        }
    }
}
