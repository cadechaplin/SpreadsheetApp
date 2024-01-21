// See https://aka.ms/new-console-template for more information
/*
 template matter?
 do I need to make sure its between 0 and 100?
 delete function? 
 counting method?
 what is the point of get; set;? 
 
 */


Console.WriteLine("Input numbers seperated by space");
string mystring = Console.ReadLine();
string[] nums = mystring.Split(' ');




BST<int> myBST = new BST<int>(); 
for (int i = 0; i < nums.Length; i++)
{
    myBST.insert(int.Parse(nums[i]));
}


Console.Write("Nodes in Numerical order: ");
myBST.inOrderTraversal();
Console.WriteLine();
Console.WriteLine("Tree Statistics:");
Console.WriteLine("Number of Nodes: " + myBST.nodeCount());
Console.WriteLine("Levels in Tree: " + myBST.countLevels());
Console.WriteLine("Theoretical Levels in Tree: " + myBST.theoLevels());
