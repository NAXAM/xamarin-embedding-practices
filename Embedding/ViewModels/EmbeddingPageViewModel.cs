using Embedding.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Embedding.ViewModels
{
    public interface INavigationService
    {
        Task<bool> GoBackAsync();

        Task<bool> NavigateAsync(string key, IDictionary<string, string> parameters = null);
    }

    public partial class EmbeddingPageViewModel
    {
        public WalkthroughModel[] Walkthroughs { get; set; }

        readonly INavigationService navigationService;

        public EmbeddingPageViewModel() : this(null) { }

        public EmbeddingPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Walkthroughs = new[] {
                new WalkthroughModel {
                    Content = "a dark grey or dark brown crypto-crystalline substance which has an almost vitreous lustre, and when pure appears structureless to the unaided eye.",
                    Image = "bg_walkthrough_1_1"
                },
                new WalkthroughModel {
                    Content = "Lutheran Church in an attempt to heal the breach which, since the death of Luther, had been widening between the extreme Lutherans and the Crypto-Calvinists.",
                    Image = "bg_walkthrough_1_2"
                },
                new WalkthroughModel {
                    Content = "Christianity, was at least indifferent to its dogmas and rites, cherishing a sentimental recollection of the older and more glorious days of the empire.",
                    Image = "bg_walkthrough_1_3"
                },
                new WalkthroughModel {
                    Content = "a Portuguese Marano or Crypto-Jew, who came to England in the reign of Charles I.",
                    Image = "bg_walkthrough_1_4"
                }
            };
            //Walkthroughs = new[] {
            //    new WalkthroughModel
            //    {
            //        Content = "a dark grey or dark brown crypto-crystalline substance which has an almost vitreous lustre, and when pure appears structureless to the unaided eye.",
            //        Image = "bg_walkthrough_2_1"
            //    },
            //    new WalkthroughModel
            //    {
            //        Content = "Lutheran Church in an attempt to heal the breach which, since the death of Luther, had been widening between the extreme Lutherans and the Crypto-Calvinists.",
            //        Image = "bg_walkthrough_2_2"
            //    },
            //    new WalkthroughModel
            //    {
            //        Content = "Christianity, was at least indifferent to its dogmas and rites, cherishing a sentimental recollection of the older and more glorious days of the empire.",
            //        Image = "bg_walkthrough_2_3"
            //    }
            //};
        }

        int _CurrentWalkthroughIndex;
        public int CurrentWalkthroughIndex
        {
            get => _CurrentWalkthroughIndex;
            set
            {
                SetProperty(ref _CurrentWalkthroughIndex, value);
                RaisePropertyChanged(nameof(CurrentWalkthroughContent));
            }
        }

        public string CurrentWalkthroughContent => Walkthroughs[CurrentWalkthroughIndex].Content;

        ICommand _NextCommand;
        public ICommand NextCommand
        {
            get { return (_NextCommand = _NextCommand ?? new Command<object>(ExecuteNextCommand, CanExecuteNextCommand)); }
        }
        bool CanExecuteNextCommand(object obj) => true;
        async void ExecuteNextCommand(object obj)
        {
            if (CurrentWalkthroughIndex < Walkthroughs.Length - 1)
            {
                CurrentWalkthroughIndex += 1;
                return;
            }

            if (await navigationService?.NavigateAsync("end_of_walkthrough") != true)
            {
                CurrentWalkthroughIndex = 0;
            }

        }
    }

    partial class EmbeddingPageViewModel : INotifyPropertyChanged
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
