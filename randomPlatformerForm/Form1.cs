using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace randomPlatformerForm
{
    public partial class Form1 : Form
    {
        private int milliSeconds;
        private int seconds;
        private bool goLeft, goRight, jumping, isGameOver;
        public int jumpSpeed;
        public int force;
        public int score = 0;
        public int playerSpeed = 7;
        public int horizontalSpeed = 5;
        public int verticalSpeed = 3;
        public int enemyOneSpeed = 5;
        public int enemyTwoSpeed = 3;
        public int enemyThreeSpeed = 1;
        public Form1()
        {
            InitializeComponent();
           
        }

        private void MainGameEvent(object sender, EventArgs e)
        {
            speedRunTimer.Start();
            txtScore.Text = "Score: " + score;
            player.Top += jumpSpeed;
            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls )
            {
                if (x is PictureBox)
                {
                    if ((string) x.Tag == "platform")
                    {
                        if(player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;
                            if ((string) x.Name == "horizontalPlatform" && goLeft == false ||
                                (string) x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }
                        x.BringToFront();
                    }

                    if ((string) x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string) x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            speedRunTimer.Stop();
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine +
                                            "You died. Press Enter to Restart";
                        }
                    }
                }
            }

            horizontalPlatform.Left -= horizontalSpeed;
            if (horizontalPlatform.Left < 0 ||
                horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top -= verticalSpeed;
            if (verticalPlatform.Top < 0 || verticalPlatform.Top > 800)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;
            if (enemyOne.Left < plat3.Left || enemyOne.Left + enemyOne.Width > plat3.Left + plat3.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }
            enemyTwo.Left -= enemyTwoSpeed;
            if (enemyTwo.Left < plat2.Left || enemyTwo.Left + enemyTwo.Width > plat2.Left + plat2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }
            enemyThree.Left -= enemyThreeSpeed;
            if (enemyThree.Left < plat8.Left || enemyThree.Left + enemyThree.Width > plat8.Left + plat8.Width)
            {
                enemyThreeSpeed = -enemyThreeSpeed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                speedRunTimer.Stop();
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell and died :(";
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 30)
            {
                speedRunTimer.Stop();
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "YOU WON! In: "+seconds+" seconds And: "+milliSeconds+" milliseconds.";
                seconds = 0;
                milliSeconds = 0;

            }
            else
            {
                txtScore.Text = "Score: " + score + Environment.NewLine + "Collect all the YELLOw!";
                timerFinnish.Text = "Seconds: " +seconds+"."+milliSeconds;
            }
        }

        private void gameTime(object sender, EventArgs e)
        {
            milliSeconds++;
            if (milliSeconds == 10)
            {
                seconds++;
                milliSeconds = 0;
            }
        }


        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            } if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }

        }

        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            gameTimer.Start();
            speedRunTimer.Start();

            txtScore.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }

                // reset the position of player, platform and enemies;
                player.Left = 72;
                player.Top = 290;
                enemyOne.Left = 601;
                enemyOne.Top = 320;
                enemyTwo.Left = 371;
                enemyTwo.Top = 365;
                enemyThree.Left = 644;
                enemyThree.Top = 100;
                
            }
        }
    }
}
