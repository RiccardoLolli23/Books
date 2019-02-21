﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            lst = lst_primary;
        }

       

        private void btn_richiesta1_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=1&rep=" + txt_REP.Text.ToLower()+"&sez="+txt_SEZ.Text.ToLower();
            Richiesta(url);
        }

        private void btn_richiesta2_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=2";
            Richiesta(url);
        }

        private void btn_richiesta3_Click_1(object sender, RoutedEventArgs e)
        {
            if (Date1.SelectedDate != null)
            {
                if (Date2.SelectedDate != null)
                {
                    string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=3&d1=" + Date1.SelectedDate + "&d2=" + Date2.SelectedDate;
                    Richiesta(url);
                }
                else
                    MessageBox.Show("Select date", "Errore");
            }
            else
                MessageBox.Show("Select date", "Errore");

        }

        private void btn_richiesta_4_Click(object sender, RoutedEventArgs e)
        {
            int n;

            if (int.TryParse(txt_ID.Text, out n))
            {
                if (n <= 5 && n > 0)
                {
                    string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=4&id=" + txt_ID.Text;
                    Richiesta(url);
                }
                else
                    MessageBox.Show("Index should be lower or equal than 5", "Errore");
            }
            else
                MessageBox.Show("The input should be a number between 1 and 5", "Errore");
        }

        async static void Richiesta(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        mycontent = await content.ReadAsStringAsync();
                        
                        lst.Items.Clear();
                        mycontent = mycontent.Replace("],","");
                        mycontent = mycontent.Replace("]","");
                        mycontent = mycontent.Replace(",[","[");
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
                        }
                    }
                }
            }
        }
    }
}
