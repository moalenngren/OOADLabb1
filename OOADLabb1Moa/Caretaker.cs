using System;
using System.Collections.Generic;
using System.Linq;

namespace OOADLabb1Moa
{
    public class Caretaker 
    {

        //new
        private static List<Memento> mementoList = new List<Memento>();

        public static List<Memento> MementoList { get; }
        //old
        //private Memento _memento; 

        /* old
        public Memento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        } */

        //new
        //save state of the originator
        public static void SaveState(Originator orig)
        {
            mementoList.Add(orig.CreateMemento());
        }

        //new
        //restore state of the originator
        public static void RestoreState(Originator orig, int stateNumber)
        {
            orig.SetMemento(mementoList[stateNumber]);
        }

    }
}
