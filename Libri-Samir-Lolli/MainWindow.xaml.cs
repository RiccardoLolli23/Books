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
            string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=3&d1="+txt_DATA1.Text+"&d2="+txt_DATA2.Text;
            Richiesta(url);
        }

        private void btn_richiesta_4_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://10.13.100.2/Lolli/Tp/Server.php?op=4&id=" + txt_ID.Text;
            Richiesta(url);
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
                        string[] s = mycontent.Split('[');                        
                        int count = 0;
                        string[][] a;

                        for (int i = 0; i < s.Length; i++)
                        {
                            if (s[i] != "")
                            {
                               
                                count++;
                            }
                        }
                        a = new string[count][];
                        count = 0;
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (s[i] != "")
                            {
                                a[count] = s[i].Split(',');
                                count++;
                            }
                        }
                        for (int j = 0; j < a[0].Length; j++)
                            for (int i = 0; i < a.GetLength(0) ; i++)
                        {
                            
                                lst.Items.Add(a[i][j]);
                        }
                    }

                }

            }
        }
    }
}
