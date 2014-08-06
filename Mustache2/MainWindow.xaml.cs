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

namespace Mustache2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        menuGUI gameMenu;




        public MainWindow()
        {
            InitializeComponent();
        }

        private void hide_start_buttons()
        {
            button_Worlds.Visibility = Visibility.Collapsed;
            button_Endless.Visibility = Visibility.Collapsed;
            button_leaderboard.Visibility = Visibility.Collapsed;
        }


        private void event_menu_click(object sender, RoutedEventArgs e)
        {
            var parent = sender as Button;
            string parent_tag = parent.Tag.ToString();
            

            if(parent_tag == "Worlds")
            {
                //MessageBox.Show("You clicked Worlds");

                hide_start_buttons();
                gameMenu = new menuGUI(this, 1);

            }
            else if(parent_tag == "Endless")
            {
                MessageBox.Show("You clicked Endless");
            }
            else if(parent_tag == "Leaderboard")
            {
                MessageBox.Show("You clicked Leaderboard");
            }
            else
            {
                MessageBox.Show("No button Clicked");
            }
        }
    }

}
