using System.Windows.Forms;

namespace BackgammonUI
{
    partial class MainMenuForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.welcomeLbl = new System.Windows.Forms.Label();
            this.firstPlayerLbl = new System.Windows.Forms.Label();
            this.secondPlayerLbl = new System.Windows.Forms.Label();
            this.txtName1 = new System.Windows.Forms.TextBox();
            this.txtName2 = new System.Windows.Forms.TextBox();
            this.btnRollDice1 = new System.Windows.Forms.Button();
            this.btnRollDice2 = new System.Windows.Forms.Button();
            this.diceImageBox1 = new System.Windows.Forms.PictureBox();
            this.diceImageBox2 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.diceImageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diceImageBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // welcomeLbl
            // 
            this.welcomeLbl.AutoSize = true;
            this.welcomeLbl.Font = new System.Drawing.Font("Narkisim", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLbl.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.welcomeLbl.Location = new System.Drawing.Point(55, 48);
            this.welcomeLbl.Name = "welcomeLbl";
            this.welcomeLbl.Size = new System.Drawing.Size(603, 43);
            this.welcomeLbl.TabIndex = 0;
            this.welcomeLbl.Text = "Welcome To Backgammon Game";
            // 
            // firstPlayerLbl
            // 
            this.firstPlayerLbl.AutoSize = true;
            this.firstPlayerLbl.Font = new System.Drawing.Font("Trebuchet MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstPlayerLbl.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.firstPlayerLbl.Location = new System.Drawing.Point(58, 160);
            this.firstPlayerLbl.Name = "firstPlayerLbl";
            this.firstPlayerLbl.Size = new System.Drawing.Size(149, 29);
            this.firstPlayerLbl.TabIndex = 1;
            this.firstPlayerLbl.Text = "First Player ";
            // 
            // secondPlayerLbl
            // 
            this.secondPlayerLbl.AutoSize = true;
            this.secondPlayerLbl.Font = new System.Drawing.Font("Trebuchet MS", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondPlayerLbl.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.secondPlayerLbl.Location = new System.Drawing.Point(58, 262);
            this.secondPlayerLbl.Name = "secondPlayerLbl";
            this.secondPlayerLbl.Size = new System.Drawing.Size(178, 29);
            this.secondPlayerLbl.TabIndex = 2;
            this.secondPlayerLbl.Text = "Second Player ";
            // 
            // txtName1
            // 
            this.txtName1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName1.Location = new System.Drawing.Point(249, 160);
            this.txtName1.MaxLength = 15;
            this.txtName1.Name = "txtName1";
            this.txtName1.Size = new System.Drawing.Size(173, 30);
            this.txtName1.TabIndex = 3;
            this.txtName1.Tag = "txtName1";
            this.txtName1.TextChanged += new System.EventHandler(this.textInTextBoxChanged);
            // 
            // txtName2
            // 
            this.txtName2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName2.Location = new System.Drawing.Point(249, 261);
            this.txtName2.MaxLength = 15;
            this.txtName2.Name = "txtName2";
            this.txtName2.Size = new System.Drawing.Size(173, 30);
            this.txtName2.TabIndex = 4;
            this.txtName2.Tag = "txtName2";
            this.txtName2.TextChanged += new System.EventHandler(this.textInTextBoxChanged);
            // 
            // btnRollDice1
            // 
            this.btnRollDice1.Enabled = false;
            this.btnRollDice1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRollDice1.Location = new System.Drawing.Point(441, 155);
            this.btnRollDice1.Name = "btnRollDice1";
            this.btnRollDice1.Size = new System.Drawing.Size(168, 42);
            this.btnRollDice1.TabIndex = 5;
            this.btnRollDice1.Tag = "Btn1";
            this.btnRollDice1.Text = "Roll Dice";
            this.btnRollDice1.UseVisualStyleBackColor = true;
            this.btnRollDice1.Click += new System.EventHandler(this.rollButtonClicked);
            // 
            // btnRollDice2
            // 
            this.btnRollDice2.Enabled = false;
            this.btnRollDice2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRollDice2.Location = new System.Drawing.Point(441, 257);
            this.btnRollDice2.Name = "btnRollDice2";
            this.btnRollDice2.Size = new System.Drawing.Size(168, 42);
            this.btnRollDice2.TabIndex = 6;
            this.btnRollDice2.Tag = "Btn2";
            this.btnRollDice2.Text = "Roll Dice";
            this.btnRollDice2.UseVisualStyleBackColor = true;
            this.btnRollDice2.Click += new System.EventHandler(this.rollButtonClicked);
            // 
            // diceImageBox1
            // 
            this.diceImageBox1.Location = new System.Drawing.Point(656, 140);
            this.diceImageBox1.Name = "diceImageBox1";
            this.diceImageBox1.Size = new System.Drawing.Size(80, 80);
            this.diceImageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.diceImageBox1.TabIndex = 7;
            this.diceImageBox1.TabStop = false;
            // 
            // diceImageBox2
            // 
            this.diceImageBox2.Location = new System.Drawing.Point(656, 241);
            this.diceImageBox2.Name = "diceImageBox2";
            this.diceImageBox2.Size = new System.Drawing.Size(80, 80);
            this.diceImageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.diceImageBox2.TabIndex = 8;
            this.diceImageBox2.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(317, 378);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(168, 41);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.exitClicked);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.diceImageBox2);
            this.Controls.Add(this.diceImageBox1);
            this.Controls.Add(this.btnRollDice2);
            this.Controls.Add(this.btnRollDice1);
            this.Controls.Add(this.txtName2);
            this.Controls.Add(this.txtName1);
            this.Controls.Add(this.secondPlayerLbl);
            this.Controls.Add(this.firstPlayerLbl);
            this.Controls.Add(this.welcomeLbl);
            this.Name = "MainMenuForm";
            this.Text = "Backgammon Welomes You";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.diceImageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diceImageBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label welcomeLbl;
        private Label firstPlayerLbl;
        private Label secondPlayerLbl;
        private TextBox txtName1;
        private TextBox txtName2;
        private Button btnRollDice1;
        private Button btnRollDice2;
        private PictureBox diceImageBox1;
        private PictureBox diceImageBox2;
        private Button btnExit;
    }
}

