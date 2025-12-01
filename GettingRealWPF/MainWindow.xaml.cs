using System;
using System.Collections.Generic;
using System.Linq;
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
using GettingRealWPF.ViewModels;

namespace GettingRealWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            DatPicker.SelectedDate = DateTime.Today;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NavnInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NavnInputBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TidInputBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TidInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

        private void DatoPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAvailableTimes();
        }

        private void RefreshAvailableTimes()
        {
            if (DatoPicker.SelectedDate is null)
            {
                AvailableTimesComboBox.ItemsSource = null;
                return;
            }

            var date = DatoPicker.SelectedDate.Value.Date;
            var open = new TimeSpan(9, 0, 0);
            var close = new TimeSpan(17, 0, 0);
            var step = TimeSpan.FromMinutes(30);

            var slots = GenerateTimeSlots(date, open, close, step);
            AvailableTimesComboBox.ItemsSource = slots.Select(dt => dt.ToString("HH:mm")).ToList();
            AvailableTimesComboBox.SelectedIndex = AvailableTimesComboBox.Items.Count > 0 ? 0 : -1;
        }

        private static List<DateTime> GenerateTimeSlots(DateTime date, TimeSpan open, TimeSpan close, TimeSpan step)
        {
            var result = new List<DateTime>();
            for (var t = open; t < close; t = t.Add(step))
            {
                result.Add(date + t);
            }
            return result;
        }
    }
}