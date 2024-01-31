namespace myfunctions;

using System;
using System.Collections.Generic;

public class HW2Prog
{
    private List<int> numbers;
    private Random generator;
    public HW2Prog()
    {
        numbers = new List<int>();
        generator = new Random();
    }

    public string run()
    {
        // Creating a List of integers
        for (int i = 0; i < 10000; i++)
        {
            numbers.Add(generator.Next(0, 20001));
        }
        

        return "Method 1: " + method1(DeepCopy(numbers)) + "\nThe time complexity of this is O(n) since "
                            + " \n" + "Method 2: " + method2(DeepCopy(numbers)) + " \n"
                            + "Method 3: " + method3(DeepCopy(numbers)) + "\n ";;; // return to output to screen
    }
    public int method1(List<int> curNums) // do not alter list and use hash set
    {
        HashSet<int> numberSet = new HashSet<int>();
        for (int i = 0; i < curNums.Count; i++)
        {
            numberSet.Add(curNums[i]);
        }

        return numberSet.Count;
    }
    public int method2(List<int> curNums) // remove all duplicates from the list?
    {
        /* incorrect method. Changes the list.
        int i = 0;
        int j;
        while(i < curNums.Count)
        {
            j = i + 1;
            while(j < curNums.Count)
            {
                if (curNums[i] == curNums[j])
                {
                    curNums.RemoveAt(j);
                }
                else
                {
                    j++;
                }


            }

            i++;
            
        }
    */
        if (curNums.Count == 0) // return 0 if list empty
        {
            return 0;
        }

        int uniqueNumsCount = 1; // first element must be unique since there is no predecessor
        for (int i = 1; i < curNums.Count; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (curNums[i] == curNums[j]) //check all previous elements to see if this element has occured, once found leave loop
                {
                    j = i; //exits loop
                }else if (j == i - 1) // last iteration reached and still unique
                {
                    uniqueNumsCount++;
                }

            }
        }
        return uniqueNumsCount;
    }
    public int method3(List<int> curNums) // use already implemented sorting algorithm
    {
        if (curNums.Count == 0)
        {
            return 0;
        }

        int count = 1;
        curNums.Sort();
        for (int i = 0; i < curNums.Count - 1; i++)
        {
            if (curNums[i] != curNums[i + 1])
            {
                count++;
            }
        }

        return count;
    }

    public List<int> DeepCopy(List<int> ogList)
    {
        List<int> copy = new List<int>();
        for (int i = 0; i < ogList.Count; i++)
        {
            copy.Add(ogList[i]);
        }

        return copy;
    }
}