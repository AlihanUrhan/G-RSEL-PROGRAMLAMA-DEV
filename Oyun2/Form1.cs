using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Oyun2
{
    public partial class Form1 : Form
    {
        private List<Button> cardButtons;
        private Button firstClicked, secondClicked;
        private System.Windows.Forms.Timer timerShow;
        private System.Windows.Forms.Timer timerHide;
        private System.Windows.Forms.Timer gameTimer;
        private DateTime startTime;
        private List<string> cardValues;
        private Random random;
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }
        private void InitializeGame()
        {

            cardButtons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16 };
            timerShow = new System.Windows.Forms.Timer { Interval = 4000 };
            timerHide = new System.Windows.Forms.Timer { Interval = 1000 };
            gameTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            gameTimer.Tick += GameTimer_Tick;
            timerShow.Tick += TimerShow_Tick;
            timerHide.Tick += TimerHide_Tick;
            random = new Random();
            cardValues = new List<string> { "A", "A", "B", "B", "C", "C", "D", "D", "E", "E", "F", "F", "G", "G", "H", "H" };
            button17.Click += button17_Click;

            StartNewGame();
        }

        private void StartNewGame()
        {
            ShuffleCards();
            foreach (var button in cardButtons)
            {
                button.Text = button.Tag.ToString(); // Kartlarý baþlangýçta göster
                button.Enabled = true;
                button.Click += CardButton_Click;
            }
            firstClicked = null;
            secondClicked = null;
            startTime = DateTime.Now;
            gameTimer.Start();
            timerShow.Start(); // 4 saniye sonra kartlarý kapat
        }

        private void ShuffleCards()
        {
            var shuffledValues = cardValues.OrderBy(x => random.Next()).ToList();
            for (int i = 0; i < cardButtons.Count; i++)
            {
                cardButtons[i].Tag = shuffledValues[i];
            }
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            if (timerHide.Enabled) return;

            var clickedButton = sender as Button;

            if (firstClicked == null)
            {
                firstClicked = clickedButton;
                firstClicked.Text = firstClicked.Tag.ToString();
                return;
            }

            secondClicked = clickedButton;
            secondClicked.Text = secondClicked.Tag.ToString();

            if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                firstClicked.Enabled = false;
                secondClicked.Enabled = false;
                firstClicked = null;
                secondClicked = null;
                CheckForWinner();
                return;
            }

            timerHide.Start();
        }
        private void TimerHide_Tick(object sender, EventArgs e)
        {
            timerHide.Stop();
            firstClicked.Text = "?";
            secondClicked.Text = "?";
            firstClicked = null;
            secondClicked = null;
        }

        private void TimerShow_Tick(object sender, EventArgs e)
        {
            timerShow.Stop();
            foreach (var button in cardButtons)
            {
                button.Text = "?";
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            var elapsedTime = DateTime.Now - startTime;
            this.Text = $"Süre: {elapsedTime.TotalSeconds:F1} saniye";
        }

        private void CheckForWinner()
        {
            if (cardButtons.All(button => !button.Enabled))
            {
                gameTimer.Stop();
                var elapsedTime = DateTime.Now - startTime;
                MessageBox.Show($"Tebrikler! Oyunu {elapsedTime.TotalSeconds:F1} saniyede tamamladýnýz.", "Oyun Bitti");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}