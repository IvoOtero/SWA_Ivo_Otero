using System;
using System.Dynamic;
using System.Linq.Expressions;

namespace CodingDojo1
{
    class Program
    {
        static void Main(string[] args)
        {
            int userInput;

            do
            {
                //intro 
                Console.WriteLine("Choose a number between 1 and 3 (press '0' to exit the program): ");
                userInput = int.Parse(Console.ReadLine());

                switch (userInput)
                {
                    case 0:
                        Console.WriteLine("Sorry to see you leave. See ya!");
                        break;

                    case 1:
                        TestStackOfObects();
                        break;

                    case 2:
                        TestStackOfInts();
                        break;

                    case 3:
                        TestStackOfStrings();
                        break;

                    default:
                        Console.WriteLine("You can´t just write something else, I´m sorry :/");
                        break;
                }
            } while (userInput != 0);



        }

        private static void TestStackOfStrings()
        {
            //intro
            Console.WriteLine("Number 2 means that you are testing with a stack of Strings (so interesting)!\n");

            //new Stack of any type of Objects
            Stack<String> stackOfStrings = new Stack<String>();

            //push numbers into the stack
            stackOfStrings.Push("!!!!");
            stackOfStrings.Push("implementation");
            stackOfStrings.Push("Stack");
            stackOfStrings.Push("to test");
            stackOfStrings.Push("way");
            stackOfStrings.Push("great");
            stackOfStrings.Push("such a");
            stackOfStrings.Push("Wow,");

            //print
            Console.WriteLine("FULL STACK IN ORDER:\n");
            if (stackOfStrings._currentItem != null)
            {
                Item<String> tmp = stackOfStrings._currentItem;
                while (tmp.next != null)
                {
                    Console.WriteLine($" -> {tmp.data}");
                    tmp = tmp.next;
                }
                Console.WriteLine($" -> {tmp.data}\n");

            }

            //peek
            Console.WriteLine($"Peeking.... found the element [{stackOfStrings.Peek()}]\n");

            //pop
            Console.WriteLine($"Deleting... element [{stackOfStrings.Pop()}] was succesfully deleted!\n");

            //print
            Console.WriteLine("FULL STACK IN ORDER:\n");
            if (stackOfStrings._currentItem != null)
            {
                Item<String> tmp = stackOfStrings._currentItem;
                while (tmp.next != null)
                {
                    Console.WriteLine($" -> {tmp.data}");
                    tmp = tmp.next;
                }
                Console.WriteLine($" -> {tmp.data}\n");

            }
        }

        private static void TestStackOfInts()
        {
            //intro
            Console.WriteLine("Number 2 means that you are testing with a stack of integers (so interesting)!\n");

            //new Stack of any type of Objects
            Stack<int> stackOfInts = new Stack<int>();

            //push numbers into the stack
            stackOfInts.Push(3);
            stackOfInts.Push(4);
            stackOfInts.Push(45);
            stackOfInts.Push(23);
            stackOfInts.Push(42);
            stackOfInts.Push(69);
            stackOfInts.Push(1038338738);

            //print
            Console.WriteLine("FULL STACK IN ORDER:\n");
            if (stackOfInts._currentItem != null)
            {
                Item<int> tmp = stackOfInts._currentItem;
                while (tmp.next != null)
                {
                    Console.WriteLine($" -> {tmp.data}");
                    tmp = tmp.next;
                }
                Console.WriteLine($" -> {tmp.data}\n");

            }

            //peek
            Console.WriteLine($"Peeking.... found the element [{stackOfInts.Peek()}]\n");

            //pop
            Console.WriteLine($"Deleting... element [{stackOfInts.Pop()}] was succesfully deleted!\n");

            //print
            Console.WriteLine("FULL STACK IN ORDER:\n");
            if (stackOfInts._currentItem != null)
            {
                Item<int> tmp = stackOfInts._currentItem;
                while (tmp.next != null)
                {
                    Console.WriteLine($" -> {tmp.data}");
                    tmp = tmp.next;
                }
                Console.WriteLine($" -> {tmp.data}\n");

            }
        }

        private static void TestStackOfObects()
        {
            //intro
            Console.WriteLine("Number 1 means that you´re testing the stack implementation with a stack of different objects as follows:\n");

            //new Stack of any type of Objects
            Stack<Object> stackOfObjects = new Stack<Object>();

            //new Items to push onto the stack
            Item<Object> _item1 = new Item<Object>() { data = "Ivo", next = null };
            Item<Object> _item2 = new Item<Object>() { data = 24, next = null };
            Item<Object> _item3 = new Item<Object>() { data = 99.7782629, next = null };
            Item<Object> _item4 = new Item<Object>() { data = "Miki", next = null };
            Item<Object> _item5 = new Item<Object>() { data = "Malen", next = null };


            //push new item on top of stack
            stackOfObjects.Push(_item1);
            stackOfObjects.Push(_item2);
            stackOfObjects.Push(_item3);
            stackOfObjects.Push(_item4);
            stackOfObjects.Push(_item5);

            //print
            printStack(stackOfObjects);

            //peek
            Console.WriteLine($"Peeking.... found the element [{stackOfObjects.Peek()}]\n");

            //pop
            Console.WriteLine($"Deleting... element [{stackOfObjects.Pop()}] was succesfully deleted!\n");

            printStack(stackOfObjects);
        }

        private static void printStack(Stack<Object> stack)
        {
            if (stack._currentItem != null)
            {
                Item<Object> tmp = stack._currentItem;

                Console.WriteLine("The Stack will be printed in order: \n");
                while (tmp.next != null)
                {
                    Console.WriteLine($" -> {tmp.data}");
                    tmp = tmp.next;
                }
                Console.WriteLine($" -> {tmp.data}\n");
            }

        }
    }
}
