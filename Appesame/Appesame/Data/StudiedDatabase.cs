using Appesame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Appesame.Data
{
    class StudiedDatabase
    {
        public ObservableCollection<ExamModel> ExamModelList { get; set; }
        public IList<ItemModel> FlashcardModelList { get; set; }
        public IList<ItemModel> RecordingModelList { get; set; }
        public StudiedDatabase()
        {
            ExamModelList = new ObservableCollection<ExamModel>
            {
                new ExamModel("Chris"),
                new ExamModel("Georgios")
            };

            FlashcardModelList = new List<ItemModel>
            {
                new ItemModel("Chrisf"),
                new ItemModel("Georgiosf")
            };

            RecordingModelList = new List<ItemModel>
            {
                new ItemModel("Chrisr"),
                new ItemModel("Georgiosr")
            };
        }
    }
}
