// --------------------------------------------------------------------------------------------------------------
// FILENAME: MainForm.cs
//
// Copyright © 2012 - 2013 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: The source file for the MainForm.
//
// --------------------------------------------------------------------------------------------------------------
//  
// This sample demonstrates the use of the Symbol.Sensor class library available in EMDK for .NET
// for accessing the Sensor functionality.  This sample demonstrates only a few important operations
// related to sensors. For a detailed information on all sensorType operations, refer to the EMDK help file.
// 
// This sample is provided for demonstration purpose only and is not intended for use in the production environment.
// 
//-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Symbol.Sensor;

namespace CS_SensorSample1
{
    /// <summary>
    /// Mainform class which starts the first available or selected sensor and displays the data received from these sensors.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Variables

        private API mySensorAPI = null;
        private int scaleFactor = 1;

        private FormResizer formResizer = null;
        public delegate void DisplayDataCallback(TextBox textBox, string displayString);

        #endregion Variables

        #region Contructor

        /// <summary>
        /// MainForm constructor which initializes the component
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // This sample is designed to run on different devices with different resolutions.
            // The FormResizer object is used to resize the form and its controls.
            // The FormResizer class does not contain any Sensor related code
            formResizer = new FormResizer(this, 240, 280); // Sending the original width and height

