using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class BinSearchTree<T> 
        where T : IComparable<T>
    {
        #region Fields

        BSTNode<T> m_root;

        #endregion

        #region Properties

        public BSTNode<T> Root { get => m_root; protected set => m_root = value; }

        #endregion

        #region Ctor

        public BinSearchTree()
        {
            m_root = null;
        }

        #endregion

        #region Methods

        public void Clear()
        {
            m_root = null;
        }

        public void Insert(T value)
        {
            m_root = Insert(m_root, value);
        }

        private BSTNode<T> Insert(BSTNode<T> root, T value)
        {
            //Case when main root node is empty
            if (root == null)
            {
                return new BSTNode<T>(value);
            }
            //If main root is not empty we need to decide where to inseert another Node
            if (value.CompareTo(root.Data) == 1) // value greater then data. Need to go right
            {
                root.Right = Insert(root.Right, value);
            }
            else if (value.CompareTo(root.Data) == -1)//Value is less then data. Need to go left.
            {
                root.Left = Insert(root.Left, value);
            }
            
            return root;
        }

        public List<T> GetInorderTransversal()
        {
            var l = new List<T>();

            GetInorderTransversal(m_root, l);

            return l;
        }

        private void GetInorderTransversal(BSTNode<T> root, List<T> result)
        {
            if (root == null)
            {
                return;
            }

            GetInorderTransversal(root.Left, result);

            result.Add(root.Data);

            GetInorderTransversal(root.Right, result);
        }

        public BSTNode<T> Search(BSTNode<T> root, T key)
        {
            //Case when root is empty
            if (root == null)
            {
                return null;
            }
            else if (root.Data.Equals(key))//Case when Node was found
            {
                return root;
            }
            else if (key.CompareTo(root.Data) == 1)//Need to go right
            {
                return Search(root.Right, key);
            }
            else if (key.CompareTo(root.Data) == -1)//Need to go left
            {
                return Search(root.Left, key);
            }

            return null;//Nothing was found
        }

        public T RightMin(BSTNode<T> root)
        {
            var temp = root;

            while (temp.Left != null)
            {
                temp = temp.Left;
            }

            return temp.Data;
        }

        public BSTNode<T> RemoveNode(BSTNode<T> root, T key)
        {
            //Case if BST is empty
            if (root == null)
            {
                return null;
            }
            //If BST is not empty we need to find the node for removing
            if (key.CompareTo(root.Data) == 1)//Need to go right
            {
                root.Right = RemoveNode(root.Right, key);
            }
            else if (key.CompareTo(root.Data) == -1)//Need to go left
            {
                root.Left = RemoveNode(root.Left, key);
            }
            //Element for delition was found
            else if(key.Equals(root.Data))
            {
                //Now we need to look at 3 cases
                //1) Element has no children
                if (root.Left == null && root.Right == null)
                {
                    return null;
                }
                //2) Element has left child
                if (root.Right == null)
                {
                    return root.Left;
                }
                //3) Element has right child
                if(root.Left == null)
                {
                    return root.Right;
                }
                //4 Element has both root and right children
                //1) Find most right node with subnodes.
                //Take data from left subnode

                var rightMin = RightMin(root.Right);

                root.Data = rightMin;

                root.Right = RemoveNode(root.Right, rightMin);
            }

            return root;
        }

       

        #endregion
    }
}
