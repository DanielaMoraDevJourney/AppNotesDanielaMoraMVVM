using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppNotesDanielaMoraMAUI.ViewModels
{
    internal class NotesViewModel_DMO : ObservableObject, IQueryAttributable
    {
        public ObservableCollection<NoteViewModel_DMO> AllNotes_DMO { get; }
        public ICommand NewCommand_DMO { get; }
        public ICommand SelectNoteCommand_DMO { get; }

        public NotesViewModel_DMO()
        {
            AllNotes_DMO = new ObservableCollection<NoteViewModel_DMO>(Models.Note_DMO.LoadAll().Select(n => new NoteViewModel_DMO(n)));
            NewCommand_DMO = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand_DMO = new AsyncRelayCommand<NoteViewModel_DMO>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.NotePage_DMO));
        }

        private async Task SelectNoteAsync(NoteViewModel_DMO note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.NotePage_DMO)}?load={note.Identifier_DMO}");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                NoteViewModel_DMO matchedNote = AllNotes_DMO.FirstOrDefault(n => n.Identifier_DMO == noteId);

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes_DMO.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                NoteViewModel_DMO matchedNote = AllNotes_DMO.FirstOrDefault(n => n.Identifier_DMO == noteId);

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes_DMO.Move(AllNotes_DMO.IndexOf(matchedNote), 0);
                }

                // If note isn't found, it's new; add it.
                else
                    AllNotes_DMO.Insert(0, new NoteViewModel_DMO(Models.Note_DMO.Load(noteId)));
            }
        }
    }
}
