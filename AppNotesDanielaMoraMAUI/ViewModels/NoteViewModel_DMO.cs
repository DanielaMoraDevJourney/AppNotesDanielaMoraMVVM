using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppNotesDanielaMoraMAUI.ViewModels
{
    internal class NoteViewModel_DMO : ObservableObject, IQueryAttributable
    {
        private Models.Note_DMO _noteDMO;

        public string? Text_DMO
        {
            get => _noteDMO.Text_DMO;
            set
            {
                if (_noteDMO.Text_DMO != value)
                {
                    _noteDMO.Text_DMO = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date_DMO => _noteDMO.Date_DMO;

        public string? Identifier_DMO => _noteDMO.Filename_DMO;

        public ICommand SaveCommand_DMO { get; private set; }
        public ICommand DeleteCommand_DMO { get; private set; }

        public NoteViewModel_DMO()
        {
            _noteDMO = new Models.Note_DMO();
            SaveCommand_DMO = new AsyncRelayCommand(Save_DMO);
            DeleteCommand_DMO = new AsyncRelayCommand(Delete_DMO);
        }

        public NoteViewModel_DMO(Models.Note_DMO note)
        {
            _noteDMO = note;
            SaveCommand_DMO = new AsyncRelayCommand(Save_DMO);
            DeleteCommand_DMO = new AsyncRelayCommand(Delete_DMO);
        }

        private async Task Save_DMO()
        {
            _noteDMO.Date_DMO = DateTime.Now;
            SaveNote();
            await Shell.Current.GoToAsync($"..?saved={_noteDMO.Filename_DMO}");
        }

        private async Task Delete_DMO()
        {
            DeleteNote();
            await Shell.Current.GoToAsync($"..?deleted={_noteDMO.Filename_DMO}");
        }

        private void SaveNote()
        {
            var filePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, _noteDMO.Filename_DMO);
            System.IO.File.WriteAllText(filePath, _noteDMO.Text_DMO);
        }

        private void DeleteNote()
        {
            var filePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, _noteDMO.Filename_DMO);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                var filename = query["load"].ToString();
                _noteDMO = LoadNote_DMO(filename);
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _noteDMO = LoadNote_DMO(_noteDMO.Filename_DMO);
            RefreshProperties();
        }

        private Models.Note_DMO LoadNote_DMO(string filename)
        {
            var filePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!System.IO.File.Exists(filePath))
                throw new System.IO.FileNotFoundException("Unable to find file on local storage.", filePath);

            return new Models.Note_DMO
            {
                Filename_DMO = filename,
                Text_DMO = System.IO.File.ReadAllText(filePath),
                Date_DMO = System.IO.File.GetLastWriteTime(filePath)
            };
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text_DMO));
            OnPropertyChanged(nameof(Date_DMO));
        }
    }
}
