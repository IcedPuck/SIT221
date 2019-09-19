using System;
using System.Collections.Generic;
using System.Text;

namespace Vector
{
    class RandomizedQuickSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            QuickSort(sequence, comparer, 0, sequence.Length - 1);
        }
        public void QuickSort<K>(K[] sequence, IComparer<K> comparer, int a, int b) where K : IComparable<K>
        {
            if(a >= b)
            {
                return;
            }
            int left = a;
            int right = b - 1;
            K pivot = sequence[b];
            K temp;
            while (left <= right)
            {
                while(comparer.Compare(sequence[left], pivot) < 0)
                {
                    left++;
                }
                while(comparer.Compare(sequence[right],pivot) > 0)
                {
                    right--;
                }
                if(left <= right)
                {
                    temp = sequence[left];
                    sequence[left] = sequence[right];
                    sequence[right] = temp;
                    left++;
                    right--;
                }
            }
            temp = sequence[left];
            sequence[left] = sequence[b];
            sequence[b] = temp;
            QuickSort(sequence, comparer, a, left - 1);
            QuickSort(sequence, comparer, left + 1, b);
        }
    }
}
