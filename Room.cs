﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public bool IsBooked { get; set; }

        public Room(int roomNumber)
        {
            RoomNumber = roomNumber;
            IsBooked = false;
        }
    }
}
