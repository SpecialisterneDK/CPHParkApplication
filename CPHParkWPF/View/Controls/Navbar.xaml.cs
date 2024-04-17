using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CPHParkWPF.View.Controls;
/// <summary>
/// Interaction logic for Navbar.xaml
/// </summary>
public partial class Navbar : UserControl {
    public Navbar() {
        InitializeComponent();
    }

    private void MoveToInitPage(object sender, RoutedEventArgs e) {
        NavigationService navigationService = NavigationService.GetNavigationService(Parent);
        Page page = new InitializationPage();
        navigationService.Navigate(page);
    }

    private void MoveToMigrationPage(object sender, RoutedEventArgs e) {
        NavigationService navigationService = NavigationService.GetNavigationService(Parent);
        Page page = new MigrationPage();
        navigationService.Navigate(page);
    }

    private void MoveToAddPage(object sender, RoutedEventArgs e) {
        NavigationService navigationService = NavigationService.GetNavigationService(Parent);
        Page page = new AddPage();
        navigationService.Navigate(page);
    }
    private void MoveToReportPage(object sender, RoutedEventArgs e) {
        NavigationService navigationService = NavigationService.GetNavigationService(Parent);
        Page page = new ReportPage();
        navigationService.Navigate(page);
    }
    private void MoveToRemarkPage(object sender, RoutedEventArgs e) {
        NavigationService navigationService = NavigationService.GetNavigationService(Parent);
        //Page page = new RemarkPage();
        //navigationService.Navigate(page);
    }
}
