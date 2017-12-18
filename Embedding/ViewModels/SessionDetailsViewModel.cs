
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Embedding.Models;

namespace Embedding.ViewModels
{
    public partial class SessionDetailsViewModel
    {
        public SessionDetailModel Details { get; private set; }

        readonly INavigationService navigationService;

        public SessionDetailsViewModel() : this(null)
        {

        }

        public SessionDetailsViewModel(SessionDetailModel details)
        {
            Details = details;
        }
    }


    partial class SessionDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value)) return;

            property = value;

            RaisePropertyChanged(propertyName);
        }
    }
}
