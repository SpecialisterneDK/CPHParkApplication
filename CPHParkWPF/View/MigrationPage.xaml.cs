using CPHParkWPF.Model;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CPHParkWPF.View;
/// <summary>
/// Interaction logic for MigrationPage.xaml
/// </summary>
public partial class MigrationPage : Page, INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public MigrationPage() {
        InitializeComponent();
    }

    private async void Migrate(object sender, RoutedEventArgs e) {
        this.stdout.Text = $"Migrate has been called at {DateTime.Now}. Please wait for at least 30 seconds.";
        this.stdout.Text = await CPHParkController.RunMigrate(this.AltAlle.PathValue, this.JSON.PathValue);
    }

    public void NotifyPropertyChanged(string propertyName) {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

}