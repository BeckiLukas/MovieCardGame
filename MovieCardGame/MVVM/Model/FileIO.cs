using FileHelpers;
using OMDbApiNet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCardGame.MVVM.Model
{

    [DelimitedRecord( "," )]
    public class Movie
    {
        public string Title;
        public string Year;
        public string Runtime;
        public string Poster;
        public string Metascore;
        public string ImdbRating;
        public string ImdbVotes;
        public string ImdbID;
        public string Type;
        public string BoxOffice;
    }


    class FileIO
    {
        private readonly string csvName = "C:/Users/lukas/source/repos/MovieCardGame/MovieCardGame/Movies.csv";
        private readonly string textFileName = "C:/Users/lukas/source/repos/MovieCardGame/MovieCardGame/Top 250 Movies.txt";

        private FileHelperEngine<Movie> engine;
        public List<Movie> Movies { get; set; }

        public FileIO()
        {
            engine = new FileHelperEngine<Movie>();
            Movies = new List<Movie>();
        }

        public void AddMovieItem( Item item )
        {
            Movie movie = new Movie();
            movie.Title = item.Title;
            movie.Year = item.Year;
            movie.Runtime = item.Runtime.Split( ' ' )[0];
            movie.Poster = item.Poster;
            movie.Metascore = item.Metascore;
            movie.ImdbVotes = item.ImdbVotes.Replace( ",", "" );
            movie.ImdbRating = item.ImdbRating;
            movie.ImdbID = item.ImdbId;
            movie.Type = item.Type;
            movie.BoxOffice = item.BoxOffice.Substring( 1 ).Replace( ",", "" );
            Movies.Add( movie );
        }

        public bool ReadMoviesFromCSV()
        {
            var fileInfo = new FileInfo( csvName );
            if ( !fileInfo.Exists || fileInfo.Length == 0 )
            {
                return false;
            }
            Movies = new List<Movie>( engine.ReadFile(csvName) );
            return true;
        }

        public void SaveMoviesToCSV()
        {
            engine.WriteFile( csvName, Movies );
        }

        public List<string> ReadFromTextFile()
        {
            var lines = new List<string>();
            string line;
            try
            {
                StreamReader sr = new StreamReader(textFileName);
                do
                {
                    line = sr.ReadLine();
                    if ( line != null )
                    {
                        lines.Add( line );
                    }
                }
                while ( line != null );
                sr.Close();
            }
            catch ( Exception e )
            {
                Console.WriteLine( "Exception: " + e.Message );
            }
            finally
            {
                Console.WriteLine( "Executing finally block." );
            }
            return lines;
        }
    }
}
