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
        public MainPage()
        {
            this.InitializeComponent();
            InItHotelRooms();
        }
        public void InItHotelRooms()
        {
            //standard room objects
            HotelRoom room1 = new StandardRoom("Standard", "101", "Single", 90, true, 0);
            HotelRoom room2 = new StandardRoom("Standard", "102", "Single", 90, true, 0);
            HotelRoom room3 = new StandardRoom("Standard", "201", "Twin", 110, true, 0);
            HotelRoom room4 = new StandardRoom("Standard", "202", "Twin", 110, true, 0);
            HotelRoom room5 = new StandardRoom("Standard", "203", "Twin", 110, true, 0);
            HotelRoom room6 = new StandardRoom("Standard", "301", "Triple", 120, true, 0);
            HotelRoom room7 = new StandardRoom("Standard", "302", "Triple", 120, true, 0);
            //deluxe room objects
            HotelRoom room8 = new DeluxeRoom("Deluxe", "204", "Twin", 140, true, 0);
            HotelRoom room9 = new DeluxeRoom("Deluxe", "205", "Twin", 140, true, 0);
            HotelRoom room10 = new DeluxeRoom("Deluxe", "303", "Triple", 210, true, 0);
            HotelRoom room11 = new DeluxeRoom("Deluxe", "304", "Triple", 210, true, 0);

        }
    }
}
