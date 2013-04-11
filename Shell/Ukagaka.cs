﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Shell
{
    public partial class Ukagaka : UkagakaForm
    {
        private NotifyIcon _icon;
        private ContextMenu _contextMenu;

        private List<UkagakaForm> loadedForms = new List<UkagakaForm>();

        public Ukagaka()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            InitializeContextMenu();
        }

        private void Shell_Load(object sender, EventArgs e)
        {
            SetBackgroundTransparent();
            InitializeControls();
            LoadMainMenu();
        }

        private void Shell_FormClosing(object sender, FormClosingEventArgs e)
        {
            _icon.Visible = false;
            MessageBox.Show("bye");
        }

        private void InitializeNotifyIcon()
        {
            _icon = new System.Windows.Forms.NotifyIcon();
            string iconPath = AppSettings.Settings.Instance.Shell_IconPath;
            _icon.Icon = new System.Drawing.Icon(iconPath);
            _icon.MouseDoubleClick +=
                new System.Windows.Forms.MouseEventHandler
                    (Icon_MouseDoubleClick);

            _icon.Visible = true;
        }

        private void InitializeContextMenu()
        {
            _contextMenu = new ContextMenu();
            _contextMenu.MenuItems.Add(0, new MenuItem("居中显示", new System.EventHandler(SetControlLocationToCenter_Click)));
            _contextMenu.MenuItems.Add(1, new MenuItem("告别", new System.EventHandler(Exit_Click)));
            _icon.ContextMenu = _contextMenu;
        }

        private void SetBackgroundTransparent()
        {
            //let background be transparent
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.TransparencyKey = Color.Black;
            this.BackColor = Color.Black;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Bounds = Screen.PrimaryScreen.Bounds;
            dragControl1.DesignMode(true);
        }

        private void InitializeControls()
        {
            //set pic source
            this.picSakura.Image = global::Shell.Properties.Resources.surface0000;
            this.picKero.Image = global::Shell.Properties.Resources.surface1101;

            //set Dialog Panel
            dialogPanelSakura.BackColor = Color.Gray;
            dialogPanelKero.BackColor = Color.Gray;
            dialogPanelSakura.FlowDirection = FlowDirection.TopDown;
            dialogPanelKero.FlowDirection = FlowDirection.TopDown;
        }

        private void Icon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeWindowState();
        }

        private void picSakura_Click(object sender, EventArgs e)
        {
            dialogPanelSakura.Visible = (dialogPanelSakura.Visible == true) ? false : true;

        }

        private void picKero_Click(object sender, EventArgs e)
        {
            dialogPanelKero.Visible = (dialogPanelKero.Visible == true) ? false : true;
        }

        private void SetControlLocationToCenter_Click(object sender, EventArgs e)
        {
            SetControlLocationToCenter();
        }

        private void SetControlLocationToCenter()
        {
            picSakura.Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            picKero.Location = new Point(ClientSize.Width / 2 - 100, ClientSize.Height / 2 + 200);
            dialogPanelSakura.Location = new Point(ClientSize.Width / 2 - 250, ClientSize.Height / 2 - 50);
            dialogPanelKero.Location = new Point(ClientSize.Width / 2 - 300, ClientSize.Height / 2 + 200);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}