using System;
using System.Windows.Input;

namespace GettingRealWPF.Commands   
{

    //binder viewmodel og UI

    public sealed class RelayCommand : ICommand    
//Implementering af ICommand der binder UI-elementer til metoder i ViewModel
//uden at skrive event handlers i code-behind
    {
        //execute den metode der køres når brugeren aktiverer kommandoen
        private readonly Action<object?> execute;
        private readonly Func<object?, bool>? canExecute;
        //canExecute bestemmer om kommandoen kan køres(om knappen skal være enabled/disabled)

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => execute(parameter);

        public event EventHandler? CanExecuteChanged 
          //CanExecuteChanged opdatere UI automatisk når betingelser ændrer sig
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}
