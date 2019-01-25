using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PRG2Assignment
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //global lists
        List<HotelRoom> roomList = new List<HotelRoom>(); //stores all the room objects
        List<Guest> guestList = new List<Guest>(); //stores all the guests objects
        List<HotelRoom> availableRooms = new List<HotelRoom>(); //stores all the available room objects
        List<HotelRoom> tempRoomList = new List<HotelRoom>(); //stores selected room objects

        public MainPage()
        {
            this.InitializeComponent();
            InitHotelRooms(); //create room objects and stores into roomList 
            InitGuests(); //create guest objects and stores into guestList
            CheckAvailability(); //checks for room that is available and put it into a list
        }
        public void InitGuests() {
            //first guest information
            Stay s1 = new Stay(Convert.ToDateTime("26-Jan-2019"), Convert.ToDateTime("02-Feb-2019"));
            Membership m1 = new Membership("Gold", 280);
            Guest g1 = new Guest("Amelia", "S1234567A", s1, m1, false);
            //second guest information
            Stay s2 = new Stay(Convert.ToDateTime("25-Jan-2019"), Convert.ToDateTime("31-Jan-2019"));
            Membership m2 = new Membership("Ordinary", 0);
            Guest g2 = new Guest("Bob", "G1234567A", s2, m2, false);
            //third guest information
            Stay s3 = new Stay(Convert.ToDateTime("01-Feb-2019"), Convert.ToDateTime("06-Feb-2019"));
            Membership m3 = new Membership("Silver", 190);
            Guest g3 = new Guest("Cody", "G234567A", s3, m3, false);
            //fourth guest information
            Stay s4 = new Stay(Convert.ToDateTime("28-Jan-2019"), Convert.ToDateTime("10-Feb-2019"));
            Membership m4 = new Membership("Gold", 10);
            Guest g4 = new Guest("Edda", "S3456789A", s4, m4, false);

            //adding guest objects into guestList
            guestList.Add(g1);
            guestList.Add(g2);
            guestList.Add(g3);
            guestList.Add(g4);
        } //create guest objects

        public void InitHotelRooms()
        {
            //standard room objects
            HotelRoom room1 = new StandardRoom("Standard", "101", "Single", 90, false, 0);
            HotelRoom room2 = new StandardRoom("Standard", "102", "Single", 90, true, 0);
            HotelRoom room3 = new StandardRoom("Standard", "201", "Twin", 110, true, 0);
            HotelRoom room4 = new StandardRoom("Standard", "202", "Twin", 110, false, 0);
            HotelRoom room5 = new StandardRoom("Standard", "203", "Twin", 110, true, 0);
            HotelRoom room6 = new StandardRoom("Standard", "301", "Triple", 120, true, 0);
            HotelRoom room7 = new StandardRoom("Standard", "302", "Triple", 120, false, 0);
            //deluxe room objects
            HotelRoom room8 = new DeluxeRoom("Deluxe", "204", "Twin", 140, true, 0);
            HotelRoom room9 = new DeluxeRoom("Deluxe", "205", "Twin", 140, true, 0);
            HotelRoom room10 = new DeluxeRoom("Deluxe", "303", "Triple", 210, false, 0);
            HotelRoom room11 = new DeluxeRoom("Deluxe", "304", "Triple", 210, true, 0);
            //adding to roomList
            roomList.Add(room1);
            roomList.Add(room2);
            roomList.Add(room3);
            roomList.Add(room4);
            roomList.Add(room5);
            roomList.Add(room6);
            roomList.Add(room7);
            roomList.Add(room8);
            roomList.Add(room9);
            roomList.Add(room10);
            roomList.Add(room11);
        } //create room objects

        public void CheckAvailability() {
            foreach (HotelRoom room in roomList)
            {
                if (room.IsAvail == true) //checks whether the room is available if it is, add it into available list
                {
                    availableRooms.Add(room);
                }
                else if (room.IsAvail == false) 
                {
                    availableRooms.Remove(room);
                }
            }
        }

        public void RefreshList() {
            lvRoomsSelected.ItemsSource = null;
            lvRoomsSelected.ItemsSource = tempRoomList;
            lvAvailableRooms.ItemsSource = null;
            lvAvailableRooms.ItemsSource = availableRooms;
        }

        private void checkInBtn_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            foreach (Guest g in guestList)
            {
                if (guestTxt.Text != g.Name) //if guest.text cannot be found in the guestList.Name
                {
                    check = true;
                }
            }
            if (check == true)
            {
                Stay s = new Stay(DateTime.Parse(checkInDatePicker.Date.ToString()), DateTime.Parse(checkOutDatePicker.Date.ToString()));
                Membership m = new Membership("Ordinary", 0);
                Guest guest = new Guest(guestTxt.Text, passportTxt.Text, s, m, false);
                guestList.Add(guest);
                RefreshList();
                // to be done: remove the selected room(s) from its available room list and 'give' it to the guest and display a check-in successful message [2.1.5]
            }
            else if (check == false)
            {
                //put a message by welcoming the user back and 'give' the selected room(s) to this guest and display another message saying check-in is successful
            }
        }

        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            double p = 0;
            HotelRoom r = (HotelRoom)lvAvailableRooms.SelectedItem;
            if (r.RoomType == "Standard") //check for standard or deluxe and allow which checkbox to be checked [2.1.3]
            {
                if (addWifiCheckBox.IsChecked == true)
                {
                    p += 10;
                }
                if (addBreakfastCheckBox.IsChecked == true)
                {
                    p += 20;
                }
            }
            else if (r.RoomType == "Deluxe")
            {
                if (addBedCheckBox.IsChecked == true)
                {
                    p = 25;
                }
            }
            r.DailyRate += p;
            r.IsAvail = false;
            if (r.IsAvail == false)
            {
                availableRooms.Remove(r);
            }
            tempRoomList.Add(r);
            RefreshList();
        }

        private void removeRoomBtn_Click(object sender, RoutedEventArgs e) //removes the selected room from the selected listview [2.1.4]
        {
            HotelRoom r = (HotelRoom)lvRoomsSelected.SelectedItem;
            r.IsAvail = true;
            if (r.IsAvail == true) //checks whether the room is available
            {
                availableRooms.Add(r);
            }
            if (r.RoomType == "Standard") //check for standard or deluxe and allow which checkbox to be checked [2.1.3]
            {
                if (addWifiCheckBox.IsChecked == true)
                {
                    r.DailyRate = r.DailyRate - 10;
                }
                if (addBreakfastCheckBox.IsChecked == true)
                {
                    r.DailyRate = r.DailyRate - 20;
                }
            }
            else if (r.RoomType == "Deluxe")
            {
                if (addBedCheckBox.IsChecked == true)
                {
                    r.DailyRate = r.DailyRate - 25;
                }
            }
            tempRoomList.Remove(r);
            RefreshList();
        }

        private void checkRoomsBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshList(); //loads available rooms into the availableroom listview [2.1.2]
        }
    }
}
