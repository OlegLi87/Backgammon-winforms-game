using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using BackgammonEngine;
using Microsoft.CSharp.RuntimeBinder;

namespace BackgammonUI
{
    public partial class MainMenuForm : Form
    {
        private int[] rolledDices = new int[2];
        public MainMenuForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
        }

        private void textInTextBoxChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Button buttonToManipulate = textBox.Tag.ToString().IndexOf("1") != -1 ? btnRollDice1 : btnRollDice2;
            string text = textBox.Text;

            if (text.Length > 0)
                buttonToManipulate.Enabled = true;
            else
                buttonToManipulate.Enabled = false;
        }

        private void rollButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int indexOf_1 = clickedButton.Tag.ToString().IndexOf("1");

            TextBox textBoxToDisable = indexOf_1 != -1 ? txtName1 : txtName2;
            PictureBox diceImageBoxToManipulate = indexOf_1 != -1 ? diceImageBox1 : diceImageBox2;
            int indexOfThrownDice = indexOf_1 != -1 ? 0 : 1;

            int diceNumber = new Random().Next(1, 7);
            UIRenderingAssistant.PlaceDiceImageWithinPictureBox(new int[] { diceNumber }, new PictureBox[] { diceImageBoxToManipulate });

            clickedButton.Enabled = false;
            textBoxToDisable.Enabled = false;
            rolledDices[indexOfThrownDice] = diceNumber;

            if (rolledDices[0] != 0 && rolledDices[1] != 0)
                if (!isStateResetForAnotherRoll())
                    displayGameForm();
        }

        private bool isStateResetForAnotherRoll()
        {
            if (rolledDices[0] == rolledDices[1])
            {
                MessageBox.Show("Draw!!!Throw dices once again!");

                btnRollDice1.Enabled = true;
                btnRollDice2.Enabled = true;
                diceImageBox1.Image = null;
                diceImageBox2.Image = null;
                rolledDices[0] = 0;
                rolledDices[1] = 0;
                return true;
            }
            return false;
        }

        private void displayGameForm()
        {
            string[] players = determinePlayersOrder();
            MessageBox.Show(players[0] + " starts the game!");

            ToggleMainMenuFormDisplay();
            new BackgammonGameForm(players, this).Show();
        }
        // hides current form or in case it's allerady hidden restarts whole application.
        internal void ToggleMainMenuFormDisplay()
        {
            if (Opacity == 1)
                Opacity = 0;
            else
                Application.Restart();
        }

        private string[] determinePlayersOrder()
        {
            string[] players = new string[2];
            if (rolledDices[0] > rolledDices[1])
            {
                players[0] = txtName1.Text;  // Starting player name will be held at 0 index.
                players[1] = txtName2.Text;
            }
            else
            {
                players[0] = txtName2.Text;
                players[1] = txtName1.Text;
            }
            return players;
        }

        private void exitClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}