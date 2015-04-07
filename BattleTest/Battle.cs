using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTest
{
    class Battle
    {
        List<Unit> team1 = new List<Unit>();
        List<Unit> team2 = new List<Unit>();

        public string log;

        public Battle (List<Unit> _team1, List<Unit> _team2)
        {
            foreach (Unit u in _team1)
            {
                team1.Add(u.Clone() as Unit);
            }
            foreach (Unit u in _team2)
            {
                team2.Add(u.Clone() as Unit);
            }
        }

        public bool DoBattle()
        {
            bool? result = null;

            while (result == null)
            {
                log += string.Format("\n=== {0} {1} {2} VS {3} {4} {5} ===", team1[0].hp, team1[1].hp, team1[2].hp, team2[0].hp, team2[1].hp, team2[2].hp);
                result = SingleTeamFight(team1, team2);
                result = SingleTeamFight(team2, team1);
            }
            log += "\nResult = " + result.ToString();
            return result.Value;
        }

        private bool? SingleTeamFight(List<Unit> _team1, List<Unit> _team2)
        {
            
            byte alive = 3;
            foreach (Unit u in _team1)
            {
                if (!u.isAlive)
                {
                    alive--;
                    continue;
                }
                Unit enemy = ChooseEnemy(_team2);
                if (enemy == null)
                    return _team2 == team2;
                Unit ally = u;
                SingleFight(ref ally, ref enemy);
            }
            if (alive == 0)
                return team1 == _team2;
            return null;
        }

        private void SingleFight(ref Unit u1, ref Unit u2)
        {
            System.Threading.Thread.Sleep(3);
            Random r = new Random();
            if (u2.dodge > r.Next(100))
            {
                log += "(dodge)";
                return;
            }
                
            System.Threading.Thread.Sleep(3);
            if (u2.block > r.Next(100))
            {
                log += "(block)";
                return;
            }
                
            System.Threading.Thread.Sleep(3);
            int crit = 1;
            if (u1.crit > r.Next(100))
            {
                log += "(crit)";
                crit = 2;
            }
                
            u2.hp -= (int) (u1.damage * crit * ((100 - u2.dodge) / 100));
            if (u2.hp <= 0)
            {
                u2.isAlive = false;
                u2.hp = 0;
            }
                
        }

        private Unit ChooseEnemy(List<Unit> team)
        {
            List<Unit> alive = new List<Unit>();
            foreach(Unit u in team)
            {
                if (u.isAlive && u.hp > 0)
                    alive.Add(u);
            }
            if (alive.Count == 0)
                return null;
            if (alive.Count == 1)
                return alive[0];
            double minhp = alive[0].hp;
            foreach (Unit u in alive)
            {
                if (u.hp < minhp)
                    minhp = u.hp;
            }
            return alive.First<Unit>(x => x.hp == minhp);
        }
    }
}
