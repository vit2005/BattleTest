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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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

        private void GenerateUnit_Click(object sender, RoutedEventArgs e)
        {
            Random r1 = new Random();
            Random r2 = new Random();
            Random r3 = new Random();
            SUM4_Copy.Text = SUM4.Text;
            int max = Convert.ToInt32(SUM4_Copy.Text);
            int sum1, sum2, sum3, atk1,atk2,atk3,def1,def2,def3,agi1,agi2,agi3;
            sum1 = r1.Next(max);            SUM1_Copy.Text = sum1.ToString();
            sum2 = r2.Next(max - sum1);     SUM2_Copy.Text = sum2.ToString();
            sum3 = max - sum1 - sum2;       SUM3_Copy.Text = sum3.ToString();
            atk1 = r1.Next(sum1);           ATK1_Copy.Text = atk1.ToString();
            def1 = r1.Next(sum1 - atk1);    DEF1_Copy.Text = def1.ToString();
            agi1 = sum1 - atk1 - def1;      AGI1_Copy.Text = agi1.ToString();
            atk2 = r2.Next(sum2);           ATK2_Copy.Text = atk2.ToString();
            def2 = r2.Next(sum2 - atk2);    DEF2_Copy.Text = def2.ToString();
            agi2 = sum2 - atk2 - def2;      AGI2_Copy.Text = agi2.ToString();
            atk3 = r3.Next(sum3);           ATK3_Copy.Text = atk3.ToString();
            def3 = r3.Next(sum3 - atk3);    DEF2_Copy.Text = def2.ToString();
            agi3 = sum3 - atk3 - def3;      AGI3_Copy.Text = agi3.ToString();
        }

    }
}
