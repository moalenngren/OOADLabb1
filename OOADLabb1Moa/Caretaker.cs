using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OOADLabb1Moa
{
    public class Caretaker
    {
        private static Stack<Memento> undoStack = new Stack<Memento>();

        private static Stack<Memento> redoStack = new Stack<Memento>();

        public static List<Memento> MementoList { get; }

        public void Undo()
        {
            if (undoStack.Count > 1)
            {
                Memento state = undoStack.Pop();
                redoStack.Push(state);
            }
        }

        public void Redo()
        {
            if (redoStack.Count != 0)
            { 
                Memento state = redoStack.Pop();
                undoStack.Push(state);
            }

        }

        public void Do(Originator orig)
        {
            undoStack.Push(orig.CreateMemento());
            redoStack.Clear();
        }

        public void RestoreState(Originator orig)
        {
            if (undoStack.Count != 0)
                orig.SetMemento(undoStack.Peek());
        }
    



}
}
