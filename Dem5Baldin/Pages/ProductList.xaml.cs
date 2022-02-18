using Dem5Baldin.Database;
using Dem5Baldin.Windows;
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

namespace Dem5Baldin.Pages
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    {
        public ProductList()
        {
            InitializeComponent();
            UpdateData();
        }

        private void UpdateData()
        {
            LVProducts.ItemsSource = EfModel.Init().Products.ToList();
        }

        private void BtChangeCostClick(object sender, RoutedEventArgs e)
        {
            new ChangeMinCost().ShowDialog();
        }

        private void BtAddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductEditPage(new Product()));
        }

        private void BtEditClick(object sender, RoutedEventArgs e)
        {
            if (LVProducts.SelectedItems.Count > 0)
            {
                Product product = LVProducts.SelectedItem as Product;
                NavigationService.Navigate(new ProductEditPage(product));
            }
            

            
        }

        private void ProductListChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateData();
        }
    }
}
