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
using System.Xml.Linq;

namespace Client
{
    /// <summary>
    /// Interaction logic for Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        BasketService.BasketClient client;
        MainWindow startPage;
        public Basket(BasketService.BasketClient cl, MainWindow mw)
        {
            InitializeComponent();
            client = cl;
            startPage = mw;
            XElement xElement = client.GetCurrentBasket();
            dgItemsFromBasket.ItemsSource = Item.XmlToItem(xElement);
            int total = client.GetTotal();
            lTotalCount.Content = total.ToString();
        }

        private void bUpdateBasket_Click(object sender, RoutedEventArgs e)
        {
            XElement xElement = client.GetCurrentBasket();           
            dgItemsFromBasket.ItemsSource = Item.XmlToItem(xElement);
            int total = client.GetTotal();
            lTotalCount.Content = total.ToString();
        }

        private void bAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItemsFromBasket.SelectedItems.Count != 0)
                {
                    foreach (Item item in dgItemsFromBasket.SelectedItems)
                    {
                        client.AddItem(item.ID);
                    }
                    XElement xElement = client.GetCurrentBasket();

                    dgItemsFromBasket.ItemsSource = Item.XmlToItem(xElement);
                    int total = client.GetTotal();
                    lTotalCount.Content = total.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Empty row was chosen");
            }
        }       

        private void bPay1_Click(object sender, RoutedEventArgs e)
        {
            client.PayPurchase();            
            this.Close();
            SuccessfulPurchase sp = new SuccessfulPurchase(startPage);
            sp.Show();
            
        }

        private void bDeleteItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItemsFromBasket.SelectedItems.Count != 0)
                {
                    foreach (Item item in dgItemsFromBasket.SelectedItems)
                    {
                        client.DeleteItem(item.ID);
                    }
                    XElement xElement = client.GetCurrentBasket();

                    dgItemsFromBasket.ItemsSource = Item.XmlToItem(xElement);
                    int total = client.GetTotal();
                    lTotalCount.Content = total.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Empty row was chosen");
            }
        }               
    }
}
