using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_Wpf_TheSimpleGame.Models;
using Demo_Wpf_TheSimpleGame.Business;
using Demo_Wpf_TheSimpleGame.Presentation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Media;
using Newtonsoft.Json;


namespace Demo_Wpf_TheSimpleGame.Presentation
{
    public class GameViewModel : ObservableObject
    {
        private GameBusiness _gameBusiness;

        private Gameboard _gameboard;
        private (Player playerOne, Player playerTwo) _currentPlayers;
        private Player _playerX;
        private Player _playerO;
        private string _messageBoxContent;

        public Gameboard Gameboard
        {
            get { return _gameboard; }
            set
            {
                _gameboard = value;
                OnPropertyChanged(nameof(Gameboard));
            }
        }

        public Player PlayerX
        {
            get { return _playerX; }
            set
            {
                _playerX = value;
                OnPropertyChanged(nameof(PlayerX));
            }
        }

        public Player PlayerO
        {
            get { return _playerO; }
            set
            {
                _playerO = value;
                OnPropertyChanged(nameof(PlayerO));
            }
        }

        public string MessageBoxContent
        {
            get { return _messageBoxContent; }
            set
            {
                _messageBoxContent = value;
                OnPropertyChanged(nameof(MessageBoxContent));
            }
        }

        public GameViewModel()
        {
            _gameBusiness = new GameBusiness();

            InitializeGame();
        }

        /// <summary>
        /// Initialize Game
        /// </summary>
        private void InitializeGame()
        {
            _currentPlayers = _gameBusiness.GetCurrentPlayers();

            _playerX = _currentPlayers.playerOne;
            _playerO = _currentPlayers.playerTwo;

            _gameboard = new Gameboard();

            _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
            MessageBoxContent = $"{PlayerX.Name} Moves";
        }

        /// <summary>
        /// Player Move
        /// </summary>
        public void PlayerMove(int row, int column)
        {
            if (_gameboard.GameboardPositionAvailable(new GameboardPosition(row, column)))
            {
                if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerXTurn)
                {
                    Gameboard.CurrentBoard[row][column] = Gameboard.PLAYER_PIECE_X;
                    OnPropertyChanged(nameof(Gameboard));
                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerOTurn;
                    MessageBoxContent = $"{PlayerO.Name} Moves";
                }
                else
                {
                    Gameboard.CurrentBoard[row][column] = Gameboard.PLAYER_PIECE_O;
                    OnPropertyChanged(nameof(Gameboard));
                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                    MessageBoxContent = $"{PlayerX.Name} Moves";
                }
                UpdateCurrentRoundState();
            }
        }

        /// <summary>
        /// Game Command
        /// </summary>
        internal void GameCommand(string commandName)
        {
            GameViewModel gameViewModel = new GameViewModel();
            GameView gameView = new GameView(gameViewModel);
            switch (commandName)
            {
                case "NewGame":
                    ResetBoard();
                    _gameboard.InitializeGameboard();
                    OnPropertyChanged(nameof(Gameboard));

                    MessageBoxContent = $"{PlayerX.Name} moves.";
                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                    break;

                case "Reset":
                    _gameboard.InitializeGameboard();
                    OnPropertyChanged(nameof(Gameboard));

                    MessageBoxContent = $"{PlayerX.Name} moves.";
                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                    break;

                case "QuitSave":
                    _gameBusiness.SaveAllPlayers();
                    gameView.Close();
                    break;

                default:
                    MessageBoxContent = "Button Not found";
                    break;
            }
        }

        public void ResetBoard()
        {
            _playerX.Name = "Player 1";
            _playerX.Wins = 0;
            _playerX.Losses = 0;
            _playerX.Ties = 0;

            _playerO.Name = "Player 2";
            _playerO.Wins = 0;
            _playerO.Losses = 0;
            _playerO.Ties = 0;
        }

        /// <summary>
        /// Update current round state
        /// </summary>
        public void UpdateCurrentRoundState()
        {
            GameViewModel gameViewModel = new GameViewModel();
            GameView gameView = new GameView(gameViewModel);

            _gameboard.UpdateGameboardState();
            if (_gameboard.CurrentRoundState == Gameboard.GameboardState.CatsGame)
            {
                PlayerO.Ties++;
                PlayerX.Ties++;
                MessageBoxContent = "Cat Game";
                Gameboard.CurrentRoundState = Gameboard.GameboardState.NewRound;
                _gameboard.InitializeGameboard();
            }
            else if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerXWin)
            {
                PlayerX.Wins++;
                PlayerO.Losses++;
                MessageBoxContent = $"{PlayerX.Name} Wins!";
                Gameboard.CurrentRoundState = Gameboard.GameboardState.NewRound;
                _gameboard.InitializeGameboard();
            }
            else if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerOWin)
            {
                PlayerO.Wins++;
                PlayerX.Losses++;
                MessageBoxContent = $"{PlayerO.Name} Wins!";
                Gameboard.CurrentRoundState = Gameboard.GameboardState.NewRound;
                _gameboard.InitializeGameboard();
            }
        }

        public void HelpButton()
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }
    }
}
