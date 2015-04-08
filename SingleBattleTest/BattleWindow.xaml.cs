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
        List<Unit> ally;
        List<Unit> enemies;
        Unit Choosen;
        Unit CurrentMoveUnit;
        List<Rectangle> allyRectangles;
        List<Rectangle> enemyRectangles;
        List<Rectangle> allRectangles = new List<Rectangle>();

        public BattleWindow(List<Unit> _ally, List<Unit> _enemies, List<List<string>> skills)
        {
            ally = _ally;
            enemies = _enemies;
            InitializeComponent();
            CurrentMoveUnit = _ally[0];
            ally1.StrokeThickness = 3;
            allyRectangles = new List<Rectangle>();
            ally1.Tag = _ally[0];
            allyRectangles.Add(ally1);
            ally2.Tag = _ally[1];
            allyRectangles.Add(ally2);
            ally3.Tag = _ally[2];
            allyRectangles.Add(ally3);
            enemyRectangles = new List<Rectangle>();
            enemy1.Tag = _enemies[0];
            enemyRectangles.Add(enemy1);
            enemy2.Tag = _enemies[1];
            enemyRectangles.Add(enemy2);
            enemy3.Tag = _enemies[2];
            enemyRectangles.Add(enemy3);
            foreach (Rectangle r in allyRectangles)
                allRectangles.Add(r);
            foreach (Rectangle r in enemyRectangles)
                allRectangles.Add(r);


            colorize();
        }

        void colorize()
        {
            for (int i = 0; i < ally.Count; i++)
            {
                double max = Math.Max(Math.Max(ally[i].ATK, ally[i].DEF), ally[i].AGI);
                if (max != 0)
                {
                    byte r = Convert.ToByte(((double)ally[i].ATK / max) * 255);
                    byte g = Convert.ToByte(((double)ally[i].AGI / max) * 255);
                    byte b = Convert.ToByte(((double)ally[i].DEF / max) * 255);
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

        private void setChosen(object sender, MouseButtonEventArgs e)
        {
            foreach (Rectangle r in allRectangles)
                r.StrokeThickness = 1;
            allRectangles.First<Rectangle>(x => x.Tag == CurrentMoveUnit).StrokeThickness = 3;
            Choosen = (Unit)(sender as Rectangle).Tag;

            AutoAttack.IsEnabled = enemies.Contains(Choosen);
            (sender as Rectangle).StrokeThickness = 5;
        }


    }
}
