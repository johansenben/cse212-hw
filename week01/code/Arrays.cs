public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.


        /*
            -the total number of elements in the array is equal to length
            -the value at each index should be equal to number * (index + 1)

            1. create a new empty array
            2. iterate through each index of the array
            3. set each value of the array
                -value = number * (index + 1)
            4. return the array
        */

        //create an empty array with a size equal to the length
        double[] values = new double[length];

        //loop with 1 iteration per value in length
        for (int index = 0; index < length; index++)
        {
            //the value of values[index] is number * (index + 1)
            values[index] = number * (index + 1);
        }

        //return the array
        return values;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        /*
            1. save the slice at the end of the list, then remove it from the list
            2. insert the slice at the beginning of the list
        */

        //save the length to access later
        int length = data.Count; 

        //save the slice from the end of the list
        List<int> slice = data.GetRange(length - amount, amount);

        //remove the slice from the end the end of the list
        data.RemoveRange(length - amount, amount);

        //add the saved slice to the beginning of the list
        data.InsertRange(0, slice);       
    }
}
