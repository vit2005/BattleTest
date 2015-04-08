using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTest
{
    public class Unit : ICloneable
    {
        public int ATK;
        public int DEF;
        public int AGI;

        public double hp;
        public double damage;
        public double block;
        public double crit;
        public double dodge;

        public bool isAlive;

        public Unit(int atk, int def, int agi)
        {
            ATK = atk;
            DEF = def;
            AGI = agi;

            hp = (int)(100 + 30 * Math.Pow(DEF, 1.2) + 80 * Math.Pow(AGI, 0.6));
            damage = 15 + 5 * Math.Pow(ATK, 1.2) + 10 * Math.Pow(AGI, 0.7);

            block = 0;
            if ((ATK + AGI == 0) || (ATK + AGI < DEF))
                block = (DEF > 17)? 55 : DEF;
            else
                block = DEF / (ATK + AGI) * 55;

            dodge = 0;
            if ((ATK + DEF == 0) || (ATK + DEF < Math.Pow(AGI, 1.2)))
                dodge = (Math.Sqrt(AGI * 17) + 15 > 55) ? 55 : Math.Sqrt(AGI * 17) + 15;
            else
                dodge = (Math.Sqrt(AGI * 17) + 15) / (ATK + DEF) * 55;

            crit = dodge;
            isAlive = true;
        }

        public object Clone()
        {
            Unit n = new Unit(ATK,DEF,AGI);
            n.hp = hp;
            n.damage = damage;
            n.block = block;
            n.crit = crit;
            n.dodge = dodge;
            return n;
        }
    }
}
