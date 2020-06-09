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
using System.Windows.Shapes;

namespace hotel_menadzer
{
    /// <summary>
    /// Interaction logic for NowyPracownik.xaml
    /// </summary>
    public partial class NowyPracownik : Window
    {
        pracownicy ObPracownik = new pracownicy(); //obeikt clasy pracownicy z DBModel

        /// <summary>
        /// Metoda czyszcząca pola tworzenia konta pracownika
        /// </summary>
        void Wyczysc()
        {
            TxB_ImiePr.Text = TxB_NazwiskoPr.Text=TxB_Login.Text=PassB_Kod.Password=PassB_Haslo.Password=PassB_PowHaslo.Password="";

        }


        public NowyPracownik()
        {
            InitializeComponent();
        }


        private string TmpHaslo;
        private bool SprImie = false;
        private bool SprNazw = false;
        private bool SprKPr = false;
        private bool SprHaslo = false;
        private bool SprHasloPow = false;

    //imie
        private void TxB_ImiePr_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TxB_ImiePr.Text))
            {
                LbImieBlad.Content = "Padaj imie";
                LbImieBlad.Visibility = Visibility.Visible;
                SprImie = false;

            }
            else
            {
                LbImieBlad.Visibility = Visibility.Collapsed;
                ObPracownik.imie = TxB_ImiePr.Text.Trim();  //trim usuwa biale znaki przed i po stringu
                SprImie = true;
            }
        }

       
    //nazwisko
        private void TxB_NazwiskoPr_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TxB_NazwiskoPr.Text))
            {   LbNazwiskoBlad.Content = "Podaj nazwisko";
                LbNazwiskoBlad.Visibility = Visibility.Visible;
                SprNazw = false;
            }
            else
            {
                LbNazwiskoBlad.Visibility = Visibility.Collapsed;
                ObPracownik.nazwisko = TxB_NazwiskoPr.Text.Trim();

                if(SprImie==true)
                {
                    ObPracownik.Login();
                    TxB_Login.Text = ObPracownik.login;
                }

                
                SprNazw = true;
            }
        }

            
        
        /// <summary>
        /// Zdarzenie obsługujące tworzenie konta pracownika
        /// </summary>
        
        private void Bt_Utworz_Click(object sender, RoutedEventArgs e)
        {

            

            //kod

            string _KPr = "gd84pz2";
            if (String.IsNullOrWhiteSpace(PassB_Kod.Password))
            {
                LbKodBlad.Content = "Podaj kod pracodawcy";
                LbKodBlad.Visibility = Visibility.Visible;
                SprKPr = false;
            }
            else if (PassB_Kod.Password != _KPr)
            {
                LbKodBlad.Content = "Błędny kod";
                LbKodBlad.Visibility = Visibility.Visible;
                SprKPr = false;
            }
            else
            {
                LbKodBlad.Visibility = Visibility.Collapsed;
                SprKPr = true;

            }



            //haslo

            


            if (String.IsNullOrWhiteSpace(PassB_Haslo.Password))
            {
                LbHasloBlad.Content = "Podaj hasło";
                LbHasloBlad.Visibility = Visibility.Visible;
                SprHaslo = false;
            }
            else if (PassB_Haslo.Password.Length < 8)
            {
                LbHasloBlad.Content = "Hasło musi mieć min 8 znaków";
                LbHasloBlad.Visibility = Visibility.Visible;
                SprHaslo = false;
            }
            else
            {
                TmpHaslo = PassB_Haslo.Password;
                LbHasloBlad.Visibility = Visibility.Collapsed;
                SprHaslo = true;
            }



            if (PassB_PowHaslo.Password != TmpHaslo)
            {
                LbHasloPowBlad.Content = "Hasło różni się od wpisanego wyżej";
                LbHasloPowBlad.Visibility = Visibility.Visible;
                SprHasloPow = false;
            }
            else
            {
                LbHasloPowBlad.Visibility = Visibility.Collapsed;
                ObPracownik.haslo = PassB_PowHaslo.Password;
                SprHasloPow = true;
            }


            if (SprImie == true && SprNazw == true && SprKPr == true && SprHaslo == true && SprHasloPow == true)
            {
                using(HotelEntities db=new HotelEntities())
                {
                    db.pracownicy.Add(ObPracownik);
                    db.SaveChanges();
                    MessageBox.Show("Poprawnie utworzono konto");
                }
                Wyczysc();
            }
            else
            {
                MessageBox.Show("Popraw dane");
            }
        }


        private void Bt_Anuluj_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
