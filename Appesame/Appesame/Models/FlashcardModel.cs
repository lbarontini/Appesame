
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Realms;
namespace Appesame.Models
{
    public class FlashcardModel : RealmObject
    {
        [PrimaryKey]
        public string FlahcardID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool IsMemorized { get; set; }
        public string ExamName { get; set; }
    }
}
