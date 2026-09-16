using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_DataGrid
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> products = new ObservableCollection<Product>();
        public MainWindow()
        {
            InitializeComponent();

            products.Add(new Product { Name = "Процессор Intel Core i5 11600 k", Price = 25000, Count = 10 });
            products.Add(new Product { Name = "Видеокарта RTX 4060", Price = 55000, Count = 5 });
            products.Add(new Product { Name = "Оперативная память 16GB", Price = 10000, Count = 20 });

            tableProduct.ItemsSource = products;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            int price = Convert.ToInt32(txtPrice.Text);
            int count = Convert.ToInt32(txtBuyCount.Text);
            products.Add(new Product
            {
                Name = name,
                Count = count,
                Price = price
            });
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (tableProduct.SelectedItem is Product p)
            {
                MessageBoxResult q = MessageBox.Show("Вы точно хотите удалить?",
                    "Удаление",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (q == MessageBoxResult.Yes)
                    products.Remove(p);
            }
        }
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Product p)
            {
                if (!int.TryParse(txtBuyCount.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Введите корректное количество!");
                    return;
                }

                if (quantity > p.Count)
                {
                    MessageBox.Show("Недостаточно товара на складе");
                    return;
                }

                p.Count -= quantity;

                tableProduct.ItemsSource = null;
                tableProduct.ItemsSource = products;
            }
        }
    }
}
