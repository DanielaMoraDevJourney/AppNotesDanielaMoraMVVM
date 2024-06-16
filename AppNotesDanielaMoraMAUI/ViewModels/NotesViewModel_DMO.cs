using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppNotesDanielaMoraMAUI.ViewModels
{
    internal class NotesViewModel_DMO : ObservableObject
    {
        public ObservableCollection<NoteViewModel_DMO> Notes_DMO { get; }

        public ICommand LoadNotesCommand_DMO { get; }
        public ICommand AddNoteCommand_DMO { get; }
        public ICommand DeleteNoteCommand_DMO { get; }

        public NotesViewModel_DMO()
        {
            Notes_DMO = new ObservableCollection<NoteViewModel_DMO>();
            LoadNotesCommand_DMO = new AsyncRelayCommand(LoadNotes);
            AddNoteCommand_DMO = new AsyncRelayCommand(AddNote);
            DeleteNoteCommand_DMO = new AsyncRelayCommand<NoteViewModel_DMO>(DeleteNote);
        }

        private async Task LoadNotes()
        {
            var notes = Models.Note_DMO.LoadAll();
            Notes_DMO.Clear();

            foreach (var note in notes)
            {
                Notes_DMO.Add(new NoteViewModel_DMO(note));
            }
        }

        private async Task AddNote()
        {
            await Shell.Current.GoToAsync("NotePage_DMO");
        }

        private async Task DeleteNote(NoteViewModel_DMO noteViewModel)
        {
            noteViewModel.DeleteCommand_DMO?.Execute(null);
            Notes_DMO.Remove(noteViewModel);
        }
    }
}