            // Show the version number of the sample.
            string versionNumber = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
            VersionLabel.Text = Resources.SAMPLE_VERSION + versionNumber;
        }

        #endregion Contructor

        #region Methods

        /// <summary>
        /// Intialiizes the API class and checks if atleast one sensor is supported on the device.
        /// The application will exit with error if:
        /// - Failed to initialize the sensor class library:  "Sensor Library Initialization failed! Exiting..."
        /// - Does not find at least one supported sensor: "Sensors not found! Exiting..."
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // MainMenu is needed on PocketPC (Windows Mobile) devices, because it provides the OK/Minimize button 
            // and the Windows Start button at the bottom of the application. If not, the application goes to full screen 
            // and there is no way close/minimize the app, unless using specific controls in the app to do it
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
            {
                this.Menu = new MainMenu();
            }

            try
            {
                // Initialyze the API
                mySensorAPI = new API();

                if (mySensorAPI.SupportedDevices.Length > 0)
                {
                    // Populate sensors to the drop down box
                    foreach (Device d in mySensorAPI.SupportedDevices)
                    {
                        SensorComboBox.Items.Add(d.Name);
                    }

                    // Select the first available sensor
                    SensorComboBox.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show(Resources.SENSORS_NOT_FOUND);
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show(Resources.FAILED_INIT_SENSOR_LIBRARY);
                this.Close();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            // DeInits the sensor and disposes the API class.
            if (mySensorAPI != null)
            {
                DeInitSensor();

                mySensorAPI.Dispose();
                mySensorAPI = null;
            }
        }

        /// <summary>
        /// Initializes the data event handler based on the sensor type, unit of measurement and requests the sensor to start reading the data.
        /// </summary>
        /// <param name="device">Selected sensor device</param>
        private void InitSensor(Device device)
        {
            bool isStarted = false;
            bool isSupported = true;
            OnDataHandler dataEvent = null;

            switch (device.SensorType)
            {
                case SENSOR_TYPE.ACCELEROMETER:
                    dataEvent += ProcessAccelerometerData;
                    break;

                case SENSOR_TYPE.AMBIENT_LIGHT:
                    dataEvent += ProcessAmbientLightData;
                    break;

                case SENSOR_TYPE.ECOMPASS:
                    dataEvent += ProcessECompassData;
                    break;

                case SENSOR_TYPE.HUMIDITY:
                    dataEvent += ProcessHumidityData;
                    break;

                case SENSOR_TYPE.MOTION:
                    dataEvent += ProcessMotionData;
                    break;

                case SENSOR_TYPE.ORIENTATION:
                    dataEvent += ProcessOrientationData;
                    break;

                case SENSOR_TYPE.PROXIMITY:
                    dataEvent += ProcessProximityData;
                    break;

                case SENSOR_TYPE.TEMPERATURE:
                    dataEvent += ProcessTemperatureData;
                    break;

                case SENSOR_TYPE.TILT_ANGLE:
                    dataEvent += ProcessTiltAngleData;
                    break;

                default:
                    isSupported = false;
                    break;
            }

            if (isSupported)
            {
                UnitTextBox.Text = device.UOM.ToString();

                isStarted = mySensorAPI.StartSensor(device, dataEvent, out scaleFactor);
            }

            if (isStarted)
            {
                try
                {
                    // Show data on the screen for the first time until it receives from events
                    SensorDataCollection data = mySensorAPI.GetSensorData();

                    if ((dataEvent != null) && (data != null))
                    {
                        dataEvent(data);
                    }
                }
                catch
                {
                    // TextBox will be empty in case of a failure initially
                }
            }
            else
            {
                MessageBox.Show(Resources.FAILED_TO_START_SENSOR);
            }
        }

        /// <summary>
        /// Request sensor to stop sampling and disable sensor. 
        /// If any error occurs duing this process, the application with exit with error message.
        /// </summary>
        private void DeInitSensor()
        {
            if (false == mySensorAPI.StopSensor())
            {
                MessageBox.Show(Resources.FAILED_TO_CLOSE_SENSOR);
            }

            DataTextBox.Text = "";
            UnitTextBox.Text = "";
        }

        /// <summary>
        /// On combo box selection, deinit the previously started sensor and
        /// initialize the newly selected sensor to start reading the data.
        /// </summary>
        private void SensorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeInitSensor();

            Device newSensorDevice = mySensorAPI.SupportedDevices[SensorComboBox.SelectedIndex];
            InitSensor(newSensorDevice);
        }

        /// <summary>
        /// Displays the data in the text box control.
        /// </summary>
        /// <param name="textBox">TextBox control</param>
        /// <param name="data">String data to be displayed</param>
        private void DisplayData(TextBox textBox, string data)
        {
            try
            {
                if (textBox != null)
                {
                    if (textBox.InvokeRequired)
                    {
                        // Not in the UI therad, so passing to the UI thread
                        DisplayDataCallback d = new DisplayDataCallback(DisplayData);
                        this.Invoke(d, new object[] { textBox, data });
                    }
                    else
                    {
                        // In the UI therad, so display the text
                        textBox.Text = data;
                    }
                }
            }
            catch
            {
                // Display error. No need to show.
            }
        }

        #endregion Methods

        #region OnData Event Methods

        /// <summary>
        /// The data event callback method which is used to receive the accelerometer data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param> 
        private void ProcessAccelerometerData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS))
            {
                AccelerometerData data = (AccelerometerData)dataCollection.GetFirst;

                double X = Math.Round((double)data.X / scaleFactor, 4);
                double Y = Math.Round((double)data.Y / scaleFactor, 4);
                double Z = Math.Round((double)data.Z / scaleFactor, 4);

                string displayString = X.ToString() + ", " + Y.ToString() + ", " + Z.ToString();

                DisplayData(DataTextBox, displayString);
            }
        }

        /// <summary>
        /// The data event callback method which is used to receive the tilt angle data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param> 
        private void ProcessTiltAngleData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS))
            {
                TiltAngleData data = (TiltAngleData)dataCollection.GetFirst;

                double X = Math.Round((double)data.X / scaleFactor, 4);
                double Y = Math.Round((double)data.Y / scaleFactor, 4);
                double Z = Math.Round((double)data.Z / scaleFactor, 4);

                string displayString = X.ToString() + ", " + Y.ToString() + ", " + Z.ToString();

                DisplayData(DataTextBox, displayString);
            }
        }

        /// <summary>
        /// The data event callback method which is used to receive the ambient light data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param> 
        private void ProcessAmbientLightData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS))
            {
                AmbientLightData data = (AmbientLightData)dataCollection.GetFirst;

                double value = Math.Round((double)data.Value / scaleFactor, 4);

                DisplayData(DataTextBox, value.ToString());
            }
        }

        /// <summary>
        /// The data event callback method which is used to receive the ECompass data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param> 
        private void ProcessECompassData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS))
            {
                ECompassData data = (ECompassData)dataCollection.GetFirst;

                double value = Math.Round((double)data.Value / scaleFactor, 4);

                DisplayData(DataTextBox, value.ToString());
            }
        }

        /// <summary>
        /// The data event callback method which is used to receive the Humidity data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param> 
        private void ProcessHumidityData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS))
            {
                HumidityData data = (HumidityData)dataCollection.GetFirst;

                double value = Math.Round((double)data.Value / scaleFactor, 4);

                DisplayData(DataTextBox, value.ToString());
            }
        }

        /// <summary>
        /// The data event callback method which is used to receive the motion data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param> 
        private void ProcessMotionData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS)) 
            {
                MotionData data = (MotionData)dataCollection.GetFirst;

                StringBuilder displayString = new StringBuilder();

                foreach (DEVICE_MOTION d in data.Values)
                {
                    displayString.Append(d.ToString() + " ");
                }

                DisplayData(DataTextBox, displayString.ToString());
            }
        }

        /// <summary>
        /// The data event callback method which is used to receive the orientation data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param>
        private void ProcessOrientationData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS))
            {
                OrientationData data = (OrientationData)dataCollection.GetFirst;

                string displayString = data.Value.ToString();

                DisplayData(DataTextBox, displayString);
            }
        }

        /// <summary>
        /// The data event callback method which is used to receive the proximity data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param>
        private void ProcessProximityData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS))
            {
                ProximityData data = (ProximityData)dataCollection.GetFirst;

                double value = Math.Round((double)data.Value / scaleFactor, 4);

                DisplayData(DataTextBox, value.ToString());
            }
        }

        /// <summary>
        /// The data event callback method which is used to receive the temperature data.
        /// </summary>
        /// <param name="dataCollection">Sensor data collection</param>
        private void ProcessTemperatureData(SensorDataCollection dataCollection)
        {
            if ((dataCollection != null) && (dataCollection.Result == Results.SUCCESS))
            {
                TemperatureData data = (TemperatureData)dataCollection.GetFirst;

                double value = Math.Round((double)data.Value / scaleFactor, 4);

                DisplayData(DataTextBox, value.ToString());
            }
        }

        #endregion OnData Event Methods
    }
}