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
using System.Windows.Media.TextFormatting;
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
                    Border oneBorder = new Border();
                    Grid oneGrid = new Grid();
                    oneBorder.Child = oneGrid;
                    kacsak.Children.Add(oneBorder);
                    RowDefinition firstRow = new RowDefinition();
                    RowDefinition secondRow = new RowDefinition();
                    RowDefinition thirdRow = new RowDefinition();

                    oneGrid.RowDefinitions.Add(firstRow);
                    oneGrid.RowDefinitions.Add(secondRow);
                    oneGrid.RowDefinitions.Add(thirdRow);

                    TextBlock NameTextBlock = new TextBlock();
                    TextBlock LengthTextBlock = new TextBlock();
                    Button SellButton = new Button();

                    oneGrid.Children.Add(NameTextBlock);
                    oneGrid.Children.Add(LengthTextBlock);
                    oneGrid.Children.Add(SellButton);

                    Grid.SetRow(LengthTextBlock, 1);
                    Grid.SetRow(SellButton, 2);
                    
                    SellButton.Background= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#484848"));
                    SellButton.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("red"));
                    SellButton.Foreground= new SolidColorBrush((Color)ColorConverter.ConvertFromString("white"));



                    NameTextBlock.Text = $"Név: {item.name}";
                    LengthTextBlock.Text = $"Hossz: {item.length}";
                    SellButton.Content = "Eladás";
                    oneBorder.Margin = new Thickness(5);
                    oneBorder.CornerRadius = new CornerRadius(10);
                    oneBorder.Padding = new Thickness(5);
                    oneBorder.Width = 100;
                    oneBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("white"));
                    oneBorder.BorderThickness = new Thickness(2);
                    oneBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#484848"));
                    NameTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("white"));
                    LengthTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("white"));
                    //oneBlock.Text = $"Kacsa neve: {item.name}, kacsa hossza: {item.length}";

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
                    name = KacsaNameTextBox.Text,
                    length = KacsaLengthTextBox.Text
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
