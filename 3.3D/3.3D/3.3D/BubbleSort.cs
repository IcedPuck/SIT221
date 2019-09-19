using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    public class BubbleSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            int length = sequence.Length;
            K temp;
            for (int i = 0; i <= length - 1; i++)
            {
                if (comparer.Compare(sequence[i], sequence[i + 1]) > 0)
                {
                    temp = sequence[i];
                    sequence[i] = sequence[i + 1];
                    sequence[i + 1] = temp;
                }
            }
        }
    }
}
