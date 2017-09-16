using System;


namespace CS_RFID2_Host_Sample
{
	/// <summary>
	/// Summary description for ValidationException.
	/// </summary>
	public class ValidationException : System.ApplicationException 
	{
		public ValidationException(string msg):base(msg)
		{
			
		}
	}
}
