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
        List<HotelRoom> hotelList = new List<HotelRoom>(); //stores all the hotel objects
        List<Guest> guestList = new List<Guest>(); //stores all the guests objects
        List<HotelRoom> availableRooms = new List<HotelRoom>(); //stores all the available room objects
        List<HotelRoom> tempRoomList = new List<HotelRoom>(); //stores selected room objects

        public MainPage()
        {
            this.InitializeComponent();
            InitData(); //create room objects and stores into hotelList 
            CheckAvailability(); //checks for room that is available and put it into a list
        }


        public void InitData() //create room objects
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
            //adding to hotelList
            hotelList.Add(room1);
            hotelList.Add(room2);
            hotelList.Add(room3);
            hotelList.Add(room4);
            hotelList.Add(room5);
            hotelList.Add(room6);
            hotelList.Add(room7);
            hotelList.Add(room8);
            hotelList.Add(room9);
            hotelList.Add(room10);
            hotelList.Add(room11);

            //first guest information
            Stay s1 = new Stay(Convert.ToDateTime("26-Jan-2019"), Convert.ToDateTime("02-Feb-2019"));
            List<HotelRoom> roomList1 = s1.RoomList;
            s1.AddRoom(room1);
            Membership m1 = new Membership("Gold", 280);
            Guest g1 = new Guest("Amelia", "S1234567A", s1, m1, false);
            //second guest information
            Stay s2 = new Stay(Convert.ToDateTime("25-Jan-2019"), Convert.ToDateTime("31-Jan-2019"));
            List<HotelRoom> roomList2 = s2.RoomList;
            s2.AddRoom(room7);
            Membership m2 = new Membership("Ordinary", 0);
            Guest g2 = new Guest("Bob", "G1234567A", s2, m2, false);
            //third guest information
            Stay s3 = new Stay(Convert.ToDateTime("01-Feb-2019"), Convert.ToDateTime("06-Feb-2019"));
            List<HotelRoom> roomList3 = s3.RoomList;
            s3.AddRoom(room4);
            Membership m3 = new Membership("Silver", 190);
            Guest g3 = new Guest("Cody", "G234567A", s3, m3, false);
            //fourth guest information
            Stay s4 = new Stay(Convert.ToDateTime("28-Jan-2019"), Convert.ToDateTime("10-Feb-2019"));
            List<HotelRoom> roomList4 = s4.RoomList;
            s4.AddRoom(room10);
            Membership m4 = new Membership("Gold", 10);
            Guest g4 = new Guest("Edda", "S3456789A", s4, m4, false);

            //adding guest objects into guestList
            guestList.Add(g1);
            guestList.Add(g2);
            guestList.Add(g3);
            guestList.Add(g4);

        }

        public void CheckAvailability() {
            foreach (HotelRoom room in hotelList)
            {
                if (room.IsAvail == true) //checks whether the room is available if it is, add it into available list which is use to be displayed on the listview
                {
                    availableRooms.Add(room);
                }
                else if (room.IsAvail == false) 
                {
                    availableRooms.Remove(room);
                }
            }
        } //uses a foreach loop to filter for rooms that are available

        public void RefreshList() { //updates the listview
            lvRoomsSelected.ItemsSource = null;
            lvRoomsSelected.ItemsSource = tempRoomList;
            lvAvailableRooms.ItemsSource = null;
            lvAvailableRooms.ItemsSource = availableRooms;
        } //updates the listview

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
                Stay s = new Stay(DateTime.Parse(checkInDatePicker.Date.ToString()), DateTime.Parse(checkOutDatePicker.Date.ToString())); //get datestart and dateend
                Membership m = new Membership("Ordinary", 0); 
                Guest guest = new Guest(guestTxt.Text, passportTxt.Text, s, m, false); //create guest info 1:1
                guestList.Add(guest); //add into guestList
                string roomscheckedin = "";
                for (var i = 0; i < tempRoomList.Count; i++)
                {
                    HotelRoom r = tempRoomList[i];
                    r.IsAvail = false;
                    r.NoOfOccupants = Convert.ToInt32(noOfAdultTxt.Text) + Convert.ToInt32(noOfChildrentxt.Text);
                    HotelRoom h = new StandardRoom(r.RoomType, r.RoomNumber, r.BedConfiguration, r.DailyRate, r.IsAvail, r.NoOfOccupants);
                    s.AddRoom(h);
                    List<HotelRoom> roomList = s.RoomList;
                    string guestStayDetails = "";
                    foreach (HotelRoom room in roomList)
                    {
                        guestStayDetails += room.ToString() + guest.ToString();
                    }
                    roomsBookedTxt.Text = guestStayDetails;
                    roomscheckedin += r.RoomNumber.ToString() + " ";
                }
                tempRoomList.Clear();
                RefreshList();
                //if (r.RoomType == "Standard")                                  
                //{    
                //}
                //else if (r.RoomType == "Deluxe")
                //{
                //r.IsAvail = false;
                //r.NoOfOccupants = Convert.ToInt32(noOfAdultTxt.Text) + Convert.ToInt32(noOfChildrentxt.Text);
                //HotelRoom h = new StandardRoom(r.RoomType, r.RoomNumber, r.BedConfiguration, r.DailyRate, r.IsAvail, r.NoOfOccupants);
                //s.AddRoom(h);
                //}
                // to be done: remove the selected room(s) from its available room list and 'give' it to the guest and display a check-in successful message [2.1.5]
                statusUpdateText.Text = "Status update: Rooms" + " " + roomscheckedin +"checked in successfully.";
                roomscheckedin = "";
            }
            else if (check == false)
            {
                Stay s = new Stay(DateTime.Parse(checkInDatePicker.Date.ToString()), DateTime.Parse(checkOutDatePicker.Date.ToString())); //get datestart and dateend
                foreach (Guest g in guestList)
                {
                    if (guestTxt.Text == g.Name)
                    {

                    }
                }
                //put a message by welcoming the user back and 'give' the selected room(s) to this guest and display another message saying check-in is successful [2.1.6]
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

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (guestTxt.Text != "" || guestTxt.Text != " ")
            {
                foreach (Guest guest in guestList)
                {
                    if (guest.Name == guestTxt.Text)
                    {
                        lvAvailableRooms.ItemsSource = guest.HotelStay.RoomList;
                    }
                    else if (guest.PPNumber == passportTxt.Text)
                    {
                        lvAvailableRooms.ItemsSource = guest.HotelStay.RoomList;
                    }
                }
            }
        }

        private void CheckOutBtn_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
