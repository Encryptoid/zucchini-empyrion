using Eleon.Modding;
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
    }
}
