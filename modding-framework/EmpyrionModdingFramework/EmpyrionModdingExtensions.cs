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
            Log("Starting message: " + msg);

            await RequestManager.SendGameRequest(CmdId.Request_InGameMessage_SinglePlayer, new IdMsgPrio()
            {
                id = entityId,
                msg = msg,
                prio = (byte)prio,
                time = time
            });
            Log("Finished message: " + msg);
        }

        public async Task ShowDialog(int entityId, string msg)
        {
            await RequestManager.SendGameRequest(CmdId.Request_ShowDialog_SinglePlayer, new DialogBoxData()
            {
                Id = entityId,
                MsgText = msg
            });
        }
    }
}
