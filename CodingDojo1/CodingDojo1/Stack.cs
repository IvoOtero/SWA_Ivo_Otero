using System;


namespace CodingDojo1
{
    class Item<T>
    {
        public Item<T> next { get; set; } //saves the reference to the next element in the stack-list
        public T data { get; set; }    // stores the data/value of the item (can be of any type)

        public override string ToString()
        {
            return $"{this.data}";
        }
    }

    class Stack<T>
    {
        public Item<T> _currentItem { get; set;  }   //first and current item on top of the stack


        //returns object from top of the stack 
        public T Peek()
        {
            if (_currentItem == null)
            {
                return default(T);
            }
            else
            {
                return _currentItem.data;
            }
            

        }

        // The Push Method adds a new object to the top of the stack
        public void Push(T item)
        {
            if (_currentItem == null)
            {    
                _currentItem = new Item<T>() {next = null, data = item}; //if the stack is empty, the new entry is placed on top of the stack
            }
            else
            {
                Item<T> tmp = new Item<T>() {next = _currentItem, data = item}; //new entry on top of the stack, with the current element being the next in the list
                _currentItem = tmp; // new entry becomes the current item (now on top of the stack)
            }
        }


        // the Pop Method removes the item on top of the stack (following the LIFO-Principle)
        public T Pop()
        {
            if (_currentItem == null)
            {
                throw new Exception();
            }
            else
            {
                T tmp = _currentItem.data; //data of the current item atop of stack is stored in a temporary element
                _currentItem = _currentItem.next; //the second element of the list is placed atop of the stack (as cur element is removed)
                return tmp; //the removed element stored in "tmp" is returned
            }
        }

    }
}