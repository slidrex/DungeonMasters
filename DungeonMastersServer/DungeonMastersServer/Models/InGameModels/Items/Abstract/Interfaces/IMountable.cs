using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonMastersServer.Models.InGameModels.Items.Abstract.Interfaces
{
    interface IMountable
    {
        void OnMount();
        void OnUnmount();
    }
}
