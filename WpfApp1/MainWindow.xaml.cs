using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.Views;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> _users;
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            _users = new ObservableCollection<User>()
            {
                new User { Name = "Tom", Company = "WallMart", Email = "tom@gmail.com", Salary = 10_000},
                new User { Name = "Alex", Company = "BestBuy", Email = "alex@gmail.com", Salary = 12_500},
                new User { Name = "Marry", Company = "Wendys", Email = "marry@gmail.com", Salary = 8_000}
            };
            bodyGrid.Children.Add(new Home(_users));
        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            if (tag.Equals("5"))
            {
                Application.Current.Shutdown();
                return;
            }
            UserControl userControl = tag switch
            {
                "1" => new Home(_users),
                "2" => new CreateUser(),
                "3" => new About(),
                "4" => new Settings(),
            };
            bodyGrid.Children.Clear();
            bodyGrid.Children.Add(userControl);

            if (userControl is CreateUser createUserControl)
            {
                createUserControl.UserCreateNotify += (user) =>
                {
                    _users.Add(user);
                    bodyGrid.Children.Clear();
                    bodyGrid.Children.Add(new Home(_users));
                };
            }
        }
    }
}