using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleBattleTest
{
    public enum skillType
    {
        attack = 1,
        heal = 2,
        debuff = 3,
        buff = 4,
        control = 5,
        summon = 6
    };
    public abstract class ISkill
    {
        public string name;
        public string id;
        public string description;
        public skillType type;
        public byte lvl;
        public bool onEnemy;

        public abstract void Use(ref List<Unit> allys, ref List<Unit> enemies, ref Unit u1, ref Unit u2);
    }
}
