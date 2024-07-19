using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCardGame.MVVM.Model
{
    class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
        }

        public Deck(List<Movie> movies)
        {
            cards = new List<Card>();
            foreach ( Movie movie in movies )
            {
                cards.Add( new Card( movie ) );
            }
        }

        public void AddCard( Card card )
        {
            cards.Add( card );
        }

        public Card DrawCard()
        {
            if ( CardsRemaining == 0 )
            {
                throw new InvalidOperationException( "No cards left in the deck." );
            }
            Card card = cards[0];
            cards.RemoveAt( 0 );
            return card;
        }

        public void Shuffle()
        {
            for ( int i = cards.Count - 1; i > 0; i-- )
            {
                int j = random.Next(i + 1);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public int CardsRemaining => cards.Count;
    }
}