using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private User selectedUser;
        private ObservableCollection<User> _users;
        public Home(ObservableCollection<User> users)
        {
            InitializeComponent();
            _users = users;
            UsersListView.ItemsSource = _users;
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListView.SelectedItem != null)
            {
                selectedUser = _users.FirstOrDefault(e => e.Id == ((User)UsersListView.SelectedItem).Id);
                NameTextBox.Text = selectedUser.Name;
                SalaryTextBox.Text = selectedUser.Salary.ToString();
                EmailTextBox.Text = selectedUser.Email;
                CompanyTextBox.Text = selectedUser.Company;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            decimal.TryParse(SalaryTextBox.Text, out decimal salary);
            var user = new User
            {
                Name = NameTextBox.Text,
                Salary = salary,
                Email = EmailTextBox.Text,
                Company = CompanyTextBox.Text
            };

            if (Checker(user))
            {
                return;
            }

            selectedUser.Name = user.Name;
            selectedUser.Salary = user.Salary;
            selectedUser.Email = user.Email;
            selectedUser.Company = user.Company;

            //Refresh list
            UsersListView.Items.Refresh();
        }
        private bool Checker(User selectedUser)
        {
            var result = User.Check(selectedUser);
            if (result.Count > 0)
            {
                var st = new StringBuilder();
                foreach (var item in result)
                {
                    st.Append(item.ErrorMessage + Environment.NewLine);
                }
                MessageBox.Show(st.ToString());
            }
            return result.Count > 0;
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListView.SelectedItem != null)
            {
                User selectedUser = UsersListView.SelectedItem as User;
                _users.Remove(selectedUser);

                NameTextBox.Clear();
                SalaryTextBox.Clear();
                EmailTextBox.Clear();
                CompanyTextBox.Clear();
            }
        }
    }
}
