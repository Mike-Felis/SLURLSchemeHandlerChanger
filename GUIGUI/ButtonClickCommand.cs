using System.Windows.Input;

using System.Windows;

public class ButtonClickCommand : ICommand
{


    public bool CanExecute(object parameter)
    {
        return true;
    }

    public event EventHandler CanExecuteChanged;

    // コマンドが実行された時の処理
    public void Execute(object parameter)
    {
                        MessageBox.Show("clicked" );

    }
}