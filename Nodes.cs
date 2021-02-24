using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace PriorityQueue
{
    internal class Nodes<T> : IEnumerable<T>
    {
        List<Node<T>> nodes = new List<Node<T>>();

        public int Count => nodes.Count - 1;

        public Node<T> First => (Count <= 0) ? null : nodes[1];

        public Node<T> Last => (Count <= 0) ? null : nodes[nodes.Count - 1];

        public Node<T> this[int idx]
        {
            get
            {
                if (idx == 0)
                    throw new IndexOutOfRangeException("0번째 Index는 사용되지 않습니다!");

                return nodes[idx];
            }

            set
            {
                if (idx == 0)
                    throw new IndexOutOfRangeException("0번째 Index는 사용되지 않습니다!");

                nodes[idx] = value;
            }
        }

        public Nodes()
        {        
            nodes.Add(null);
        }

        public void Add(Node<T> node) => nodes.Add(node);

        public void Clear()
        {
            nodes.Clear();
            nodes.Add(null);
        }

        public void Remove(Node<T> node) => nodes.Remove(node);

        public Node<T> Find(T item)
        {
            return nodes.Find(node => node?.Item.Equals(item) == true);

        }

        public IEnumerator<T> GetEnumerator()
        {
            return new NodesEnumeraotr(nodes);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new NodesEnumeraotr(nodes);
        }

        class NodesEnumeraotr : IEnumerator<T>
        {
            List<Node<T>> nodes;
            int position = 0;

            public T Current
            {
                get
                {
                    try
                    {
                        return nodes[position].Item;
                    }
                    catch (IndexOutOfRangeException)
                    {

                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;


            public NodesEnumeraotr(List<Node<T>> nodes)
            {
                this.nodes = nodes;
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                position++;
                return position < nodes.Count;
            }

            public void Reset()
            {
                position = 0;
            }
        }

    }

}
