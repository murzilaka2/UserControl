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
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : UserControl
    {
        public delegate void UserCreateHandler(User user);
        public event UserCreateHandler UserCreateNotify;
        public CreateUser()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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

            UserCreateNotify?.Invoke(user);
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
    }

}
