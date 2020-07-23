using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;
using System.Globalization;
using System.Resources;
using BackgammonEngine;

namespace BackgammonUI
{
    static class UIRenderingAssistant
    {
        internal static void PlaceDiceImageWithinPictureBox(int[] dices, PictureBox[] diceImageHolders)
        {
            for (int i = 0; i < dices.Length; i++)
            {
                string dieImageToFind = "Die" + dices[i];
                ResourceSet rsrcSet = BackgammonUI.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                var keyValueEnumarator = rsrcSet.GetEnumerator();

                while (keyValueEnumarator.MoveNext())
                {
                    if (keyValueEnumarator.Key.ToString().Equals(dieImageToFind))
                        diceImageHolders[i].Image = keyValueEnumarator.Value as Image;
                }
            }
        }

        internal static void PopulateGameBoard_WithTriangles(PictureBox gameBoard, List<Triangle> triangles)
        {
            int boardHorizontalBorderWidth = 20;
            int boardVerticalBorderWidth = 26;
            int boardMiddleBorderWidth = 51;
            Size boardSize = gameBoard.Size;

            foreach (Triangle t in triangles)
            {
                int index = t.Index;
                if (index < 0)
                {
                    int Yposition = index == -1 ? boardHorizontalBorderWidth : boardHorizontalBorderWidth + t.Height;
                    t.Location = new Point(-3 + gameBoard.Size.Width - boardVerticalBorderWidth - boardMiddleBorderWidth - 6 * t.Size.Width
                  , Yposition);
                }
                else if (index < 6)
                    t.Location = new Point(boardSize.Width - boardVerticalBorderWidth - t.Size.Width * (index + 1)
                       , boardHorizontalBorderWidth);
                else if (index < 12)
                    t.Location = new Point(boardSize.Width - boardVerticalBorderWidth - boardMiddleBorderWidth - t.Size.Width * (index + 1)
                       , boardHorizontalBorderWidth);
                else if (index < 18)
                    t.Location = new Point(boardSize.Width - boardVerticalBorderWidth - boardMiddleBorderWidth - t.Size.Width * (24 - index)
                       , boardSize.Height - boardVerticalBorderWidth - t.Height + 6);
                else
                    t.Location = new Point(boardSize.Width - boardVerticalBorderWidth - t.Size.Width * (24 - index)
                      , boardSize.Height - boardVerticalBorderWidth - t.Height + 6);

                gameBoard.Controls.Add(t);
            }
        }

        internal static void PopulateTriangles_WithCheckerImages(List<Triangle> triangles, GameState gameState, IEnumerator<PictureBox> imageHoldersEnumerator)
        {
            //// wiping out all checkerimageholders from triangles before replacing
            foreach (Triangle t in triangles)
                t.Controls.Clear();

            Image yellowChecker = global::BackgammonUI.Properties.Resources.yellow_checker;
            yellowChecker.Tag = Color.Yellow;
            Image blueChecker = global::BackgammonUI.Properties.Resources.blue_checker;
            blueChecker.Tag = Color.Blue;

            for (int i = 0; i < gameState.Triangles.Count; i++)
            {
                if (gameState.Triangles[i].Count == 0)
                    continue;

                var checkerImage = gameState.Triangles[i].Peek() == 'W' ? yellowChecker : blueChecker;
                for (int k = 0; k < gameState.Triangles[i].Count; k++)
                {
                    imageHoldersEnumerator.MoveNext();
                    imageHoldersEnumerator.Current.Image = checkerImage;
                    triangles[i].Add(imageHoldersEnumerator.Current);
                }
            }
            // populating eaten checkers triangles
            if (gameState.EatenBlackCheckers.Count != 0 || gameState.EatenWhiteCheckers.Count != 0)
            {
                for (int i = 1; i < 3; i++)
                {
                    var eatenCheckers = i == 1 ? gameState.EatenWhiteCheckers : gameState.EatenBlackCheckers;
                    var checkerImage = i == 1 ? yellowChecker : blueChecker;
                    Triangle eatenCheckersTriangle = triangles.Find(t => t.Index == -i);
                    if (eatenCheckers.Count > 0)
                    {
                        for (int k = 0; k < eatenCheckers.Count; k++)
                        {
                            imageHoldersEnumerator.MoveNext();
                            imageHoldersEnumerator.Current.Image = checkerImage;
                            eatenCheckersTriangle.Add(imageHoldersEnumerator.Current);
                        }
                    }
                }
            }
        }

        internal static void RenderTextualData(BackgammonGameForm.EssentialsForTextualDataRendering textualEssentials, GameState gameState)
        {
            char activeColor = gameState.PlayingColor != 'N' ? gameState.PlayingColor : gameState.RollingColor;
            textualEssentials.MessageLabel.Text = gameState.PlayingColor != 'N' ? "Please Make a Move" : "Please Roll Your Dices";
            textualEssentials.NameLabel.Text = activeColor == 'W' ? textualEssentials.FirstPlayerName : textualEssentials.SecondPlayerName;
            textualEssentials.NameLabel.ForeColor = activeColor == 'W' ? Color.Yellow : Color.Blue;

            for (int i = 0; i < 2; i++)
            {
                Label labelToCenter = i == 0 ? textualEssentials.NameLabel : textualEssentials.MessageLabel;
                centerLabel(labelToCenter, textualEssentials.FormWidth, textualEssentials.GameBoardWidth);
            }
        }

        private static void centerLabel(Label lbl, int formWidth, int boardWidth)
        {
            int centerX = formWidth - (formWidth - boardWidth) / 2;
            lbl.Location = new Point(centerX - lbl.Size.Width / 2, lbl.Location.Y);
        }
    }
}
