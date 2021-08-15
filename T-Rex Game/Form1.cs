﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Game
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed = 10;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        Random rnd = new();

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void gameEvent(object sender, EventArgs e) // TIMER
        {
            trex.Top += jumpSpeed;
            scoreText.Text = "Score: " + score;
            if (jumping && force<0)
            {
                jumping = false;
            }
            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
                jumpSpeed = 12;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle") // confirmar o (string)
                {
                    x.Left -= obstacleSpeed;
                    if (x.Left + x.Width < -120)
                    {
                        x.Left = this.ClientSize.Width + rnd.Next(200,800);
                        score++;
                    }
                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        scoreText.Text += " Press R to Restart";
                    }
                }
            }
            if (trex.Top >= 380 && !jumping)
            {
                force = 12; // set the force to 8 -> Verificar isto 
                trex.Top = floor.Top - trex.Height;
                jumpSpeed = 0;
            }
            if (score >= 10)
            {
                obstacleSpeed = 15;
            }
        }

        private void keyIsDown(object sender, KeyEventArgs e) // PRESSED KEY
        {
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e) // DEPRESSED KEY
        {
            if (e.KeyCode == Keys.R)
            {
                resetGame();
            }
            if (jumping)
            {
                jumping = false;
            }
        }

        public void resetGame()
        {
            force = 12; 
            trex.Top = floor.Top - trex.Height;
            jumpSpeed = 0; 
            jumping = false; 
            score = 0;
            obstacleSpeed = 10; 
            scoreText.Text = "Score: " + score; 
            trex.Image = Properties.Resources.running; 

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    int position = rnd.Next(600, 1000);
                    x.Left = 640 + (x.Left + position + x.Width * 3);
                }
            }

            gameTimer.Start(); 
        }
    }
}
