using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceiling
{
    class Program
    {
        static void Main(string[] args)
        {
            string variables = Console.ReadLine();
            string[] tokens = variables.Split(' ');
            int n = int.Parse(tokens[0]);//number of ceiling prototypes
            int k = int.Parse(tokens[1]);//number of layers

            List<BST> shapes = new List<BST>();

            for(int i = 0; i < n; i++)
            {
                string data = Console.ReadLine();
                string[] values = data.Split(' ');
                BST newTree = new BST();
                for (int x = 0; x < k; x++)
                {
                    newTree.add(int.Parse(values[x]));
                }
                bool shapeExists = false;
                foreach(BST tree in shapes)
                {
                    if (newTree.isSameShape(tree))
                    {
                        shapeExists = true;
                    }
                }
                if (!shapeExists)
                {
                    shapes.Add(newTree);
                }                
            }
            
            Console.Out.WriteLine(shapes.Count);
            //Console.Read();
        }
    }

    class BST
    {
        private Node root;
        private class Node
        {
            public int value;
            public Node leftChild, rightChild;
            public Node(int value)
            {
                this.value = value;
            }
        }

        public void add(int value)
        {
            if(root == null)
            {
                root = new Node(value);
            }
            else
            {
                Node nextNode = this.root;
                Node prevNode = null;

                while (nextNode != null)
                {
                    prevNode = nextNode;
                    if(value < nextNode.value)
                    {
                        nextNode = nextNode.leftChild;
                    }
                    else if(value > nextNode.value)
                    {
                        nextNode = nextNode.rightChild;
                    }
                    else
                    {
                        return;//same value
                    }
                }
                Node n = new Node(value);
                if (n.value < prevNode.value)
                {
                    prevNode.leftChild = n;
                }
                else
                {
                    prevNode.rightChild = n;
                }
            }
        }

        public bool isSameShape(BST other)
        {
            return areSameShape(this.root, other.root);
        }

        private bool areSameShape(Node a, Node b)
        {
            if(a == null && b == null)
            {
                return true;
            }

            if(a != null && b != null)
            {
                return (areSameShape(a.leftChild, b.leftChild) && areSameShape(a.rightChild,b.rightChild));
            }
            return false;
        }
    }
}
