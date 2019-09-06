using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public class Heap<K, D> where K : IComparable<K>
    {

        // 这是一个嵌套的Node类，其目的是表示堆的节点。
        private class Node : IHeapifyable<K, D>
        {
            // 数据字段表示有效负载。
            public D Data { get; set; }
            // Key字段用于对二进制最小（最大）堆策略的元素进行排序，即父节点的密钥小于（大于）其子节点的密钥。
            public K Key { get; set; }
            // “位置”字段反映基于阵列的内部数据结构中节点的位置（索引）。
            public int Position { get; set; }

            public Node(K key, D value, int position)
            {
                Data = value;
                Key = key;
                Position = position;
            }

            // 这是Node类的ToString（）方法。
            // 它将节点打印为元组（'键值'，'有效载荷'，'索引'）}。
            public override string ToString()
            {
                return "(" + Key.ToString() + "," + Data.ToString() + "," + Position + ")";
            }
        }

        // ---------------------------------------------------------------------------------
        // 这里开始描述Heap <K，D>类的方法和属性

        public int Count { get; private set; }

        // Heap <K，D>的数据节点内部存储在List集合中。
        // 请注意，索引为0的元素是虚节点。
        //通过Min（）返回给用户的堆的最顶层元素被索引为1。
        private List<Node> data = new List<Node>();

        // 我们引用给定的比较器来对堆中的元素进行排序。
        //根据比较器，我们可能得到二进制Min-Heap或二进制Max-Heap。
        // 在前一种情况下，比较器必须按键的升序对元素进行排序，并在后一种情况下以降序进行。
        private IComparer<K> comparer;

        //我们希望用户通过给定的参数指定比较器。
        public Heap(IComparer<K> comparer)
        {
            this.comparer = comparer;

            // 当用户无法提供默认比较器时，我们使用默认比较器。
            // 这意味着对类型K的限制，例如类声明中的“where K：IComparable <K>”。
            if (this.comparer == null)
            {
                this.comparer = Comparer<K>.Default;
            }

            // 我们通过在位置0创建一个虚节点来简化Heap <K，D>的实现。
            // 这允许实现以下属性：
            // 具有索引i的节点的子节点具有索引2 * i和2 * i + 1（如果它们存在）。
            data.Add(new Node(default(K), default(D), 0));
        }

        // 此方法返回堆的最顶层（最小值或最大值）。
        // 它不会删除元素，只返回已转换为IHeapifyable <K，D>接口的节点。
        public IHeapifyable<K, D> Min()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The heap is empty.");
            }
            return data[1];
        }

        // 插入堆<K，D>基于私有UpHeap（）方法
        public IHeapifyable<K, D> Insert(K key, D value)
        {
            Count++;
            Node node = new Node(key, value, Count);
            data.Add(node);
            UpHeap(Count);
            return node;
        }

        private void UpHeap(int start)
        {
            int position = start;
            while (position != 1)
            {
                if (comparer.Compare(data[position].Key, data[position / 2].Key) < 0)
                {
                    Swap(position, position / 2);
                }
                position = position / 2;
            }
        }
        private void DownHeap(int start)
        {
            int position = start;
            while (position * 2 <= Count - 1)
            {
                if (comparer.Compare(data[position].Key, data[position * 2].Key) > 0
                    && data[position * 2].Position.Equals(data[Count - 1].Position))
                {
                    Swap(position, position * 2);
                    position = position * 2;
                }
                else if (comparer.Compare(data[position].Key, data[position * 2].Key) > 0
                    && comparer.Compare(data[position * 2].Key, data[position * 2 + 1].Key) < 0)
                {
                    Swap(position, position * 2);
                    position = position * 2;
                }
                else if (comparer.Compare(data[position].Key, data[position * 2 + 1].Key) > 0
                    && comparer.Compare(data[position * 2 + 1].Key, data[position * 2].Key) < 0)
                {
                    Swap(position, position * 2 + 1);
                    position = position * 2 + 1;
                }
                else
                {
                    break;
                }
            }
        }

        // 此方法交换表示堆的列表中的两个元素。
        // 需要在解决方案中交换节点时使用它，例如 在DownHeap（）中你需要开发。
        private void Swap(int from, int to)
        {
            Node temp = data[from];
            data[from] = data[to];
            data[to] = temp;
            data[to].Position = to;
            data[from].Position = from;
        }

        public void Clear()
        {
            for (int i = 0; i <= Count; i++)
            {
                data[i].Position = -1;
            }
            data.Clear();
            data.Add(new Node(default(K), default(D), 0));
            Count = 0;
        }

        public override string ToString()
        {
            if (Count == 0)
            {
                return "[]";
            }
            StringBuilder s = new StringBuilder();
            s.Append("[");
            for (int i = 0; i < Count; i++)
            {
                s.Append(data[i + 1]);
                if (i + 1 < Count) s.Append(",");
            }
            s.Append("]");
            return s.ToString();
        }

        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.
        public IHeapifyable<K, D> Delete()//只需要删除一次顶部的东西就行，然后重排
        {
            // You should replace this plug by your code.
            //throw new NotImplementedException();
            if (Count == 0)
            {
                throw new InvalidOperationException("The heap is empty.");
            }
            Swap(data[Count].Position, data[1].Position);
            Count -= 1;
            int position = 1;
            DownHeap(position);
            return data[Count + 1];
        }

        // Builds a minimum binary heap using the specified data according to the bottom-up approach.
        public IHeapifyable<K, D>[] BuildHeap(K[] keys, D[] value)
        {
            // You should replace this plug by your code.
            //throw new NotImplementedException();
            Clear();
            if (Count != 0)
            {
                throw new IndexOutOfRangeException();
            }
            Count = Count + 1;
            for(int i = 0; (i < keys.Length) && (i < value.Length); i++)
            {
                Insert(keys[i], value[i]);
            }
            return data.ToArray();
        }

        public void DecreaseKey(IHeapifyable<K, D> element, K new_key)
        {
            // You should replace this plug by your code.
            //throw new NotImplementedException();
            if (data != null)
            {
                throw new IndexOutOfRangeException();
            }
            if()
            {
                throw new IndexOutOfRangeException();
            }
        }

    }
}
