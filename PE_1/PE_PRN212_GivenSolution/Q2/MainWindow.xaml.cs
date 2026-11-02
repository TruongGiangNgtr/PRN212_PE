using Microsoft.IdentityModel.Tokens;
using Q2.Entities;
using System.Configuration;
using System.Diagnostics;
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

namespace Q2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private HashSet<int> _checkedSupplier = new();

        public MainWindow()
        {
            InitializeComponent();

            LoadProducts();
            LoadCategories();
            LoadSuppliers();

            LoadSuppliers_AddProduct();
            LoadCategories_AddProduct();
        }

        private void LoadSuppliers_AddProduct()
        {
            using var db = new OnlineStoreDbContext();
            var suppliers = db.Suppliers.OrderBy(s => s.SupplierName).ToList();
            lbSuppliers.ItemsSource = suppliers;
        }

        private void LoadCategories_AddProduct()
        {
            using var db = new OnlineStoreDbContext();
            var categories = db.Categories.OrderBy(c => c.CategoryName).ToList();
            cmbCategoryAddProduct.ItemsSource = categories;
            cmbCategoryAddProduct.DisplayMemberPath = "CategoryName";
            cmbCategoryAddProduct.SelectedValuePath = "CategoryId";
            cmbCategoryAddProduct.SelectedIndex = 0;
        }

        private void LoadSuppliers()
        {
            using var db = new OnlineStoreDbContext();

            var suppliers = db.Suppliers.ToList();

            suppliers.Insert(0, new Supplier
            {
                SupplierId = 0,
                SupplierName = "All"
            });
            cmbSupplier.ItemsSource = suppliers;
            cmbSupplier.DisplayMemberPath = "SupplierName";
            cmbSupplier.SelectedValuePath = "SupplierId";
            cmbSupplier.SelectedIndex = 0;
        }

        private void LoadCategories()
        {
            using var db = new OnlineStoreDbContext();

            var categories = db.Categories.ToList();

            categories.Insert(0, new Category
            {
                CategoryId = 0,
                CategoryName = "All"
            });
            cmbCategory.ItemsSource = categories;
            cmbCategory.DisplayMemberPath = "CategoryName";
            cmbCategory.SelectedValuePath = "CategoryId";
            cmbCategory.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            using var db = new OnlineStoreDbContext();

            var products = db.Products.Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.Price,
                p.Stock,
                CategoryName = p.Category.CategoryName
            }).ToList();

            dgProducts.ItemsSource = products;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            // lay id cua category va supplier duoc chon
            if (cmbCategory.SelectedValue != null && cmbSupplier.SelectedValue != null)
            {
                int categoryId = (int)cmbCategory.SelectedValue;
                int supplierId = (int)cmbSupplier.SelectedValue;

                // Filter products based on selected category and supplier
                using var db = new OnlineStoreDbContext();
                var filteredProducts = db.Products
                    .Where(p => (categoryId == 0 || p.CategoryId == categoryId) && (supplierId == 0 || p.Suppliers.Any(s => s.SupplierId == supplierId)))
                    .Select(p => new
                    {
                        p.ProductId,
                        p.ProductName,
                        p.Price,
                        p.Stock,
                        CategoryName = p.Category.CategoryName
                    })
                    .ToList();

                dgProducts.ItemsSource = filteredProducts;
            }
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cmbCategory.SelectedIndex= 0;
            cmbSupplier.SelectedIndex= 0;
            LoadProducts();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var productName = txtProductName.Text;
            var stringPrice = txtPrice.Text;
            var stringStock = txtStock.Text;
            var selectedCategory = cmbCategoryAddProduct.SelectedItem;
            var selectedSuppliers = lbSuppliers.SelectedItems;
            if (productName.IsNullOrEmpty())
            {
                MessageBox.Show("Product Name must not be empty.");
                return;
            }

            if (!double.TryParse(stringPrice, out var price) || stringPrice is null)
            {
                MessageBox.Show("Invalid price format.");
                return;
            }

            if (!int.TryParse(stringStock, out var stock) || stringStock is null)
            {
                MessageBox.Show("Invalid stock format.");
                return;
            }

            if (_checkedSupplier.Count() != 1)
            {
                MessageBox.Show("Please select exactly one supplier.");
                return;
            }

            using var db = new OnlineStoreDbContext();
            var product = new Product
            {
                ProductName = productName,
                Price = (decimal)price,
                Stock = stock,
                CategoryId = (int)cmbCategoryAddProduct.SelectedValue
            };
            db.Products.Add(product);
            db.SaveChanges();

            LoadProducts();
            ClearForm();
        }

        private void SupplierCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Supplier supplier)
            {
                _checkedSupplier.Add(supplier.SupplierId);
            }
        }

        private void SupplierCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is Supplier supplier)
            {
                _checkedSupplier.Remove(supplier.SupplierId);
            }
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtProductName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cmbCategoryAddProduct.SelectedIndex = 0;

            var suppliers = lbSuppliers.ItemsSource;
            lbSuppliers.ItemsSource = null;
            lbSuppliers.ItemsSource = suppliers;
            _checkedSupplier.Clear();
        }
    }
}