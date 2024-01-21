// See https://aka.ms/new-console-template for more information
/*
 template matter?
 do I need to make sure its between 0 and 100?
 delete function? 
 counting method?
 
 */

/*
Console.WriteLine("Input numbers seperated by space");
string mystring = Console.ReadLine();
string[] nums = mystring.Split(' ');
Console.WriteLine(mystring);
*/
int[] tempNums = {1,2,4,753,1231,123,24,1,2,47};

BST<int> myClass = new BST<int>(); 
for (int i = 0; i < tempNums.Length; i++)
{
    myClass.insert(tempNums[i]);
}
myClass.inOrderTraversal();
Console.WriteLine("exit");