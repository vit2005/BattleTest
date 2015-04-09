using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleBattleTest.Skills
{
    public class Heal : ISkill
    {
        public Heal(byte lvl)
        {
            base.id = "1";
            base.name = "Heal";
            base.description = "Heal ally";
            base.type = skillType.heal;
            base.onEnemy = false;
            base.lvl = lvl;
        }

        public override void Use(ref List<Unit> allys, ref List<Unit> enemies, ref Unit u1, ref Unit u2)
        {
            double newhp = u2.hp + 15 * Math.Pow(lvl, 1.2);
            u2.hp = (newhp > u2.maxhp) ? u2.maxhp : newhp;
        }
    }
}
