﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BaseModels
{
    // Smart-Home abstract class for every Smarthome Item and the qualities all Items have in common
    public abstract class ItemBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Room { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public ItemBase(string name, string description, string room, int id)
        {
            Name = name;
            Description = description;
            Id = id;
            Room = room;


        }
    }
}
