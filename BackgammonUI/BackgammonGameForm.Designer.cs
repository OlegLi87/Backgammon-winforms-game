using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace BackgammonUI
{
    partial class BackgammonGameForm : Form
    {
        private PictureBox boardImageBox;
        private Label nameLbl;
        private Label messageLabel;
        private Button rollBtn;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgammonGameForm));
            this.boardImageBox = new System.Windows.Forms.PictureBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.rollBtn = new System.Windows.Forms.Button();
            this.diceImageHolder1 = new System.Windows.Forms.PictureBox();
            this.diceImageHolder2 = new System.Windows.Forms.PictureBox();
            this.toMainMenuBtn = new System.Windows.Forms.Button();
            this.exitGameBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.boardImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diceImageHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diceImageHolder2)).BeginInit();
            this.SuspendLayout();
            // 
            // boardImageBox
            // 
            this.boardImageBox.Image = ((System.Drawing.Image)(resources.GetObject("boardImageBox.Image")));
            this.boardImageBox.Location = new System.Drawing.Point(12, 6);
            this.boardImageBox.Name = "boardImageBox";
            this.boardImageBox.Size = new System.Drawing.Size(784, 591);
            this.boardImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.boardImageBox.TabIndex = 0;
            this.boardImageBox.TabStop = false;
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Font = new System.Drawing.Font("Narkisim", 40.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.nameLbl.Location = new System.Drawing.Point(942, 41);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(159, 67);
            this.nameLbl.TabIndex = 1;
            this.nameLbl.Text = "Oleg";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Malgun Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.ForeColor = System.Drawing.Color.Maroon;
            this.messageLabel.Location = new System.Drawing.Point(854, 122);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(258, 41);
            this.messageLabel.TabIndex = 2;
            this.messageLabel.Text = "Please Roll Dices";
            // 
            // rollBtn
            // 
            this.rollBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rollBtn.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rollBtn.Location = new System.Drawing.Point(930, 217);
            this.rollBtn.Name = "rollBtn";
            this.rollBtn.Size = new System.Drawing.Size(131, 47);
            this.rollBtn.TabIndex = 3;
            this.rollBtn.Text = "Roll Dices";
            this.rollBtn.UseVisualStyleBackColor = true;
            this.rollBtn.Click += new System.EventHandler(this.rollBtn_Click);
            // 
            // diceImageHolder1
            // 
            this.diceImageHolder1.Location = new System.Drawing.Point(851, 264);
            this.diceImageHolder1.Name = "diceImageHolder1";
            this.diceImageHolder1.Size = new System.Drawing.Size(100, 50);
            this.diceImageHolder1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.diceImageHolder1.TabIndex = 4;
            this.diceImageHolder1.TabStop = false;
            // 
            // diceImageHolder2
            // 
            this.diceImageHolder2.Location = new System.Drawing.Point(1016, 264);
            this.diceImageHolder2.Name = "diceImageHolder2";
            this.diceImageHolder2.Size = new System.Drawing.Size(100, 50);
            this.diceImageHolder2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.diceImageHolder2.TabIndex = 5;
            this.diceImageHolder2.TabStop = false;
            // 
            // toMainMenuBtn
            // 
            this.toMainMenuBtn.Font = new System.Drawing.Font("Narkisim", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toMainMenuBtn.Location = new System.Drawing.Point(814, 486);
            this.toMainMenuBtn.Name = "toMainMenuBtn";
            this.toMainMenuBtn.Size = new System.Drawing.Size(156, 93);
            this.toMainMenuBtn.TabIndex = 6;
            this.toMainMenuBtn.Text = "To Main Menu";
            this.toMainMenuBtn.UseVisualStyleBackColor = true;
            this.toMainMenuBtn.Click += new System.EventHandler(this.toMainMenuBtn_Click);
            // 
            // exitGameBtn
            // 
            this.exitGameBtn.Font = new System.Drawing.Font("Narkisim", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitGameBtn.Location = new System.Drawing.Point(1005, 486);
            this.exitGameBtn.Name = "exitGameBtn";
            this.exitGameBtn.Size = new System.Drawing.Size(156, 93);
            this.exitGameBtn.TabIndex = 7;
            this.exitGameBtn.Text = "Exit Game";
            this.exitGameBtn.UseVisualStyleBackColor = true;
            this.exitGameBtn.Click += new System.EventHandler(this.exitGameBtn_Click);
            // 
            // BackgammonGameForm
            // 
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1182, 603);
            this.Controls.Add(this.exitGameBtn);
            this.Controls.Add(this.toMainMenuBtn);
            this.Controls.Add(this.diceImageHolder2);
            this.Controls.Add(this.diceImageHolder1);
            this.Controls.Add(this.rollBtn);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.nameLbl);
            this.Controls.Add(this.boardImageBox);
            this.Name = "BackgammonGameForm";
            this.Text = "Backgammon By Oleg Livcha";
            ((System.ComponentModel.ISupportInitialize)(this.boardImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diceImageHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diceImageHolder2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private PictureBox diceImageHolder1;
        private PictureBox diceImageHolder2;
        private Button toMainMenuBtn;
        private Button exitGameBtn;
    }
}
