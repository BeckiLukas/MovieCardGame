using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MovieCardGame.Core
{
    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" )
        {
            if ( !string.IsNullOrEmpty( propertyName ) )
            {
                PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }
    }
}
