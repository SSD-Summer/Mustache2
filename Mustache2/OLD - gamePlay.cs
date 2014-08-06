using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mustashe_ic
{
    /// <summary>
    /// Holds all the form controls and game items for playing.
    /// </summary>
    internal class gamePlay
    {
        //Tuple<tileClass, int, int>[,] board;
        static tileClass[,] board; //2D array of tileClass used to display 
        
        int n; //Defines the board as an N by N 
        int mode; //0 for endless - 1 - 6? for worlds
        int sub_mode; // 1 - for easiest  --- 10? for hardest

        //Tile dimensions -- Dependent on the size of the board and number of tiles generated
        int xTileDim;
        int yTileDim;

        public static int score;
        public static int lives;
        public int timer { set; get; } //Probably will change to helper class

        public static int correctTileSelected;
        public static int totalTileSelected;

        private static int count; //This is the variable used to keep track of how often to hide a tile
        private static int hide_speed = 2; //ints used as random vars

        //int image_num;//uses rand to select a random image number
        //int num, min_val, max_val;//integers for random number generator
        private static Random rand; //Random generator - Will probably move
       
        private static Queue<Tuple<int, int>> hiddenList; //Used as holder for hidden tiles - Stores x and y coordinate of tile in tuple

        public static System.Windows.Forms.Label label_timer, label_score;
        public static System.Windows.Forms.FlowLayoutPanel fpanel_Lives;
        public System.Windows.Forms.Panel panel_tile_holder;




        /// <summary>
        /// Initalizes an instance of the game
        /// </summary>
        /// <param name="g"> The form the game will be played on.</param>
        /// <param name="size">The number of tiles that the board will have. n x n </param>
        /// <param name="mode">Specifys the mode of play. World(world 1, 2, 3, ....) or Endless</param>
        public gamePlay(gameMain g, int size, int world, int sub_world)
        {
            score = 0; //beginning score to zero
            timer = 30; // Starting time 30 secs
            lives = 2;
            n = size; //size of tile board - nxn

            
            
            ////Lives label generation 
            fpanel_Lives = new System.Windows.Forms.FlowLayoutPanel();
            fpanel_Lives.Location = new Point(1, 1);
            fpanel_Lives.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));
            fpanel_Lives.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            for (int i = 0; i < 3; ++i)
            {
                System.Windows.Forms.PictureBox tmp = new System.Windows.Forms.PictureBox();
                tmp.Image = global::Mustache_ic___V2.Properties.Resources.lives;
                tmp.Size = new Size(50, 50);
                fpanel_Lives.Controls.Add(tmp);
            }

            //timer label generation
            label_timer = new System.Windows.Forms.Label();
            label_timer.Text = timer.ToString();
            label_timer.TextAlign = ContentAlignment.TopCenter;
            label_timer.Font = new System.Drawing.Font("Comic Sans MS", 16F, FontStyle.Bold);
            label_timer.Location = new System.Drawing.Point(g.Width/2 - label_timer.Width, 1);
            label_timer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            //score label generation
            label_score = new System.Windows.Forms.Label();
            label_score.Text = score.ToString();
            label_score.TextAlign = ContentAlignment.TopRight;
            label_score.Font = new System.Drawing.Font("Comic Sans MS", 16F, FontStyle.Bold);
            label_score.Location = new System.Drawing.Point(g.Width-label_score.Width-10, 1);
            label_score.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
            //tile panel generation
            panel_tile_holder = new System.Windows.Forms.Panel();
            panel_tile_holder.Size = new System.Drawing.Size(g.Width, g.Height);
            panel_tile_holder.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left));
            //panel_tile_holder.AutoSize = true;
            panel_tile_holder.Location = new System.Drawing.Point(0, 100);


            g.Controls.Add(panel_tile_holder);
            g.Controls.Add(fpanel_Lives);
            g.Controls.Add(label_timer);
            g.Controls.Add(label_score);

            mode = world;
            sub_mode = sub_world;
            //Initalize static members of tileclass
            tileClass.setAnimalType(world);
            tileClass.correctTileCount = 0;
            tileClass.totalTileCount = 0;

            correctTileSelected = 0;
            totalTileSelected = 0;

            init_Tile_Dimension();
            init_board(); //initializes the board
            //hide_speed = 3;//How quickly tiles hide, 0 - 3 secs  

            rand = new Random();  //needed for random generation
            count = rand.Next(hide_speed); //get random tile wait time
            hiddenList = new Queue<Tuple<int, int>>(); //initalizes queue to hold the hidden tiles
        }


        private void init_Tile_Dimension()
        {
            //I subtract 30 and 44 to add some spacing inbetween the tiles. Need to figure out ratio so that it can auto scale based on the number of tiles.
            xTileDim = (gameMain.ActiveForm.Width / n) - 30;
            yTileDim = (gameMain.ActiveForm.Height / n) - 44;

            tileClass.imageList(xTileDim,yTileDim);
        }
        
        /// <summary>
        /// Initalizes the tile board onto panel_tile_holder
        /// </summary>
        /// <param name="size">The number of tiles on the game board. size x size </param>
        private void init_board() //Creates a size X size grid of tiles 
        {
            board = new tileClass[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)    // This is where I print the gameboard into the panel 
                {
                    //715x790 - current size of form 6-12-14
                    board[i, j] = new tileClass(i, j);
                    board[i, j].tile.Size = new System.Drawing.Size(xTileDim, yTileDim);
                    board[i, j].tile.BackColor = Color.Transparent;

                    board[i, j].tile.Location = new System.Drawing.Point(i * (xTileDim - 3) + 60, j * (yTileDim + 5) + 5);

                    board[i, j].tileImage();

                    panel_tile_holder.Controls.Add(board[i, j].tile);
                }
            }
        }

        /// <summary>
        /// One "Turn" of the game. Decrements the count variable. If there are 2 or more hidden tiles, un-hide one. 
        /// If count is '0' then hide a random tile then generate a new counter.
        /// </summary>
        public void gameTick(System.Windows.Forms.Timer time) //Ran each sec for the alloted time 
        {
            --count; //decremete counter for hiding tile
            --timer;
            if(lives < 0)
            {
                time.Dispose();
            }
            if (hiddenList.Count >= 2) //if there are 1 or more hidden tiles unhide one
            {
                //Would like to add a way to randomize the queue if we countine with this method in the future
                var temp = hiddenList.Dequeue();
                board[int.Parse(temp.Item1.ToString()), int.Parse(temp.Item2.ToString())].tile.Show();
            }

            if (count < 0)
            { 
                //when the count is 0 its tile to hide a tile 

                int aa = rand.Next(1, n); //Needs to be based on number of tiles on the board
                for (int x = 0; x < aa; ++x)
                {
                    int i = rand.Next(n);
                    int j = rand.Next(n);  //Gather random i and j values 
                    hideTileImage(i, j);
                }
            }
            label_timer.Text = timer.ToString();
        }

        public static void hideTileImage(int i, int j)
        {
            Tuple<int, int> temp = Tuple.Create(i, j);
            if (!hiddenList.Contains(temp) || !board[i,j].clicked)
            {
                board[i, j].get_random_regularImage();
                hiddenList.Enqueue(temp);  //add them to the queue
                board[i, j].tile.Hide();  //hide the associated tile 
                count = rand.Next(hide_speed); //get random tile wait time
            }
            count = rand.Next(hide_speed); //get random tile wait time
        }

        /// <summary>
        /// Hides the game controllers.
        /// </summary>
        public void hideGameControls()
        {
            panel_tile_holder.Hide();
            //label_lives.Hide();
            fpanel_Lives.Hide();
            label_score.Hide();
            label_timer.Hide();
        }

//METHOD TO HIDE RESULTS PAGE
        public void hideGameResults()
        {

        }
    }
}
