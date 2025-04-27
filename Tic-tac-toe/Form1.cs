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
        private string[] board = new string[9];
        private bool isPlayerXTurn = true;
        public TicTacToeWindow()
        {
            InitializeComponent();
            ResetBoard();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            // Находим индекс кнопки в массиве
            int index = int.Parse(clickedButton.Name.Substring(6)) - 1;

            // Если кнопка уже нажата, ничего не делаем
            if (board[index] != null)
                return;

            // Обновляем состояние кнопки
            board[index] = isPlayerXTurn ? "X" : "O";
            clickedButton.Text = board[index];

            // Проверяем на победу
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
            // Меняем ход
            isPlayerXTurn = !isPlayerXTurn;
        }

        // Функция для проверки победы
        private bool CheckForWin()
        {
            // Все возможные комбинации победных линий
            int[,] winningCombinations = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Горизонтальные линии
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Вертикальные линии
                {0, 4, 8}, {2, 4, 6}             // Диагонали
            };

            // Проверка всех комбинаций
            for (int i = 0; i < 8; i++)
            {
                if (board[winningCombinations[i, 0]] != null &&
                    board[winningCombinations[i, 0]] == board[winningCombinations[i, 1]] &&
                    board[winningCombinations[i, 0]] == board[winningCombinations[i, 2]])
                {
                    return true; // Победа
                }
            }
            return false; // Нет победы
        }

        // Функция для сброса состояния доски
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
            isPlayerXTurn = true; // Начинает X
        }
    }
}
