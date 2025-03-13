

# Hotel Management System

The **Hotel Management System** is a desktop application built using **C#** and **Windows Forms**. It allows hotel staff to manage rooms, guests, and bookings efficiently. The application uses **MySQL** as the backend database and **XAMPP** for hosting the database.

---

## Features

1. **Room Management**:
   - Add, edit, and delete rooms.
   - View a list of all rooms and their booking status.

2. **Guest Management**:
   - Add, edit, and delete guests.
   - View a list of all guests.

3. **Booking Management**:
   - Book a room for a guest.
   - View all bookings with details like room number, guest name, and booking date.
   - Cancel bookings and mark rooms as available.

4. **Database Integration**:
   - Uses **MySQL** for data storage.
   - **XAMPP** for hosting the MySQL database.

5. **User-Friendly UI**:
   - Modern and intuitive interface with **TabControl** for easy navigation.
   - **DataGridView** for displaying data in a tabular format.
   - **ComboBox** and **DateTimePicker** for efficient data input.

---

## Prerequisites

Before running the application, ensure you have the following installed:

1. **Visual Studio** (2019 or later) with **.NET Desktop Development** workload.
2. **XAMPP** for hosting the MySQL database.
3. **MySQL Connector/NET** (installed via NuGet Package Manager).

---

## Setup Instructions

### Step 1: Clone the Repository
Clone the project repository to your local machine:
```bash
git clone https://github.com/your-username/hotel-management-system.git
```

### Step 2: Set Up the Database
1. **Install XAMPP**:
   - Download and install [XAMPP](https://www.apachefriends.org/index.html).
   - Start **Apache** and **MySQL** from the XAMPP Control Panel.

2. **Create the Database**:
   - Open **phpMyAdmin** by navigating to `http://localhost/phpmyadmin`.
   - Create a new database named `hotelmanagementdb`.

3. **Create Tables**:
   - Run the following SQL script to create the required tables:
     ```sql
     USE hotelmanagementdb;

     CREATE TABLE Rooms (
         RoomID INT PRIMARY KEY AUTO_INCREMENT,
         RoomNumber INT NOT NULL,
         IsBooked BOOLEAN NOT NULL DEFAULT FALSE
     );

     CREATE TABLE Guests (
         GuestID INT PRIMARY KEY AUTO_INCREMENT,
         Name VARCHAR(100) NOT NULL
     );

     CREATE TABLE Bookings (
         BookingID INT PRIMARY KEY AUTO_INCREMENT,
         RoomID INT,
         GuestID INT,
         BookingDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
         FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
         FOREIGN KEY (GuestID) REFERENCES Guests(GuestID)
     );
     ```

### Step 3: Configure the Connection String
1. Open the `App.config` file in the project.
2. Update the connection string to match your MySQL credentials:
   ```xml
   <connectionStrings>
       <add name="HotelManagementDB" 
            connectionString="Server=localhost;Database=hotelmanagementdb;Uid=root;Pwd=;" 
            providerName="MySql.Data.MySqlClient" />
   </connectionStrings>
   ```
   - Replace `root` and the empty password (`Pwd=`) with your MySQL credentials if you’ve set a password.

### Step 4: Build and Run the Application
1. Open the project in **Visual Studio**.
2. Build the solution by pressing `Ctrl + Shift + B`.
3. Run the application by pressing `F5`.

---

## Usage

### Room Management
1. Navigate to the **Rooms** tab.
2. Use the **Add Room** button to add a new room.
3. Use the **Edit Room** and **Delete Room** buttons to modify or remove rooms.

### Guest Management
1. Navigate to the **Guests** tab.
2. Use the **Add Guest** button to add a new guest.
3. Use the **Edit Guest** and **Delete Guest** buttons to modify or remove guests.

### Booking Management
1. Navigate to the **Bookings** tab.
2. Select a room and guest from the dropdowns.
3. Choose a booking date using the **DateTimePicker**.
4. Click **Book Room** to create a booking.
5. Use the **Cancel Booking** button to remove a booking and mark the room as available.

---

## Project Structure

```
HotelManagementSystem/
├── App.config                # Configuration file for database connection
├── Form1.cs                 # Main form with UI and event handlers
├── DatabaseHelper.cs        # Helper class for database operations
├── Room.cs                  # Room entity class
├── Guest.cs                 # Guest entity class
├── Booking.cs               # Booking entity class
└── README.md                # Project documentation
```

---


## Contributing

Contributions are welcome! If you'd like to contribute, please follow these steps:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeatureName`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/YourFeatureName`).
5. Open a pull request.

---

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## Contact

For any questions or feedback, please contact:
- **Aayush Mishra**  
- **Email**: aayushmishra169@gmail.com 
- **GitHub**: [mishra-aayush21](https://github.com/mishra-aayush21)

---

Enjoy using the **Hotel Management System**! 😊

--- 
