using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace OOADLabb1Moa
{
    public class StartPageViewModel : INotifyPropertyChanged
    {

        private string succeed;

        public string Succeed
        {
            get { return succeed; }
            set
            {
                if (succeed != value)
                {
                    SetProperty(ref succeed, value);
                }
            }
        }

        private string sentence;

        public string Sentence 
        {
            get { return sentence; }
            set 
            {
                if (sentence != value)
                {
                    SetProperty(ref sentence, value);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand WordCommand { private set; get; }

        public StartPageViewModel()
        {
            Sentence = "..........";
            WordCommand = new Command<string>(
                execute: WordButton,
                canExecute: obj => { return true; }
            );

            Succeed = "";
        }

        private void WordButton(string obj)
        {
            if(Sentence == "..........")
            {
                Sentence = obj;
            } else if (!Sentence.Contains(obj) && Sentence != "¿ DONDE ESTÁ LA HELADERÍA ?")
            {
                Sentence += " " + obj;

                if (Sentence == "¿ DONDE ESTÁ LA HELADERÍA ?")
                {
                    Succeed = "KORREKT! MUY BIÉN!";
                }

            } 
            RefreshCanExecute();
        }

        private void RefreshCanExecute()
        {
            (WordCommand as Command).ChangeCanExecute();
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) {
            if (Object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        } 

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
