using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GettingRealWPF.Models;
using GettingRealWPF.Commands;

namespace GettingRealWPF.ViewModels

    //kun binding og commands
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Lister til ComboBoxe
        public ObservableCollection<Service> Services { get; } = new();
        public ObservableCollection<Hairdresser> Hairdressers { get; } = new();
        public ObservableCollection<string> AvailableTimes { get; } = new();

        // Backing fields
        private Service? selectedService;
        public Service? SelectedService
        {
            get => selectedService;
            set
            {
                selectedService = value;
                OnPropertyChanged(nameof(SelectedService));
                RefreshAvailableTimes();
                UpdateConfirmCanExecute();
            }
        }

        private Hairdresser? selectedHairdresser;
        public Hairdresser? SelectedHairdresser
        {
            get => selectedHairdresser;
            set
            {
                selectedHairdresser = value;
                OnPropertyChanged(nameof(SelectedHairdresser));
                RefreshAvailableTimes();
                UpdateConfirmCanExecute();
            }
        }

        private DateTime? selectedDate = DateTime.Today;
        public DateTime? SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                RefreshAvailableTimes();
                UpdateConfirmCanExecute();
            }
        }

        private string? selectedTime;
        public string? SelectedTime
        {
            get => selectedTime;
            set
            {
                selectedTime = value;
                OnPropertyChanged(nameof(SelectedTime));
                UpdateConfirmCanExecute();
            }
        }

        private string? navn;
        public string? Navn
        {
            get => navn;
            set
            {
                navn = value;
                OnPropertyChanged(nameof(Navn));
                UpdateConfirmCanExecute();
            }
        }

        private string? telefon;
        public string? Telefon
        {
            get => telefon;
            set
            {
                telefon = value;
                OnPropertyChanged(nameof(Telefon));
                UpdateConfirmCanExecute();
            }
        }

        public ICommand ConfirmCommand { get; }

              public MainViewModel()
        {
            // Midlertidige data (flyttes til repositories senere)
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
            var step = TimeSpan.FromMinutes(15);
            var duration = SelectedService.Duration;

            for (var start = open; start + duration <= close; start += step)
            {
                var startDt = SelectedDate.Value.Date + start;
                // TODO: filtrer optagede tider fra repository
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
            // TODO: Gem booking via repository
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