using System;
using System.Collections.Generic;
using System.Text;

namespace CodingDojo6.MessageBarProperties
{
    public class Message
    {
        public string Text{ get; set; }
        public MessageState State { get; set; }

        public Message(string text, MessageState state)
        {
            this.Text = text;
            this.State = state;
        }
    }
}
