using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleBattleTest
{
    public class Unit : ICloneable
    {
        public int ATK;
        public int DEF;
        public int AGI;

        public double hp;
        public double maxhp;
        public double damage;
        public double block;
        public double crit;
        public double dodge;

        public bool isAlive;
        public bool canMove;

        public List<IEffect> Effects;

        public Unit(int atk, int def, int agi)
        {
            ATK = atk;
            DEF = def;
            AGI = agi;

            Calc();

            isAlive = true;
            canMove = true;
            Effects = new List<IEffect>();
        }

        public void Calc()
        {
            isAlive = true;
            canMove = true;
            Effects = new List<IEffect>();

            //hp = (int)(100 + 30 * Math.Pow(DEF, 1.2) + 80 * Math.Pow(AGI, 0.6));
            hp = (int)(50 + 15 * Math.Pow(DEF, 1.4) + 30 * Math.Pow(AGI, 0.7));
            maxhp = hp;
            //damage = 15 + 5 * Math.Pow(ATK, 1.2) + 10 * Math.Pow(AGI, 0.7);
            damage = 40 + 8 * Math.Pow(ATK, 1.2) + 10 * Math.Pow(AGI, 0.7);

            //block = 0;
            block = Math.Sqrt(AGI * 5);

            //if ((ATK + AGI == 0) || (ATK + AGI < DEF))
            //    block = (DEF > 17) ? 25 : DEF;
            //else
            //    block = (double)DEF / (double)((ATK + AGI) * 55);

            //dodge = 0;
            dodge = Math.Sqrt(AGI * 15);

            //if ((ATK + DEF == 0) || (ATK + DEF < Math.Pow(AGI, 1.2)))
            //    dodge = (Math.Sqrt(AGI * 17) + 15 > 55) ? 55 : Math.Sqrt(AGI * 17) + 15;
            //else
            //    dodge = (double)(Math.Sqrt(AGI * 17) + 15) / (double)((ATK + DEF) * 55);

            crit = dodge * 2;
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
