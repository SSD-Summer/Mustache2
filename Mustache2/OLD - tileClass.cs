using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;


namespace Mustashe_ic
{

    class tileClass
    {
        //Bool value whether or not the tile is the correct one. May need to be improved or changed
        public bool clicked;
        //The button - may need to switch to image
        public System.Windows.Forms.Button tile { get; set; }
        //Static image Lists to hold the images from the resouce file
        public static System.Windows.Forms.ImageList alive_imageList;
        public static System.Windows.Forms.ImageList dead_imageList;
        public static List<Tuple<int, int>> imageCatagorys;

        public static int correctTileCount;
        public static int totalTileCount;

        //Used to return random numbers
        static RandomDist rand;

        int xLoc, yLoc;
        int imageHideCounter;
        System.Windows.Forms.Timer imageTime;

        static System.Timers.Timer timer = new System.Timers.Timer();

        //Var used to distinguish which animals are correct for the current game --- Updated everytime a new game is started
        static int animalType; //

        /// <summary>
        /// Default constructor. Sets: 
        ///     correctObject=false
        ///     Allocates a new button for tile
        ///     assigns buttons event to tile_clicked
        /// </summary>
        public tileClass()
        {
            tile = new System.Windows.Forms.Button();
      

        }

        public tileClass(int x, int y)
        {
            tile = new System.Windows.Forms.Button();
            tile.Click += new EventHandler(tile_clicked);
            xLoc = x;
            yLoc = y;
            clicked = false;
        }

        public static void setAnimalType(int type)
        {
            animalType = type;
            
        }

