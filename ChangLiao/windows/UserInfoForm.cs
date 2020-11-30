using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Forms;
using ChangLiao.DB;
using ChangLiao.Model.ReciveModel;
using ChangLiao.Model.SendModel;
namespace ChangLiao.windows
{
    public partial class UserInfoForm : DSkinForm
    {
        private FriendModel friend;
        private GetUserData data;
        public UserInfoForm()
        {
            InitializeComponent();
        }
        public UserInfoForm(FriendModel model)
        {
            friend = model;
            InitializeComponent();
            personalPanl1.setFriend(model);
        }

        public UserInfoForm(GetUserData model)
        {
            data = model;
            InitializeComponent();
            personalPanl1.setDataBeforeLoad(model);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
