
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appesame.Models
{
    public class ExamModel : RealmObject
    {
        [PrimaryKey]
        public string Name { get; set; }
        public IList<ItemModel> FlashcardList { get; }
        public IList<ItemModel> RecordingList { get; }

    }
}
