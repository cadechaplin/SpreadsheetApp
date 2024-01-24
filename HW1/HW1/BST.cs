using System;

class BST<T>where T : IComparable<T>
{
    private Node<T> root = null;
    

    public BST()
    {
        
    }

    
    public void insert(T passedData)
    {
        if (root == null)
        {
            root = new Node<T>(passedData);
            
            return;
        }

        root.insert(passedData);
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
        return (int)Math.Ceiling(Math.Log2(this.nodeTraversalCount() + 1));
    }

    public int nodeTraversalCount()
    {
        if (root == null)
        {
            return 0;
        }

        return root.count();
    }
    
}