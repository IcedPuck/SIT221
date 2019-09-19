using System;
using System.Collections.Generic;

namespace Vector
{
    internal class MergeSortBottomUp : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            throw new NotImplementedException();
        }
        public void Merge<K>(K[] a, K[] b, IComparer<K> comparer, int start, int insert) where K : IComparable<K>
        {
            int end1 = Math.Min(start + insert, a.Length);
            int end2 = Math.Min(start + 2 * insert, a.Length);

            int x = start;
            int y = start + insert;
            int z = start;
            while(x < end1 && y < end2)
            {
                if(comparer.Compare(a[x], a[y])< 0)
                {
                    b[z++] = a[x++];
                }
                else
                {
                    b[z++] = a[y++];
                }
            }
            if(x < end1)
            {
                Array.Copy(a, x, b, z, end1 - x);
            }
            else if(y < end2)
            {
                Array.Copy(a, y, b, z, end2 - y);
            }
        }
        public void MergesortBottomUp<K>(K[] orignal, IComparer<K> comparer) where K : IComparable<K>
        {
            int n = orignal.Length;
            K[] srceen = orignal;
            K[] destition = new K[n];
            K[] temp;
            for (int i = 1; i < n; i = i * 2)
            {
                for(int j = 0; j < n; j += 2 * i)
                {
                    Merge(srceen, destition, comparer, j, i);
                }
                temp = srceen;
                srceen = destition;
                destition = temp;
            }
            if(orignal != srceen)
            {
                Array.Copy(srceen, 0, orignal, 0, n);
            }
        }
    }
}