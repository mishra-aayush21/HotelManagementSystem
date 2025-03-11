using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public partial class Form1 : Form
    {
        private List<Room> rooms = new List<Room>();
        private List<Guest> guests = new List<Guest>();
        private List<Booking> bookings = new List<Booking>();
        

        public Form1()
        {
            InitializeComponent();
            cmbRooms.SelectedIndexChanged += cmbRooms_SelectedIndexChanged; // Add this line
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            int roomNumber = int.Parse(txtRoomNumber.Text);
            Room newRoom = new Room(roomNumber);
            rooms.Add(newRoom);
            cmbRooms.Items.Add(roomNumber);
            MessageBox.Show("Room added successfully!");
        }

        private void btnAddGuest_Click(object sender, EventArgs e)
        {
            string guestName = txtGuestName.Text;
            Guest newGuest = new Guest(guestName);
            guests.Add(newGuest);
            cmbGuests.Items.Add(guestName);
            MessageBox.Show("Guest added successfully!");
        }

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            int selectedRoomNumber = (int)cmbRooms.SelectedItem;
            string selectedGuestName = (string)cmbGuests.SelectedItem;

            Room selectedRoom = rooms.Find(r => r.RoomNumber == selectedRoomNumber);
            Guest selectedGuest = guests.Find(g => g.Name == selectedGuestName);

            if (selectedRoom != null && selectedGuest != null)
            {
                if (!selectedRoom.IsBooked)
                {
                    Booking newBooking = new Booking(selectedRoom, selectedGuest);
                    bookings.Add(newBooking);
                    selectedRoom.IsBooked = true;
                    UpdateBookingsGrid();
                    MessageBox.Show("Room booked successfully!");
                }
                else
                {
                    MessageBox.Show("Room is already booked!");
                }
            }
        }

        private void UpdateBookingsGrid()
        {
            dgvBookings.Rows.Clear();
            foreach (var booking in bookings)
            {
                dgvBookings.Rows.Add(booking.Room.RoomNumber, booking.Guest.Name);
            }
        }
        private void cmbRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add your logic here
            // For example, you can display the selected room number:
            if (cmbRooms.SelectedItem != null)
            {
                int selectedRoomNumber = (int)cmbRooms.SelectedItem;
                MessageBox.Show("Selected Room: " + selectedRoomNumber);
            }
        }
    }
}