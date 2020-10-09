
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appesame.Models
{
    public class ExamModel : RealmObject
    {
        public string name { get; set; }

        [PrimaryKey]
        public string examID { get; set; } = Guid.NewGuid().ToString();

    }
}
