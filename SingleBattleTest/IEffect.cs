using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleBattleTest
{
    public abstract class IEffect
    {
        public string name;
        public string id;
        public string description;
        public byte moves;

        public abstract void Use(Unit u, List<Unit> team);
    }
}
