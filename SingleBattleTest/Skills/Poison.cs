using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleBattleTest.Skills
{
    public class Poison : ISkill
    {
        public Poison(byte lvl)
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
            if (r.Next(100) < Math.Sqrt(lvl * 25) + 55)
                u2.Effects.Add(new SingleBattleTest.Effect.EPoison());
        }
    }
}
