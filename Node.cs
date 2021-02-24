using System;
using System.Collections.Generic;
using System.Linq;

namespace PriorityQueue
{
    internal class Node<T>
    {
        public T Item { get; set; }

        public float Priority { get; set; }

        public int Index { get; set; }

        public override string ToString()
        {
            return Item.ToString();
        }

        public static bool operator <=(Node<T> leftNode, Node<T> rightNode) => leftNode.Priority <= rightNode.Priority;
        public static bool operator >=(Node<T> leftNode, Node<T> rightNode) => leftNode.Priority >= rightNode.Priority;
        public static bool operator <(Node<T> leftNode, Node<T> rightNode) => leftNode.Priority < rightNode.Priority;
        public static bool operator >(Node<T> leftNode, Node<T> rightNode) => leftNode.Priority > rightNode.Priority;


    }
}
