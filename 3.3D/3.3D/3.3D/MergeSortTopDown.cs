using System;
using System.Collections.Generic;
using System.Text;
using Vector;

namespace Vector
{
    class MergeSortTopDown : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            mergeSort(sequence, comparer);
        }
        public void merge<K>(K[] S1, K[] S2, K[] S, IComparer<K> comparer) where K : IComparable<K>
        {
            int i = 0;
            int j = 0;
            while(i + j < S.Length)
            {
                if(j == S2.Length || (i < S1.Length && comparer.Compare(S1[i],S2[j])<0))
                {
                    S[i + j] = S1[i++];
                }
                else
                {
                    S[i + j] = S2[j++];
                }
            }
        }
        public void mergeSort<K>(K[] S, IComparer<K> comparer) where K : IComparable<K>
        {
            int n = S.Length;
            if(n < 2)
            {
                return;
            }
            int mid = n / 2;
            K[] S1 = new K[mid];
            K[] S2 = new K[S.Length - mid];
            Array.Copy(S, 0, S1, 0, mid);
            Array.Copy(S, mid, S2, 0, mid);

            mergeSort(S1, comparer);
            mergeSort(S2, comparer);
            merge(S1, S2, S, comparer);
        }
    }
}
