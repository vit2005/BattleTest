using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class Unit : ICloneable
    {
        public int ATK;
        public int DEF;
        public int AGI;

		public int points;
        public string utitle;

        public double hp;
        public double maxhp;
        public double damage;
        public double block;
        public double crit;
        public double dodge;

        public bool isAlive;
        public bool canMove;

        public List<IEffect> Effects;
        public List<Item> Items;

        public Unit(int atk, int def, int agi, int _points, string _title = "", bool _isAlive = true)
        {
            ATK = atk;
            DEF = def;
            AGI = agi;
			
			points = _points;
            utitle = _title;
            Items = new List<Item>();

            Calc();

            isAlive = _isAlive;
        }

        public void Calc()
        {
            isAlive = true;
            canMove = true;
            Effects = new List<IEffect>();

            int bATK = 0, bDEF = 0, bAGI = 0;

            foreach(Item i in Items)
            {
                switch(i.atribute)
                {
                    case (Item.Atribute.ATK):
                        bATK += i.amount;
                        break;
                    case (Item.Atribute.DEF):
                        bDEF += i.amount;
                        break;
                    case (Item.Atribute.AGI):
                        bAGI += i.amount;
                        break;
                }
            }

            //hp = (int)(100 + 30 * Math.Pow(DEF, 1.2) + 80 * Math.Pow(AGI, 0.6));
            //damage = 15 + 5 * Math.Pow(ATK, 1.2) + 10 * Math.Pow(AGI, 0.7);
			hp = (int)(50 + 15 * Math.Pow(DEF + bDEF, 1.4) + 30 * Math.Pow(AGI + bAGI, 0.7));
            damage = 40 + (double) 8 * Math.Pow(ATK + bATK, 1.2) + (double) 10 * Math.Pow(AGI + bAGI, 0.7);
			maxhp = hp;

            //block = 0;
            //if ((ATK + AGI == 0) || (ATK + AGI < DEF))
            //    block = (DEF > 17) ? 55 : DEF;
            //else
			//block = (double)DEF / (double)((ATK + AGI) * 55);

            //dodge = 0;
            //if ((ATK + DEF == 0) || (ATK + DEF < Math.Pow(AGI, 1.2)))
            //    dodge = (Math.Sqrt(AGI * 17) + 15 > 55) ? 55 : Math.Sqrt(AGI * 17) + 15;
            //else
			//dodge = (double)(Math.Sqrt(AGI * 17) + 15) / (double)((ATK + DEF) * 55);
			block = Math.Sqrt(DEF + bDEF);
			dodge = Math.Sqrt(AGI + bAGI);
		
            crit = dodge;
        }

        public object Clone()
        {
            Unit n = new Unit(ATK,DEF,AGI,points);
			n.Calc ();
            return n;
        }
    }
