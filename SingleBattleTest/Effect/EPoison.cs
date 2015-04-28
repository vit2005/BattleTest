using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleBattleTest.Effect
{
    public class EPoison : IEffect
    {
        public EPoison()
        {
            base.id = "1";
            base.name = "Poison";
            base.description = "Poison enemy";
            base.moves = 3;
        }

        public override void Use(ref List<Unit> team, ref Unit u)
        {
            u.hp -= u.maxhp / 10;
            
            if (u.hp <= 0)
            {
                u.isAlive = false;
            }
        }
    }
}
