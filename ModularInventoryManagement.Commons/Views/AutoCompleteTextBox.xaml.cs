using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModularInventoryManagement.Commons.Views
{
    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>
    public partial class AutoCompleteTextBox : UserControl
    {
        private IList<object> SuggestionList;
        public Action<object> AddSelectionToSomeList;

        public AutoCompleteTextBox()
        {
            InitializeComponent();
            SuggestionList = new List<object>();

            // add event handlers
            AutoTextBox.TextChanged += OnTextChanged;
            AutoTextBox.LostKeyboardFocus += (s, e) => {
                SuggestionBox.Visibility = Visibility.Collapsed;
            };
            AutoTextBox.GotFocus += (s, e) => {
                if (SuggestionBox.ItemsSource is null) return;
                SuggestionBox.Visibility = Visibility.Visible;
            };
            SuggestionBox.SelectionChanged += OnSuggestionSelectionChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(DataContext is IEnumerable<object> autoCompleteData)) return;

            string currentStr = AutoTextBox.Text;
            SuggestionList.Clear();

            SuggestionList = autoCompleteData.Where(item => item.ToString()
                                                .ToLowerInvariant()
                                                .Contains(currentStr.ToLowerInvariant()))
                                            .ToList();

            if (SuggestionList.Count <= 0 || string.IsNullOrEmpty(AutoTextBox.Text))
            {
                SuggestionBox.Visibility = Visibility.Collapsed;
                SuggestionBox.ItemsSource = null;
            } 
            else
            {
                SuggestionBox.ItemsSource = SuggestionList; 
                SuggestionBox.Visibility = Visibility.Visible;
            }
        }

        private void OnSuggestionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SuggestionBox.ItemsSource is null) return;

            SuggestionBox.Visibility = Visibility.Collapsed;
            AutoTextBox.TextChanged -= OnTextChanged;
            if (SuggestionBox.SelectedIndex != -1)
            {
                var selectedItem = SuggestionBox.SelectedItem;
                AutoTextBox.Text = selectedItem.ToString();
                AddSelectionToSomeList?.Invoke(selectedItem);
                AutoTextBox.Text = string.Empty;
                SuggestionBox.UnselectAll();
            }
            AutoTextBox.TextChanged += OnTextChanged;
        }
    }
}
