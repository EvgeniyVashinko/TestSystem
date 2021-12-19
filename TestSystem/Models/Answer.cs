using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.Models
{
    public class Answer
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsTrue { get; set; }
        public Answer()
        {
            Id = Guid.NewGuid();
        }

        public bool IsCorrectAnswer()
        {
            return !String.IsNullOrWhiteSpace(Name);
        }
    }
}
