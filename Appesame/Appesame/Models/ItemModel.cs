using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appesame.Models
{
    public class ItemModel : RealmObject
    {
        public string Name { get; set; }
        [PrimaryKey]
        public string Uri { get; set; }
        public bool IsMemorized { get; set; }

        [Backlink(nameof(ExamModel.FlashcardList))]
        public IQueryable<ExamModel> Flashcards { get; }

        [Backlink(nameof(ExamModel.RecordingList))]
        public IQueryable<ExamModel> Recordings { get; }
        public ExamModel Exam { get; set; }
    }
}
