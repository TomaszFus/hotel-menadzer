using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

        klienci Klient = new klienci();
        pokoje pokoj = new pokoje();
        rezerwacje rezerwacja = new rezerwacje();


        DateTime poczatek;
        DateTime koniec;
        TimeSpan lDni; //różnica miedzy dwoma datami
        public string l_osob { get; set; }
        //string nr_p_str;
        //int nr_p_int;
        double _koszt;

        



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
                    if(String.IsNullOrWhiteSpace(TxB_LiczbaDni.Text))
                    {
                        TxB_Pokoj.IsReadOnly = true;
                    }
                    else
                    {
                        TxB_Pokoj.IsReadOnly = false;
                    }
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
            PokojeGrid();
            RezerwacjeGrid();
            KlienciGrid();
            using (HotelEntities db=new HotelEntities())
            {
                // DataGridP.ItemsSource = db.pokoje.ToList(); //datagrid w zakładce pokoje
                // DataGridW.ItemsSource = db.rezerwacje.ToList(); //data grig w zakaladce wynajecia
                
                //combobox liczba osob
                //ComBox_LiczbaOs.ItemsSource = db.pokoje.Where(s => s.status == "wolny").GroupBy(s=>s.liczba_osob).ToList(); //zmienić
            
            }
            
            
        }
        //!!!!!!!!!!!!!!!!!!!!!!
        //private void ComBox_LiczbaOs_SelectionChanged_1(object sender, SelectionChangedEventArgs e) //zmienić
        //{
        //    l_osob = this.ComBox_LiczbaOs.SelectedValue.ToString();
        //    using (HotelEntities db=new HotelEntities())
        //    {
        //        //combobox pokoj
        //        ComBox_Pokoj.ItemsSource = db.pokoje.Where(s => s.status == "wolny").Where(o => o.liczba_osob == l_osob).ToList();
                
                
                

        //    }
        //}

        //private void ComBox_Pokoj_SelectionChanged(object sender, SelectionChangedEventArgs e)  //zmienić
        //{
        //    nr_p_str = this.ComBox_Pokoj.SelectedValue.ToString();
        //    nr_p_int = int.Parse(nr_p_str);

        //    using (HotelEntities db=new HotelEntities())
        //    {
                
        //        pokoj = db.pokoje.FirstOrDefault(p => p.nr_pokoju == nr_p_int);
        //        string lDni_str = lDni.Days.ToString();
        //        _koszt = pokoj.cena*int.Parse(lDni_str);
        //        TxB_Koszt.Text = _koszt.ToString();

        //    }

        //}

        private void RB_Gotowka_Checked(object sender, RoutedEventArgs e)
        {
            rezerwacja.rodzaj_platnosci = "gotowka";
        }

        private void RB_Karta_Checked(object sender, RoutedEventArgs e)
        {
            rezerwacja.rodzaj_platnosci = "karta";
        }

        private void Bt_Rejestracja_Click(object sender, RoutedEventArgs e)
        {
            Rejestracja();
            Wyczysc();
            PokojeGrid();
            RezerwacjeGrid();
            KlienciGrid();


        }

        private void Bt_Koszt_Click(object sender, RoutedEventArgs e)
        {
            Koszt();
        }

        private void Bt_WyczyscRejKl_Click(object sender, RoutedEventArgs e)
        {
            Wyczysc();
        }


        private void Bt_NowyPokoj_Click(object sender, RoutedEventArgs e)
        {
            NowyPokoj();
            PokojeGrid();
        }



        public void Rejestracja()
        {
            //przypisanie danych klienta
            if (String.IsNullOrWhiteSpace(TxB_ImieKl.Text))
            {
                Lb_ImieBlad.Content = "Podaj imie";
                Lb_ImieBlad.Visibility = Visibility.Visible;
            }
            else
            {
                Lb_ImieBlad.Visibility = Visibility.Collapsed;
                Klient.imie = TxB_ImieKl.Text;
            }

            if (String.IsNullOrWhiteSpace(TxB_NazwiskoKl.Text))
            {
                Lb_NazwiskoBlad.Content = "Podaj nazwisko";
                Lb_NazwiskoBlad.Visibility = Visibility.Visible;
            }
            else
            {
                Lb_NazwiskoBlad.Visibility = Visibility.Collapsed;
                Klient.nazwisko = TxB_NazwiskoKl.Text;
            }

            if (String.IsNullOrWhiteSpace(TxB_TelefonKl.Text))
            {
                Lb_TelBlad.Content = "Podaj telefon";
                Lb_TelBlad.Visibility = Visibility.Visible;
            }
            else
            {
                
                try
                {
                    Lb_TelBlad.Visibility = Visibility.Collapsed;
                    Klient.telefon = int.Parse(TxB_TelefonKl.Text);
                }
                catch (Exception ex)
                {
                    Lb_TelBlad.Content=ex.Message;
                    Lb_TelBlad.Visibility = Visibility.Visible;
                    //throw;
                }
                
            }


            if (String.IsNullOrWhiteSpace(TxB_TelefonKl.Text))
            {
                Lb_NrDowBlad.Content = "Podaj nr dowodu";
                Lb_NrDowBlad.Visibility = Visibility.Visible;
            }
            else
            {

                try
                {
                    Lb_NrDowBlad.Visibility = Visibility.Collapsed;
                    Klient.nr_dowodu = int.Parse(TxB_NrDowOsKl.Text);
                }
                catch (Exception ex)
                {
                    Lb_NrDowBlad.Content = ex.Message;
                    Lb_NrDowBlad.Visibility = Visibility.Visible;
                    //throw;
                }

            }






            


            //przypisanie danych rezerwacji
            rezerwacja.od = poczatek;
            rezerwacja.@do = koniec;
            rezerwacja.nr_pokoju = pokoj.nr_pokoju;
            rezerwacja.id_klienta = Klient.id_klienta;
            //rodzaj platonosci ustawiony w obsludze radio
            rezerwacja.koszt = _koszt;
            pokoj.status = "zajety";


            try
            {
                using (HotelEntities db = new HotelEntities())
                {
                    db.klienci.Add(Klient);
                    db.rezerwacje.Add(rezerwacja);
                    db.pokoje.AddOrUpdate(pokoj);
                    db.SaveChanges();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }

            

        }

        
        
        public void Koszt()
        {
            string nr_p_str = TxB_Pokoj.Text;
            int nr_p_int = int.Parse(nr_p_str);
            using (HotelEntities db = new HotelEntities())
            {
                try
                { 
                    pokoj = db.pokoje.FirstOrDefault(p => p.nr_pokoju == nr_p_int);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nie ma takiego pokoju");
                    //throw;
                }

               
            }
            if (pokoj==null)
            {
                MessageBox.Show("Nie ma takiego pokoju");
            }
            else
            {
                if (pokoj.status=="zajety")
                {
                    MessageBox.Show("Pokój zajęty");
                }
                else
                {
                    string lDni_str = lDni.Days.ToString();
                    _koszt = pokoj.cena * int.Parse(lDni_str);
                    TxB_Koszt.Text = _koszt.ToString();
                }
            }
                
                

            
        }
        
        
        public void Wyczysc()
        {
            TxB_ImieKl.Text = TxB_NazwiskoKl.Text = TxB_TelefonKl.Text = TxB_NrDowOsKl.Text = TxB_DzienOd.Text = TxB_DzienDo.Text = TxB_LiczbaDni.Text = TxB_Koszt.Text = TxB_Pokoj.Text = null;
            RB_Gotowka.IsChecked = RB_Karta.IsChecked = false;
            //ComBox_LiczbaOs.SelectedIndex = 0;
            //ComBox_Pokoj.SelectedIndex = 0;
            //ComBox_LiczbaOs.SelectedItem = null;


        }


        private void Bt_ZakonczPobyt_Click(object sender, RoutedEventArgs e)
        {
            ZakonczPobyt();
            RezerwacjeGrid();
            KlienciGrid();
        }


        public void PokojeGrid()
        {
            using (HotelEntities db = new HotelEntities())
            {
                DataGridP.ItemsSource = db.pokoje.ToList(); //datagrid w zakładce pokoje
                
            }
        }

        public void RezerwacjeGrid()
        {
            using (HotelEntities db = new HotelEntities())
            {
               
                DataGridW.ItemsSource = db.rezerwacje.ToList(); //data grig w zakaladce wynajecia
                

            }
        }


        public void KlienciGrid()
        {
            using (HotelEntities db = new HotelEntities())
            {

                DataGridK.ItemsSource = db.klienci.ToList(); //data grig w zakaladce klienci


            }
        }

        public void NowyPokoj()
        {
            bool LOsobOK = false;
            bool CenaOK = false;
            if (string.IsNullOrEmpty(TxB_NP_LO.Text))
            {
                Lb_NP_error2.Content = "Podaj wartość";
                Lb_NP_error2.Visibility = Visibility.Visible;
                LOsobOK = false;
            }
            else
            {
                try
                {
                    Lb_NP_error2.Visibility = Visibility.Collapsed;
                    pokoj.liczba_osob = TxB_NP_LO.Text;
                    LOsobOK = true;
                }
                catch (Exception ex)
                {
                    Lb_NP_error2.Content = ex.Message;
                    Lb_NP_error2.Visibility = Visibility.Visible;
                    LOsobOK = false;
                    //throw;
                }
            }


            if (string.IsNullOrEmpty(TxB_NP_Cena.Text))
            {
                Lb_NP_error3.Content = "Podaj wartość";
                Lb_NP_error3.Visibility = Visibility.Visible;
                CenaOK = false;
            }
            else
            {
                try
                {
                    Lb_NP_error3.Visibility = Visibility.Collapsed;
                    pokoj.cena = double.Parse(TxB_NP_Cena.Text);
                    CenaOK = true;
                }
                catch (Exception ex)
                {
                    Lb_NP_error3.Content = ex.Message;
                    Lb_NP_error3.Visibility = Visibility.Visible;
                    CenaOK = false;
                    //throw;
                }
            }

            pokoj.status = "wolny";

            if (LOsobOK && CenaOK == true)
            {
                try
                {
                    using (HotelEntities db = new HotelEntities())
                    {
                        db.pokoje.Add(pokoj);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.Message);
                    throw;
                }
            }
        }

        public void ZakonczPobyt()
        {
            rezerwacja = (rezerwacje)DataGridW.SelectedItem;
            if (rezerwacja != null)
            {
                if (MessageBox.Show("Zakonczyć pobyt?","Potwierdzenie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    using (HotelEntities db=new HotelEntities())
                    {
                        Klient = db.klienci.Where(i => i.id_klienta == rezerwacja.id_klienta).First();
                        pokoj = db.pokoje.Where(i => i.nr_pokoju == rezerwacja.nr_pokoju).First();
                        if (Klient != null)
                        {
                            var rezKoniec = (from item in db.rezerwacje where item.id_rezerwacji == rezerwacja.id_rezerwacji select item).First();
                            if(rezKoniec!=null)
                            {
                                db.rezerwacje.Remove(rezKoniec);
                            }
                            db.klienci.Remove(Klient);
                            pokoj.status = "wolny";
                        }
                        db.SaveChanges();
                    }
                    
                }
                else
                {
                    // Do not close the window  
                }
            }
            
        }

        
    }
}
