using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleBattleTest.Effect;

namespace SingleBattleTest.Skills
{
    public class SPoison : ISkill
    {
        public SPoison(byte lvl)
        {
            base.id = "3";
            base.name = "Strike";
            base.description = "Hit enemy";
            base.type = skillType.attack;
            base.onEnemy = true;
            base.lvl = lvl;
        }

        public override void Use(ref List<Unit> allys, ref List<Unit> enemies, ref Unit u1, ref Unit u2)
        {
            System.Threading.Thread.Sleep(3);
            Random r = new Random();
            if (r.Next(100) < Math.Sqrt(lvl * 25) + 55) //TODO poison resist
                u2.Effects.Add(new EPoison());
        }
    }
}
