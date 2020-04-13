using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public class BinaryTree<T>
        where T : IComparable, IComparable<T>
    {
        public Node<T> Root { get; set; }

        public void Add(T data)
        {
            if (Root == null)
            {
                Root = new Node<T>(data);
                return;
            }

            Root.Add(data);
        }        

        public List<T> PreOrder()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            return PreOrder(Root);
        }

        private List<T> PreOrder(Node<T> node)
        {
            var list = new List<T>();

            if (node != null)
            {
                list.Add(node.Data);

                if (node.Left != null)
                {
                    list.AddRange(PreOrder(node.Left));
                }

                if (node.Right != null)
                {
                    list.AddRange(PreOrder(node.Right));
                }
            }

            return list;
        }

        public List<T> PostOrder()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            return PostOrder(Root);
        }

        private List<T> PostOrder(Node<T> node)
        {
            var list = new List<T>();

            if (node != null)
            {   
                if (node.Left != null)
                {
                    list.AddRange(PostOrder(node.Left));
                }

                if (node.Right != null)
                {
                    list.AddRange(PostOrder(node.Right));
                }

                list.Add(node.Data);
            }

            return list;
        }

        public List<T> InOrder()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            return InOrder(Root);
        }

        private List<T> InOrder(Node<T> node)
        {
            var list = new List<T>();

            if (node != null)
            {
                if (node.Left != null)
                {
                    list.AddRange(InOrder(node.Left));
                }

                list.Add(node.Data);

                if (node.Right != null)
                {
                    list.AddRange(InOrder(node.Right));
                }
            }
            return list;
        }

        public void Remove(T data)
        {
            Node<T> parent = null;
            Node<T> left = null;
            Node<T> right = null;

            if (Root.Data.Equals(data))
            {
                left = Root.Left;
                right = Root.Right;

                if (right != null)
                {
                    Root = right;
                    if (left != null)
                    {
                        Root.Add(left);
                    }                    
                }
                else
                {
                    Root = left;
                }
            }
            else
            {
                parent = GetParent(data, Root);

                if (parent == null)
                {
                    return;
                }

                if (parent.Left.Data.Equals(data))
                {
                    left = parent.Left.Left;
                    right = parent.Left.Right;

                    if (right != null)
                    {
                        parent.Left = right;

                        if (left != null)
                        {
                            parent.Left.Add(left);
                        }
                    }
                    else
                    {
                        parent.Left = left;
                    }
                }
                else
                {
                    left = parent.Right.Left;
                    right = parent.Right.Right;

                    if (right != null)
                    {
                        parent.Right = right;

                        if (left != null)
                        {
                            parent.Right.Add(left);
                        }
                    }
                    else
                    {
                        parent.Right = left;
                    }
                }                
            }
        }      

        private Node<T> GetParent(T data, Node<T> node)
        {
            if (node.Contains(data))
            {
                return node;
            }
            else
            {
                if (data.CompareTo(node.Data) == -1)
                {
                    if (node.Left != null)
                    {
                        return GetParent(data, node.Left);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        return GetParent(data, node.Right);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

    }
}
