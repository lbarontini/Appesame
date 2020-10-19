using Appesame.Models;
using Java.Sql;
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
        //Database instantiated as singleton use new RealmConfiguration() { ShouldDeleteIfMigrationNeeded = true } for developing
        public static Realm DatabaseInstance = Realm.GetInstance();

        //getter for the exams
        public static IEnumerable<ExamModel> GetAllExams() => DatabaseInstance.All<ExamModel>();

        //getter for all the items
        public static IEnumerable<object> GetAllItems(string itemName, string examName)
        {
            switch (itemName)
            {
                case "Flashcard":
                    return DatabaseInstance.All<FlashcardModel>().Where(f => f.ExamName == examName);                    
                case "Recording":
                    return DatabaseInstance.All<RecordingModel>().Where(f => f.ExamName == examName);
                case "Cmap":
                    return DatabaseInstance.All<CmapModel>().Where(f => f.ExamName == examName);
                case "Exercise":
                    return DatabaseInstance.All<ExerciseModel>().Where(f => f.ExamName == examName);
                default:
                    return null;                   
            }
        }
        
        //method for adding an exam
        public static void AddExam(String examName)
        {
            ExamModel exam = new ExamModel();
            exam.name = examName;
            DatabaseInstance.Write(() => DatabaseInstance.Add(exam));
        }

        //method for adding one of the Item
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
                case "Cmap":
                    var CmapToadd = new CmapModel()
                    {
                        Name = fileName,
                        Uri = fileUri,
                        ExamName = examName,
                        IsMemorized = false
                    };
                    DatabaseInstance.Write(() => DatabaseInstance.Add(CmapToadd, true));
                    break;
                case "Exercise":
                    var ExerciseToadd = new ExerciseModel()
                    {
                        Name = fileName,
                        Uri = fileUri,
                        ExamName = examName,
                        IsMemorized = false
                    };
                    DatabaseInstance.Write(() => DatabaseInstance.Add(ExerciseToadd, true));
                    break;
                default:                    
                    break;
            }  
        }

        //method for deleting one exam and the relative item
        public static void DeleteExam(object exam)
        {
            var examTodelete = exam as ExamModel;
            // implementation of onCascade;
            var flashcardsToDelete = DatabaseInstance.All<FlashcardModel>().Where(f => f.ExamName == examTodelete.name).ToList();
            foreach (FlashcardModel flashcard in flashcardsToDelete)
                DatabaseInstance.Write(() => DatabaseInstance.Remove(flashcard));
            var recordingsToDelete = DatabaseInstance.All<RecordingModel>().Where(f => f.ExamName == examTodelete.name).ToList();
            foreach (RecordingModel recording in recordingsToDelete)
                DatabaseInstance.Write(() => DatabaseInstance.Remove(recording));
            var cmapsToDelete = DatabaseInstance.All<CmapModel>().Where(f => f.ExamName == examTodelete.name).ToList();
            foreach (CmapModel cmap in cmapsToDelete)
                DatabaseInstance.Write(() => DatabaseInstance.Remove(cmap));
            var exercisesToDelete = DatabaseInstance.All<ExerciseModel>().Where(f => f.ExamName == examTodelete.name).ToList();
            foreach (ExerciseModel exercise in exercisesToDelete)
                DatabaseInstance.Write(() => DatabaseInstance.Remove(exercise));
            DatabaseInstance.Write(() => DatabaseInstance.Remove(exam as ExamModel));
        }

        //Method for deleting one specific item
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
                case "Cmap":
                    DatabaseInstance.Write(() => DatabaseInstance.Remove(Item as CmapModel));
                    break;
                case "Exercise":
                    DatabaseInstance.Write(() => DatabaseInstance.Remove(Item as ExerciseModel));
                    break;
                default:
                    break;
            }
        }
    }
}
