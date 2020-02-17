using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Demo_Wpf_TheSimpleGame.Business;
using Demo_Wpf_TheSimpleGame.Presentation;

namespace MySimpleGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public GameViewModel GameViewModel { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //
            // instantiate the view model
            //
            GameViewModel gameViewModel = new GameViewModel();

            //
            // instantiate the view, set data context, and show window.
            //
            GameView gameView = new GameView(gameViewModel);
            gameView.DataContext = gameViewModel;
            gameView.Show();
        }
    }
}