        /// <summary>
        /// Creates two static ImageList. alive_imageList stores non-moustache images, dead_imageList stores
        /// moustache images. The images are sized to 90 x 90
        /// </summary>
        public static void imageList(int x, int y)
        {
            alive_imageList = new System.Windows.Forms.ImageList();
            alive_imageList.ImageSize = new Size(x, y); //makes the images same size as button
            dead_imageList = new System.Windows.Forms.ImageList();
            dead_imageList.ImageSize = new Size(x, y);//makes the images same size as button

            imageCatagorys = new List<Tuple<int, int>>(4);

            //0 - 2 are dinos
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.steg);
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.t_rex);
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.bronc);
            imageCatagorys.Add(new Tuple<int, int>(0, 2));
            //3 - 5 are house 
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.bird);
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.dog);
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.cat);
            imageCatagorys.Add(new Tuple<int, int>(3, 5));
            //6 - 8 are water
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.crab);
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.goldfish);
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.jellyfish);
            imageCatagorys.Add(new Tuple<int, int>(6, 8));
            //9 - 11 are farm
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.pig);
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.sheep);
            alive_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.chicken);
            imageCatagorys.Add(new Tuple<int, int>(9, 11));
            ///////////////////////////////////////////////////////////////////////////////////////////

            //0 - 2 are dinos
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.steg_moustashe);
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.t_rex_moustashe);
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.bron_moustache);

            //3 - 5 are house
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.bird_moustache); 
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.dog_moustashe);
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.cat_moustache);

            //6 - 8 are water
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.crab_moustache);
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.goldfish_moustache);
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.jellyfish_moustache);

            //8 - 11 are farm
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.pig_moustache);
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.sheep_moustache);
            dead_imageList.Images.Add(global::Mustache_ic___V2.Properties.Resources.chicken_moustache);

            dead_imageList.Images.Add("X",global::Mustache_ic___V2.Properties.Resources.X);
            
            //Random number generator init
            rand = new RandomDist(imageCatagorys[animalType - 1].Item2 - 1, 1);

        }


       
        /// <summary>
        /// Sets the image on the tile. If image_num == 1, alive image; else dead image
        /// </summary>
        /// <param name="x">Width of tile image is being attached too</param>
        /// <param name="y">Height of tile image is being attached too</param>
        public void tileImage()
        {
            //sets images to the tiles. If image_num = 0, moustache pic is displayed on tile and a pause is in place, if image_num = 1, normal pic

            int a =rand.equalRandom(0, 12);

            totalTileCount++;

            if(totalTileCount > 6 && correctTileCount < totalTileCount/3)
            {
                a = (int)rand.getRandomNormal();
            }
            

            if (a >= imageCatagorys[animalType - 1].Item1 && a <= imageCatagorys[animalType - 1].Item2)
                correctTileCount++;

            this.tile.BackColor = Color.Transparent;
            this.tile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tile.FlatAppearance.BorderSize = 0;
            
            this.tile.BackgroundImage = alive_imageList.Images[a]; //GET AN ERRORS SOMTIMES, a=-1?!

            this.tile.Tag = a;
            this.tile.Show();
        }

        /// <summary>
        /// Hides button associated with tile
        /// </summary>
        public void hideTile()
        {
            this.tile.Hide();
        }

        public void get_random_regularImage()
        {
            this.tile.Enabled = true;
            int tempTag = Convert.ToInt32(this.tile.Tag.ToString());
            int newTag = rand.equalRandom(0, 12);
            if (totalTileCount > 9 && correctTileCount < totalTileCount / 3)
            {
                newTag = (int)rand.getRandomNormal();
            }
            else
            {
                while (newTag == tempTag)
                    newTag = rand.equalRandom(0, 12);
            }
            this.tile.Tag = newTag;
            this.tile.BackgroundImage = alive_imageList.Images[newTag];
            this.tile.Image = null;
            //this.tile.Image = alive_imageList.Images[newTag];

            if (newTag >= imageCatagorys[animalType - 1].Item1 && newTag <= imageCatagorys[animalType - 1].Item2)
                correctTileCount++;
            totalTileCount++;
        }


        /// <summary>
        /// The tag of a image is passed to allow for correct mustache image to be displayed
        /// </summary>
        /// <param name="tag">Image tag that identifies the normal image corresponding to its mustache image</param>
        /// <returns></returns>
        public static Image getMustacheImage(int tag)
        {
            return dead_imageList.Images[tag];
        }



        /// <summary>
        /// Sets up all events after a tile is clicked. Determines if image is correct hit or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tile_clicked(object sender, EventArgs e)
        {
            clicked = true;
            this.tile.Enabled = false;
            
            int tileTag = Convert.ToInt32(sender.GetType().GetProperty("Tag").GetValue(sender));
            
            //0 - 2 are dinos 
            //3 - 5 are house
            //6 - 8 are water
            //9 - 11 are farm

            gamePlay.totalTileSelected++;
            
            if(tileTag >= imageCatagorys[animalType-1].Item1 && tileTag <= imageCatagorys[animalType-1].Item2)
            {
                gamePlay.score += 200;
                gamePlay.label_score.Text = gamePlay.score.ToString();
                this.tile.BackgroundImage = dead_imageList.Images[tileTag];
                //this.tile.Image = dead_imageList.Images[tileTag];
                //System.Media.SoundPlayer player = new System.Media.SoundPlayer(global::Mustache_ic___V2.Properties.Resources.correct);
                //player.Play();
                this.tile.Show();
                correctTileCount--;
                gamePlay.correctTileSelected++;
            }
            else
            {
                if (gamePlay.lives >= 0)
                {
                    gamePlay.fpanel_Lives.Controls.RemoveAt(gamePlay.lives);
                    gamePlay.lives--;
                    //System.Media.SoundPlayer player = new System.Media.SoundPlayer(global::Mustache_ic___V2.Properties.Resources.wrong);
                    //player.Play();

                    this.tile.Image = dead_imageList.Images["X"];
                }

            }

            imageTime = new System.Windows.Forms.Timer();
            imageTime.Tick += new EventHandler(imageTimerTick);
            imageTime.Interval = 500;
            imageTime.Disposed += new EventHandler(hide_image);
            imageHideCounter = 2;
            imageTime.Start();           

            //----TESTING FADE OR ROATATING IMAGE AFTER CLICK
            //this.tile.BackgroundImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
           
            

        }

        private void imageTimerTick(Object sender, EventArgs e)
        {
            --imageHideCounter;
            if(imageHideCounter <1)
            {
                totalTileCount--;
                imageTime.Dispose();
            }
        }

        private void hide_image(Object sender, EventArgs e)
        {
            gamePlay.hideTileImage(this.xLoc, this.yLoc);
        }

    }
}
