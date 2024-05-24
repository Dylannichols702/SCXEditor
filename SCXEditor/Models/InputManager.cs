using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCXEditor.Models
{
    public class InputManager
    {
        private static InputManager? instance;

        public event EventHandler? OnWKeyPressed;
        public event EventHandler? OnAKeyPressed;
        public event EventHandler? OnSKeyPressed;
        public event EventHandler? OnDKeyPressed;
        public event EventHandler<OnNoteKeyPressedEventArgs>? OnTapNoteKeyPressed;
        public event EventHandler<OnNoteKeyPressedEventArgs>? OnHoldNoteKeyPressed;
        public class OnNoteKeyPressedEventArgs : EventArgs
        {
            public int column;
        }
        public event EventHandler? OnSaveHotkeyPressed;
        public static InputManager Instance
        {
            get
            {
                if (instance == null) return new InputManager();
                return instance;
            }
        }

        public InputManager()
        {
            instance = this;
        }

        public void WKeyPressed() => OnWKeyPressed?.Invoke(this, EventArgs.Empty);
        public void AKeyPressed() => OnAKeyPressed?.Invoke(this, EventArgs.Empty);
        public void SKeyPressed() => OnSKeyPressed?.Invoke(this, EventArgs.Empty);
        public void DKeyPressed() => OnDKeyPressed?.Invoke(this, EventArgs.Empty);
        public void TapNoteKeyPressed(int col) => OnTapNoteKeyPressed?.Invoke(this, new OnNoteKeyPressedEventArgs { column = col });
        public void HoldNoteKeyPressed(int col) => OnHoldNoteKeyPressed?.Invoke(this, new OnNoteKeyPressedEventArgs { column = col });
        public void SaveHotkeyPressed() => OnSaveHotkeyPressed?.Invoke(this, EventArgs.Empty);
    }
}
