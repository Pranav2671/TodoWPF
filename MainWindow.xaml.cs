using System.Windows;

namespace TodoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // ✅ Explicitly set DataContext (in case XAML fails)
            this.DataContext = new ViewModels.TodoViewModel();
        }
        
        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}