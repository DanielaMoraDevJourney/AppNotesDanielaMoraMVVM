namespace AppNotesDanielaMoraMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.NotePage_DMO), typeof(Views.NotePage_DMO));
        }
    }
}
