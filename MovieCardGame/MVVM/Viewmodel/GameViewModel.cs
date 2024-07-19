using MovieCardGame.Core;
using MovieCardGame.MVVM.Model;
using MovieCardGame.MVVM.View;
using OMDbApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using OMDbApiNet.Model;
using System.Windows.Documents;

namespace MovieCardGame.MVVM.Viewmodel
{

   
    class GameViewModel : ObservableObject
    {

        private readonly string apiKey = "8863752a";
        private FileIO fileIO;
        private OmdbClient omdb;

        private int _playerPoints;
        public int PlayerPoints
        {
            get => _playerPoints;
            set
            {
                if ( _playerPoints != value )
                {
                    _playerPoints = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _opponentPoints;
        public int OpponentPoints
        {
            get => _opponentPoints;
            set
            {
                if ( _opponentPoints != value )
                {
                    _opponentPoints = value;
                    OnPropertyChanged();
                }
            }
        }

        private Deck _playerDeck;
        public Deck PlayerDeck 
        {
            get => _playerDeck;
            set
            {
                if ( _playerDeck != value )
                {
                    _playerDeck = value;
                    OnPropertyChanged();
                }
            }
        }

        private Deck _opponentDeck;
        public Deck OpponentDeck
        {
            get => _opponentDeck;
            set
            {
                if ( _opponentDeck != value )
                {
                    _opponentDeck = value;
                    OnPropertyChanged();
                }
                
            }
        }

        private Card _currentPlayerCard;
        public Card CurrentPlayerCard 
        {
            get => _currentPlayerCard;
            set
            {
                if ( _currentPlayerCard != value )
                {
                     _currentPlayerCard = value;
                     OnPropertyChanged();
                }
                
            }
        }

        private Card _currentOpponentCard;
        public Card CurrentOpponentCard
        {
            get => _currentOpponentCard;
            set
            {
                if ( _currentOpponentCard != value )
                {
                    _currentOpponentCard = value;
                    OnPropertyChanged();
                }

            }
        }

        public RelayCommand ImdbRatingHit {  get; set; }
        public RelayCommand ImdbVotesHit { get; set; }
        public RelayCommand MetascoreHit { get; set; }
        public RelayCommand RuntimeHit { get; set; }
        public RelayCommand BoxOfficeResultHit { get; set; }
        public GameViewModel() 
        {
            omdb = new OmdbClient( apiKey );
            fileIO = new FileIO();
            if ( !fileIO.ReadMoviesFromCSV() )
            {
                FetchTop250Movies();
            }
            var movies =fileIO.Movies;
            var maxIndex = movies.Count-1;
            var sublist = movies.GetRange(0, (int)(maxIndex/2)); // (gets elements 5,6,7,8,9)
            var sublist2 = movies.GetRange((int)((maxIndex/2)+1), (int)((maxIndex/2)));
            PlayerDeck = new Deck( sublist );
            OpponentDeck = new Deck( sublist2 );
            PlayerDeck.Shuffle();
            OpponentDeck.Shuffle();
            CurrentPlayerCard = PlayerDeck.DrawCard();
            CurrentOpponentCard = OpponentDeck.DrawCard();
            PlayerPoints = 0;
            OpponentPoints = 0;

            ImdbRatingHit = new RelayCommand(
                ( o ) =>
                {
                    if ( CurrentPlayerCard.ImdbRating == CurrentOpponentCard.ImdbRating )
                    {
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                    else if ( CurrentPlayerCard.ImdbRating > CurrentOpponentCard.ImdbRating )
                    {
                        PlayerPoints++;
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                    }
                    else
                    {
                        OpponentPoints++;
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                } );

            ImdbVotesHit = new RelayCommand(
                ( o ) =>
                {
                    if ( CurrentPlayerCard.ImdbVotes == CurrentOpponentCard.ImdbVotes )
                    {
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                    else if( CurrentPlayerCard.ImdbVotes > CurrentOpponentCard.ImdbVotes )
                    {
                        PlayerPoints++;
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                    }
                    else
                    {
                        OpponentPoints++;
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                } );

            MetascoreHit = new RelayCommand(
                ( o ) =>
                {
                    if ( CurrentPlayerCard.Metascore == CurrentOpponentCard.Metascore )
                    {
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                    else if( CurrentPlayerCard.Metascore > CurrentOpponentCard.Metascore )
                    {
                        PlayerPoints++;
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                    }
                    else
                    {
                        OpponentPoints++;
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                } );

            RuntimeHit = new RelayCommand(
                ( o ) =>
                {
                    if ( CurrentPlayerCard.Runtime == CurrentOpponentCard.Runtime )
                    {
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                    else if( CurrentPlayerCard.Runtime > CurrentOpponentCard.Runtime )
                    {
                        PlayerPoints++;
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                    }
                    else
                    {
                        OpponentPoints++;
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                } );

            BoxOfficeResultHit = new RelayCommand(
                ( o ) =>
                {
                    if ( CurrentPlayerCard.BoxOfficeResult == CurrentOpponentCard.BoxOfficeResult )
                    {
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                    else if( CurrentPlayerCard.BoxOfficeResult > CurrentOpponentCard.BoxOfficeResult )
                    {
                        PlayerPoints++;
                        CurrentOpponentCard = OpponentDeck.DrawCard();
                    }
                    else
                    {
                        OpponentPoints++;
                        CurrentPlayerCard = PlayerDeck.DrawCard();
                    }
                } );

            

        }

        public GameViewModel( Deck playerDeck, Deck opponentDeck )
        {
            PlayerDeck = playerDeck;
            OpponentDeck = opponentDeck;
            CurrentPlayerCard = playerDeck.DrawCard();
            CurrentOpponentCard = opponentDeck.DrawCard();
        }

        private void FetchTop250Movies()
        {
            var titels = fileIO.ReadFromTextFile();

            var movies = new List<Movie>();
            foreach ( var titel in titels )
            {
                Item item = omdb.GetItemByTitle( titel, OmdbType.Movie );
                if ( item != null )
                {
                    fileIO.AddMovieItem( item );
                }
            }
        }

    }
}
