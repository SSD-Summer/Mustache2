using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mustache2
{
    class menuGUI
    {
        //Panels for worlds
        System.Windows.Controls.StackPanel panel_World1, panel_world2, panel_world3, panel_world4;
        //Lists of sub-worlds for each world
        List<System.Windows.Controls.Button> button_list_world1, button_list_world2, button_list_world3, button_list_world4;
        //Dimensions for gui sizing --- totalX == width of entire window, totalY == height of entire window
        double dimension_totalX, dimension_totalY;
        //this will hold a reference to the main window's grid
        //If we can find a better way to do this that would probably be better becase
        System.Windows.Controls.Grid win;
        int counttest;

        menuGUI()
        {
        }
        /// <summary>
        /// Constructor that inializes the menu based on start screen selection
        /// </summary>
        /// <param name="cur"> The current main window, should just pass 'this'</param>
        /// <param name="mode"> 1 == Worlds, 2 == Endless, ....</param>
        public menuGUI(MainWindow cur, int mode)
        {
            win = cur.Root_Window;
            dimension_totalX = cur.Width;
            dimension_totalY = cur.Height;
            if (mode == 1)
            {
                initWorldMenu();
            }
            else if (mode == 2)
            {
            }
        }

        public void initWorldMenu()
        {
            //Just showing one world now just to get something working and until we finalize how exactly we want to display the worlds
            panel_World1 = new System.Windows.Controls.StackPanel();
            panel_World1.Width = dimension_totalX;
            panel_World1.Height = dimension_totalY;
            win.Children.Add(panel_World1);
            for (int i = 0; i < 3; ++i)
            {
                System.Windows.Controls.Button butt = new System.Windows.Controls.Button();
                butt.Name = "button_world_1_" + (i + 1);
                butt.Tag = i;
                butt.Content = "World 1 - " + (i + 1);
                butt.Width = 75;
                butt.Click += new System.Windows.RoutedEventHandler(event_gui_click);
                panel_World1.Children.Add(butt);
            }
        }

        private void hideWorlds()
        {
            panel_World1.Visibility = System.Windows.Visibility.Collapsed;
            panel_world2.Visibility = System.Windows.Visibility.Collapsed;
            panel_world3.Visibility = System.Windows.Visibility.Collapsed;
            panel_world4.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void event_gui_click(object sender, EventArgs e)
        {
        }
        //Just an example of how to setup a timer event using the current gameTimer class
        //public void timer_tick(object sender, EventArgs e)
        //{
        // var time = sender as DispatcherTimer;
        // if(Convert.ToInt32(time.Tag) < 0)
        // {
        // time.Stop();
        // }
        // else
        // {
        // time.Tag = Convert.ToInt32(time.Tag) - 1;
        // }
        //
        // * event code here *
        //}
    }
}
