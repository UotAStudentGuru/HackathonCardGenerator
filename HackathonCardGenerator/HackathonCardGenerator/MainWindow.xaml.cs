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
using HackathonCardGenerator.Helpers;
using HackathonCardGenerator.ViewModels;

namespace HackathonCardGenerator
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

        private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var participants = (DataContext as MainWindowViewModel).Participants;
            CardGenerator.Generator(participants,"C:/cards");
        }
    }
}
