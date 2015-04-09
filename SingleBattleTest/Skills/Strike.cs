using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleBattleTest.Skills
{
    public class Strike : ISkill
    {
        public Strike(byte lvl)
        {
            base.id = "2";
            base.name = "Strike";
            base.description = "Hit enemy x3 damage";
            base.type = skillType.attack;
            base.onEnemy = true;
            base.lvl = lvl;
        }

        public override void Use(ref List<Unit> allys, ref List<Unit> enemies, ref Unit u1, ref Unit u2)
        {
            System.Threading.Thread.Sleep(3);
            Random r = new Random();
            if (r.Next(100) < Math.Sqrt(lvl*25)+55)
                u2.hp -= (int)(u1.damage * 3 * ((100 - u2.dodge) / 100));
        }
    }
}
