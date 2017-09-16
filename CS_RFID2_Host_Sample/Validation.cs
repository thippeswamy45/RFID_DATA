using System;
using System.Collections;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CS_RFID2_Host_Sample
{
	/// <summary>
	/// Summary description for Validation.
	/// </summary>
	public class Validation
	{
		
		#region Private Variables
		private	ArrayList asciiListNum= new ArrayList(); 
		private ArrayList asciiListAlphaNum= new ArrayList();
		private ArrayList asciiListByte= new ArrayList();
		#endregion
		
		#region Construtor
		public Validation()
		{
			generateAlphaNumList();
			generateNumList();
			generateByte();
		}
		#endregion

		#region IsValid Methods

		public void IsValidNumber(string paramNumber,out string validationResult)
		{
			string patternNum = "[0-9]*";
			string patternFloatNum =@"([0-9]*[.][0-9]*)";
			 validationResult="";
			//For Number
				if( paramNumber.Trim().Equals(string.Empty))
					validationResult+="Please provide ";
				else if( new Regex(patternNum).Match(paramNumber).Length != paramNumber.Length && new Regex(patternFloatNum).Match(paramNumber).Length != paramNumber.Length)
					validationResult+="Should Be Numeric Value"+Environment.NewLine;
		}
		public void IsValidAlphaNum(string paramString,out string validationResult )
		{
			string patternAlphaNum = @"((\w)*(\s)*(\w)*[.]*)*";
			validationResult="";
			//For AlphaNumeric
			if( paramString.Trim().Equals(string.Empty))
				validationResult+="Please provide ";
			if( new Regex(patternAlphaNum).Match(paramString).Length != paramString.Length)
				validationResult+="Not in Proper format"+Environment.NewLine;
			
		}
		public void IsValidPhoneNumber(string paramPhoneNumber,out string validationResult)
		{
			string patternPhoneNumber = @"\((?<AreaCode>\d{3})\)\s*(?<Number>\d{7})(?x)";  
			validationResult="";
			//For PhoneNumber
			if( paramPhoneNumber.Trim().Equals(string.Empty))
				validationResult+="Please provide ";
			if(new Regex(patternPhoneNumber).Match(paramPhoneNumber).Length != paramPhoneNumber.Length)
				validationResult+="Not in Proper Format"+Environment.NewLine+"Phone Number Format- Area Code of 3 digit and Phone Number of 7 digit ";
			
		}
		public void IsValidSCACFormat(string paramSCAC,out string validationResult)
		{
			string patternSCAC="[a-zA-Z]{2,4}";
			validationResult = "";
			if( paramSCAC.Trim().Equals(string.Empty))
				validationResult+="Please provide ";
			if(new Regex(patternSCAC).Match(paramSCAC).Length != paramSCAC.Length)
				validationResult+="Not in Proper Format"+Environment.NewLine+"SCAC is two-to-four-letter code";
		}

		public void IsNotEmpty(string paramField,out string validationResult)
		{
			validationResult = "";
			if(paramField.Equals(string.Empty))
				validationResult="Please provide ";
		}

		public void IsValidIPAddress(string paramIpAddress,out string validationResult)
		{
			validationResult="";
			if( paramIpAddress.Trim().Equals(string.Empty))
				validationResult+="Please provide ";
			else
			{
				string[] strIP = paramIpAddress.Trim().Split('.');
				if(strIP.Length == 4)
					for(int i= 0; i< strIP.Length ;i++)
					{
						if(!( Convert.ToInt16(strIP[i])>=0 && Convert.ToInt16(strIP[i])<256))
							validationResult+="Provide proper "+Environment.NewLine;
					}
				else
					validationResult+="Provide proper "+Environment.NewLine;
			}

		}
		public void IsValidByte(string paramByteStr,out string validationResult)
		{
			validationResult = "";
			try
			{
					byte paramByte = Convert.ToByte(paramByteStr,10);
			}
			catch
			{
				validationResult+="value should be in range of 0-255 "+Environment.NewLine;
			}
		
		}
		#endregion

		#region Populate List for Key Press Events
		public void generateNumList()
		{
			asciiListNum.Add(8);
			for(int i=48;i<58;i++)
				asciiListNum.Add(i);
		}
		public void generateByte()
		{
			asciiListByte.Add(8);
			for(int i=48;i<58;i++)
				asciiListByte.Add(i);
		}
		public void generateAlphaNumList()
		{
			asciiListAlphaNum.Add(8);
			asciiListAlphaNum.Add(46);
			asciiListAlphaNum.Add(32);
			asciiListAlphaNum.Add(34);
			asciiListAlphaNum.Add(35);
			asciiListAlphaNum.Add(36);
			asciiListAlphaNum.Add(40);
			asciiListAlphaNum.Add(41);
			asciiListAlphaNum.Add(43);
			asciiListAlphaNum.Add(44);
			asciiListAlphaNum.Add(42);
			asciiListAlphaNum.Add(45);
			asciiListAlphaNum.Add(47);
			asciiListAlphaNum.Add(58);
			asciiListAlphaNum.Add(60);
			asciiListAlphaNum.Add(62);
			asciiListAlphaNum.Add(64);
			asciiListAlphaNum.Add(95);
			asciiListAlphaNum.Add(96);
			for(int i=48;i<58;i++)
				asciiListAlphaNum.Add(i);
			for(int i=65;i<91;i++)
				asciiListAlphaNum.Add(i);
			for(int i=97;i<123;i++)
				asciiListAlphaNum.Add(i);
		}
		#endregion

		#region Methods for KeyPress Events

		public void DisableKeysForNumbers(KeyPressEventArgs e)
		{
			if (!asciiListNum.Contains((int)e.KeyChar) && e.KeyChar != 46 )
				e.Handled = true ;
		}
		public void DisableKeysForPhoneNumber(KeyPressEventArgs e)
		{
			if (!asciiListNum.Contains((int)e.KeyChar))
				e.Handled = true ;
		}
		public void DisableKeysForAlphaNum(KeyPressEventArgs e)
		{
			if	(!asciiListAlphaNum.Contains((int)e.KeyChar) )
				e.Handled = true ;
		}
		public void DisableKeysForByte(KeyPressEventArgs e)
		{
			if	(!asciiListByte.Contains((int)e.KeyChar) )
				e.Handled = true ;
		}
		#endregion



	}
}
