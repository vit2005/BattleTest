using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace exp_test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> lvl_exp;
        void azaza()
        {
            lvl_exp = new List<int>();
            lvl_exp.Add(0);
            for (int i = 1; i < 200; i++)
            {
                lvl_exp.Add((int)((double)Math.Pow(i, 2.67) * (double)100));
            }
        }

        void calcLvlExp()
        {
            lvlexp.Text = lvl_exp[Convert.ToInt32(lvl.Text)].ToString();
        }
        public MainWindow()
        {
            InitializeComponent();
            azaza();
            
        }

        private void CalcLvLExp_Click(object sender, RoutedEventArgs e)
        {
            calcLvlExp();
        }

        void CalcMatch(int dangeon_lvl)
        {
            int player_lvl = Convert.ToInt32(lvl.Text);
            int current_exp = Convert.ToInt32(exp.Text);

            int expected_player_lvl = (int)(player_lvl / 10) + 1;

            int added_exp = (int)(((double)Math.Pow(player_lvl, 1.67) * (double)50) * Math.Pow(10, (dangeon_lvl - expected_player_lvl)));
            int result_exp = current_exp + added_exp;


            exp.Text = result_exp.ToString();
        }


        #region sadfasdfa
        private void w1_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(1);
        }
        private void w2_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(2);
        }
        private void w3_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(3);
        }
        private void w4_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(4);
        }
        private void w5_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(5);
        }
        private void w6_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(6);
        }
        private void w7_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(7);
        }
        private void w8_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(8);
        }
        private void w9_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(9);
        }
        private void w10_Click(object sender, RoutedEventArgs e)
        {
            CalcMatch(10);
        }

        #endregion
    }
}
