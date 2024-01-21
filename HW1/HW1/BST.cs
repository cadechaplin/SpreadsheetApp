using System;

class BST<T>where T : IComparable<T>
{
    private Node<T> root = null;
    private int count;

    public BST()
    {
        count = 0;
    }

    public void insert(T passedData)
    {
        if (root == null)
        {
            root = new Node<T>(passedData);
            count++;
            return;
        }

        if (root.insert(passedData))
        {
            count++;
        }
    }

    public void inOrderTraversal()
    {
        if (this.root == null)
        {
            return;
            
        }
        this.root.inOrderTraversal();
    }
}