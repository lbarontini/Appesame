using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appesame.Models
{
    public class FlashcardModel : RealmObject
    {
        public string Name { get; set; }
        [PrimaryKey]
        public string Uri { get; set; }
        public bool IsMemorized { get; set; }
        public string ExamName { get; set; }
    }
}
