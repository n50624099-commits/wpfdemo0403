using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using wpfdemo0403.classes;
using wpfdemo0403.database;
using wpfdemo0403.pages;

namespace wpfdemo0403
{
    public partial class MainWindow : Window
    {
        
        private List<Товар> _allProducts;
        private List<Товар> _filteredProducts;

        public MainWindow()
        {
            InitializeComponent();

            
            if (AppSession.CurrentUser != null)
            {
                txtCurrentUser.Text = $"{AppSession.CurrentUser.Fio} ({AppSession.CurrentUser.RoleName})";
            }
            else
            {
                txtCurrentUser.Text = "Гость";
            }

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                
                _allProducts = classes.database.Context.Товар.ToList();

               
                var manufacturers = _allProducts
                    .Where(p => !string.IsNullOrEmpty(p.Производитель))
                    .Select(p => p.Производитель)
                    .Distinct()
                    .ToList();

                manufacturers.Insert(0, "Все");
                cmbПроизводитель.ItemsSource = manufacturers;
                cmbПроизводитель.SelectedIndex = 0;

                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных из БД: {ex.Message}");
            }
        }

        private void ApplyFilters()
        {
            if (_allProducts == null) return;

            string searchText = txtSearchText.Text?.ToLower().Trim() ?? "";
            string selectedManufacturer = cmbПроизводитель.SelectedItem?.ToString();

            var query = _allProducts.AsEnumerable();

            
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var terms = searchText.Split(' ');
                query = query.Where(p => terms.All(t =>
                    $"{p.Наименование} {p.Описание}".ToLower().Contains(t)));
            }

            
            if (!string.IsNullOrEmpty(selectedManufacturer) && selectedManufacturer != "Все")
            {
                query = query.Where(p => p.Производитель == selectedManufacturer);
            }

            _filteredProducts = query.ToList();
            ApplySort();
        }

        private void ApplySort()
        {
            if (cmbСортировка.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortText = selectedItem.Content.ToString();

                switch (sortText)
                {
                    case "Сначала дешевые":
                        _filteredProducts = _filteredProducts.OrderBy(p => p.Цена_со_скидкой).ToList();
                        break;
                    case "Сначала дорогие":
                        _filteredProducts = _filteredProducts.OrderByDescending(p => p.Цена_со_скидкой).ToList();
                        break;
                }
            }

           
            icProducts.ItemsSource = null;
            icProducts.ItemsSource = _filteredProducts;
        }

        private void txtSearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cmbПроизводитель_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cmbСортировка_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplySort();
        }

        private void btnExitUser_Click(object sender, RoutedEventArgs e)
        {
           
            AppSession.Clear(); 

            
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}