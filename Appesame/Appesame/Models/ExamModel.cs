
namespace Appesame.Models
{
    public class ExamModel
    {
        public ExamModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
