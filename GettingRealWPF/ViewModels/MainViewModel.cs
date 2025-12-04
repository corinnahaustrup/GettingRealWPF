using GettingRealWPF.Commands;
using GettingRealWPF.Models;
using GettingRealWPF.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GettingRealWPF.ViewModels

    //kun binding og commands
{ 
    public class MainViewModel : INotifyPropertyChanged
    {
        //når værdier ændres i viewmodel på properties får UI besked
        private readonly ServiceRepository serviceRepo;
        private BookingRepository bookingRepository;

        //Lister til ComboBoxe
        //ObservableCollection<> er en liste som automatisk giver UI besked når der tilføjes/fjernes items
        //= new() initialeserer hver liste med det samme, hvis der skal tilføjes nye items i constructoren
        public ObservableCollection<Service> Services { get; } = new();
        public ObservableCollection<Hairdresser> Hairdressers { get; } = new();
        public ObservableCollection<string> AvailableTimes { get; } = new();
        public ObservableCollection<Booking> Bookings { get; } = new();


        //Selected properties (binder til UI)
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

        // Command binding
        public ICommand ConfirmCommand { get; }

        public MainViewModel()
        {
            // Hent services fra repository
            serviceRepo = new ServiceRepository();
            bookingRepository = new BookingRepository();
            foreach (var s in serviceRepo.GetAllServices())
                Services.Add(s);

            // Dummy frisører
            Hairdressers.Add(new Hairdresser(1, "Jafaar"));
            Hairdressers.Add(new Hairdresser(2, "Jacob"));

            // Standardvalg
            SelectedService = Services.FirstOrDefault();
            SelectedHairdresser = Hairdressers.FirstOrDefault();

            // Command binding
            ConfirmCommand = new RelayCommand(_ => Confirm(), _ => CanConfirm());

            foreach (var b in bookingRepository.GetBookings())
                Bookings.Add(b);

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
            // Her kan du vise en besked eller gemme booking
            Console.WriteLine($"Booking bekræftet: {Navn} har valgt {SelectedService?.Name} hos {SelectedHairdresser?.Name} kl. {SelectedTime}");
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