using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace AppNotesDanielaMoraMAUI.Models
{
    internal class Note_DMO
    {
        public string? Filename_DMO { get; set; }
        public string? Text_DMO { get; set; }
        public DateTime Date_DMO { get; set; }

        public Note_DMO()
        {
            Filename_DMO = $"{Path.GetRandomFileName()}.notes.txt";
            Date_DMO = DateTime.Now;
            Text_DMO = "";
        }

        public void Save() =>
            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, Filename_DMO), Text_DMO);

        public void Delete() =>
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, Filename_DMO));

        public static Note_DMO Load(string filename)
        {
            filename = Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return new Note_DMO
            {
                Filename_DMO = Path.GetFileName(filename),
                Text_DMO = File.ReadAllText(filename),
                Date_DMO = File.GetLastWriteTime(filename)
            };
        }

        public static IEnumerable<Note_DMO> LoadAll()
        {
            string appDataPath = FileSystem.AppDataDirectory;

            return Directory

                    // Select the file names from the directory
                    .EnumerateFiles(appDataPath, "*.notes.txt")

                    // Each file name is used to load a note
                    .Select(filename => Note_DMO.Load(Path.GetFileName(filename)))

                    // With the final collection of notes, order them by date
                    .OrderByDescending(note => note.Date_DMO);
        }
    }
}
