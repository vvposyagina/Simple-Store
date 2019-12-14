using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BasketService.BasketClient client;

        public MainWindow()
        {
            InitializeComponent();
            client = new BasketService.BasketClient("WSHttpBinding_IBasket");            
            client.Start();
        }
        public void Clear()
        {
            lbItemsLack.Items.Clear();
        }
        private void bGoToBasket_Click(object sender, RoutedEventArgs e)
        {
            Basket basket_xaml = new Basket(client, this);
            basket_xaml.Show();
        }

        private void bSearch_Click(object sender, RoutedEventArgs e)
        {
            bAddToBasket_Copy.IsEnabled = false;
            string query = tbSearch.Text;
            lbItemsLack.Items.Clear();

            XElement xElement = client.Search(query);              
            dgSearchResults.ItemsSource = Item.XmlToItem(xElement);

            if (dgSearchResults.Items.Count != 0)
                bAddToBasket_Copy.IsEnabled = true;
        }

        private void bAddToBasket_Copy_Click(object sender, RoutedEventArgs e)
        {
            if(dgSearchResults.SelectedItems.Count != 0)
            {
                foreach(Item item in dgSearchResults.SelectedItems)
                {
                    if (!client.AddItem(item.ID))                        
                    {
                        lbItemsLack.Items.Add(String.Format("Item {0} is not in stock", item.Name));
                    }
                    else
                    {
                        item.Count--;
                        lbItemsLack.Items.Add(String.Format("Item {0} was added", item.Name));
                    }                    
                }  
            }
        }
    }
}
