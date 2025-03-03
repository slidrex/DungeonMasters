using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using ServerCore;

namespace DungeonMastersServer.MessageHandlers
{
    public abstract class SingletonService<T> : IInitable where T : SingletonService<T>
    {
        public static T Service { get; set; }

        public virtual void Init()
        {
            Service = (T)this; 
        }
    }
}