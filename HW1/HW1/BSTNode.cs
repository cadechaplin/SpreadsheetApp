using System;

class Node<T>where T : IComparable<T>
{
    private T data;
    private Node<T> right;
    private Node<T> left;
    public Node(T passedData)
    {
        data = passedData;
        right = null;
        left = null;

    }
    private bool equalTo(T value1, T value2)
    {
        return value1.CompareTo(value2) == 0;
    }
    private bool IsGreaterThan(T value1, T value2)
    {
        return value1.CompareTo(value2) > 0;
    }
    public bool insert(T passedData)
    {
        if (this.equalTo(this.data, passedData)) // no douplicates
        {
            return false;
        }

        if (this.IsGreaterThan(this.data, passedData))
        {
            if (this.left == null)
            {
                this.left = new Node<T>(passedData);
                return true;
            }
            return this.left.insert(passedData);
        }
        if (this.right == null)
        {
            this.right = new Node<T>(passedData);
            return true;
        }
        return this.right.insert(passedData);
        

    }

    public void inOrderTraversal()
    {
        if (this.left != null)
        {
            this.left.inOrderTraversal();
        }

        Console.Write(this.data + " ");

        if (this.right != null)
        {
            this.right.inOrderTraversal();
        }
    }
}