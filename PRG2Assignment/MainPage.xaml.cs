﻿using System;
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
        List<HotelRoom> roomList = new List<HotelRoom>();
        List<Guest> guestList = new List<Guest>();
        public MainPage()
        {
            this.InitializeComponent();
            InitHotelRooms();
            InitRooms();
            InitGuests();

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
        }

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
        }
        public void InitRooms() {
            List<HotelRoom> availableRooms = new List<HotelRoom>();
            foreach (HotelRoom room in roomList)
            {
                if (room.IsAvail == true)
                {
                    availableRooms.Add(room);
                }
            }
            lvAvailableRooms.ItemsSource = availableRooms;
        }

        private void checkInBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (HotelRoom room in lvAvailableRooms.Items)
            {
             
            }
        }

        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            double p = 0;
            HotelRoom r = (HotelRoom)lvAvailableRooms.SelectedItem;
            if (r.RoomType == "Standard")
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
            List<HotelRoom> tempRoomList = new List<HotelRoom>();
            tempRoomList.Add(r);
            lvRoomsSelected.ItemsSource = tempRoomList;
        }
    }
}
