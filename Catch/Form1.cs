using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catch
{
    public partial class Form1 : Form
    {
        //Hero variables
        Rectangle hero = new Rectangle(280, 540, 40, 10);
        int heroSpeed = 10;

        //Ball variables 
        int ballSize = 10;
        int ballSpeed = 8;

        //List of balls
        List<Rectangle> ballList = new List<Rectangle>();
        List<int> ballSpeeds = new List<int>();
        List<string> ballColours = new List<string>();

        int score = 0;
        int time = 500;

        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush goldBrush = new SolidBrush(Color.Gold);

        Random randGen = new Random();
        int randValue = 0;

        int groundHeight = 50;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }

        private void gameTime_Tick(object sender, EventArgs e)
        {
            //drop the ball objects down the screen
            for (int i = 0; i < ballList.Count(); i++)
            {
                //get new position of y
                int y = ballList[i].Y + ballSpeed;

                //update the ball object
                ballList[i] = new Rectangle(ballList[i].X, y, ballSize, ballSize);
            }
            //Move Player
            if (leftPressed == true && hero.X > 0)
            {
                hero.X -= heroSpeed;
            }
            if (rightPressed == true && hero.X < this.Width - hero.Width)
            {
                hero.X += heroSpeed;
            }

            //create new ball if it is time and different colour 
            randValue = randGen.Next(0, 100);

            if (randValue < 20)
            {
                if (randValue < 10)
                {
                    randValue = randGen.Next(0, this.Width);

                    Rectangle ball = new Rectangle(randValue, 0, ballSize, ballSize);
                    ballList.Add(ball);
                    ballColours.Add("green");
                    ballSpeeds.Add(randGen.Next(5, 15));
                }
                else if (randValue < 13)
                {
                    randValue = randGen.Next(0, this.Width);

                    Rectangle ball = new Rectangle(randValue, 0, ballSize, ballSize);
                    ballList.Add(ball);
                    ballColours.Add("red");
                    ballSpeeds.Add(randGen.Next(5, 15));
                }
                else if (randValue < 15)
                {
                    randValue = randGen.Next(0, this.Width);

                    Rectangle ball = new Rectangle(randValue, 0, ballSize, ballSize);
                    ballList.Add(ball);
                    ballColours.Add("gold");
                    ballSpeeds.Add(randGen.Next(5, 15));
                }
            }

            //remove ball from list if it has gone off the screen
            for (int i = 0; i < ballList.Count(); i++)
            {
               if (ballList[i].Y > this.Height - groundHeight)
                {
                    ballList.RemoveAt(i);
                }
            } 
            
            // check for collision between ball and player
            for (int i = 0; i < ballList.Count(); i++)
            {
                if (ballList[i].IntersectsWith(hero))
                {
                    if (ballColours[i] == "green")
                    {
                        score++;
                    }
                    else if (ballColours[i] == "red")
                    {
                        score--;
                    }
                    else
                    {
                        time = time + 20;
                    }
                    ballList.RemoveAt(i);
                    ballColours.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);
                }
            }

            //decrease time and check if time has run out
            time--;
            if (time == 0)
            {
                gameTimer.Stop();
            }


            //redraw the screen
            Refresh();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //update labels
            timeLabel.Text = $"Time Left: {time}";
            scoreLabel.Text = $"Score: {score}";

            //draw ground
            e.Graphics.FillRectangle(greenBrush, 0, this.Height - groundHeight, this.Width, groundHeight);

            //draw hero
            e.Graphics.FillRectangle(whiteBrush, hero);

            //draw balls
            for (int i = 0; i < ballList.Count(); i++)
            {
                if (ballColours[i] == "green")
                {
                    e.Graphics.FillEllipse(greenBrush, ballList[i]);
                }
                else if (ballColours[i] == "red")
                {
                    e.Graphics.FillEllipse(redBrush, ballList[i]);
                }
                else if (ballColours[i] == "gold")
                {
                    e.Graphics.FillEllipse(goldBrush, ballList[i]);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
