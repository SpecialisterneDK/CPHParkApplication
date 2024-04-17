using CPHParkWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CPHParkWPF.View;
/// <summary>
/// Interaction logic for ReportPage.xaml
/// </summary>
public partial class ReportPage : Page, INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public ReportPage() {
        InitializeComponent();
    }

    private async void Report(object sender, RoutedEventArgs e) {
        if (this.ReportType.SelectedItem is ComboBoxItem) {
            this.stdout.Text = $"Report has been called at {DateTime.Now}. Please wait for at least 30 seconds.";

            this.stdout.Text = await CPHParkController.RunReport(this.OutputLocation.PathValue, this.JSON.PathValue, this.FileName.Text, (this.ReportType.SelectedItem as ComboBoxItem).Content.ToString() ?? "");
        } else {
            this.stdout.Text = $"Please chooce report type.";
        }
    }

    public void NotifyPropertyChanged(string propertyName) {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

}