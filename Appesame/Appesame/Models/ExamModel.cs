
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appesame.Models
{
    public class ExamModel : RealmObject
    {
        [PrimaryKey]
        public string examID { get; set; } = Guid.NewGuid().ToString();
        public string name { get; set; }
    }
}
