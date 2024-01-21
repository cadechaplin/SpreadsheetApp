using System;

class BST<T>where T : IComparable<T>
{
    private Node<T> root = null;
    private int count { get; set; }

    public BST()
    {
        count = 0;
    }

    public int nodeCount()
    {
        return count;
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

    public int countLevels()
    {
        if (root == null)
        {
            return 0;
        }

        return root.checkLevels(1);
    }

    public int theoLevels()
    {
        return (int)Math.Ceiling(Math.Log2(this.count + 1)) - 1;
    }
}