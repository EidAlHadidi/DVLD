using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
namespace My_DVLD
{
    public partial class ctrlUserInformation : UserControl
    {
        public ctrlUserInformation()
        {
            InitializeComponent();
        }

        public void FillData(int UserID)
        {
            clsUser User = clsUser.Find(UserID);
            ctrlPersonInformation1.FillData(User.PersonID);
            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName.ToString();
            lblIsActive.Text = User.IsActive ? "Yes" : "No";
        }

    }
}
