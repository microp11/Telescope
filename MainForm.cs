/*
 * microp11 2022, Paul Maxan
 * 
 * This file is part of the telescope below.
 * 
 * The telescope is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * The code is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with the telescope.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace telescope
{
    public partial class FormTelescope : Form
    {
        private StellariumServer sReferenceToServer;
        private TcpListener sReferenceToListener;
        private Task stellariumTask;
        private OrbitronDdeServer orbitronDdeServer;

        private TrackingDataItem trackingDataItem = new();

        const int MAX_TEXT_SIZE = 1000;

        public FormTelescope()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (HandleStellarium()) ToggleStartButton();
        }

        private bool HandleStellarium()
        {
            bool result = false;
            Debug.WriteLine("00");
            if (sReferenceToServer == null)
            {
                //start listening
                try
                {
                    Debug.WriteLine("01");
                    stellariumTask = new(delegate ()
                    {
                        Debug.WriteLine("02");
                        StellariumServer telescopeServer = new(ServerIP.Text, (int)ServerPort.Value, ReferencesCallback, PositionCallback);
                        Debug.WriteLine("020");
                    });
                    Debug.WriteLine("03");
                    stellariumTask.Start();

                    Debug.WriteLine("Stellarium Telescope Started. Awaiting coordinates...");
                    rtbListener.Text = "Stellarium Telescope Started. Awaiting coordinates..." + Environment.NewLine + rtbListener.Text;

                    result = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("03e");
                    rtbListener.Text = ex.Message + Environment.NewLine + Environment.NewLine + rtbListener.Text;
                }
            }
            else
            {
                //stop listening
                try
                {
                    Debug.WriteLine("04");
                    sReferenceToListener.Server.Close();
                    Debug.WriteLine("05");
                    Application.DoEvents();
                    sReferenceToServer.ListenerAllowedToRun = false;
                    Debug.WriteLine("06");
                    Application.DoEvents();
                    rtbListener.Text = "Stellarium Telescope stopped." + Environment.NewLine + rtbListener.Text;
                    Debug.WriteLine("07");
                    sReferenceToServer = null;
                    Debug.WriteLine("08");

                    result = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("09");
                    rtbListener.Text = ex.Message + Environment.NewLine + rtbListener.Text;
                }
            }
            return result;
        }

        private void ReferencesCallback(StellariumServer server, TcpListener listener)
        {
            try
            {
                if (server == null && listener == null)
                {
                    //this indicates a streaming error
                    //we close the connection
                    if (sReferenceToServer != null) sReferenceToServer.ListenerAllowedToRun = false;

                    //notify and allow user to restart
                    rtbListener.Invoke((MethodInvoker)delegate
                    {
                        rtbListener.Text = "Streaming error. Try again." + Environment.NewLine + rtbListener.Text;
                    });
                    ToggleStartButton();
                }
                sReferenceToServer = server;
                sReferenceToListener = listener;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ToggleStartButton()
        {
            button1.Invoke((MethodInvoker)delegate
            {
                if (button1.Text == "Start")
                {
                    button1.Text = "Stop";
                }
                else
                {
                    button1.Text = "Start";
                }
            });
            labelAddress.Invoke((MethodInvoker)delegate
            {
                labelAddress.Enabled = !labelAddress.Enabled;
            });
            labelPort.Invoke((MethodInvoker)delegate
            {
                labelPort.Enabled = !labelPort.Enabled;
            });
            ServerIP.Invoke((MethodInvoker)delegate
            {
                ServerIP.Enabled = !ServerIP.Enabled;
            });
            ServerPort.Invoke((MethodInvoker)delegate
            {
                ServerPort.Enabled = !ServerPort.Enabled;
            });

        }

        private byte[] PositionCallback(byte[] bytes)
        {
            byte[] result = new byte[256];

            try
            {
                //LSB
                ushort length = (ushort)((ushort)(bytes[1] << 8) + bytes[0]);
                ushort typ = (ushort)((ushort)(bytes[3] << 8) + bytes[2]);
                long time = bytes[11] << 56 |
                    bytes[10] << 48 |
                    bytes[9] << 40 |
                    bytes[8] << 32 |
                    bytes[7] << 24 |
                    bytes[6] << 16 |
                    bytes[5] << 8 |
                    bytes[4];

                uint RA = (uint)(bytes[15] << 24) |
                    (uint)(bytes[14] << 16) |
                    (uint)(bytes[13] << 8) |
                    bytes[12];

                int DEC = (int)(
                    (uint)(bytes[19] << 24) |
                    (uint)(bytes[18] << 16) |
                    (uint)(bytes[17] << 8) |
                    bytes[16]);

                float az = BitConverter.ToSingle(bytes, 20);
                Debug.WriteLine(az);

                float el = BitConverter.ToSingle(bytes, 24);
                Debug.WriteLine(el);

                result = new byte[24];
                Array.Copy(bytes, 0, result, 0, 20); //status is zero, 4 bytes
                result[0] = 24;

                lock(Locker.locker)
                {
                    trackingDataItem.SN = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                    trackingDataItem.AZ = az;
                    trackingDataItem.EL = el;
                }

                CalculateAndDisplayCorrectedTrackingData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                rtbListener.Text = ex.Message + Environment.NewLine + rtbListener.Text;
            }
            return result;
        }

        private void CalculateAndDisplayCorrectedTrackingData()
        {
            TrackingDataItem tdi = new();
            lock (Locker.locker)
            {
                tdi = trackingDataItem;
            }

            int azimuthSpill = 1; //neutral
            Chk360.Invoke((MethodInvoker)delegate
            {
                if (Chk360.Checked) azimuthSpill = 360;
            });
            Chk0.Invoke((MethodInvoker)delegate
            {
                if (Chk0.Checked) azimuthSpill = 0;
            });

            float newAzimuth = 0.0F;
            newAzimuth = tdi.AZ;
            switch (azimuthSpill)
            {
                case 0:
                    if ((tdi.AZ >= 350) && (tdi.AZ < 360)) newAzimuth = -(360 - tdi.AZ);
                    break;

                case 360: //359 going over 360, 1 becomes 361
                    if (tdi.AZ <= 10) newAzimuth = 360 + tdi.AZ;
                    break;

                default:
                    newAzimuth = tdi.AZ;
                    break;
            }

            lock (Locker.locker)
            {
                trackingDataItem.newAZ = newAzimuth;
            }

            labelTrackingData.Invoke((MethodInvoker)delegate
            {
                labelTrackingData.Text = CreateOrbitronTrackingDataString(tdi);
            });

            rtbListener.Invoke((MethodInvoker)delegate
            {
                if (rtbListener.Text.Length > MAX_TEXT_SIZE) rtbListener.Text = rtbListener.Text.Substring(0, MAX_TEXT_SIZE);
                rtbListener.Text = $"AZ {tdi.AZ:0.0000}° \t EL {trackingDataItem.EL:0.0000}° \t {newAzimuth:0.0000}°" + Environment.NewLine + rtbListener.Text;
            });
        }

        private void FormTelescope_FormClosing(object sender, FormClosingEventArgs e)
        {
            orbitronDdeServer.Unregister();
        }

        private string CreateOrbitronTrackingDataString(TrackingDataItem trackingDataItem)
        {
            return $"SN\"{trackingDataItem.SN}\" AZ{trackingDataItem.newAZ} EL{trackingDataItem.EL}";
        }

        private void FormTelescope_Load(object sender, EventArgs e)
        {
            orbitronDdeServer = new OrbitronDdeServer(trackingDataItem);
            orbitronDdeServer.Register();
        }
    }
}