using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BattleTest;

namespace SingleBattleTest
{
    /// <summary>
    /// Логика взаимодействия для BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        List<Unit> allys;
        List<Unit> enemies;
        Unit Choosen;
        Unit CurrentMoveUnit;
        List<Rectangle> allyRectangles;
        List<Rectangle> enemyRectangles;
        List<Rectangle> allRectangles = new List<Rectangle>();

        public BattleWindow(List<Unit> _ally, List<Unit> _enemies, List<List<string>> skills)
        {
            allys = _ally;
            enemies = _enemies;
            InitializeComponent();

            setRectangles();
            colorize();
            fillHP();
        }

        #region Initialization
        private void setRectangles()
        {
            CurrentMoveUnit = allys[0];
            ally1.StrokeThickness = 3;
            allyRectangles = new List<Rectangle>();
            ally1.Tag = allys[0];
            allyRectangles.Add(ally1);
            ally2.Tag = allys[1];
            allyRectangles.Add(ally2);
            ally3.Tag = allys[2];
            allyRectangles.Add(ally3);
            enemyRectangles = new List<Rectangle>();
            enemy1.Tag = enemies[0];
            enemyRectangles.Add(enemy1);
            enemy2.Tag = enemies[1];
            enemyRectangles.Add(enemy2);
            enemy3.Tag = enemies[2];
            enemyRectangles.Add(enemy3);
            foreach (Rectangle r in allyRectangles)
                allRectangles.Add(r);
            foreach (Rectangle r in enemyRectangles)
                allRectangles.Add(r);
        }

        private void colorize()
        {
            for (int i = 0; i < allys.Count; i++)
            {
                double max = Math.Max(Math.Max(allys[i].ATK, allys[i].DEF), allys[i].AGI);
                if (max != 0)
                {
                    byte r = Convert.ToByte(((double)allys[i].ATK / max) * 255);
                    byte g = Convert.ToByte(((double)allys[i].AGI / max) * 255);
                    byte b = Convert.ToByte(((double)allys[i].DEF / max) * 255);
                    allyRectangles[i].Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                }

            }

            for (int i = 0; i < enemies.Count; i++)
            {
                double max = Math.Max(Math.Max(enemies[i].ATK, enemies[i].DEF), enemies[i].AGI);
                if (max != 0)
                {
                    byte r = Convert.ToByte((double)enemies[i].ATK / max * 255);
                    byte g = Convert.ToByte((double)enemies[i].AGI / max * 255);
                    byte b = Convert.ToByte((double)enemies[i].DEF / max * 255);
                    enemyRectangles[i].Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                }
            }
        }

        private void fillHP()
        {
            ally1HP.Content = allys[0].hp.ToString();
            ally2HP.Content = allys[1].hp.ToString();
            ally3HP.Content = allys[2].hp.ToString();
            enemy1HP.Content = enemies[0].hp.ToString();
            enemy2HP.Content = enemies[1].hp.ToString();
            enemy3HP.Content = enemies[2].hp.ToString();
        }

        #endregion

        private void refreshRectangles()
        {
            foreach (Rectangle r in allRectangles)
                r.StrokeThickness = 1;
            allRectangles.First<Rectangle>(x => x.Tag == CurrentMoveUnit).StrokeThickness = 3;
        }

        private void setChosen(object sender, MouseButtonEventArgs e)
        {
            refreshRectangles();
            Choosen = (Unit)(sender as Rectangle).Tag;

            AutoAttack.IsEnabled = enemies.Contains(Choosen) && Choosen.isAlive;
            (sender as Rectangle).StrokeThickness = 5;
        }

        private void makeAllSkillsDisable()
        {
            AutoAttack.IsEnabled = false;
            UseSkill1.IsEnabled = false;
            UseSkill2.IsEnabled = false;
            UseSkill3.IsEnabled = false;
            UseSkill4.IsEnabled = false;
        }

        #region Fight
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
                    return _team2 == enemies;
                Unit ally = u;
                SingleFight(ref ally, ref enemy);
            }
            if (alive == 0)
                return allys == _team2;
            return null;
        }

        private void SingleFight(ref Unit u1, ref Unit u2)
        {
            u1.canMove = false;
            System.Threading.Thread.Sleep(3);
            Random r = new Random();
            if (u2.dodge > r.Next(100))
            {
                Log.Text += "\nu2 dodged";
                return;
            }

            System.Threading.Thread.Sleep(3);
            if (u2.block > r.Next(100))
            {
                Log.Text += "\nu2 blocked";
                return;
            }

            System.Threading.Thread.Sleep(3);
            int crit = 1;
            if (u1.crit > r.Next(100))
            {
                Log.Text += "\nu1 did crit";
                crit = 2;
            }

            u2.hp -= (int)(u1.damage * crit * ((100 - u2.dodge) / 100));
            if (u2.hp <= 0)
            {
                u2.isAlive = false;
                u2.hp = 0;
            }
            Log.Text += string.Format("\nu1 hit [{0}]_HP. u1.hp={1}. u2.hp={2}", (int)(u1.damage * crit * ((100 - u2.dodge) / 100)), u1.hp, u2.hp);
        }

        private Unit ChooseEnemy(List<Unit> team)
        {
            List<Unit> alive = new List<Unit>();
            foreach (Unit u in team)
            {
                if (u.isAlive && u.hp > 0)
                    alive.Add(u);
            }
            if (alive.Count == 0)
                return null;
            if (alive.Count == 1)
                return alive[0];
            //double minhp = alive[0].hp;
            //foreach (Unit u in alive)
            //{
            //    if (u.hp < minhp)
            //        minhp = u.hp;
            //}
            //return alive.First<Unit>(x => x.hp == minhp);
            double maxdmg = alive[0].damage;
            foreach (Unit u in alive)
            {
                if (u.damage > maxdmg)
                    maxdmg = u.damage;
            }
            return alive.First<Unit>(x => x.damage == maxdmg);
        }

        private void AutoAttack_Click(object sender, RoutedEventArgs e)
        {
            DoAllyMove("AutoAttack");
        }

        private void DoAllyMove(string usedMove)
        {
            makeAllSkillsDisable();

            if (usedMove == "AutoAttack")
            {
                SingleFight(ref CurrentMoveUnit, ref Choosen);
                if (enemies.FirstOrDefault<Unit>(x => x.isAlive == true) == null )
                {
                    MessageBox.Show("Victory");
                    this.Close();
                    return;
                }
                Unit CurrentMoveUnitTemp = allys.FirstOrDefault<Unit>(x => x.canMove && x.isAlive);
                if (CurrentMoveUnitTemp == null)
                {
                    botMove();
                }
                else
                {
                    CurrentMoveUnit = CurrentMoveUnitTemp;
                }
            }

            fillHP();
            refreshRectangles();
        }

        private void botMove()
        {
            Log.Text += "\n=== Enemy turn ===";
            bool? result = SingleTeamFight(enemies, allys);
            if ((result != null) && (!result.Value))
            {
                MessageBox.Show("Defeat");
                this.Close();
                return;
            }
            Log.Text += "\n=== Your turn ===";
            foreach (Unit u in allys)
                u.canMove = true;
            CurrentMoveUnit = allys.First<Unit>(x => x.isAlive);
        }

        #endregion

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentMoveUnit.canMove = false;
            Unit CurrentMoveUnitTemp = allys.FirstOrDefault<Unit>(x => x.canMove && x.isAlive);
            if (CurrentMoveUnitTemp == null)
                botMove();
            else
                CurrentMoveUnit = CurrentMoveUnitTemp;
            fillHP();
            refreshRectangles();
        }

        private void SurrenderButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
