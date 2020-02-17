using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using Demo_Wpf_TheSimpleGame.Presentation;

//using TicTacToe.ConsoleApp.Model;

namespace Demo_Wpf_TheSimpleGame.Models
{
    public class Gameboard : ObservableObject
    {
        #region ENUMS

        public const string PLAYER_PIECE_X = "Red";
        public const string PLAYER_PIECE_O = "Yellow";
        public const string PLAYER_PIECE_NONE = "Black";

        public enum GameboardState
        {
            NewRound,
            PlayerXTurn,
            PlayerOTurn,
            PlayerXWin,
            PlayerOWin,
            CatsGame
        }

        #endregion

        #region FIELDS
        
        private const int MAX_NUM_OF_COLUMNS = 7;
        private const int MAX_NUM_OF_ROWS = 6;

        private string[][] _currentBoard;

        #endregion

        #region PROPERTIES

        public int MaxNumOfColumns
        {
            get { return MAX_NUM_OF_COLUMNS; }
        }

        public string[][] CurrentBoard
        {
            get { return _currentBoard; }
            set
            {
                _currentBoard = value;
                OnPropertyChanged(nameof(CurrentBoard));
            }
        }

        public int MaxNumOfRows
        {
            get { return MAX_NUM_OF_ROWS; }
        }

        public GameboardState CurrentRoundState { get; set; }
        #endregion

        #region CONSTRUCTORS

