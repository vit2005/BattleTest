using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SingleBattleTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Unit> team1 = new List<Unit>();
        static List<Unit> team2 = new List<Unit>();
        int count = 0;
        static bool shff = true;
        static int max = 0;
        static string text = "";
        List<string> skills = new List<string>() { "skill_1", "skill_2", "skill_3", "skill_4", "skill_5", "skill_6", "skill_7", "skill_8", "skill_9", "skill_10", "skill_11", "skill_12", "skil_l3", "skil_l4", "skil_l5", "skill_16" };

        public MainWindow()
        {
            InitializeComponent();
            FillSkills();
        }

        void FillSkills()
        {
            foreach(string skill in skills)
            {
                SkillsCombobox.Items.Add(skill);
            }
        }



        #region 123

        private void TextChanged1(object sender, TextChangedEventArgs e)
        {
            try
            {
                SUM1.Text = (Convert.ToInt32(ATK1.Text) + Convert.ToInt32(DEF1.Text) + Convert.ToInt32(AGI1.Text)).ToString();
            }
            catch { }

        }

        private void TextChanged2(object sender, TextChangedEventArgs e)
        {
            try
            {
                SUM2.Text = (Convert.ToInt32(ATK2.Text) + Convert.ToInt32(DEF2.Text) + Convert.ToInt32(AGI2.Text)).ToString();
            }
            catch { }
        }

        private void TextChanged3(object sender, TextChangedEventArgs e)
        {
            try
            {
                SUM3.Text = (Convert.ToInt32(ATK3.Text) + Convert.ToInt32(DEF3.Text) + Convert.ToInt32(AGI3.Text)).ToString();
            }
            catch { }
        }

        private void TextChanged1_Copy(object sender, TextChangedEventArgs e)
        {
            try
            {
                SUM1_Copy.Text = (Convert.ToInt32(ATK1_Copy.Text) + Convert.ToInt32(DEF1_Copy.Text) + Convert.ToInt32(AGI1_Copy.Text)).ToString();
            }
            catch { }
        }

        private void TextChanged2_Copy(object sender, TextChangedEventArgs e)
        {
            try
            {
                SUM2_Copy.Text = (Convert.ToInt32(ATK2_Copy.Text) + Convert.ToInt32(DEF2_Copy.Text) + Convert.ToInt32(AGI2_Copy.Text)).ToString();
            }
            catch { }
        }

        private void TextChanged3_Copy(object sender, TextChangedEventArgs e)
        {
            try
            {
                SUM3_Copy.Text = (Convert.ToInt32(ATK3_Copy.Text) + Convert.ToInt32(DEF3_Copy.Text) + Convert.ToInt32(AGI3_Copy.Text)).ToString();
            }
            catch { }
        }

        private void TextChanged4(object sender, TextChangedEventArgs e)
        {
            try
            {
                SUM4.Text = (Convert.ToInt32(SUM1.Text) + Convert.ToInt32(SUM2.Text) + Convert.ToInt32(SUM3.Text)).ToString();
            }
            catch { }
        }

        private void TextChanged4_Copy(object sender, TextChangedEventArgs e)
        {
            try
            {
                SUM4_Copy.Text = (Convert.ToInt32(SUM1_Copy.Text) + Convert.ToInt32(SUM2_Copy.Text) + Convert.ToInt32(SUM3_Copy.Text)).ToString();
            }
            catch { }
        }

        #endregion
        #region azaza

        private void Remove1_Click(object sender, RoutedEventArgs e)
        {
            SkillsList1.Items.Remove(SkillsList1.SelectedItem);
        }

        private void Remove2_Click(object sender, RoutedEventArgs e)
        {
            SkillsList2.Items.Remove(SkillsList2.SelectedItem);
        }

        private void Remove3_Click(object sender, RoutedEventArgs e)
        {
            SkillsList3.Items.Remove(SkillsList3.SelectedItem);
        }

        private void Add1_Click(object sender, RoutedEventArgs e)
        {
            if ((SkillsCombobox.SelectedItem.ToString() != "") && (SkillsList1.Items.Count < 4) && (SkillsList1.Items.IndexOf(SkillsCombobox.SelectedItem) == -1))
                SkillsList1.Items.Add(SkillsCombobox.SelectedItem.ToString());
        }

        private void Add2_Click(object sender, RoutedEventArgs e)
        {
            if ((SkillsCombobox.SelectedItem.ToString() != "") && (SkillsList2.Items.Count < 4) && (SkillsList2.Items.IndexOf(SkillsCombobox.SelectedItem) == -1))
                SkillsList2.Items.Add(SkillsCombobox.SelectedItem.ToString());
        }

        private void Add3_Click(object sender, RoutedEventArgs e)
        {
            if ((SkillsCombobox.SelectedItem.ToString() != "") && (SkillsList3.Items.Count < 4) && (SkillsList3.Items.IndexOf(SkillsCombobox.SelectedItem) == -1))
                SkillsList3.Items.Add(SkillsCombobox.SelectedItem.ToString());
        }

        #endregion

        private void GenerateUnit_Click(object sender, RoutedEventArgs e)
        {
            GenerateEnemy();
        }

        private void GenerateEnemy()
        {
            Random r = new Random();
            Thread.Sleep(3);
            SUM4_Copy.Text = SUM4.Text;
            max = Convert.ToInt32(SUM4_Copy.Text);
            int sum1, sum2, sum3, atk1, atk2, atk3, def1, def2, def3, agi1, agi2, agi3;
            sum1 = (int)max / 3;
            sum2 = sum1;
            sum3 = max - sum1 - sum2;
            atk1 = r.Next(sum1);
            Thread.Sleep(3);
            def1 = r.Next(sum1 - atk1);
            agi1 = sum1 - atk1 - def1;
            Thread.Sleep(3);
            atk2 = r.Next(sum2);
            Thread.Sleep(3);
            def2 = r.Next(sum2 - atk2);
            agi2 = sum2 - atk2 - def2;
            Thread.Sleep(3);
            atk3 = r.Next(sum3);
            Thread.Sleep(3);
            def3 = r.Next(sum3 - atk3);
            agi3 = sum3 - atk3 - def3;

            SUM1_Copy.Text = sum1.ToString();
            SUM2_Copy.Text = sum2.ToString();
            SUM3_Copy.Text = sum3.ToString();
            ATK1_Copy.Text = atk1.ToString();
            DEF1_Copy.Text = def1.ToString();
            AGI1_Copy.Text = agi1.ToString();
            ATK2_Copy.Text = atk2.ToString();
            DEF2_Copy.Text = def2.ToString();
            AGI2_Copy.Text = agi2.ToString();
            ATK3_Copy.Text = atk3.ToString();
            DEF3_Copy.Text = def3.ToString();
            AGI3_Copy.Text = agi3.ToString();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ATK1.Text = "0";
            ATK2.Text = "0";
            ATK3.Text = "0";
            DEF1.Text = "0";
            DEF2.Text = "0";
            DEF3.Text = "0";
            AGI1.Text = "0";
            AGI2.Text = "0";
            AGI3.Text = "0";
            ATK1_Copy.Text = "0";
            ATK2_Copy.Text = "0";
            ATK3_Copy.Text = "0";
            DEF1_Copy.Text = "0";
            DEF2_Copy.Text = "0";
            DEF3_Copy.Text = "0";
            AGI1_Copy.Text = "0";
            AGI2_Copy.Text = "0";
            AGI3_Copy.Text = "0";
            SUM1_Copy.Text = "0";
            SUM2_Copy.Text = "0";
            SUM3_Copy.Text = "0";
            SUM4_Copy.Text = "0";
            text = "";
            RefreshTeams();
        }

        private void RefreshTeams()
        {
            team1 = new List<Unit>();
            team2 = new List<Unit>();

            team1.Add(new Unit(Convert.ToInt16(ATK1.Text), Convert.ToInt16(DEF1.Text), Convert.ToInt16(AGI1.Text)));
            team1.Add(new Unit(Convert.ToInt16(ATK2.Text), Convert.ToInt16(DEF2.Text), Convert.ToInt16(AGI2.Text)));
            team1.Add(new Unit(Convert.ToInt16(ATK3.Text), Convert.ToInt16(DEF3.Text), Convert.ToInt16(AGI3.Text)));
            team2.Add(new Unit(Convert.ToInt16(ATK1_Copy.Text), Convert.ToInt16(DEF1_Copy.Text), Convert.ToInt16(AGI1_Copy.Text)));
            team2.Add(new Unit(Convert.ToInt16(ATK2_Copy.Text), Convert.ToInt16(DEF2_Copy.Text), Convert.ToInt16(AGI2_Copy.Text)));
            team2.Add(new Unit(Convert.ToInt16(ATK3_Copy.Text), Convert.ToInt16(DEF3_Copy.Text), Convert.ToInt16(AGI3_Copy.Text)));
        }

        private List<Unit> GenerateEnemyTeam()
        {
            List<Unit> team2 = new List<Unit>();
            Random r = new Random();
            int sum1, sum2, sum3, atk1, atk2, atk3, def1, def2, def3, agi1, agi2, agi3;
            sum1 = (int)max / 3;
            sum2 = sum1;
            sum3 = max - sum1 - sum2;
            atk1 = r.Next(sum1);
            Thread.Sleep(3);
            def1 = r.Next(sum1 - atk1);
            agi1 = sum1 - atk1 - def1;
            Thread.Sleep(3);
            atk2 = r.Next(sum2);
            Thread.Sleep(3);
            def2 = r.Next(sum2 - atk2);
            agi2 = sum2 - atk2 - def2;
            Thread.Sleep(3);
            atk3 = r.Next(sum3);
            Thread.Sleep(3);
            def3 = r.Next(sum3 - atk3);
            agi3 = sum3 - atk3 - def3;

            team2.Add(new Unit(atk1, def1, agi1));
            team2.Add(new Unit(atk2, def2, agi2));
            team2.Add(new Unit(atk3, def3, agi3));

            return team2;
        }

        private List<List<string>> GetSkillsSet()
        {
            List<List<string>> skillsSet = new List<List<string>>();
            List<string> skills = new List<string>();
            foreach (object s in SkillsList1.Items)
            {
                skills = new List<string>();
                skills.Add(s.ToString());
            }
            skillsSet.Add(skills);
            foreach (object s in SkillsList2.Items)
            {
                skills = new List<string>();
                skills.Add(s.ToString());
            }
            skillsSet.Add(skills);
            foreach (object s in SkillsList3.Items)
            {
                skills = new List<string>();
                skills.Add(s.ToString());
            }
            skillsSet.Add(skills);
            return skillsSet;
        }

        private void Fight_Click(object sender, RoutedEventArgs e)
        {
            if ((SkillsList1.Items.Count < 4) || (SkillsList2.Items.Count < 4) || (SkillsList3.Items.Count < 4))
            {
                MessageBox.Show("Fill skills");
                return;
            }
            if (SUM4_Copy.Text == "0")
                GenerateEnemy();
            RefreshTeams();

            BattleWindow battle = new BattleWindow(team1, team2, GetSkillsSet());
            battle.Show();
        }

    }
}
