﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projects
{
    /// <summary>
    /// Interaction logic for frmSimon.xaml
    /// </summary>
    public partial class frmSimon : Window
    {
        enum Colors
        {
            Red, 
            Blue, 
            Green, 
            Yellow
        }

        List<Colors> turnColors = new List<Colors>();

        public frmSimon()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer sp = new SoundPlayer(Properties.Resources.Coin);
            sp.PlaySync();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
