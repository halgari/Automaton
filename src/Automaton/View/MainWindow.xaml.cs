using System.Windows;
using System.Windows.Controls;

namespace Automaton.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MaxHeight = SystemParameters.WorkArea.Height;
        }
    }
}