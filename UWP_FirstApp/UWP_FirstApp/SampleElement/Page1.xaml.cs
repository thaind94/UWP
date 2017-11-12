using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_FirstApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page1 : Page
    {
        public Page1()
        {
            this.InitializeComponent();
        }


        private void MyCheckBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CheckBoxResult.Text = (bool)MyCheckBox.IsChecked ? "True" : "False";
            if(CheckBoxResult.Text == "True")
            {

            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButtonResult.Text = (bool)YesRadioButton.IsChecked ? "Yes" : "No";
        }

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxResult == null) return;
            var combo = (ComboBox)sender;
            var selectedItem = (ComboBoxItem)combo.SelectedItem;
            ComboBoxResult.Text = selectedItem.Content.ToString();
        }

        private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = MyListBox.Items.Cast<ListBoxItem>().Where(item => item.IsSelected).Select(item => item.Content.ToString()).ToArray();
            ListBoxResult.Text = string.Join(",", selectedItems);
        }

        private void MyToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtonResult.Text = (bool)MyToggleButton.IsChecked ? "Enabled" : "Disabled";
        }

        private void MyToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitchResult.Text = MyToggleSwitch.IsOn.ToString();
        }
    }
}
