using Dem5Baldin.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace Dem5Baldin.Pages
{
    /// <summary>
    /// Interaction logic for ProductEditPage.xaml
    /// </summary>
    public partial class ProductEditPage : Page
    {
        Product product;
        public ProductEditPage(Product product)
        {
            this.product = product;
            DataContext = product;
            InitializeComponent();
            CbProductTypes.ItemsSource = EfModel.Init().ProductTypes.ToList();
        }

        private void ImageChangeClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog { Filter = "Jpeg files|*.jpg|All Files|*.*" };
           if (openFile.ShowDialog() == true)
            {
                product.Image = File.ReadAllBytes(openFile.FileName);
            }

        }

        private void BtSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if(product.ID ==0)
                EfModel.Init().Products.Add(product);
                EfModel.Init().SaveChanges();
            }catch(DbEntityValidationException ex)
            {
                MessageBox.Show(String.Join(Environment.NewLine, ex.EntityValidationErrors.Last().ValidationErrors.Select(ve => ve.ErrorMessage)));
            }
        }

        private void BtDelClick(object sender, RoutedEventArgs e)
        {
            if(product.ProductSales.Count > 0)
            {
                MessageBox.Show("Запрещено удалять ! Уже продавали");
                return;
            }

                product.ProductMaterials.Clear();
                product.ProductCostHistories.Clear();
                EfModel.Init().Products.Remove(product);
                EfModel.Init().SaveChanges();
            
        }
    }
}
