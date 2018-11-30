using System;
namespace OOADLabb1Moa
{

    public class Originator 
    {
        private string _state;

        public string State
        {
            get { return _state; }
            set

            {
                _state = value;
                Console.WriteLine("State = " + _state);
            }
        }


        public Memento CreateMemento()
        {

            return (new Memento(_state));
        }

        public void SetMemento(Memento memento)
        {
            Console.WriteLine("Restoring state...");
            State = memento.State;
        }

        //new
        public void SetState(string state){
            this._state = state;
        }
    }

}
