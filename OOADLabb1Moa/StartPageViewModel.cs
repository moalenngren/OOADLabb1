using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace OOADLabb1Moa
{
    public class StartPageViewModel : INotifyPropertyChanged
    {
        Originator orig = new Originator();

        Caretaker care = new Caretaker();

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
        public ICommand OperatorCommand { private set; get; }

        public StartPageViewModel()
        {

            Sentence = "..........";

            orig.SetState(Sentence);
            care.Do(orig);


            WordCommand = new Command<string>(
                execute: WordButton,
                canExecute: obj => { return true; }
            );

            OperatorCommand = new Command<string>(
                execute: OperatorButton,
                canExecute: obj => { return true; }
            );

            Succeed = "";

        }

        private void WordButton(string obj)
        {
            if (Sentence == "..........")
            {
                Sentence = obj;

                orig.SetState(obj);
                care.Do(orig);

            }
            else if (!Sentence.Contains(obj) && Sentence != "¿ DONDE ESTÁ LA HELADERÍA ?")
            {
                Sentence += " " + obj;


                orig.SetState(Sentence);
                care.Do(orig);


                if (Sentence == "¿ DONDE ESTÁ LA HELADERÍA ?")
                {
                    Succeed = "KORREKT! MUY BIÉN!";

                }

            }
            RefreshCanExecute();
        }

        private void OperatorButton(string obj)
        {
            if (obj == "UNDO")
            {
                care.Undo();
                care.RestoreState(orig);
                Sentence = orig.State;

            }
            else
            { //REDO
                care.Redo();
                care.RestoreState(orig);
                Sentence = orig.State;
            }
            RefreshCanExecute();
        }

        private void RefreshCanExecute()
        {
            (WordCommand as Command).ChangeCanExecute();
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
