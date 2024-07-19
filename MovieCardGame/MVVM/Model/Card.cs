using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCardGame.MVVM.Model
{
    public enum Type 
    { 
        Movie, 
        Series, 
        Game 
    };
    class Card
    {
        public string Name {  get; set; }
        public Type Type { get; set; }

        public string Image {  get; set; }
        public int Runtime { get; set; }

        public int ImdbVotes { get; set; }
        public float ImdbRating { get; set; }
        public short Metascore { get; set; }
        public decimal BoxOfficeResult { get; set; }

        public Card(Movie movie)
        {
            Name = movie.Title;
            Type = Type.Movie;
            Image = movie.Poster;
            Runtime = Int32.Parse( movie.Runtime );
            ImdbVotes = Int32.Parse( movie.ImdbVotes );
            ImdbRating = float.Parse( movie.ImdbRating );
            Metascore = Int16.Parse( movie.Metascore );
            try
            {
                BoxOfficeResult = Int32.Parse( movie.BoxOffice );
            }
            catch( System.FormatException e) 
            {
                BoxOfficeResult = -1;
            }
        }
        public Card( string name, Type type, string image, int runtime, int imdbVotes, float imdbRating, short metascore, int boxOfficeResult )
        {
            Name = name;
            Type = type;
            Image = image;
            Runtime = runtime;
            ImdbVotes = imdbVotes;
            ImdbRating = imdbRating;
            Metascore = metascore;
            BoxOfficeResult = boxOfficeResult;
        }
        public Card()
        {
            Name = "Name";
            Type = Type.Movie;
            Image = "C:/Users/lukas/source/repos/MovieCardGame/MovieCardGame/Images/Pizza.jpg";
            Runtime = 100;
            ImdbVotes = 800;
            ImdbRating = 5.4F;
            Metascore = 99;
            BoxOfficeResult = 12345678;
        }
    }
}
