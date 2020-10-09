using Appesame.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Appesame.Data
{
    public static class DataService
    {
        public static Realm DatabaseInstance = Realm.GetInstance(new RealmConfiguration() { ShouldDeleteIfMigrationNeeded = true });

        public static IEnumerable<ExamModel> GetAllExams() => DatabaseInstance.All<ExamModel>();
        public static IEnumerable<object> GetAllItems(string itemName, string examName)
        {
            switch (itemName)
            {
                case "Flashcard":
                    return DatabaseInstance.All<FlashcardModel>().Where(f => f.ExamName == examName);                    
                case "Recording":
                    return DatabaseInstance.All<RecordingModel>().Where(f => f.ExamName == examName);                    
                default:
                    return null;                   
            }
        }
         
        public static void AddExam(String examName)
        {
            ExamModel exam = new ExamModel();
            exam.name = examName;
            DatabaseInstance.Write(() => DatabaseInstance.Add(exam));
        }
        public static void AddItem(string examName, string itemName, string fileName, string fileUri)
        {
            switch (itemName)
            {
                case "Flashcard":
                   var FlashcardToadd = new FlashcardModel()
                    {
                        Name = fileName,
                        Uri = fileUri,
                        ExamName = examName,
                        IsMemorized = false
                    };
                    DatabaseInstance.Write(() => DatabaseInstance.Add(FlashcardToadd, true));
                    break;
                case "Recording":
                    var RecordingToadd = new RecordingModel()
                    {
                        Name = fileName,
                        Uri = fileUri,
                        ExamName = examName,
                        IsMemorized = false
                    };
                    DatabaseInstance.Write(() => DatabaseInstance.Add(RecordingToadd, true));
                    break;
                default:                    
                    break;
            }  
        }
        public static void DeleteExam(object exam)
        {
            var examTodelete = exam as ExamModel;
            var flashcardsToDelete = DatabaseInstance.All<FlashcardModel>().Where(f => f.ExamName == examTodelete.name).ToList();
            foreach (FlashcardModel flashcard in flashcardsToDelete)
                DatabaseInstance.Write(() => DatabaseInstance.Remove(flashcard));
            var recordingsToDelete = DatabaseInstance.All<RecordingModel>().Where(f => f.ExamName == examTodelete.name).ToList();
            foreach (RecordingModel recording in recordingsToDelete)
                DatabaseInstance.Write(() => DatabaseInstance.Remove(recording));
            DatabaseInstance.Write(() => DatabaseInstance.Remove(exam as ExamModel));
        }
        public static void DeleteItem(object Item, string itemName)
        {
            switch (itemName)
            {
                case "Flashcard":
                    DatabaseInstance.Write(() => DatabaseInstance.Remove(Item as FlashcardModel));
                    break;
                case "Recording":
                    DatabaseInstance.Write(() => DatabaseInstance.Remove(Item as RecordingModel));
                    break;
                default:
                    break;
            }
        }
    }
}
