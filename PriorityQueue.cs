using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PriorityQueue
{
    public class PriorityQueue<T> : IEnumerable<T>
    {
        Nodes<T> nodes = new Nodes<T>();

        public PriorityQueue()
        {
            
        }
      
        public int Count => nodes.Count;

        public void Enqueue(T item, float value)
        {
            var node = new Node<T>()
            {
                Item = item,
                Priority = value,
                Index = nodes.Count + 1
            };

            nodes.Add(node);
            CascadeUp(node);
        }

        public T Dequeue()
        {
            var firstNode = nodes.First;
            if (firstNode == null)
                return default(T);

            if (nodes.Count < 2)
            {
                nodes.Remove(firstNode);
                return firstNode.Item;
            }

            var lastNode = nodes.Last;
              
            nodes.Remove(lastNode);

            lastNode.Index = firstNode.Index;
            nodes[lastNode.Index] = lastNode;

            CascadeDown(lastNode);

            return firstNode.Item;
        }

        public void UpdatePriority(T item, float prioirty)
        {
            if (!Contains(item))
                throw new InvalidOperationException("itme이 해당 Queue에 없습니다");

            var node = nodes.Find(item);
            node.Priority = prioirty;

            Update(node);
        }

        public void Remove(T item)
        {
            if (!Contains(item))
                throw new InvalidOperationException("itme이 해당 Queue에 없습니다");

            var node = nodes.Find(item);
            var changedNode = nodes.Last;
            nodes.Remove(changedNode);

            if (node == changedNode)
                return;

            changedNode.Index = node.Index;
            nodes[changedNode.Index] = changedNode;

            Update(changedNode);
        }

        public bool Contains(T item) => nodes.Contains(item);

        public void Clear() => nodes.Clear();

        void Update(Node<T> node)
        {
            var parentNodeIdx = node.Index >> 1;

            if (parentNodeIdx > 0 && node < nodes[parentNodeIdx])
                CascadeUp(node);
            else
                CascadeDown(node);
        }

        void CascadeUp(Node<T> node)
        {
            if (nodes.Count <= 1)
                return;

            var parentIdx = node.Index;
            while (parentIdx > 1)
            {
                parentIdx >>= 1;
                var parentNode = nodes[parentIdx];

                if (parentNode <= node)
                    break;

                nodes[node.Index] = parentNode;
                parentNode.Index = node.Index;

                node.Index = parentIdx;
            }

            nodes[node.Index] = node;
        }

        void CascadeDown(Node<T> node)
        {
            var leftParentIdx = node.Index * 2;
            
            while (leftParentIdx <= nodes.Count)
            {
                var rightParentIdx = leftParentIdx + 1;

                var leftParentNode = nodes[leftParentIdx];
                var rightParentNode = (rightParentIdx <= nodes.Count) ? nodes[rightParentIdx] : null;

                var targetParentNode = (rightParentNode != null && rightParentNode <= leftParentNode) ? rightParentNode : leftParentNode;
                if (targetParentNode >= node)
                    break;

                var targetParentIdx = targetParentNode.Index;
                targetParentNode.Index = node.Index;
                nodes[targetParentNode.Index] = targetParentNode;

                node.Index = targetParentIdx;

                leftParentIdx = node.Index * 2;
            }

            nodes[node.Index] = node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodes.GetEnumerator();
        }
    
    }  
    
}
