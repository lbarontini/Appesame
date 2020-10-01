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
        public static IEnumerable<ItemModel> GetAllItems(string itemName, string examName)
        {
            var examModel = DatabaseInstance.All<ExamModel>().Where(e => e.Name == examName).First();
            if (itemName == "Flashcards")
                return examModel.FlashcardList;
            else return null;
         }

        //public static IEnumerable<ItemModel> GetAllRecordings() => DatabaseInstance.RecordingModelList;
        public static void AddExam(String examName)
        {
            ExamModel exam = new ExamModel();
            exam.Name = examName;
            DatabaseInstance.Write(() => DatabaseInstance.Add(exam));
        }
        public static void AddItem(string examName, string ItemName, string fileName, string fileUri)
        {
            var itemToadd = new ItemModel();
            itemToadd.Name = fileName;
            itemToadd.Uri = fileUri;
            itemToadd.Exam = new ExamModel { Name = examName };

            var examModel = DatabaseInstance.All<ExamModel>().Where(e => e.Name == examName).First();
            
            if (ItemName == "Flashcard")
            {
                examModel.FlashcardList.Add(itemToadd);
            }
            DatabaseInstance.Write(() => DatabaseInstance.Add(examModel,true));
        }
        public static void DeleteExam(object exam) => DatabaseInstance.Write(() => DatabaseInstance.Remove(exam as ExamModel));
        public static void DeleteItem(object Item) => DatabaseInstance.Write(() => DatabaseInstance.Remove(Item as ItemModel));
    }
}
