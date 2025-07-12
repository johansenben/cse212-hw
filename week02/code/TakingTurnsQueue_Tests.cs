using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 1 - Run test cases and record any defects the test code finds in the comment above the test method.
// DO NOT MODIFY THE CODE IN THE TESTS in this file, just the comments above the tests. 
// Fix the code being tested to match requirements and make all tests pass. 

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3) and
    // run until the queue is empty
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: 
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        /* 
            1. initialize people and queue
            2. iterate through the queue until the queue is empty 
                -fails if:
                    -the iteration count is more than equal to the length of the expected array
                    -person.Name != expected name

            fixes: 
            PersonQueue.Enqueue - Insert changed to Add
                -it was adding each new person to the beginning of the queue instead of the end
                -the initial queue was [Sue, Tim, Bob], but needs to be [Bob, Tim, Sue]
        */

        //create people
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);

        //expected order
        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        //initialize queue
        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        //iterate through the queue until the queue is empty
        int i = 0;
        while (players.Length > 0)
        {
            //stop trying if the queue hasn't been emptied yet
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            //get next person
            var person = players.GetNextPerson();

            //does person have the expected name
            Assert.AreEqual(expectedResult[i].Name, person.Name);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3)
    // After running 5 times, add George with 3 turns.  Run until the queue is empty.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
    // Defect(s) Found: 
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        /*
            1. initialize people and queue
            2. check the first 5 people in the queue
            3. add George
            4. iterate through the rest of the queue

            fixes:
                none (it passed after the change to the finiteRepetition test)
        */

        //create people
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);
        var george = new Person("George", 3);

        //expected order
        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, george, sue, tim, george, tim, george];

        //initialize queue
        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        //check the first 5 people in the queue
        int i = 0;
        for (; i < 5; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        //add George
        players.AddPerson("George", 3);

        //iterate through the rest of the queue
        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);

            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (Forever), Sue (3)
    // Run 10 times.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: 
    public void TestTakingTurnsQueue_ForeverZero()
    {
        /*
            1. initialize people and queue
            2. iterate through the queue until only Tim is left (Tim has infinite turns)
            3. check if Tim is still in the queue

            fixes:
                -if Turns <= 0 -> re-add the person to the queue
                -0 turns means infinite turns
        */

        //create people
        var timTurns = 0;
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        //expected order
        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        //initialize queue
        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        //iterate through the queue until only Tim is left
        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that the people with infinite turns really do have infinite turns.
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Tim (Forever), Sue (3)
    // Run 10 times.
    // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
    // Defect(s) Found: 
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        /*
            1. initialize people and queue
            2. iterate through the queue until only Tim is left (Tim has infinite turns)
            3. check if Tim is still in the queue

            fixes:
                -none (it passed after the changes to the previous tests)
        */

        //create people
        var timTurns = -3;
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        //expected order
        Person[] expectedResult = [tim, sue, tim, sue, tim, sue, tim, tim, tim, tim];

        //initialize queue
        var players = new TakingTurnsQueue();
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that the people with infinite turns really do have infinite turns.
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
    }

    [TestMethod]
    // Scenario: Try to get the next person from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message.
    // Defect(s) Found: 
    public void TestTakingTurnsQueue_Empty()
    {
        /*
            1. try to get the next person
            2. it should fail to get the next person

            fixes:
                none (it passed)
        */

        //initialize empty queue
        var players = new TakingTurnsQueue();

        try
        {
            //getNextPerson should fail if it works
            players.GetNextPerson();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("No one in the queue.", e.Message);
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
}