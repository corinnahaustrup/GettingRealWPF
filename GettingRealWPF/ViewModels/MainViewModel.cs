using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GettingRealWPF.Models;
using GettingRealWPF.Commands;

namespace GettingRealWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged //notifier UI at refresh
    {
        //Services er i en ObservableCollection<Service> i viewmodel
        public ObservableCollection<Service> Services { get; } = new(); 
        public ObservableCollection<Hairdresser> Hairdressers { get; } = new();
        public ObservableCollection<string> AvailableTimes { get; } = new();

        private Service? _selectedService;
        public Service? SelectedService
        {
            get => _selectedService;
            set { _selectedService = value; OnPropertyChanged(nameof(SelectedService)); RefreshAvailableTimes(); UpdateConfirmCanExecute(); }
        }

        private Hairdresser? _selectedHairdresser;
        public Hairdresser? SelectedHairdresser
        {
            get => _selectedHairdresser;
            set { _selectedHairdresser = value; OnPropertyChanged(nameof(SelectedHairdresser)); RefreshAvailableTimes(); UpdateConfirmCanExecute(); }
        }

        private DateTime? _selectedDate = DateTime.Today;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); RefreshAvailableTimes(); UpdateConfirmCanExecute(); }
        }

        private string? _selectedTime;
        public string? SelectedTime
        {
            get => _selectedTime;
            set { _selectedTime = value; OnPropertyChanged(nameof(SelectedTime)); UpdateConfirmCanExecute(); }
        }

        private string? _navn;
        public string? Navn
        {
            get => _navn;
            set { _navn = value; OnPropertyChanged(nameof(Navn)); UpdateConfirmCanExecute(); }
        }

        private string? _telefon;
        public string? Telefon
        {
            get => _telefon;
            set { _telefon = value; OnPropertyChanged(nameof(Telefon)); UpdateConfirmCanExecute(); }
        }

        public ICommand ConfirmCommand { get; }

        public MainViewModel()
        {
            // Demo data
            Services.Add(new Service(1, "Klip", 299m, TimeSpan.FromMinutes(30)));
            Services.Add(new Service(2, "Farve", 599m, TimeSpan.FromMinutes(60)));
            Hairdressers.Add(new Hairdresser(1, "Ammari"));
            Hairdressers.Add(new Hairdresser(2, "Sara"));

            SelectedService = Services.FirstOrDefault();
            SelectedHairdresser = Hairdressers.FirstOrDefault();

            ConfirmCommand = new RelayCommand(_ => Confirm(), _ => CanConfirm());

            RefreshAvailableTimes();
        }

        private void RefreshAvailableTimes()
        {
            AvailableTimes.Clear();
            if (SelectedDate is null || SelectedService is null) return;

            var open = new TimeSpan(9, 0, 0);
            var close = new TimeSpan(17, 0, 0);
            var step = TimeSpan.FromMinutes(15); // finer granularity
            var duration = SelectedService.Duration;

            for (var start = open; start + duration <= close; start += step)
            {
                var startDt = SelectedDate.Value.Date + start;
                // TODO: exclude existing bookings via repository later
                AvailableTimes.Add(startDt.ToString("HH:mm"));
            }

            SelectedTime = AvailableTimes.FirstOrDefault();
        }

        private bool CanConfirm()
        {
            return SelectedService != null
                   && SelectedHairdresser != null
                   && SelectedDate != null
                   && !string.IsNullOrWhiteSpace(SelectedTime)
                   && !string.IsNullOrWhiteSpace(Navn)
                   && !string.IsNullOrWhiteSpace(Telefon);
        }

        private void Confirm()
        {
            // TODO: Persist booking. For now just placeholder logic.
            // Keep UI popups out of ViewModel; raise event or use messaging if needed.
        }

        private void UpdateConfirmCanExecute()
        {
            if (ConfirmCommand is RelayCommand rc)
                rc.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}