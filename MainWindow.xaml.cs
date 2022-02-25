using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework
{
    /// This code generates a list of couriers and parcels
    /// Interaction logic for MainWindow.xaml
    /// Alberto Perez
    public partial class MainWindow : Window
    {
        //list of all couriers that will be created
        List<Courier> courier_list = new List<Courier>();
        //keep track of id of each created courier
        int num_id = 0;
        //path of the Log.txt file
        string path = @"C:\Users\super\Desktop\coursework\Coursework_solution\Log.txt";

        public MainWindow()
        {
            InitializeComponent();
            //Deletes file if already created
            File.Delete(path);
            TextWriter file = new StreamWriter(path);
            file.Close();
        }


        //This file checks if file exists and either creates the file or appends to it the "line"
        public void addLine(string line)
        {
            //here it creates the file
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            //here it appends to the file
            else if (File.Exists(path))
            {
                using (var file = new StreamWriter(path, true))
                {
                    file.WriteLine(DateTime.Now.ToString("HH:mm") + " " + DateTime.Now.ToString("MM/dd/yyyy") + line);
                }
            }
        }


        //This function checks if any postcode inputed is a valid postcode
        public bool checkPostcode(string postcode)
        {
            //Checks if postcode starts with "EH"
            if (postcode[0] == 'E' && postcode[1] == 'H')
            {
                int number = Int32.Parse(postcode.Remove(0, 2));
                if (number > 0)
                {
                    int count = 0;
                    //Checks if the area is between 1-23 as it is a Bike Courier
                    while (count < 23)
                    {
                        if (number == count)
                        {
                            return true;
                        }
                        count++;
                    }
                    return false;
                }
                else { return false; }
            }
            else { return false; }
        }


        //This function is used when inputing the areas each courier can go to
        public List<string> postcodeInput(string postcodes)
        {
            //Splits areas between spaces
            string[] words = postcodes.Split(' ');
            List<string> areas = new List<string>();
            //Checks if areas/postcodes are are valid
            foreach (string postcode in words)
            {
                if (checkPostcode(postcode))
                {
                    areas.Add(postcode);
                }
            }
            return areas;
        }


        //this function updates the grid adding the new couriers
        private void btn_Update_Grid_Click(object sender, RoutedEventArgs e)
        {
            ListBox_Courier.Items.Clear();
            int count = 0;
            //Loops through the 22 different postcodes
            while (count < 23)
            {
                string postcode = "EH"+count;
                string test = count.ToString();
                //loops though all of couriers to print them 1 by 1
                foreach (Courier i in courier_list)
                {
                    foreach (string y in i.postcodes){
                        if (y == postcode)
                        {
                            //Different output depending on Courier type
                            if (i is Walk_C)
                            {
                                ListBox_Courier.Items.Add("Walker number " + i.Id + " can go to: " + i.postcodes[0]);

                            }
                            else if (i is Bike_C)
                            {
                                ListBox_Courier.Items.Add("Bike number " + i.Id + " can go to: " + i.postcodes[0]);
                            }
                            //as van can have more than 1 postcode it loops thought and prints it several times
                            else if (i is Van_C)
                            {
                                ListBox_Courier.Items.Add("Van number " + i.Id + " can go to: " + y);
                            }
                            break;
                        }
                    }
                }
                count++;
            }
        }


        //Here there are 3 diferent buttons that create a diferent instance of  Courier
        private void btn_CreateW_Click(object sender, RoutedEventArgs e)
        {
            string walk_postcode = txt_addCourier.Text;
            List<String> postcodes = postcodeInput(walk_postcode);
            if (postcodes.Count == 1)
            {
                string number = postcodes[0].Remove(0, 2);
                //Checks if postcode is between 1-4 as Courier is walker 
                if (number == "1" || number == "2" || number == "3" || number == "4")
                {
                    //adds Courier type Walk to courier list
                    courier_list.Add(new Walk_C(num_id, postcodes));
                    //adds line to Log.txt
                    addLine(" New Courier Added – id=" + num_id + ", type=Walker");
                    num_id++;
                }
            }
            else MessageBox.Show("invalid postcode");
        }

        private void btn_CreateB_Click(object sender, RoutedEventArgs e)
        {
            string bike_postcode = txt_addCourier.Text;
            List<String> postcodes = postcodeInput(bike_postcode);
            if (postcodes.Count == 1)
            {
                //adds Courier type Cycle to courier list
                courier_list.Add(new Bike_C(num_id, postcodes));
                //adds to Log.txt
                addLine(" New Courier Added – id=" + num_id + ", type=Cycle");
                num_id++;
            }
            else MessageBox.Show("invalid postcode");
        }

        private void btn_CreateV_Click(object sender, RoutedEventArgs e)
        {
            string van_postcode = txt_addCourier.Text;
            List<String> postcodes = postcodeInput(van_postcode);
            if (postcodes.Count > 0)
            {
                //adds Courier type Van to courier list
                courier_list.Add(new Van_C(num_id, postcodes));
                //adds to Log.txt
                addLine(" New Courier Added – id=" + num_id + ", type=Van");
                num_id++;
            }
            else
            {
                MessageBox.Show("No valid postcodes");
            }
        }


        //This here creates a parcel and then assignes it to a courier
        private void btn_CreateP_Click(object sender, RoutedEventArgs e)
        {
            //Gets input from the user
            string text_name = txt_addParcelName.Text;
            string text_address = txt_addParcelAddress.Text;
            //Creates the parcel 
            Parcel p = new Parcel(text_address, text_name);
            if (checkPostcode(p.Postcode))
            {
                foreach (Courier c in courier_list)
                {
                    //Checks if list of parcels of each courier is full
                    if (c.parcel_list.Count < c.Num_parcels)
                    {
                        foreach (string y in c.postcodes)
                        {
                            //Fits the parcel into the first Courier with same postcode available
                            if (y == p.Postcode)
                            {
                                c.parcel_list.Add(p);
                                addLine(" New Parcel Added ("+text_address+", \""+text_name+"\")  Allocated to Courier "+c.Id);
                                MessageBox.Show(c.Id.ToString());
                                break;
                            }
                            
                        }
                        break;
                    }
                }
            }
            
        }


        //updates the parcel listbox showing all parcels of a courier
        private void btn_Update_Parcel_Click(object sender, RoutedEventArgs e)
        {
            ListBox_Parcel.Items.Clear();
            string text = txt_readCourierId.Text;
            foreach (Courier c in courier_list)
            {
                string id = c.Id.ToString();
                if (text == id)
                {
                    string test = c.Id.ToString();
                    foreach (Parcel p in c.parcel_list)
                    {
                        ListBox_Parcel.Items.Add("Courier number "+ c.Id +" has parcel: "+ p.Name + ", " + p.Postcode + " " + p.Delivery_id);
                    }
                    break;
                }
            }
            
        }
    }
}
