//--------------------------------------------------------------------------------------------------------------
// FILENAME: API.cs
//
// Copyright © 2012 - 2013 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: Implements the code which communicates with the Symbol.Sensor class library.
//
// --------------------------------------------------------------------------------------------------------------
//  
// This sample demonstrates the use of the Symbol.Sensor class library available in EMDK for .NET
// for accessing the Sensor functionality.  This sample demonstrates only a few important operations
// related to sensors. For a detailed information on all sensor operations, refer to the EMDK help file.
// 
// This sample is provided for demonstration purpose only and is not intended for use in the production environment.
// 
//-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Symbol.Sensor;

namespace CS_SensorSample1
{
    /// <summary>
    /// The API class which calls sensor class library to initialize, starts sensor, stop sensors, dispose, etc.
    /// </summary>
    class API: IDisposable
    {
        #region Variables

        private SensorManager pSensorManager = null;
        Sensor pSensorObj = null;

        #endregion Variables

        #region Constructor Destructor

        /// <summary>
        /// API constructor which initilizes the SensorManager.
        /// </summary>
        public API()
        {
            pSensorManager = new SensorManager();
        }

        /// <summary>
        /// API destructor which disposes the SensorManager.
        /// </summary>
        ~API()
        {
            this.Dispose();
        }

        #endregion Constructor Destructor

        #region IDisposable Members

        public void Dispose()
        {
            if (pSensorManager != null)
            {
                pSensorManager.Dispose();
                pSensorManager = null;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Check for sensors availability
        /// </summary>
        public Device[] SupportedDevices
        {
            get
            {
                return pSensorManager.SupportedDevices;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// This method adds first available sensor of sensorType, enables, set the sample rate for the devices which supports
        /// sampling based on rate, attaches the notifier and starts sampling.
        /// In case of failure, the sensorType is closed and returns null.
        /// </summary>
        /// <param name="device">Device object of the supported sensor</param>
        /// <param name="onDataDelegate">Callback delegate</param>
        /// <param name="scaleFactor">Sensor data scale factor</param>
        /// <returns>Returns true if success, false otherwise</returns>
        public bool StartSensor(Device device, OnDataHandler onDataDelegate, out int scaleFactor)
        {
            bool retValue = false;
            scaleFactor = 1;

            try
            {
                // Create the sensor instance with given device
                pSensorObj = pSensorManager.AddSensor(device);

                if (pSensorObj != null)
                {
                    pSensorObj.Enable();
                    scaleFactor = pSensorObj.Config.ScaleFactor;

                    if (pSensorObj.Device.SamplingType != SAMPLING_TYPE.NONE)
                    {
                        // Register for events and start sampling
                        SetSampleRate();
                        pSensorObj.OnData += onDataDelegate;

                        if (Results.SUCCESS == pSensorObj.StartSampling())
                        {
                            retValue = true;
                        }
                    }
                    else
                    {
                        retValue = true;
                    }
                }
            }
            finally
            {
                if (false == retValue)
                {
                    // Stops the sensor if there is any failure
                    StopSensor();
                }
            }

            return retValue;
        }

        /// <summary>
        /// Stop the sensor. 
        /// </summary>
        public bool StopSensor()
        {
            bool retValue = true;

            if (pSensorObj != null)
            {
                try
                {
                    try
                    {
                        if (pSensorObj.IsEnabled)
                        {
                            // Stops sampling and disables the sensor
                            if (pSensorObj.Device.SamplingType != SAMPLING_TYPE.NONE)
                            {
                                pSensorObj.StopSampling();
                            }

                            pSensorObj.Disable();
                        }
                    }
                    finally
                    {
                        // Removes the sensor from sensor manager
                        pSensorManager.DeleteSensor(pSensorObj);
                        pSensorObj = null;
                    }
                }
                catch
                {
                    // Error occurred while closing the sensor
                    retValue = false;
                }
            }

            return retValue;
        }

        /// <summary>
        /// Get sensor data
        /// </summary>
        /// <returns>Resutns sensor data collection</returns>
        public SensorDataCollection GetSensorData()
        {
            if (pSensorObj != null)
            {
                return pSensorObj.GetSensorData();
            }

            return null;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Set the sample rate to RATE_1_HZ if the sensor supports sampling based on rate.
        /// </summary>
        private void SetSampleRate()
        {
            SAMPLE_RATE rate = SAMPLE_RATE.RATE_1_HZ;

            try
            {
                if (pSensorObj.Device.SamplingType == SAMPLING_TYPE.ON_RATE)
                {
                    switch (pSensorObj.Device.SensorType)
                    {
                        case SENSOR_TYPE.ACCELEROMETER:
                            ((Accelerometer)pSensorObj).Config.SampleRate = rate;
                            break;

                        case SENSOR_TYPE.TILT_ANGLE:
                            ((TiltAngle)pSensorObj).Config.SampleRate = rate;
                            break;

                        case SENSOR_TYPE.ECOMPASS:
                            ((ECompass)pSensorObj).Config.SampleRate = rate;
                            break;

                        case SENSOR_TYPE.TEMPERATURE:
                            ((Temperature)pSensorObj).Config.SampleRate = rate;
                            break;

                        case SENSOR_TYPE.HUMIDITY:
                            ((Humidity)pSensorObj).Config.SampleRate = rate;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch
            {
                // Ignore if any error occurs while setting sample rate and continue to use default sample rate.
            }
        }

        #endregion Private Methods
    }
}
