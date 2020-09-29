using System;
using System.Linq.Expressions;

namespace CodingDojo1
{
    class Program
    {
        static void Main(string[] args)
        {

            //new Stack
            Stack<Object> _stack1 = new Stack<Object>();


            //new Items to push onto the stack
            Item<Object> _item1 = new Item<Object>() { data = "Ivo", next = null };
            Item<Object> _item2 = new Item<Object>() { data = 24, next = null };
            Item<Object> _item3 = new Item<Object>() { data = new int[5], next = null };
            Item<Object> _item4 = new Item<Object>() { data = "Miki", next = null };
            Item<Object> _item5 = new Item<Object>() { data = "Malen", next = null };



            //push new item on top of stack
            _stack1.Push(_item1);
            _stack1.Push(_item2);
            _stack1.Push(_item3);
            _stack1.Push(_item4);
            _stack1.Push(_item5);


            //print
            Console.WriteLine(_item5.data);
            printStack(_stack1);

            //peek
            Console.WriteLine(_stack1.Peek());

            //pop
            Console.WriteLine(_stack1.Pop());
            printStack(_stack1);












        }

        private static void printStack(Stack<Object> stack)
        {
            if (stack._currentItem != null)
            {
                Console.WriteLine("The Stack will be printed in order: \n");
                while (stack._currentItem.next != null)
                {
                    Console.WriteLine($" -> {stack._currentItem.data}");
                    stack._currentItem = stack._currentItem.next;
                }
                Console.WriteLine($" -> {stack._currentItem.data}\n");
            }
            
        }
    }
}
