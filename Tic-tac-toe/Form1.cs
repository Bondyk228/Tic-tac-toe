using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_tac_toe
{
    public partial class TicTacToeWindow: Form
    {
        // Масив для зберігання значень кнопок
        private string[] board = new string[9];
        // Змінна для визначення черги гравців
        private bool isPlayerXTurn = true;
        public TicTacToeWindow()
        {
            InitializeComponent();
            ResetBoard();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            // За індексацією від нуля визначаємо кнопку button(цифра)
            int index = int.Parse(clickedButton.Name.Substring(6)) - 1;

            // Якщо кнопка вже має значення Х або О, нічого не відбудеться.
            if (board[index] != null)
                return;

            // Перевіряємо, чи це хід гравця X true, якщо false то О
            board[index] = isPlayerXTurn ? "X" : "O";
            // Встановлюємо значення Х або О на кнопку 
            clickedButton.Text = board[index];

            // Перевірка на перемогу або нічию
            if (CheckForWin())
            {
                MessageBox.Show($"{(isPlayerXTurn ? "X" : "O")} Виграв!");
                ResetBoard();
                return;
            }
            if (!board.Contains(null))
            {
                MessageBox.Show("Нічія!");
                ResetBoard();
                return;
            }
            // Зміна черги гравців
            isPlayerXTurn = !isPlayerXTurn;
        }

        // Функція для перевірки на перемогу
        private bool CheckForWin()
        {
            // Усі можливі виграшні комбінації
            int[,] winningCombinations = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Горизонтальні лінії
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Вертикальні лінії
                {0, 4, 8}, {2, 4, 6}             // Діагоналі
            };

            // Перевірка кожної комбінації
            for (int i = 0; i < 8; i++)
            {
                if (board[winningCombinations[i, 0]] != null &&
                    board[winningCombinations[i, 0]] == board[winningCombinations[i, 1]] &&
                    board[winningCombinations[i, 0]] == board[winningCombinations[i, 2]])
                {
                    return true; // Перемога
                }
            }
            return false; // Якщо нема перемоги
        }

        // Функція для скидання дошки
        private void ResetBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = null;
                Button button = Controls.Find($"button{i + 1}", true).FirstOrDefault() as Button;
                if (button != null)
                {
                    button.Text = "";
                }
            }
            isPlayerXTurn = true; // Починає гравець X
        }
    }
}
