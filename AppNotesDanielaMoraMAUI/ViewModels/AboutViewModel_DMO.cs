using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppNotesDanielaMoraMAUI.ViewModels
{
    internal class AboutViewModel_DMO : ObservableObject
    {
        public string Title_DMO => AppInfo.Name;
        public string Version_DMO => AppInfo.VersionString;
        public string MoreInfoUrl_DMO => "https://aka.ms/maui";
        public string Message_DMO => "This app is written in XAML and C# with .NET MAUI.";
        public ICommand ShowMoreInfoCommand_DMO { get; }

        public AboutViewModel_DMO()
        {
            ShowMoreInfoCommand_DMO = new AsyncRelayCommand(ShowMoreInfo);
        }

        private async Task ShowMoreInfo()
        {
            await Launcher.Default.OpenAsync(MoreInfoUrl_DMO);
        }
    }
}
