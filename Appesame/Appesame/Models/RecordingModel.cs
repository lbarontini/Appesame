using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appesame.Models
{
    public class RecordingModel : RealmObject
    {
        [PrimaryKey]
        public string FlahcardID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool IsMemorized { get; set; }
        public string ExamName { get; set; }
    }
}
