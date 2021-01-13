using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Models;

namespace TestSystem.Services
{
    public class CustomFileService : FileService
    {
        public override Test ReadTestFromFile(string filepath)
        {
            return base.ReadTestFromFile(filepath);
        }
    }
}
