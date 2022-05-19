﻿/*
Copyright 2009-2022 Intel Corporation

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Windows.Forms;

namespace MeshCentralRouter
{
    public partial class MappingStats : Form
    {
        public MapUserControl mapControl;

        public MappingStats(MapUserControl mapControl)
        {
            this.mapControl = mapControl;
            InitializeComponent();
            Translate.TranslateControl(this);
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            kvmInBytesLabel.Text = string.Format(((mapControl.mapper.bytesToClient == 1) ? Translate.T(Properties.Resources.OneByte) : Translate.T(Properties.Resources.XBytes)), mapControl.mapper.bytesToClient);
            kvmOutBytesLabel.Text = string.Format(((mapControl.mapper.bytesToServer == 1) ? Translate.T(Properties.Resources.OneByte) : Translate.T(Properties.Resources.XBytes)), mapControl.mapper.bytesToServer);
            kvmCompInBytesLabel.Text = string.Format(((mapControl.mapper.bytesToClientCompressed == 1) ? Translate.T(Properties.Resources.OneByte) : Translate.T(Properties.Resources.XBytes)), mapControl.mapper.bytesToClientCompressed);
            kvmCompOutBytesLabel.Text = string.Format(((mapControl.mapper.bytesToServerCompressed == 1) ? Translate.T(Properties.Resources.OneByte) : Translate.T(Properties.Resources.XBytes)), mapControl.mapper.bytesToServerCompressed);
            if (mapControl.mapper.bytesToClient == 0) {
                inRatioLabel.Text = "0%";
            } else {
                inRatioLabel.Text = (100 - ((mapControl.mapper.bytesToClientCompressed * 100) / mapControl.mapper.bytesToClient)) + "%";
            }
            if (mapControl.mapper.bytesToServer == 0) {
                outRatioLabel.Text = "0%";
            } else {
                outRatioLabel.Text = (100 - ((mapControl.mapper.bytesToServerCompressed * 100) / mapControl.mapper.bytesToServer)) + "%";
            }
        }

        private void KVMStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer.Enabled = false;
        }

        private void KVMStats_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
            refreshTimer_Tick(this, null);
            Text += " - " + mapControl.node.name;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            mapControl.closeStats();
        }
    }
}
