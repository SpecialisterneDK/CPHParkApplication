using CPHParkWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace CPHParkWPF.View;
/// <summary>
/// Interaction logic for InitializationPage.xaml
/// </summary>
public partial class InitializationPage : Page, INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    public InitializationPage() {
        InitializeComponent();
    }

    private async void Initialize(object sender, RoutedEventArgs e) {
        this.stdout.Text = $"Init has been called at {DateTime.Now}. Please wait for at least 30 seconds.";
        this.stdout.Text = await CPHParkController.RunInit(this.Locations.PathValue, Path.Combine(this.JSONLocation.PathValue, this.FileName.Text + ".json"));
    }

    public void NotifyPropertyChanged(string propertyName) {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
