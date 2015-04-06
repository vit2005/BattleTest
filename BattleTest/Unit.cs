using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTest
{
    class Unit
    {
        int ATK;
        int DEF;
        int AGI;

        float hp;
        float damage;
        float block;
        float crit;
        float dodge;

        public Unit(int atk, int def, int agi)
        {
            ATK = atk;
            DEF = def;
            AGI = agi;

            hp = 100 + 15 * DEF;
            damage = 15 + 2 * ATK;
            block = 100 / DEF;
            crit = 100 / AGI;
            dodge = 100 / AGI;
        }
    }
}
