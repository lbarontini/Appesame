using Appesame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Appesame.Data
{
    public static class DataService
    {
        private static StudiedDatabase DatabaseInstance { get; set; }

        static DataService() 
        { DatabaseInstance = new StudiedDatabase();}
        public static ObservableCollection<ExamModel> GetAllExams() => DatabaseInstance.ExamModelList;
        public static IEnumerable<ItemModel> GetAllFlashcards() => DatabaseInstance.FlashcardModelList;
        public static IEnumerable<ItemModel> GetAllRecordings() => DatabaseInstance.RecordingModelList;
        public static void AddExam (ExamModel exam) => DatabaseInstance.ExamModelList.Add(exam);
        public static void DeleteExam(ExamModel exam) => DatabaseInstance.ExamModelList.Remove(exam);
    }
}
