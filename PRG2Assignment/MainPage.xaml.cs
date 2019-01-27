using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
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
            HotelRoom room1 = new StandardRoom("Standard", "101", "Single", 90, false, 2);
            HotelRoom room2 = new StandardRoom("Standard", "102", "Single", 90, true, 2);
            HotelRoom room3 = new StandardRoom("Standard", "201", "Twin", 110, true, 4);
            HotelRoom room4 = new StandardRoom("Standard", "202", "Twin", 110, false, 4);
            HotelRoom room5 = new StandardRoom("Standard", "203", "Twin", 110, true, 4);
            HotelRoom room6 = new StandardRoom("Standard", "301", "Triple", 120, true, 6);
            HotelRoom room7 = new StandardRoom("Standard", "302", "Triple", 120, false, 6);
            //deluxe room objects
            HotelRoom room8 = new DeluxeRoom("Deluxe", "204", "Twin", 140, true, 4);
            HotelRoom room9 = new DeluxeRoom("Deluxe", "205", "Twin", 140, true, 4);
            HotelRoom room10 = new DeluxeRoom("Deluxe", "303", "Triple", 210, false, 6);
            HotelRoom room11 = new DeluxeRoom("Deluxe", "304", "Triple", 210, true, 6);
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
            Guest g1 = new Guest("Amelia", "S1234567A", s1, m1, true);
            //second guest information
            Stay s2 = new Stay(Convert.ToDateTime("25-Jan-2019"), Convert.ToDateTime("31-Jan-2019"));
            List<HotelRoom> roomList2 = s2.RoomList;
            s2.AddRoom(room7);
            Membership m2 = new Membership("Ordinary", 0);
            Guest g2 = new Guest("Bob", "G1234567A", s2, m2, true);
            //third guest information
            Stay s3 = new Stay(Convert.ToDateTime("01-Feb-2019"), Convert.ToDateTime("06-Feb-2019"));
            List<HotelRoom> roomList3 = s3.RoomList;
            s3.AddRoom(room4);
            Membership m3 = new Membership("Silver", 190);
            Guest g3 = new Guest("Cody", "G234567A", s3, m3, true);
            //fourth guest information
            Stay s4 = new Stay(Convert.ToDateTime("28-Jan-2019"), Convert.ToDateTime("10-Feb-2019"));
            List<HotelRoom> roomList4 = s4.RoomList;
            s4.AddRoom(room10);
            Membership m4 = new Membership("Gold", 10);
            Guest g4 = new Guest("Edda", "S3456789A", s4, m4, true);

            //adding guest objects into guestList
            guestList.Add(g1);
            guestList.Add(g2);
            guestList.Add(g3);
            guestList.Add(g4);


            StandardRoom srroom101 = (StandardRoom)room1;
            srroom101.RequireWifi = true;
            srroom101.RequireBreakfast = true;

            StandardRoom srroom302 = (StandardRoom)room7;
            srroom101.RequireBreakfast = true;

            StandardRoom srroom202 = (StandardRoom)room4;
            srroom202.RequireBreakfast = true;

            DeluxeRoom srroom303 = (DeluxeRoom)room10;
            srroom303.AdditionalBed = true;

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
                if (guestTxt.Text != g.Name)
                {
                    check = true;
                }
            }
            if (passportTxt.Text == "" || passportTxt.Text == " ")
            {
                statusUpdateText.Text = "Please enter a Passport Number";
            }
            else
            {
                if (check == true) //if guest.text cannot be found in the guestList.Name
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
                        if (r.RoomType == "Standard")
                        {
                            HotelRoom h = new StandardRoom(r.RoomType, r.RoomNumber, r.BedConfiguration, r.DailyRate, r.IsAvail, r.NoOfOccupants);
                            int noOfGuest = (Convert.ToInt32(noOfAdultTxt.Text) / 2) + (Convert.ToInt32(noOfChildrentxt.Text));
                            if (noOfGuest <= r.NoOfOccupants)
                            { 
                                s.AddRoom(h);
                            }
                        else
                        {
                            statusUpdateText.Text = "Add an Additional Bed\nOr Change Your Selected Choice";
                        }
                        }
                        else if (r.RoomType == "Deluxe")
                        {
                            HotelRoom h = new DeluxeRoom(r.RoomType, r.RoomNumber, r.BedConfiguration, r.DailyRate, r.IsAvail, r.NoOfOccupants);
                            int noOfGuest = (Convert.ToInt32(noOfAdultTxt.Text) / 2) + (Convert.ToInt32(noOfChildrentxt.Text));
                            if (noOfGuest < r.NoOfOccupants)
                            {
                                s.AddRoom(h);
                            }
                            else
                            {
                                statusUpdateText.Text = "Add an Additional Bed\nOr Change Your Selected Choice";
                            }
                        }
                        List<HotelRoom> roomList = s.RoomList;
                        string guestStayDetails = "";
                        foreach (HotelRoom room in roomList)
                        {
                            guestStayDetails += room.ToString() + guest.ToString();
                        }
                        roomsBookedTxt.Text = guestStayDetails;
                        roomscheckedin += r.RoomNumber.ToString() + " ";
                    }
                    tempRoomList.Clear(); //clears the selected room list
                    RefreshList(); //refreshes room list [2.1.5]
                    statusUpdateText.Text = "Status update: Rooms" + " " + roomscheckedin + "checked in successfully.";
                    roomscheckedin = "";
                }
                else if (check == false) //existing guest code goes here [2.1.6]
                {
                    for (var i = 0; i < tempRoomList.Count; i++) //loops thru the tempRoomList
                    {
                        HotelRoom r = tempRoomList[i]; 
                        r.IsAvail = false; //sets every room isAvail to false
                        r.NoOfOccupants = Convert.ToInt32(noOfAdultTxt.Text) + Convert.ToInt32(noOfChildrentxt.Text); //calculates total number of occupants
                        if (r.RoomType == "Standard") //checks if the room is standard. if it is, create a standard room object 
                        {
                            HotelRoom h = new StandardRoom(r.RoomType, r.RoomNumber, r.BedConfiguration, r.DailyRate, r.IsAvail, r.NoOfOccupants);
                            foreach (Guest g in guestList)
                            {
                                if (g.Name == guestTxt.Text)
                                {
                                    int noOfGuest = (Convert.ToInt32(noOfAdultTxt.Text) / 2) + (Convert.ToInt32(noOfChildrentxt.Text));
                                    if (noOfGuest <= r.NoOfOccupants)
                                    {
                                        g.HotelStay.AddRoom(h); //add it into the guest.RoomList
                                    }
                                    else
                                    {
                                        statusUpdateText.Text = "Add an Additional Bed\nOr Change Your Selected Choice";
                                    }
                                }
                            }
                        }
                        else if (r.RoomType == "Deluxe") //checks if the room is deluxe. if it is, create a deluxe room object
                        {
                            HotelRoom h = new DeluxeRoom(r.RoomType, r.RoomNumber, r.BedConfiguration, r.DailyRate, r.IsAvail, r.NoOfOccupants);
                            foreach (Guest g in guestList)
                            {
                                if (g.Name == guestTxt.Text)
                                {
                                    int noOfGuest = (Convert.ToInt32(noOfAdultTxt.Text) / 2) + (Convert.ToInt32(noOfChildrentxt.Text));
                                    if (noOfGuest < r.NoOfOccupants)
                                    {
                                        g.HotelStay.AddRoom(h);  //add it into the guest.RoomList
                                    }
                                    else
                                    {
                                        statusUpdateText.Text = "Add an Additional Bed\nOr Change Your Selected Choice";
                                    }
                                }
                            }
                        }
                    }
                }
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
            if (noOfAdultTxt.Text != "" || noOfChildrentxt.Text != "")
            {
                availableTxt.Text = "Available rooms:";
                RefreshList(); //loads available rooms into the availableroom listview [2.1.2]
            }
            else
            {
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            availableTxt.Text = "Available rooms:";          
            invoiceText.Text = "";
            string result = "";
            lvAvailableRooms.ItemsSource = null;
            lvAvailableRooms.Items.Clear();
            string checkwifi = "";
            string checkbreakfast = "";
            if (guestTxt.Text != "" || guestTxt.Text != " " || passportTxt.Text != "" || passportTxt.Text != " ")
            {
                foreach (Guest guest in guestList)
                {
                    if (guest.Name == guestTxt.Text || guest.PPNumber == passportTxt.Text)
                    {
                        availableTxt.Text = "Rooms Booked by: " + guest.Name + " (" + guest.PPNumber + ")\n" + "Check In: " + guest.HotelStay.CheckInDate + " Check Out: " + guest.HotelStay.CheckOutDate;
                        memberStatusText.Text = "Member Status: " + guest.Membership.Status;
                        pointsAvailableText.Text = "Points Available: " + guest.Membership.Points;
                        foreach (HotelRoom h in guest.HotelStay.RoomList)
                        {
                            lvAvailableRooms.Items.Add(h);
                            if (h.RoomType == "Standard")
                            {
                                StandardRoom sr = (StandardRoom)h;
                                if (addWifiCheckBox.IsChecked == true)
                                {
                                    checkwifi = "Yes";
                                }

                                if (addBreakfastCheckBox.IsChecked == true)
                                {
                                    checkbreakfast = "Yes";
                                }
                                double days = (guest.HotelStay.CheckOutDate - guest.HotelStay.CheckInDate).TotalDays;
                                double totalcost = guest.HotelStay.CalculateTotal();
                                double roomNumber = Convert.ToDouble(h.RoomNumber);
                                result += "You are staying for " + days + " nights in room number " + roomNumber + "\nWifi addon: " + checkwifi + "\tBreakfast addon: " + checkbreakfast + "\tTotal amount is $" + totalcost + "\n";
                                //noOfAdultTxt.Text = sr.RequireWifi.ToString();
                            }

                            else if (h.RoomType == "Deluxe")
                            {
                                DeluxeRoom dr = (DeluxeRoom)h;
                                bool checkbed = dr.AdditionalBed;
                                double days = (guest.HotelStay.CheckOutDate - guest.HotelStay.CheckInDate).TotalDays;
                                double totalcost = guest.HotelStay.CalculateTotal();
                                double roomNumber = Convert.ToDouble(h.RoomNumber);
                                result += "You are staying for " + days + " nights in room number " + roomNumber + "\nAdditional Bed " + checkbed + "\tTotal amount is $" + totalcost + "\n";
                            }
                        }                                        
                    }               
                    if (guestTxt.Text != "" && passportTxt.Text != "")
                    {
                        availableTxt.Text = "Please search by one method only.";
                    }

                    if ((availableTxt.Text == "Available rooms:") && (guestTxt.Text != "" || passportTxt.Text != ""))
                    {
                        availableTxt.Text = "No rooms under that name or passport number found.";
                    }
                }
                invoiceText.Text += result;
            }
        }

        private void CheckOutBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Guest g in guestList)
            {
                if (g.Name == guestTxt.Text || g.PPNumber == passportTxt.Text)
                {
                    //g.HotelStay.RoomList.Clear();
                    for (int i = 0; i < g.HotelStay.RoomList.Count; i++)
                    {
                        if (g.HotelStay.RoomList[i].RoomType == "Standard")
                        {
                            StandardRoom sr = (StandardRoom)g.HotelStay.RoomList[i];
                            sr.RequireWifi = false;
                            sr.RequireBreakfast = false;
                            sr.IsAvail = true;
                            availableRooms.Add(sr);
                            double totalamount = g.HotelStay.CalculateTotal();
                            double points = totalamount / 10;
                            invoiceText.Text = "Thank you for staying with us!\n You have earned " + Math.Round(points,1) + " points\nIt will be credited to your account.";
                            statusUpdateText.Text = "Check-Out Successful!";
                            double totalpoints = g.Membership.Points + points;
                            if (g.Membership.Status == "Ordinary")
                            {
                                if (totalpoints >= 200)
                                {
                                    Membership m = new Membership("Gold", Convert.ToInt32(totalpoints));
                                    g.Membership = m;
                                }
                                else if (totalpoints >= 100)
                                {
                                    Membership m = new Membership("Silver", Convert.ToInt32(totalpoints));
                                    g.Membership = m;
                                }
                            }
                            else if (g.Membership.Status == "Silver")
                            {
                                if (totalpoints >= 200)
                                {
                                    Membership m = new Membership("Gold", Convert.ToInt32(totalpoints));
                                    g.Membership = m;
                                }
                            }
                            memberStatusText.Text = "Member Status: " + g.Membership.Status;
                            pointsAvailableText.Text = "Available Points: " + Convert.ToString(totalpoints);
                            g.Membership.EarnPoints(totalamount);
                            g.HotelStay.RoomList.Remove(sr);
                        }

                        else if (g.HotelStay.RoomList[i].RoomType == "Deluxe")
                        {
                            DeluxeRoom dr = (DeluxeRoom)g.HotelStay.RoomList[i];
                            dr.AdditionalBed = false;
                            dr.IsAvail = true;
                            availableRooms.Add(dr);
                            double totalamount = g.HotelStay.CalculateTotal();
                            double points = totalamount / 10;
                            invoiceText.Text = "Thank you for staying with us!\n You have earned " + Math.Round(points, 1) + " points\nIt will be credited to your account.";
                            statusUpdateText.Text = "Check-Out Successful!";
                            double totalpoints = g.Membership.Points + points;
                            if (g.Membership.Status == "Ordinary")
                            {
                                if (totalpoints >= 200)
                                {
                                    Membership m = new Membership("Gold", Convert.ToInt32(totalpoints));
                                    g.Membership = m;
                                }
                                else if (totalpoints >= 100)
                                {
                                    Membership m = new Membership("Silver", Convert.ToInt32(totalpoints));
                                    g.Membership = m;
                                }
                            }
                            else if (g.Membership.Status == "Silver")
                            {
                                if (totalpoints >= 200)
                                {
                                    Membership m = new Membership("Gold", Convert.ToInt32(totalpoints));
                                    g.Membership = m;
                                }
                            }
                            memberStatusText.Text = "Member Status: " + g.Membership.Status;
                            pointsAvailableText.Text = "Available Points: " + Convert.ToString(totalpoints);
                            g.Membership.EarnPoints(totalamount);
                            g.HotelStay.RoomList.Remove(dr);
                        }
                    }
                    availableTxt.Text = "Available rooms:";
                    RefreshList();
                }
            }
        }

        private void extendStayBtn_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            foreach (Guest guest in guestList)
            {
                if (guest.Name == guestTxt.Text)
                {
                    guest.HotelStay.CheckOutDate = guest.HotelStay.CheckOutDate.AddDays(1); //add one day to the checkoutDate [3.3]
                    availableTxt.Text = "Rooms Booked by: " + guest.Name + " (" + guest.PPNumber + ")\n" + "Check In: " + guest.HotelStay.CheckInDate + " Check Out: " + guest.HotelStay.CheckOutDate;
                    foreach(HotelRoom room in guest.HotelStay.RoomList)
                    {
                        if (room.RoomType == "Standard")
                        {
                            StandardRoom sr = (StandardRoom)room;
                            bool checkwifi = sr.RequireWifi;
                            bool checkbreakfast = sr.RequireBreakfast;
                            double days = (guest.HotelStay.CheckOutDate - guest.HotelStay.CheckInDate).TotalDays;
                            Debug.WriteLine(days);
                            double totalcost = guest.HotelStay.CalculateTotal();
                            double roomNumber = Convert.ToInt32(room.RoomNumber);
                            result = "You are staying for " + days + " nights in room number " + roomNumber + "\nWifi addon: " + checkwifi + "\tBreakfast addon: " + checkbreakfast + "\tTotal amount is $" + totalcost + "\n";
                        }
                        else if (room.RoomType == "Deluxe")
                        {
                            DeluxeRoom dr = (DeluxeRoom)room;
                            bool checkbed = dr.AdditionalBed;
                            double days = (guest.HotelStay.CheckOutDate - guest.HotelStay.CheckInDate).TotalDays;
                            double totalcost = guest.HotelStay.CalculateTotal();
                            double roomNumber = Convert.ToInt32(room.RoomNumber);
                            result = "You are staying for " + days + " nights in room number " + roomNumber + "\nAdditional Bed " + checkbed + "\nTotal amount is $" + totalcost + "\n";
                        }
                        invoiceText.Text = result;
                    }
                }
            }
        }
    }
}
