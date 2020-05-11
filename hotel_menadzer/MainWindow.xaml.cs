using System;
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

namespace hotel_menadzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       


        public MainWindow()
        {
            InitializeComponent();
            //Bind();

        }

        DateTime poczatek;
        DateTime koniec;
        TimeSpan lDni; //różnica miedzy dwoma datami
        string l_osob;
        string nr_p_str;
        int nr_p_int;
        
        

        //combobox
        //public List <pokoje> prop_pokoje { get; set; }

        //private void Bind()
        //{
        //    HotelEntities db = new HotelEntities();
        //    var item = db.pokoje.ToList();
        //    prop_pokoje = item;
        //    DataContext = prop_pokoje;
        //}





        private void Kalendarz_1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get reference.
            var calendar = sender as Calendar;

            // ... See if a date is selected.
            if (calendar.SelectedDate.HasValue)
            {
                // ... Display SelectedDate in Title.
                DateTime date = calendar.SelectedDate.Value;
                TxB_DzienOd.Text = date.ToString();
                Kalendarz_1.Visibility = Visibility.Collapsed;
                poczatek = date;
            }
        }

        private void Kalendarz_2_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get reference.
            var calendar = sender as Calendar;

            // ... See if a date is selected.
            if (calendar.SelectedDate.HasValue)
            {
                // ... Display SelectedDate in Title.
                DateTime date = calendar.SelectedDate.Value;
                TxB_DzienDo.Text = date.ToString();
                Kalendarz_2.Visibility = Visibility.Collapsed;
                koniec = date;
                if (koniec<poczatek)
                {
                    Lb_DataDoBlad.Content = "Błędna data";
                    Lb_DataDoBlad.Visibility = Visibility.Visible;
                }
                else
                {
                    Lb_DataDoBlad.Visibility = Visibility.Collapsed;
                    lDni = koniec - poczatek;
                    TxB_LiczbaDni.Text = lDni.Days.ToString();  //róznica miedzy dwoma datami tylko dni
                }
                
            }
        }














        private void Bt_Kalendarz_Click(object sender, RoutedEventArgs e)
        {
            Kalendarz_1.Visibility = Visibility.Visible;
        }

        private void Bt_Kalendarz_2_Click(object sender, RoutedEventArgs e)
        {
            Kalendarz_2.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            using (HotelEntities db=new HotelEntities())
            {
                DataGridP.ItemsSource = db.pokoje.ToList(); //datagrid w zakładce pokoje

                //combobox liczba osob
                ComBox_LiczbaOs.ItemsSource = db.pokoje.Where(s => s.status == "wolny").ToList();
            
            }
            
            
        }
        //!!!!!!!!!!!!!!!!!!!!!!
        private void ComBox_LiczbaOs_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            l_osob = this.ComBox_LiczbaOs.SelectedValue.ToString();
            using (HotelEntities db=new HotelEntities())
            {
                //combobox pokoj
                ComBox_Pokoj.ItemsSource = db.pokoje.Where(s => s.status == "wolny").ToList();
                ComBox_Pokoj.ItemsSource = db.pokoje.Where(o => o.liczba_osob == l_osob).ToList();

            }
        }

        private void ComBox_Pokoj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nr_p_str = this.ComBox_Pokoj.SelectedValue.ToString();
            nr_p_int = int.Parse(nr_p_str);

            using (HotelEntities db=new HotelEntities())
            {
                pokoje pokoj = new pokoje();
                pokoj = db.pokoje.FirstOrDefault(p => p.nr_pokoju == nr_p_int);
                
                

            }

        }

        private void RB_Gotowka_Checked(object sender, RoutedEventArgs e)
        {
            texttest.Text = "gotówka";
        }

        private void RB_Karta_Checked(object sender, RoutedEventArgs e)
        {
            texttest.Text = "karta";
        }
    }
}
