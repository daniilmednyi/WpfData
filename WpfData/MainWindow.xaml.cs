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
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtCount.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (!int.TryParse(txtPrice.Text, out int price) || price < 0)
            {
                MessageBox.Show("Некорректная цена!");
                return;
            }

            if (!int.TryParse(txtCount.Text, out int count) || count < 0)
            {
                MessageBox.Show("Некорректное количество!");
                return;
            }
            products.Add(new Product { Name = txtName.Text, Price = price, Count = count });
        }

        private void tableProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tableProduct.SelectedItem is Product p)
            {
                txtName.Text = p.Name;
                txtPrice.Text = p.Price.ToString();
                txtCount.Text = p.Count.ToString();
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (tableProduct.SelectedItem is Product p)
            {
                MessageBoxResult q = MessageBox.Show("Вы точно хотите удалить",
                    "Удаление",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (q == MessageBoxResult.Yes)
                    products.Remove(p);
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (tableProduct.SelectedItem is Product p)
            {
                if (!int.TryParse(txtCount.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Введите корректное количество!");
                    return;
                }

                if (quantity > p.Count)
                {
                    MessageBox.Show("Недостаточно товара на складе",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
                p.Count -= quantity;
                MessageBox.Show($"Куплен {p.Name} по цене {p.Price} в кол-ве {quantity} шт");

                tableProduct.ItemsSource = null;
                tableProduct.ItemsSource = products;
            }
        }
        private void Buy1_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Product p)
            {
                if (!int.TryParse(txtCount.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Введите корректное количество!");
                    return;
                }

                if (quantity > p.Count)
                {
                    MessageBox.Show("Недостаточно товара на складе",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                {
                    MessageBox.Show($"Куплен {p.Name} по цене {p.Price} в кол-ве 1 шт");
                    p.Count -= quantity;


                    tableProduct.ItemsSource = null;
                    tableProduct.ItemsSource = products;
                }
            }
        }
    }
}