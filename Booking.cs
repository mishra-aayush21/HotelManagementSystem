using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class Booking
    {
        public Room Room { get; set; }
        public Guest Guest { get; set; }

        public Booking(Room room, Guest guest)
        {
            Room = room;
            Guest = guest;
        }
    }
}
