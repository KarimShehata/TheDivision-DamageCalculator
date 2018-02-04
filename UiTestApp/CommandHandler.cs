using System;
using System.Windows;
using System.Windows.Input;

namespace UiTestApp
{
    public class CommandHandler : ICommand
    {
        private Action m_Action;

        public CommandHandler(Action action)
        {
            m_Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return Enabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            m_Action();
        }

        #region property Enabled
        private bool m_Enabled = true;
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled != value)
                {
                    m_Enabled = value;
                    if (CanExecuteChanged != null)
                        Application.Current.Dispatcher.Invoke(() => CanExecuteChanged(this, null));
                }
            }
        }
        #endregion
    }
}