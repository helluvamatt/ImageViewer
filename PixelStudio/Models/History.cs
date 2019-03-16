using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PixelStudio.Models
{
    internal class History : INotifyPropertyChanged
    {
        private readonly List<IHistoryCommand> _History;
        private readonly int _MaxStackSize;
        private int _StackPointer = -1;

        public History(int maxStackSize)
        {
            _History = new List<IHistoryCommand>();
            _MaxStackSize = maxStackSize;
        }
        
        public bool CanUndo => _StackPointer > 0;
        public bool CanRedo => _StackPointer < _History.Count - 1;

        public void Execute(IHistoryCommand command)
        {
            if (_StackPointer < _History.Count - 1)
            {
                _History.RemoveRange(_StackPointer + 1, _History.Count - 1 - _StackPointer);
            }
            _History.Add(command);
            command.Execute();
            _StackPointer = _History.Count - 1;
        }

        public void Undo()
        {
            if (!CanUndo) return;
            _History[_StackPointer].Undo();
            OnStackPointerChanged(_StackPointer - 1);
        }

        public void Redo()
        {
            if (!CanRedo) return;
            OnStackPointerChanged(_StackPointer + 1);
            _History[_StackPointer].Execute();
        }

        public void Reset()
        {
            foreach (var cmd in _History)
            {
                cmd.Dispose();
            }
            _History.Clear();
            OnStackPointerChanged(-1);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void OnStackPointerChanged(int value)
        {
            bool canUndo = CanUndo;
            bool canRedo = CanRedo;
            _StackPointer = value;
            if (CanUndo != canUndo) OnPropertyChanged(nameof(CanUndo));
            if (CanRedo != canRedo) OnPropertyChanged(nameof(CanRedo));
        }
    }

    internal interface IHistoryCommand : IDisposable
    {
        void Execute();
        void Undo();
    }

    internal interface ILayerHistoryCommand : IHistoryCommand
    {
        LayerModel Layer { get; }
    }
}
