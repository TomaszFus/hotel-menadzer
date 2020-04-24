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
            
        }




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
                DataGridP.ItemsSource = db.pokoje.ToList();
            }
        }
    }
}
