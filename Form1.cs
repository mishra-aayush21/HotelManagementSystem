using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HotelManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadRooms();
            LoadGuests();
            LoadBookings();
            LoadAvailableRooms();
            LoadGuestsComboBox();
        }

        // Database Helper Methods
        private static DataTable ExecuteQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=hotelmanagementdb;Uid=root;Pwd=;"))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private static int ExecuteNonQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=hotelmanagementdb;Uid=root;Pwd=;"))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                return command.ExecuteNonQuery();
            }
        }

        // Load Data Methods
        private void LoadRooms()
        {
            string query = "SELECT * FROM Rooms";
            dgvRooms.DataSource = ExecuteQuery(query);
        }

        private void LoadGuests()
        {
            string query = "SELECT * FROM Guests";
            dgvGuests.DataSource = ExecuteQuery(query);
        }

        private void LoadBookings()
        {
            string query = @"
        SELECT b.BookingID, b.RoomID, r.RoomNumber, g.Name AS GuestName, b.BookingDate
        FROM Bookings b
        JOIN Rooms r ON b.RoomID = r.RoomID
        JOIN Guests g ON b.GuestID = g.GuestID";
            dgvBookings.DataSource = ExecuteQuery(query);
        }

        private void LoadAvailableRooms()
        {
            string query = "SELECT * FROM Rooms WHERE IsBooked = FALSE"; // Load all rooms
            cmbRooms.DataSource = ExecuteQuery(query);
            cmbRooms.DisplayMember = "RoomNumber";
            cmbRooms.ValueMember = "RoomID";
        }

        private void LoadGuestsComboBox()
        {
            string query = "SELECT * FROM Guests";
            cmbGuests.DataSource = ExecuteQuery(query);
            cmbGuests.DisplayMember = "Name";
            cmbGuests.ValueMember = "GuestID";
        }

        // Event Handlers
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
            {
                MessageBox.Show("Please enter a room number.");
                return;
            }

            string query = $"INSERT INTO Rooms (RoomNumber, IsBooked) VALUES ({txtRoomNumber.Text}, FALSE)";
            ExecuteNonQuery(query);
            LoadRooms();
            LoadAvailableRooms();
            MessageBox.Show("Room added successfully!");
        }

        private void btnAddGuest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGuestName.Text))
            {
                MessageBox.Show("Please enter a guest name.");
                return;
            }

            string query = $"INSERT INTO Guests (Name) VALUES ('{txtGuestName.Text}')";
            ExecuteNonQuery(query);
            LoadGuests();
            LoadGuestsComboBox();
            MessageBox.Show("Guest added successfully!");
        }

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            if (cmbRooms.SelectedItem == null || cmbGuests.SelectedItem == null)
            {
                MessageBox.Show("Please select a room and a guest.");
                return;
            }

            int roomID = (int)((DataRowView)cmbRooms.SelectedItem)["RoomID"];
            int guestID = (int)((DataRowView)cmbGuests.SelectedItem)["GuestID"];

            string bookingQuery = $@"
                INSERT INTO Bookings (RoomID, GuestID, BookingDate)
                VALUES ({roomID}, {guestID}, '{dtpBookingDate.Value:yyyy-MM-dd HH:mm:ss}')";
            ExecuteNonQuery(bookingQuery);

            string updateRoomQuery = $"UPDATE Rooms SET IsBooked = TRUE WHERE RoomID = {roomID}";
            ExecuteNonQuery(updateRoomQuery);

            LoadBookings();
            LoadAvailableRooms();
            MessageBox.Show("Room booked successfully!");
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBookings.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a booking to cancel.");
                    return;
                }

                // Get the selected row
                DataGridViewRow selectedRow = dgvBookings.SelectedRows[0];

                // Get the BookingID and RoomID from the selected row
                int bookingID = Convert.ToInt32(selectedRow.Cells["BookingID"].Value);
                int roomID = Convert.ToInt32(selectedRow.Cells["RoomID"].Value);

                // Debug messages
                MessageBox.Show($"BookingID: {bookingID}, RoomID: {roomID}");

                // Delete the booking
                string deleteBookingQuery = $"DELETE FROM Bookings WHERE BookingID = {bookingID}";
                ExecuteNonQuery(deleteBookingQuery);

                // Mark the room as available
                string updateRoomQuery = $"UPDATE Rooms SET IsBooked = FALSE WHERE RoomID = {roomID}";
                ExecuteNonQuery(updateRoomQuery);

                // Refresh the DataGridViews
                LoadBookings();
                LoadAvailableRooms();
                LoadRooms(); // Refresh the Rooms DataGridView
                MessageBox.Show("Booking canceled successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=hotelmanagementdb;Uid=root;Pwd=;"))
                {
                    connection.Open();
                    MessageBox.Show("Connection successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}