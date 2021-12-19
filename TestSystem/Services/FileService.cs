using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Models;
using TestSystem.Services.Interfaces;

namespace TestSystem.Services
{
    public class FileService
    {
        public virtual Test ReadTestFromFile(string filepath)
        {
            var test = new Test();

            if (File.Exists(filepath))
            {
                test = JsonConvert.DeserializeObject<Test>(File.ReadAllText(filepath));
            }

            return test;
        }

        public virtual Test WriteTestToFile(string filepath, Test test)
        {
            File.WriteAllText(filepath, JsonConvert.SerializeObject(test, Formatting.Indented));

            return test;
        }
    }
}
