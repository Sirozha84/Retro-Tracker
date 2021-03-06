﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace RetroTracker
{
    partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            this.Text = "О " + Editor.ProgramName;
            label1.Text = Editor.ProgramName;
            label2.Text = "Версия " + Editor.ProgramVersion;
            label3.Text = "Автор программы: " + Editor.ProgramAutor;
            linkLabel1.Text = Editor.Site;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Editor.SiteFull);
        }
    }
}
