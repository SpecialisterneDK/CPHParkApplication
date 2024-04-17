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
using System.Windows.Shapes;

namespace CPHParkWPF.View;
/// <summary>
/// Interaction logic for AddPage.xaml
/// </summary>
public partial class AddPage : Page, INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public AddPage() {
        InitializeComponent();
    }

    private async void AddCounts(object sender, RoutedEventArgs e) {
        this.stdout.Text = $"Adding content of count files:\n";

        foreach (string filePath in Directory.GetFiles(CountsLocation.PathValue)) {
            string[] splitPath = filePath.Split("\\");
            string filePart = splitPath[splitPath.Length - 1];

            if (filePart.EndsWith(".xlsx")) {
                if (int.TryParse(filePart.Replace(".xlsx", "").Replace("0", ""), out _)) {
                    this.stdout.Text += $"Adding content of count file {filePart}:\n";
                    this.stdout.Text += await CPHParkController.RunAdd(filePath, JSON.PathValue, Timeframe.Text) + "\n";
                }
            }
        }

        this.stdout.Text += "Content of All count files have been added.";
    }

    public void NotifyPropertyChanged(string propertyName) {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
