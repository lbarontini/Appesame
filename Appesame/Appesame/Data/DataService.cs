using Appesame.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Appesame.Data
{
    public static class DataService
    {
        public static Realm DatabaseInstance = Realm.GetInstance();

        public static IEnumerable<ExamModel> GetAllExams() => DatabaseInstance.All<ExamModel>();
        //public static IEnumerable<ItemModel> GetAllFlashcards() => DatabaseInstance.FlashcardModelList;
        //public static IEnumerable<ItemModel> GetAllRecordings() => DatabaseInstance.RecordingModelList;
        public static void AddExam(ExamModel exam) => DatabaseInstance.Write(() => DatabaseInstance.Add(exam));
        public static void DeleteExam(ExamModel exam) => DatabaseInstance.Write(() => DatabaseInstance.Remove(exam));
    }
}
