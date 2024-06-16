namespace AppNotesDanielaMoraMAUI.Views
{
    public partial class AllNotesPage_DMO : ContentPage
    {
        public AllNotesPage_DMO()
        {
            InitializeComponent();
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            notesCollection.SelectedItem = null;
        }
    }
}