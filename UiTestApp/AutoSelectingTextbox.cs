using System.Windows.Controls;
using System.Windows.Input;

namespace UiTestApp
{
    class AutoSelectingTextbox : TextBox
    {
        public AutoSelectingTextbox()
        {
            GotKeyboardFocus += OnGotKeyboardFocus;
        }

        private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs)
        {
            if (keyboardFocusChangedEventArgs.KeyboardDevice.IsKeyDown(Key.Tab))
                ((TextBox)sender).SelectAll();
        }
    }
}
