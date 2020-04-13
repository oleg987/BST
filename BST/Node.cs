using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public class Node<T> : IComparable<T>, IComparable
        where T : IComparable, IComparable<T>
    {
        public T Data { get; set; }        
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T data)
        {
            Data = data;
        }

        public Node(T data, Node<T> left, Node<T> right) : this(data)
        {
            Left = left;
            Right = right;
        }

        public int CompareTo(object obj)
        {
            if (obj is Node<T> item)
            {
                return Data.CompareTo(item);
            }
            else
            {
                throw new Exception("Types does not exists");
            }
        }

        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }

        public void Add(T data)
        {
            var node = new Node<T>(data);

            if (data.CompareTo(Data) == -1)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Add(data);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Add(data);
                }
            }
        }


        public void Add(Node<T> node)
        {
            if (node.Data.CompareTo(Data) == -1)
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Add(node);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Add(node);
                }
            }
        }

        public bool Contains(T data)
        {
            return Left.Data.Equals(data) || Right.Data.Equals(data);           
        }        

        public override string ToString()
        {
            return Data.ToString();
        }

    }
}
