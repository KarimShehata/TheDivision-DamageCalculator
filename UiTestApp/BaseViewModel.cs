using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UiTestApp
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Protected Methods

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Protected Methods

    }
}