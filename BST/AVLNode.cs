using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public enum States
    {
        Ballanced,
        LeftHeavy,
        RightHeavy
    }

    public class AVLNode<T> : IComparable<T> where T : IComparable
    {
        private AVLTree<T> _tree;
        private AVLNode<T> _left;
        private AVLNode<T> _right;

        public T Data { get; set; } 
        
        public AVLNode<T> Parent { get; set; }

        public AVLNode<T> Left
        {
            get => _left;
            set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }
        public AVLNode<T> Right
        {
            get => _right;
            set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }

        private int LeftHeight
        {
            get 
            {
                return MaxChildHeight(Left);
            }
        }

        private int RightHeight
        {
            get
            {
                return MaxChildHeight(Right);
            }
        }

        private States State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return States.LeftHeavy;
                }
                if(RightHeight - LeftHeight > 1)
                {
                    return States.RightHeavy;
                }

                return States.Ballanced;
            }
        }

        private int BallanceFactor { get => RightHeight - LeftHeight; }


        public AVLNode(T data, AVLTree<T> tree, AVLNode<T> parent)
        {
            Data = data;
            _tree = tree;
            Parent = parent;           
        }

        private int MaxChildHeight(AVLNode<T> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }
            return 0;
        }

        internal void Ballance()
        {
            if (State == States.RightHeavy)
            {
                if (Right != null && Right.BallanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if(State == States.LeftHeavy)
            {
                if (Left != null && Left.BallanceFactor < 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }

        private void LeftRotation()
        {
            var newRoot = Right;

            ReplaceRoot(newRoot);

            Right = newRoot.Left;

            newRoot.Left = this;
        }

        private void RightRotation()
        {
            var newRoot = Left;

            ReplaceRoot(newRoot);

            Left = newRoot.Right;

            newRoot.Right = this;
        }

        private void LeftRightRotation()
        {
            Right.RightRotation();

            LeftRotation();
        }

        private void RightLeftRotation()
        {            
            Left.LeftRotation();

            RightRotation();            
        }

        private void ReplaceRoot(AVLNode<T> newRoot)
        {
            if (Parent != null)
            {
                if (Parent.Left == this)
                {
                    Parent.Left = newRoot;
                }
                else if(Parent.Right == this)
                {
                    Parent.Right = newRoot;
                }
            }
            else
            {
                _tree.Root = newRoot;
            }

            newRoot.Parent = this.Parent;
            this.Parent = newRoot;
        }      


        #region IComparable<T>
        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }
        #endregion
    }
}
