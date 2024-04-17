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
/// Interaction logic for BackToStartButton.xaml
/// </summary>
public partial class BackToStartButton : UserControl {
    public BackToStartButton() {
        InitializeComponent();
    }

    private void BackToStartPage(object sender, RoutedEventArgs e) {
        NavigationService navigationService = NavigationService.GetNavigationService(Parent);
        navigationService.GoBack();
    }
}
