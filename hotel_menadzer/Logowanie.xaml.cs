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
    /// Interaction logic for Logowanie.xaml
    /// </summary>
    public partial class Logowanie : Window
    {
        public Logowanie()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// Zdarzenie obsługujące stworzenie konta nowego pracownika
        /// </summary>
        private void Bt_NPr_Click(object sender, RoutedEventArgs e)
        {
            NowyPracownik Win_NPr = new NowyPracownik();
            Win_NPr.ShowDialog();       //ShowDialog blokuje pierwsze okno
        }





        /// <summary>
        /// Zdarzenie obsługujące logowanie pracownika do systemu
        /// </summary>

        private void Bt_Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            
            

            using (HotelEntities db=new HotelEntities())
            {

                pracownicy pracownik = new pracownicy();

                pracownik = db.pracownicy.FirstOrDefault(p => p.login == TxB_Login.Text);
                if (pracownik is null)
                {
                    LbLoginBlad.Content = "Błędny login";
                    LbLoginBlad.Visibility = Visibility.Visible;
                }
                else
                {
                    LbLoginBlad.Visibility = Visibility.Hidden;
                    if (pracownik.haslo==PassB_Haslo.Password)
                    {
                        LbHaloBlad.Visibility = Visibility.Hidden;
                        //MessageBox.Show("Zalogowano poprawnie");
                        
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        LbHaloBlad.Content = "Błędne hasło";
                        LbHaloBlad.Visibility = Visibility.Visible;
                    }
                }
                
               

               

                


                //if (db.database.sqlquery<string>("select login from pracownicy where login='" + txb_login.text + "'").firstordefault() == txb_login.text)
                //{
                //    lbloginblad.visibility = visibility.collapsed;
                //    if (db.database.sqlquery<string>("select haslo from pracownicy where login='" + txb_login.text + "'").firstordefault() == passb_haslo.password)
                //    {
                //        lbhaloblad.visibility = visibility.collapsed;
                //        messagebox.show("zalogowano");
                //        mainwindow mainwindow = new mainwindow();
                //        mainwindow.show();
                //        close();
                //    }
                //    else
                //    {
                //        lbhaloblad.content = "błędne hasło";
                //        lbhaloblad.visibility = visibility.visible;


                //    }
                //}
                //else
                //{
                //    lbloginblad.content = "podany login jest błędny lub nie istnieje";
                //    lbloginblad.visibility = visibility.visible;
                //}

                


            }
            
        }
    }
}