        public Gameboard()
        {
            CurrentBoard = new string[7][];
            CurrentBoard[0] = new string[7];
            CurrentBoard[1] = new string[7];
            CurrentBoard[2] = new string[7];
            CurrentBoard[3] = new string[7];
            CurrentBoard[4] = new string[7];
            CurrentBoard[5] = new string[7];
            CurrentBoard[6] = new string[7];


            InitializeGameboard();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// fill the game board array with "None" enum values
        /// </summary>
        public void InitializeGameboard()
        {
            CurrentRoundState = GameboardState.NewRound;

            // Set all PlayerPiece array values to "None"
            for (int row = 0; row < MAX_NUM_OF_ROWS; row++)
            {
                for (int column = 0; column < MAX_NUM_OF_COLUMNS; column++)
                {
                    CurrentBoard[row][column] = PLAYER_PIECE_NONE;
                }
            }
        }

        /// <summary>
        /// Determine if the game board position is taken
        /// </summary>
        public bool GameboardPositionAvailable(GameboardPosition gameboardPosition)
        {
            // Confirm that the board position is empty
            // Note: gameboardPosition converted to array index by subtracting 1
            if (CurrentBoard[gameboardPosition.Row][gameboardPosition.Column] == PLAYER_PIECE_NONE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Update the game board state if a player wins or a cat's game happens.
        /// </summary>
        public void UpdateGameboardState()
        {
            //player 1 has won
            if (FourInARow(PLAYER_PIECE_X))
            {
                CurrentRoundState = GameboardState.PlayerXWin;
            }

            // A player 2 has won
            else if (FourInARow(PLAYER_PIECE_O))
            {
                CurrentRoundState = GameboardState.PlayerOWin;
            }

            // All positions filled
            else if (IsCatsGame())
            {
                CurrentRoundState = GameboardState.CatsGame;
            }
        }
        
        public bool IsCatsGame()
        {
            // All positions on board are filled and no winner
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    if (CurrentBoard[row][column] == PLAYER_PIECE_NONE)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Check for any 4 in a row.
        /// </summary>
        /// <param name="playerPieceToCheck">Player's game piece to check</param>
        /// <returns>true if a player has won</returns>
        private bool FourInARow(string playerPieceToCheck)
        {
            #region Check Rows
            //
            // Check rows for player win
            //
            if ((CurrentBoard[0][0] == playerPieceToCheck &&
                CurrentBoard[0][1] == playerPieceToCheck &&
                CurrentBoard[0][2] == playerPieceToCheck &&
                CurrentBoard[0][3] == playerPieceToCheck)
                ||
                (CurrentBoard[1][0] == playerPieceToCheck &&
                CurrentBoard[1][1] == playerPieceToCheck &&
                CurrentBoard[1][2] == playerPieceToCheck &&
                CurrentBoard[1][3] == playerPieceToCheck)
                ||
                (CurrentBoard[2][0] == playerPieceToCheck &&
                CurrentBoard[2][1] == playerPieceToCheck &&
                CurrentBoard[2][2] == playerPieceToCheck &&
                CurrentBoard[2][3] == playerPieceToCheck)
                ||
                (CurrentBoard[3][0] == playerPieceToCheck &&
                CurrentBoard[3][1] == playerPieceToCheck &&
                CurrentBoard[3][2] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck)
                ||
                (CurrentBoard[4][0] == playerPieceToCheck &&
                CurrentBoard[4][1] == playerPieceToCheck &&
                CurrentBoard[4][2] == playerPieceToCheck &&
                CurrentBoard[4][3] == playerPieceToCheck)
                ||
                (CurrentBoard[5][0] == playerPieceToCheck &&
                CurrentBoard[5][1] == playerPieceToCheck &&
                CurrentBoard[5][2] == playerPieceToCheck &&
                CurrentBoard[5][3] == playerPieceToCheck)
                ||
                (CurrentBoard[0][3] == playerPieceToCheck &&
                CurrentBoard[0][4] == playerPieceToCheck &&
                CurrentBoard[0][5] == playerPieceToCheck &&
                CurrentBoard[0][6] == playerPieceToCheck)
                ||
                (CurrentBoard[1][3] == playerPieceToCheck &&
                CurrentBoard[1][4] == playerPieceToCheck &&
                CurrentBoard[1][5] == playerPieceToCheck &&
                CurrentBoard[1][6] == playerPieceToCheck)
                ||
                (CurrentBoard[2][3] == playerPieceToCheck &&
                CurrentBoard[2][4] == playerPieceToCheck &&
                CurrentBoard[2][5] == playerPieceToCheck &&
                CurrentBoard[2][6] == playerPieceToCheck)
                ||
                (CurrentBoard[3][3] == playerPieceToCheck &&
                CurrentBoard[3][4] == playerPieceToCheck &&
                CurrentBoard[3][5] == playerPieceToCheck &&
                CurrentBoard[3][6] == playerPieceToCheck)
                ||
                (CurrentBoard[4][3] == playerPieceToCheck &&
                CurrentBoard[4][4] == playerPieceToCheck &&
                CurrentBoard[4][5] == playerPieceToCheck &&
                CurrentBoard[4][6] == playerPieceToCheck)
                ||
                (CurrentBoard[5][3] == playerPieceToCheck &&
                CurrentBoard[5][4] == playerPieceToCheck &&
                CurrentBoard[5][5] == playerPieceToCheck &&
                CurrentBoard[5][6] == playerPieceToCheck)
                ||
                (CurrentBoard[0][1] == playerPieceToCheck &&
                CurrentBoard[0][2] == playerPieceToCheck &&
                CurrentBoard[0][3] == playerPieceToCheck &&
                CurrentBoard[0][4] == playerPieceToCheck)
                ||
                (CurrentBoard[1][1] == playerPieceToCheck &&
                CurrentBoard[1][2] == playerPieceToCheck &&
                CurrentBoard[1][3] == playerPieceToCheck &&
                CurrentBoard[1][4] == playerPieceToCheck)
                ||
                (CurrentBoard[2][1] == playerPieceToCheck &&
                CurrentBoard[2][2] == playerPieceToCheck &&
                CurrentBoard[2][3] == playerPieceToCheck &&
                CurrentBoard[2][4] == playerPieceToCheck)
                ||
                (CurrentBoard[3][1] == playerPieceToCheck &&
                CurrentBoard[3][2] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck &&
                CurrentBoard[3][4] == playerPieceToCheck)
                ||
                (CurrentBoard[4][1] == playerPieceToCheck &&
                CurrentBoard[4][2] == playerPieceToCheck &&
                CurrentBoard[4][3] == playerPieceToCheck &&
                CurrentBoard[4][4] == playerPieceToCheck)
                ||
                (CurrentBoard[5][1] == playerPieceToCheck &&
                CurrentBoard[5][2] == playerPieceToCheck &&
                CurrentBoard[5][3] == playerPieceToCheck &&
                CurrentBoard[5][4] == playerPieceToCheck)
                ||
                (CurrentBoard[0][2] == playerPieceToCheck &&
                CurrentBoard[0][3] == playerPieceToCheck &&
                CurrentBoard[0][4] == playerPieceToCheck &&
                CurrentBoard[0][5] == playerPieceToCheck)
                ||
                (CurrentBoard[1][2] == playerPieceToCheck &&
                CurrentBoard[1][3] == playerPieceToCheck &&
                CurrentBoard[1][4] == playerPieceToCheck &&
                CurrentBoard[1][5] == playerPieceToCheck)
                ||
                (CurrentBoard[2][2] == playerPieceToCheck &&
                CurrentBoard[2][3] == playerPieceToCheck &&
                CurrentBoard[2][4] == playerPieceToCheck &&
                CurrentBoard[2][5] == playerPieceToCheck)
                ||
                (CurrentBoard[3][2] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck &&
                CurrentBoard[3][4] == playerPieceToCheck &&
                CurrentBoard[3][5] == playerPieceToCheck)
                ||
                (CurrentBoard[4][2] == playerPieceToCheck &&
                CurrentBoard[4][3] == playerPieceToCheck &&
                CurrentBoard[4][4] == playerPieceToCheck &&
                CurrentBoard[4][5] == playerPieceToCheck)
                ||
                (CurrentBoard[5][2] == playerPieceToCheck &&
                CurrentBoard[5][3] == playerPieceToCheck &&
                CurrentBoard[5][4] == playerPieceToCheck &&
                CurrentBoard[5][5] == playerPieceToCheck))
            {
                return true;
            }
            #endregion

            #region Check Columns
            //
            // Check columns for player win
            //
            if ((CurrentBoard[0][0] == playerPieceToCheck &&
                CurrentBoard[1][0] == playerPieceToCheck &&
                CurrentBoard[2][0] == playerPieceToCheck &&
                CurrentBoard[3][0] == playerPieceToCheck)
                ||
                (CurrentBoard[0][1] == playerPieceToCheck &&
                CurrentBoard[1][1] == playerPieceToCheck &&
                CurrentBoard[2][1] == playerPieceToCheck &&
                CurrentBoard[3][1] == playerPieceToCheck)
                ||
                (CurrentBoard[0][2] == playerPieceToCheck &&
                CurrentBoard[1][2] == playerPieceToCheck &&
                CurrentBoard[2][2] == playerPieceToCheck &&
                CurrentBoard[3][2] == playerPieceToCheck)
                ||
                (CurrentBoard[0][3] == playerPieceToCheck &&
                CurrentBoard[1][3] == playerPieceToCheck &&
                CurrentBoard[2][3] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck)
                ||
                (CurrentBoard[0][4] == playerPieceToCheck &&
                CurrentBoard[1][4] == playerPieceToCheck &&
                CurrentBoard[2][4] == playerPieceToCheck &&
                CurrentBoard[3][4] == playerPieceToCheck)
                ||
                (CurrentBoard[0][5] == playerPieceToCheck &&
                CurrentBoard[1][5] == playerPieceToCheck &&
                CurrentBoard[2][5] == playerPieceToCheck &&
                CurrentBoard[3][5] == playerPieceToCheck)
                ||
                (CurrentBoard[0][6] == playerPieceToCheck &&
                CurrentBoard[1][6] == playerPieceToCheck &&
                CurrentBoard[2][6] == playerPieceToCheck &&
                CurrentBoard[3][6] == playerPieceToCheck)
                ||
                (CurrentBoard[2][0] == playerPieceToCheck &&
                CurrentBoard[3][0] == playerPieceToCheck &&
                CurrentBoard[4][0] == playerPieceToCheck &&
                CurrentBoard[5][0] == playerPieceToCheck)
                ||
                (CurrentBoard[2][1] == playerPieceToCheck &&
                CurrentBoard[3][1] == playerPieceToCheck &&
                CurrentBoard[4][1] == playerPieceToCheck &&
                CurrentBoard[5][1] == playerPieceToCheck)
                ||
                (CurrentBoard[2][2] == playerPieceToCheck &&
                CurrentBoard[3][2] == playerPieceToCheck &&
                CurrentBoard[4][2] == playerPieceToCheck &&
                CurrentBoard[5][2] == playerPieceToCheck)
                ||
                (CurrentBoard[2][3] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck &&
                CurrentBoard[4][3] == playerPieceToCheck &&
                CurrentBoard[5][3] == playerPieceToCheck)
                ||
                (CurrentBoard[2][4] == playerPieceToCheck &&
                CurrentBoard[3][4] == playerPieceToCheck &&
                CurrentBoard[4][4] == playerPieceToCheck &&
                CurrentBoard[5][4] == playerPieceToCheck)
                ||
                (CurrentBoard[2][5] == playerPieceToCheck &&
                CurrentBoard[3][5] == playerPieceToCheck &&
                CurrentBoard[4][5] == playerPieceToCheck &&
                CurrentBoard[5][5] == playerPieceToCheck)
                ||
                (CurrentBoard[2][6] == playerPieceToCheck &&
                CurrentBoard[3][6] == playerPieceToCheck &&
                CurrentBoard[4][6] == playerPieceToCheck &&
                CurrentBoard[5][6] == playerPieceToCheck)
                ||
                (CurrentBoard[1][0] == playerPieceToCheck &&
                CurrentBoard[2][0] == playerPieceToCheck &&
                CurrentBoard[3][0] == playerPieceToCheck &&
                CurrentBoard[4][0] == playerPieceToCheck)
                ||
                (CurrentBoard[1][1] == playerPieceToCheck &&
                CurrentBoard[2][1] == playerPieceToCheck &&
                CurrentBoard[3][1] == playerPieceToCheck &&
                CurrentBoard[4][1] == playerPieceToCheck)
                ||
                (CurrentBoard[1][3] == playerPieceToCheck &&
                CurrentBoard[2][3] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck &&
                CurrentBoard[4][3] == playerPieceToCheck)
                ||
                (CurrentBoard[1][4] == playerPieceToCheck &&
                CurrentBoard[2][4] == playerPieceToCheck &&
                CurrentBoard[3][4] == playerPieceToCheck &&
                CurrentBoard[4][4] == playerPieceToCheck)
                ||
                (CurrentBoard[1][5] == playerPieceToCheck &&
                CurrentBoard[2][5] == playerPieceToCheck &&
                CurrentBoard[3][5] == playerPieceToCheck &&
                CurrentBoard[4][5] == playerPieceToCheck)
                ||
                (CurrentBoard[1][6] == playerPieceToCheck &&
                CurrentBoard[2][6] == playerPieceToCheck &&
                CurrentBoard[3][6] == playerPieceToCheck &&
                CurrentBoard[4][6] == playerPieceToCheck))
            {
                return true;
            }
            #endregion

            #region Check Diagonal 
            //
            // Check diagonals for player win
            //
            if (
                (CurrentBoard[0][0] == playerPieceToCheck &&
                 CurrentBoard[1][1] == playerPieceToCheck &&
                 CurrentBoard[2][2] == playerPieceToCheck &&
                 CurrentBoard[3][3] == playerPieceToCheck
                 )
                ||
                (CurrentBoard[1][0] == playerPieceToCheck &&
                 CurrentBoard[2][1] == playerPieceToCheck &&
                 CurrentBoard[3][2] == playerPieceToCheck &&
                 CurrentBoard[4][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[2][0] == playerPieceToCheck &&
                 CurrentBoard[3][1] == playerPieceToCheck &&
                 CurrentBoard[4][2] == playerPieceToCheck &&
                 CurrentBoard[5][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[0][1] == playerPieceToCheck &&
                 CurrentBoard[1][2] == playerPieceToCheck &&
                 CurrentBoard[2][3] == playerPieceToCheck &&
                 CurrentBoard[3][4] == playerPieceToCheck)
                 ||
                 (CurrentBoard[0][2] == playerPieceToCheck &&
                 CurrentBoard[1][3] == playerPieceToCheck &&
                 CurrentBoard[2][4] == playerPieceToCheck &&
                 CurrentBoard[3][5] == playerPieceToCheck)
                 ||
                 (CurrentBoard[0][3] == playerPieceToCheck &&
                 CurrentBoard[1][4] == playerPieceToCheck &&
                 CurrentBoard[2][5] == playerPieceToCheck &&
                 CurrentBoard[3][6] == playerPieceToCheck)
                 ||
                 (CurrentBoard[0][6] == playerPieceToCheck &&
                 CurrentBoard[1][5] == playerPieceToCheck &&
                 CurrentBoard[2][4] == playerPieceToCheck &&
                 CurrentBoard[3][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[1][6] == playerPieceToCheck &&
                 CurrentBoard[2][5] == playerPieceToCheck &&
                 CurrentBoard[3][4] == playerPieceToCheck &&
                 CurrentBoard[4][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[2][6] == playerPieceToCheck &&
                 CurrentBoard[3][5] == playerPieceToCheck &&
                 CurrentBoard[4][4] == playerPieceToCheck &&
                 CurrentBoard[5][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[0][5] == playerPieceToCheck &&
                 CurrentBoard[1][4] == playerPieceToCheck &&
                 CurrentBoard[2][3] == playerPieceToCheck &&
                 CurrentBoard[3][2] == playerPieceToCheck)
                 ||
                 (CurrentBoard[0][4] == playerPieceToCheck &&
                 CurrentBoard[1][3] == playerPieceToCheck &&
                 CurrentBoard[2][2] == playerPieceToCheck &&
                 CurrentBoard[3][1] == playerPieceToCheck)
                 ||
                 (CurrentBoard[0][3] == playerPieceToCheck &&
                 CurrentBoard[1][2] == playerPieceToCheck &&
                 CurrentBoard[2][1] == playerPieceToCheck &&
                 CurrentBoard[3][0] == playerPieceToCheck)
                 ||
                 (CurrentBoard[5][0] == playerPieceToCheck &&
                 CurrentBoard[4][1] == playerPieceToCheck &&
                 CurrentBoard[3][2] == playerPieceToCheck &&
                 CurrentBoard[2][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[4][0] == playerPieceToCheck &&
                 CurrentBoard[3][1] == playerPieceToCheck &&
                 CurrentBoard[2][2] == playerPieceToCheck &&
                 CurrentBoard[1][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[3][0] == playerPieceToCheck &&
                 CurrentBoard[2][1] == playerPieceToCheck &&
                 CurrentBoard[1][2] == playerPieceToCheck &&
                 CurrentBoard[0][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[5][1] == playerPieceToCheck &&
                 CurrentBoard[4][2] == playerPieceToCheck &&
                 CurrentBoard[3][3] == playerPieceToCheck &&
                 CurrentBoard[2][4] == playerPieceToCheck)
                 ||
                 (CurrentBoard[5][2] == playerPieceToCheck &&
                 CurrentBoard[4][3] == playerPieceToCheck &&
                 CurrentBoard[3][4] == playerPieceToCheck &&
                 CurrentBoard[2][5] == playerPieceToCheck)
                 ||
                 (CurrentBoard[5][3] == playerPieceToCheck &&
                 CurrentBoard[4][4] == playerPieceToCheck &&
                 CurrentBoard[3][5] == playerPieceToCheck &&
                 CurrentBoard[2][6] == playerPieceToCheck)
                 ||
                 (CurrentBoard[5][6] == playerPieceToCheck &&
                 CurrentBoard[4][5] == playerPieceToCheck &&
                 CurrentBoard[3][4] == playerPieceToCheck &&
                 CurrentBoard[2][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[4][6] == playerPieceToCheck &&
                 CurrentBoard[3][5] == playerPieceToCheck &&
                 CurrentBoard[2][4] == playerPieceToCheck &&
                 CurrentBoard[1][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[3][6] == playerPieceToCheck &&
                 CurrentBoard[2][5] == playerPieceToCheck &&
                 CurrentBoard[1][4] == playerPieceToCheck &&
                 CurrentBoard[0][3] == playerPieceToCheck)
                 ||
                 (CurrentBoard[5][5] == playerPieceToCheck &&
                 CurrentBoard[4][4] == playerPieceToCheck &&
                 CurrentBoard[3][3] == playerPieceToCheck &&
                 CurrentBoard[2][2] == playerPieceToCheck)
                 ||
                 (CurrentBoard[5][4] == playerPieceToCheck &&
                 CurrentBoard[4][3] == playerPieceToCheck &&
                 CurrentBoard[3][2] == playerPieceToCheck &&
                 CurrentBoard[2][1] == playerPieceToCheck)
                 ||
                 (CurrentBoard[5][3] == playerPieceToCheck &&
                 CurrentBoard[4][2] == playerPieceToCheck &&
                 CurrentBoard[3][1] == playerPieceToCheck &&
                 CurrentBoard[2][0] == playerPieceToCheck)
                 ||
                 (CurrentBoard[1][1] == playerPieceToCheck &&
                CurrentBoard[2][2] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck &&
                CurrentBoard[4][4] == playerPieceToCheck)
                ||
                (CurrentBoard[1][5] == playerPieceToCheck &&
                CurrentBoard[2][4] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck &&
                CurrentBoard[4][2] == playerPieceToCheck)
                ||
                (CurrentBoard[4][2] == playerPieceToCheck &&
                CurrentBoard[3][3] == playerPieceToCheck &&
                CurrentBoard[2][4] == playerPieceToCheck &&
                CurrentBoard[1][5] == playerPieceToCheck)
                ||
                (CurrentBoard[1][2] == playerPieceToCheck &&
                CurrentBoard[2][3] == playerPieceToCheck &&
                CurrentBoard[3][4] == playerPieceToCheck &&
                CurrentBoard[4][5] == playerPieceToCheck)
                ||
                (CurrentBoard[1][4] == playerPieceToCheck &&
                CurrentBoard[2][3] == playerPieceToCheck &&
                CurrentBoard[3][2] == playerPieceToCheck &&
                CurrentBoard[4][1] == playerPieceToCheck))
            {
                return true;
            }
            #endregion

            // No Player Has Won
            return false;
        }

        /// <summary>
        /// Add player's move to the game board.
        /// </summary>
        public void SetPlayerPiece(GameboardPosition gameboardPosition, string PlayerPiece)
        {
            // Row and column value adjusted to match array structure
            CurrentBoard[gameboardPosition.Row][gameboardPosition.Column] = PlayerPiece;

            // Change game board state to next player
            SetNextPlayer();
        }

        /// <summary>
        /// Switch the game board state to the next player.
        /// </summary>
        private void SetNextPlayer()
        {
            if (CurrentRoundState == GameboardState.PlayerXTurn)
            {
                CurrentRoundState = GameboardState.PlayerOTurn;
            }
            else
            {
                CurrentRoundState = GameboardState.PlayerXTurn;
            }
        }

        #endregion
    }
}

