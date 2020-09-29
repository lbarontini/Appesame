
using Realms;
using System;

namespace Appesame.Models
{
    public class ExamModel : RealmObject
    {
        public ExamModel()
        {
        }

        public string Name { get; set; } = Guid.NewGuid().ToString();
    }
}
