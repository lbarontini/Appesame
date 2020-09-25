using Appesame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Appesame.Data
{
    class StudiedDatabase
    {
        public List<ExamModel> ExamModelList { get; set; }
        public IList<ItemModel> FlashcardModelList { get; set; }
        public IList<ItemModel> RecordingModelList { get; set; }
        public StudiedDatabase()
        {
            ExamModelList = new List<ExamModel>
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
