using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        public AVLNode<T> Root { get; internal set; }
        public int Count { get; private set; }

        public void Add(T data)
        {
            if (Root == null)
            {
                Root = new AVLNode<T>(data, this, null);
            }
            else
            {
                AddTo(Root, data);
            }
            Count++;
        }

        private void AddTo(AVLNode<T> node, T data)
        {
            if (data.CompareTo(node.Data) == -1)
            {
                if (node.Left == null)
                {
                    node.Left = new AVLNode<T>(data, this, node);
                }
                else
                {
                    AddTo(node.Left, data);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new AVLNode<T>(data, this, node);
                }
                else
                {
                    AddTo(node.Right, data);
                }
            }
            node.Parent?.Ballance();
        }

        public bool Contains(T data)
        {
            return Find(data) != null;
        }

        private AVLNode<T> Find(T data)
        {
            var current = Root;

            while(current != null)
            {
                int result = current.Data.CompareTo(data);
                if (result > 0)
                {
                    current = current.Left;
                }
                else if(result < 0)
                {
                    current = current.Right;
                }
                else
                {
                    return current;
                }
            }
            return null;
        }

        public bool Remove(T data)
        {
            var current = Find(data);

            if (current == null)
            {
                return false;
            }

            var treeToBallance = current.Parent;
            Count--;

            if (current.Right == null)
            {
                if (current.Parent == null)
                {
                    Root = current.Left;

                    if (Root != null)
                    {
                        Root.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.Data.CompareTo(current.Data);

                    if (result > 0)
                    {
                        current.Parent.Left = current.Left;
                    }
                    else if(result < 0)
                    {
                        current.Parent.Right = current.Left;
                    }
                }
            }
            else if(current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (current.Parent == null)
                {
                    Root = current.Right;

                    if(Root != null)
                    {
                        Root.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.Data.CompareTo(current.Data);

                    if (result > 0)
                    {
                        current.Parent.Left = current.Right;
                    }
                    else if(result < 0)
                    {
                        current.Parent.Right = current.Right;
                    }
                }
            }
            else
            {
                AVLNode<T> mostLeft = current.Right.Left;

                while (mostLeft.Left != null)
                {
                    mostLeft = mostLeft.Left;
                }

                mostLeft.Parent.Left = mostLeft.Right;

                mostLeft.Left = current.Left;
                mostLeft.Right = current.Right;

                if(current.Parent == null)
                {
                    Root = mostLeft;

                    if (Root != null)
                    {
                        Root.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.Data.CompareTo(current.Data);

                    if(result > 0)
                    {
                        current.Parent.Left = mostLeft;
                    }
                    else if(result < 0)
                    {
                        current.Parent.Right = mostLeft;
                    }
                }

            }

            if(treeToBallance != null)
            {
                treeToBallance.Ballance();
            }
            else
            {
                if(Root != null)
                {
                    Root.Ballance();
                }
            }

            return true;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        #region IEnumerable<T>
        public IEnumerator<T> InOrderTraversal()
        {
            if(Root != null)
            {
                Stack<AVLNode<T>> stack = new Stack<AVLNode<T>>();
                var current = Root;

                bool nextLeft = true;

                stack.Push(current);

                while(stack.Count > 0)
                {
                    if (nextLeft)
                    {
                        while(current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }
                }

                yield return current.Data;

                if(current.Right != null)
                {
                    current = current.Right;

                    nextLeft = true;
                }
                else
                {
                    current = stack.Pop();
                    nextLeft = false;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
