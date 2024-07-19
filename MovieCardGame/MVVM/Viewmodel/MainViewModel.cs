using MovieCardGame.Core;
using MovieCardGame.MVVM.Model;
using OMDbApiNet;
using OMDbApiNet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieCardGame.MVVM.Viewmodel
{
    class MainViewModel : ObservableObject
    {

        private FileIO fileIO;
        public StartViewModel StartViewModel { get; set; }
        public GameViewModel GameViewModel { get; set; }
        private object _currentView;
        public object CurrentView
        {  
            get { return _currentView; } 
            set
            {
                _currentView = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentView));
            }    
        }

        public RelayCommand CloseCommand {  get; set; }

        public RelayCommand StartGameCommand { get; set; }
        public MainViewModel() 
        {
            fileIO = new FileIO();

            CloseCommand = new RelayCommand(
                ( o ) =>
                {
                    fileIO.SaveMoviesToCSV();
                    System.Windows.Application.Current.Shutdown();
                } );

            StartGameCommand = new RelayCommand(
                ( o ) =>
                {
                    Console.WriteLine( "Debug" );
                    CurrentView = GameViewModel;
                } );

            StartViewModel = new StartViewModel();
            GameViewModel = new GameViewModel();
            CurrentView = GameViewModel;

           
        }

        

    }
}
