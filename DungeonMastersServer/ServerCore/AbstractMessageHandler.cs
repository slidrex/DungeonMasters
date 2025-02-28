using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using ServerCore;

namespace DungeonMastersServer.MessageHandlers
{
    public abstract class MessageHandler<T> : IAbstractMessageHandler where T : MessageHandler<T>
    {
        public static T Singleton { get; set; }

        public void Init()
        {
            Singleton = (T)this; 
        }
    }
}