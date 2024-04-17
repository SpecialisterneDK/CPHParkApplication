using CPHParkWPF.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CPHParkWPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : NavigationWindow {
    public MainWindow() {
        InitializeComponent();

        Navigate(new StartPage());
    }
}