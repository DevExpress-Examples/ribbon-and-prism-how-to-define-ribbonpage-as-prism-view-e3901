﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using DevExpress.Xpf.Ribbon;

namespace Q283154 {
    [Export]
    public partial class View1 : RibbonPage {
        public View1() {
            InitializeComponent();
        }
    }
}
