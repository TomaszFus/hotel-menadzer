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

        pracownicy O_Pracownik = new pracownicy(); //obiekt klasy pracownicy

        private void Bt_NPr_Click(object sender, RoutedEventArgs e)
        {
            NowyPracownik Win_NPr = new NowyPracownik();
            Win_NPr.ShowDialog();       //ShowDialog blokuje pierwsze okno
        }

        

        private void Bt_Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            
            

            using (HotelEntities db=new HotelEntities())
            {
                if(db.Database.SqlQuery<string>("Select login from pracownicy where login='" + TxB_Login.Text + "'").FirstOrDefault()==TxB_Login.Text)
                {
                    LbLoginBlad.Visibility = Visibility.Collapsed;
                    if(db.Database.SqlQuery<string>("Select haslo from pracownicy where login='" + TxB_Login.Text + "'").FirstOrDefault() == PassB_Haslo.Password)
                    {
                        LbHaloBlad.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Zalogowano");
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
                else
                {
                    LbLoginBlad.Content = "Podany login jest błędny lub nie istnieje";
                    LbLoginBlad.Visibility = Visibility.Visible;
                }
                //db.Database.SqlQuery<string>("Select login from pracownicy where login='"+TxB_Login.Text+"'").FirstOrDefault();
                





                //var dataset = from b in db.pracownicy where b.login.StartsWith("a") select b;

                //var dane = db.pracownicy.Where(b => b.login == "adab").FirstOrDefault();
               // TxB_Login.Text = dane.ToString();
                
            }
            //MessageBox.Show(pracownik_test);
        }
    }
}
