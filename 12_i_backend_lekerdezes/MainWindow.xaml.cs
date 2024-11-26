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
using Newtonsoft.Json;

namespace _12_i_backend_lekerdezes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateTextBlock();
        }

        async void CreateTextBlock()
        {
            
            HttpClient client = new HttpClient();
            string url = "http://127.0.0.1:3000/kacsa";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string stringResponse = await response.Content.ReadAsStringAsync();
                List<kacsaClass> kacsaList = JsonConvert.DeserializeObject<List<kacsaClass>>(stringResponse);
                foreach (kacsaClass item in kacsaList)
                {
                    TextBlock oneBlock = new TextBlock();
                    oneBlock.Text = $"Kacsa neve: {item.name}, kacsa hossza: {item.length}";
                    kacsak.Children.Add(oneBlock);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        async void AddKacsa(object s, EventArgs e) {
            kacsak.Children.Clear();
            HttpClient client = new HttpClient();
            string url = "http://127.1.1.1:3000/kacsa";

            try
            {
                var jsonObject = new
                {
                    name = nev.Text,
                    length = hossz.Text
                };

                string jsonData = JsonConvert.SerializeObject(jsonObject);
                StringContent data = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                CreateTextBlock();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            

            //MessageBox.Show($"Kacsa neve: {nev.Text}, kacsa hossza: {hossz.Text}cm");
        }
    }
}
