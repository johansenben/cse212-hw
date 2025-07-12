using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: 
    //      test a queue with 6 values, where 2 have the same priority
    // Expected Result: 
    //      ["value4", "value1", "value2", "value6", "value3", "value5"]
    // Defect(s) Found: 
    //      -dequeue for loop skipped the first and last items when finding the highest priority
    //      -dequeue for loop updated highPriorityIndex when 2 values have the same priority, which caused it to use the last item with the same priority
    //      -dequeue didin't remove the item fromthe queue before returning it
    public void TestPriorityQueue_1()
    {
        /*
            #1 - value4 with a priority of 9 is the highest priority
            #2,#3 - value1 and value2 both have priority of 5, so value1 goes first, because it's first in the queue
            #4 - value6 is the next largest priority
            #5 - value3 is the next largest priority
            #6 - value5 is the lowest priority

            fixes:
                -start with i = 0 in the dequeue for loop, so it doesn't ignore the first value
                -changed >= to > in the if statement inside the dequeue for loop
                    -if it's >=, it updates the highPriorityIndex if 2 indexes have the same priority
                -changed "< _queue.Count - 1" to "< _queue.Count" in the dequeue for loop
                    -if its "< _queue.Count - 1", doesn't check the priority of the last item
                -remove the value that is going to be returned in the dequeue method
        */

        //expected order
        string[] expectedOutput = ["value4", "value1", "value2", "value6", "value3", "value5"];

        //initialize queue
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("value1", 5);
        priorityQueue.Enqueue("value2", 5);
        priorityQueue.Enqueue("value3", 3);
        priorityQueue.Enqueue("value4", 9);
        priorityQueue.Enqueue("value5", 2);
        priorityQueue.Enqueue("value6", 4);

        //iterate through the queue and check each value
        int i = 0;
        while (i < 6)
        {
            string value = priorityQueue.Dequeue();
            Assert.AreEqual(value, expectedOutput[i]);
            i++;
        }
    }

    [TestMethod]
    // Scenario: 
    //      test if an empty queue creates the expected result
    // Expected Result: 
    //      it should create an exception
    // Defect(s) Found: 
    public void TestPriorityQueue_2()
    {
        //create new empty queue
        var priorityQueue = new PriorityQueue();

        try
        {
            //dequeue should fail if it works
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    // Add more test cases as needed below.
}