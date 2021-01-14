using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem
{
    public static class ObservableCollectionExtensions
    {
        public static ObservableCollection<T> Shuffle<T>(this ObservableCollection<T> collection)
        {
            Random rnd = new Random();
            int count = collection.Count;

            for (int i = 0; i < count; i++)
            {
                int i1 = rnd.Next(count);
                int i2 = rnd.Next(count);

                T temp = collection[i1];
                collection[i1] = collection[i2];
                collection[i2] = temp;
            }

            return collection;
        }
    }
}
