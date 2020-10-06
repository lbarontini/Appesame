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
        public static IEnumerable<FlashcardModel> GetAllItems(string itemName, string examName)
        {
            return DatabaseInstance.All<FlashcardModel>().Where(f => f.ExamName == examName);
        }
         
        public static void AddExam(String examName)
        {
            ExamModel exam = new ExamModel();
            exam.Name = examName;
            DatabaseInstance.Write(() => DatabaseInstance.Add(exam));
        }
        public static void AddItem(string examName, string ItemName, string fileName, string fileUri)
        {
            var itemToadd = new FlashcardModel()
            {
                Name = fileName,
                Uri = fileUri,
                ExamName = examName,
                IsMemorized = false
            };

            DatabaseInstance.Write(() => DatabaseInstance.Add(itemToadd,true));
        }
        public static void DeleteExam(object exam) => DatabaseInstance.Write(() => DatabaseInstance.Remove(exam as ExamModel));
        public static void DeleteItem(object Item) => DatabaseInstance.Write(() => DatabaseInstance.Remove(Item as FlashcardModel));
    }
}
