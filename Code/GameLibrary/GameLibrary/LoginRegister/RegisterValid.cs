﻿using System;
using System.Windows.Forms;
using DataManager;

namespace GameLibrary
{
    /// <summary>
    /// The only purpose of this form is to inform you that your register has gone well.
    /// </summary>
    public partial class RegisterValid : Form
    {
        #region attributes

        /// <summary>
        /// The user inherited from the login-register.
        /// </summary>
        private User user;

        #endregion attributes

        #region accessors

        /// <summary>
        /// Contains the user information.
        /// </summary>
        public User User
        {
            set { user = value; }
        }

        #endregion accessors

        #region formLoad

        public RegisterValid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method puts the username of the new user in the window.
        /// </summary>
        private void RegisterValid_Load(object sender, EventArgs e)
        {
            lblConfirm.Text = $"{user.Username}, you're now registered.";
        }

        private void cmdSignIn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion formLoad
    }
}