using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using BackgammonEngine;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BackgammonUI
{
    partial class BackgammonGameForm : Form
    {
        private MainMenuForm mainMenuForm;
        private GameEngine gameEngine;
        private readonly GameState gameState;
        private EssentialsForTextualDataRendering textualEssentials;
        private List<Triangle> triangles;
        private List<PictureBox> checkerImageHolders;
        private Triangle selectedTriangle;

        internal struct EssentialsForTextualDataRendering
        {
            internal Label NameLabel, MessageLabel;
            internal string FirstPlayerName, SecondPlayerName;
            internal int FormWidth, GameBoardWidth;
        }

        internal BackgammonGameForm(string[] players, MainMenuForm mainMenuForm)
        {
            this.mainMenuForm = mainMenuForm;
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
            boardImageBox.Enabled = false;
            gameEngine = new GameEngine();
            gameState = gameEngine.GameState;
            textualEssentials = new EssentialsForTextualDataRendering
            {
                FirstPlayerName = players[0],
                SecondPlayerName = players[1],
                MessageLabel = messageLabel,
                NameLabel = nameLbl,
                FormWidth = Size.Width,
                GameBoardWidth = boardImageBox.Width
            };

            initializeTriangles();
            initializeCheckerImageHolders();
            renderGameState(true);
        }

        private void initializeTriangles()
        {
            Size triangleSize = new Size(57, 276);

            triangles = new List<Triangle>();
            for (int i = 0; i < 26; i++)
            {
                Triangle t = i < 24 ? new Triangle(i, triangleSize) : new Triangle(23 - i, triangleSize);
                t.Click += trianlgeClicked;
                triangles.Add(t);
            }
            UIRenderingAssistant.PopulateGameBoard_WithTriangles(boardImageBox, triangles);
        }

        private void initializeCheckerImageHolders()
        {
            checkerImageHolders = new List<PictureBox>();
            for (int i = 0; i < 30; i++)
            {
                PictureBox checkerImageHolder = new PictureBox();
                checkerImageHolder.SizeMode = PictureBoxSizeMode.StretchImage;
                checkerImageHolder.BackColor = Color.Transparent;
                checkerImageHolder.Click += trianlgeClicked;
                checkerImageHolders.Add(checkerImageHolder);
            }
        }

        private void renderGameState(bool hasToRenderTriangles)
        {
            if (hasToRenderTriangles)
                UIRenderingAssistant.PopulateTriangles_WithCheckerImages(triangles, gameState, checkerImageHolders.GetEnumerator());

            UIRenderingAssistant.RenderTextualData(textualEssentials, gameState);
        }

        private void trianlgeClicked(object sender, EventArgs e)
        {
            Triangle clickedTriangle;

            if (sender is PictureBox)
                clickedTriangle = (sender as PictureBox).Parent as Triangle;
            else
                clickedTriangle = sender as Triangle;

            markTriangleAsSelected(clickedTriangle);
            if (selectedTriangle != null && clickedTriangle != selectedTriangle)
            {
                if (gameEngine.TryToMove(selectedTriangle.Index, clickedTriangle.Index))
                {
                    selectedTriangle.TrySetImageHolderBackLight(Color.Transparent);
                    selectedTriangle = null;
                    renderGameState(true);
                    lockOrOpenControls();

                    if (gameState.InfoMessage != null)
                    {
                        MessageBox.Show(nestPlayerNameWithinInfoMessage());
                        if (gameState.InfoMessage.ToLower().IndexOf("winner") != -1)
                            toMainMenuBtn_Click(null, null);
                    }
                }
            }
        }

        private void rollBtn_Click(object sender, EventArgs e)
        {
            gameEngine.RollDices();

            if (gameState.InfoMessage != null)
                MessageBox.Show(nestPlayerNameWithinInfoMessage());

            if (gameState.PlayingColor != 'N')
            {
                int[] rolledDices = new int[] { gameState.RolledDices[0], gameState.RolledDices[1] };
                PictureBox[] diceImageHolders = new PictureBox[] { diceImageHolder1, diceImageHolder2 };
                UIRenderingAssistant.PlaceDiceImageWithinPictureBox(rolledDices, diceImageHolders);
                lockOrOpenControls();
            }
            renderGameState(false);
        }

        private void toMainMenuBtn_Click(object sender, EventArgs e)
        {
            mainMenuForm.ToggleMainMenuFormDisplay();
            this.Close();
        }

        private void exitGameBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lockOrOpenControls()
        {
            if (gameState.RollingColor != 'N')
            {
                rollBtn.Visible = true;
                boardImageBox.Enabled = false;
                diceImageHolder1.Image = null;
                diceImageHolder2.Image = null;
            }
            else if (gameState.PlayingColor != 'N')
            {
                rollBtn.Visible = false;
                boardImageBox.Enabled = true;
            }
        }

        private void markTriangleAsSelected(Triangle clickedTriangle)
        {
            Color imageHolderBackLightColor = gameState.PlayingColor == 'W' ? Color.Yellow : Color.Blue;

            int eatenCheckersIndex = gameState.PlayingColor == 'W' ? -1 : -2;
            Triangle eatenCheckersTriangle = triangles.Find(t => t.Index == eatenCheckersIndex);
            if (eatenCheckersTriangle.Controls.Count > 0)
            {
                eatenCheckersTriangle.TrySetImageHolderBackLight(imageHolderBackLightColor);
                selectedTriangle = eatenCheckersTriangle;
                return;
            }

            if (selectedTriangle == null && clickedTriangle.TrySetImageHolderBackLight(imageHolderBackLightColor))
            {
                selectedTriangle = clickedTriangle;
                return;
            }
            else if (selectedTriangle != null && selectedTriangle == clickedTriangle)
            {
                selectedTriangle.TrySetImageHolderBackLight(Color.Transparent);
                selectedTriangle = null;
                return;
            }
        }

        private string nestPlayerNameWithinInfoMessage()
        {
            char colorChar = gameState.InfoMessage[0];
            string playerName = colorChar == 'W' ? textualEssentials.FirstPlayerName : textualEssentials.SecondPlayerName;
            return playerName + gameState.InfoMessage.Remove(0, 1);
        }
    }
}
