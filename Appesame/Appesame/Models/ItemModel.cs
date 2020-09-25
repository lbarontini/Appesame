using System;
using System.Collections.Generic;
using System.Text;

namespace Appesame.Models
{
    public class ItemModel
    {
        public string Name { get; set; }
        public ItemModel(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
