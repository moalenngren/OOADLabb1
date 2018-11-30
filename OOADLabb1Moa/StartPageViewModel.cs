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

        //Originator o = new Originator(); //old

        Originator orig = new Originator(); //new


        Caretaker care = new Caretaker(); //old

        Stack<string> stack = new Stack<string>();

        int index = 0;

        //-------to here

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

            //old
            //o.State = "..........";
            //c.Memento = o.CreateMemento();

            //new
            orig.SetState("..........");
            Caretaker.SaveState(orig); //save state of the originator

            Sentence = "..........";
            WordCommand = new Command<string>(
                execute: WordButton,
                canExecute: obj => { return true; }
            );

            OperatorCommand = new Command<string>(
                execute: OperatorButton,
                canExecute: obj => { return true; }
            );

            Succeed = "";

            //index = 0;
        }

        private void WordButton(string obj)
        {
            if(Sentence == "..........")
            {
                Sentence = obj;

                //old - Set state here
                //o.State = Sentence;
                //c.Memento = o.CreateMemento();

                //new
                orig.SetState(obj);
                Caretaker.SaveState(orig); //save state of the originator
                index++;

            } else if (!Sentence.Contains(obj) && Sentence != "¿ DONDE ESTÁ LA HELADERÍA ?")
            {
                Sentence += " " + obj;

                //old - Set state here
                //o.State = Sentence;
                //c.Memento = o.CreateMemento();

                //new
                orig.SetState(Sentence);
                Caretaker.SaveState(orig); //save state of the originator
                index++;


                if (Sentence == "¿ DONDE ESTÁ LA HELADERÍA ?")
                {
                    Succeed = "KORREKT! MUY BIÉN!";

                    //old - Set state here
                    //o.State = Sentence;
                    //c.Memento = o.CreateMemento();


                }

            }
            //Set only here????

            RefreshCanExecute();
        }

        private void OperatorButton(string obj) {



            if (obj == "UNDO")
            {


                //old
                //o.SetMemento(c.Memento);
                //Sentence = o.State;

                if (index > 0) index --;
                //restore state of the originator
                Caretaker.RestoreState(orig, index);
                Sentence = orig.State;

            } else { //REDO
                index++;
                Caretaker.RestoreState(orig, index);
                Sentence = orig.State;
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
