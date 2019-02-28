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
using System.Net.Http;
using System.IO;
using System.Web;

namespace Libri_Samir_Lolli
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string mycontent;
        static ListBox lst;
        string[][] result;
        public MainWindow()
        {
            InitializeComponent();
            lst = lst_primary;
        }


        //ritorna i libri in base alla sezione e al reparto specificati
        async private void btn_richiesta1_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=1&rep=" + txt_REP.Text.ToLower()+"&sez="+txt_SEZ.Text.ToLower();
            string task = await(Richiesta(url));
            SplitReplace(task);
            for (int j = 0; j < result[0].Length; j++)
            {

                lst.Items.Add("Titolo: " + result[0][j]);

            }
        }

        //restituisce tutti i libri ordinati in base allo sconto applicato o che sarà applicato
        async private void btn_richiesta2_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=2";
            //Richiesta(url);
            string task = await(Richiesta(url));
            SplitReplace(task);
            for (int j = 0; j < result[0].Length; j++)
            {
               
                    lst.Items.Add("Titolo: " + result[0][j] + " Sconto: " + result[1][j]);
                
            }
        }

        //restituisce libri compresi tra due date
        async private void btn_richiesta3_Click_1(object sender, RoutedEventArgs e)
        {
            if (Date1.SelectedDate != null)
            {
                if (Date2.SelectedDate != null)
                {
                    string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=3&d1=" + Date1.DisplayDate.ToShortDateString() + "&d2=" + Date2.DisplayDate.ToShortDateString();
                    string task = await(Richiesta(url));
                    SplitReplace(task);
                    for (int j = 0; j < result[0].Length; j++)
                    {

                        lst.Items.Add("Titolo: " + result[0][j]);

                    }
                }
                else
                    MessageBox.Show("Select date", "Errore");
            }
            else
                MessageBox.Show("Select date", "Errore");

        }

        // bottone che si occupa di elencare i libri comprati dagli utenti
        async private void btn_richiesta_4_Click(object sender, RoutedEventArgs e)
        {
            int n;

            if (int.TryParse(txt_ID.Text, out n))
            {
                if (n <= 5 && n > 0)
                {
                    string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=4&id=" + txt_ID.Text;
                    string task = await(Richiesta(url));
                    SplitReplace(task);
                    
                        lst.Items.Add("Email: " + result[0][0]);
                        for (int j = 0; j < result[1].Length; j++)
                        {
                            lst.Items.Add("Titolo: " + result[1][j] + " Quantità: " + result[2][j]);

                        }                                                           
                }
                else
                    MessageBox.Show("Index should be lower or equal than 5", "Errore");
            }
            else
                MessageBox.Show("The input should be a number between 1 and 5", "Errore");
        }

        async static Task<string> Richiesta(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        
                        mycontent = await content.ReadAsStringAsync();
                        return mycontent;
                        /*
                        mycontent = mycontent.Replace("],", "");
                        mycontent = mycontent.Replace("]", "");
                        mycontent = mycontent.Replace(",[", "[");
                                                                     
                        lst.Items.Clear();
                        if (mycontent != "[")
                        {
                            //separa il risultato in array
                            string[] s = mycontent.Split('[');
                            int count = 0;
                            string[][] a;
                            //Conta il numero di array
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (s[i] != "")
                                {

                                    count++;
                                }
                            }
                            a = new string[count][];
                            count = 0;
                            //Separa gli array nei singoli elementi
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (s[i] != "")
                                {
                                    a[count] = s[i].Split(',');
                                    count++;
                                }
                            }
                            //visualizza i risultati
                            for (int j = 0; j < a[0].Length; j++)
                            {
                                for (int i = 0; i < a.GetLength(0); i++)
                                {
                                    lst.Items.Add(a[i][j]);
                                }
                            }
                        }*/
                    }
                }
            }
        }

        void SplitReplace(string stringa)
        {
            stringa = stringa.Replace("],", "");
            stringa = stringa.Replace("]", "");
            stringa = stringa.Replace(",[", "[");

            if (stringa != "[")
            {
                //separa il risultato in array
                string[] s = stringa.Split('[');
                int count = 0;
                
                //Conta il numero di array
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != "")
                    {

                        count++;
                    }
                }
                result = new string[count][];
                count = 0;
                //Separa gli array nei singoli elementi
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != "")
                    {
                        result[count] = s[i].Split(',');
                        count++;
                    }
                }
<<<<<<< HEAD
            }}
=======
            }
            else
            {
                result = new string[1][];
                result[0] = new string[1];
                result[0][0] = "";
            }
        }
        
        static void ListClear()
        {
            lst.Items.Clear();
            lst.Items.Add("RICHIESTA 1:");
            lst.Items.Add("RICHIESTA 2: Elenco dei libri scontati presenti in tutti i reparti in ordine crescente per sconto (da quelli meno a quelli più scontati)");
            lst.Items.Add("RICHIESTA 3: Elenco libri archiviati all'interno di un periodo definito da due date inserite in input");
        }
>>>>>>> commenti client
    }
}
