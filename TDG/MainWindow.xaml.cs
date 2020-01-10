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

namespace TDG
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

        private void SetA(object sender, RoutedEventArgs e)
        {
            var generator = new Random((int)DateTime.Now.Ticks);
            data.ItemsSource = Enumerable.Range(1, generator.Next(5, 20))
                .Select(i => new TDG.Models.A
                {
                    Code = i,
                    Description = $"Product {i}",
                    Value = 50m * (decimal)generator.NextDouble() 
                }).ToArray();
        }

        private void SetB(object sender, RoutedEventArgs e)
        {
            var generator = new Random((int)DateTime.Now.Ticks);
            data.ItemsSource = Enumerable.Range(1, generator.Next(5, 20))
                .Select(i => new TDG.Models.B
                {
                    UserId = Guid.NewGuid(),
                    Name = $"User {i}",
                    Enabled = generator.Next(0,1) == 1,
                }).ToArray();
        }
    }
}
