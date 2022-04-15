using Microsoft.Win32;
using Sirma.Employees;
using System;
using System.IO;
using System.Windows;

namespace Sirma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {            
            InitializeComponent();
        }

        public void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();            
            string[] input;

            if (openFileDialog.ShowDialog() ?? false)
            {
                input = File.ReadAllText(openFileDialog.FileName).Split(Environment.NewLine);
                WorkAnalyser workAnalyser = new(input);
                var max = workAnalyser.GetMaxCommonDuty();
                var result = workAnalyser.GetCommonDuties();
                commonDuties.ItemsSource = result;
                DataContext = result;
            }            
        }
    }
}
