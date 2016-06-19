using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CascIt_Console
{
	using System;
	using System.Web;

	public class ClientInfo
	{
		private readonly HttpRequestBase _request;

		public ClientInfo(HttpRequestBase request)
		{
			if(request == null)
				throw new ArgumentNullException("request");

			_request = request;
		}

		public string AnonymousId
		{
			get { return _request.AnonymousID; }
		}

		public string AppRelativeCurrentExecutionFilePath
		{
			get { return _request.AppRelativeCurrentExecutionFilePath; }
		}

		public string ApplicationPath
		{
			get { return _request.ApplicationPath; }
		}

		public int ContentLength
		{
			get { return _request.ContentLength; }
		}

		public string ContentType
		{
			get { return _request.ContentType; }
		}

		public string CurrentExecutionFilePath
		{
			get { return _request.CurrentExecutionFilePath; }
		}

		public string CurrentExecutionFilePathExtension
		{
			get { return _request.CurrentExecutionFilePathExtension; }
		}

		public string FilePath
		{
			get { return _request.FilePath; }
		}

		public string HttpMethod
		{
			get { return _request.HttpMethod; }
		}

		public bool IsAuthenticated
		{
			get { return _request.IsAuthenticated; }
		}

		public bool IsLocal
		{
			get { return _request.IsLocal; }
		}

		public bool IsSecureConnection
		{
			get { return _request.IsSecureConnection; }
		}

		public string Path
		{
			get { return _request.Path; }
		}

		public string PathInfo
		{
			get { return _request.PathInfo; }
		}

		public string PhysicalApplicationPath
		{
			get { return _request.PhysicalApplicationPath; }
		}

		public string PhysicalPath
		{
			get { return _request.PhysicalPath; }
		}

		public string RawUrl
		{
			get { return _request.RawUrl; }
		}

		public string ReadEntityBodyMode
		{
			get { return _request.ReadEntityBodyMode.ToString(); }
		}

		public string RequestType
		{
			get { return _request.RequestType; }
		}

		public int TotalBytes
		{
			get { return _request.TotalBytes; }
		}

		public string UserAgent
		{
			get { return _request.UserAgent; }
		}

		public string UserHostAddress
		{
			get { return _request.UserHostAddress; }
		}

		public string UserHostName
		{
			get { return _request.UserHostName; }
		}

		public string Browser
		{
			get { return _request.Browser.Browser; }
		}

		public bool IsCrawer
		{
			get { return _request.Browser.Crawler; }
		}

		public string BrowserId
		{
			get { return _request.Browser.Id; }
		}

		public string BrowserInputType
		{
			get { return _request.Browser.InputType; }
		}

		public bool IsMobileDevice
		{
			get { return _request.Browser.IsMobileDevice; }
		}

		public int BrowserMajorVersion
		{
			get { return _request.Browser.MajorVersion; }
		}

		public double BrowserMinorVersion
		{
			get { return _request.Browser.MinorVersion; }
		}

		public string MobileDeviceManufacturer
		{
			get { return _request.Browser.MobileDeviceManufacturer; }
		}

		public string MobileDeviceModel
		{
			get { return _request.Browser.MobileDeviceModel; }
		}

		public int NumberOfSoftKeys
		{
			get { return _request.Browser.NumberOfSoftkeys; }
		}

		public string BrowserPlatform
		{
			get { return _request.Browser.Platform; }
		}

		public string BrowserType
		{
			get { return _request.Browser.Type; }
		}

		public string BrowserVersion
		{
			get { return _request.Browser.Version; }
		}

		public bool IsWin16
		{
			get { return _request.Browser.Win16; }
		}

		public bool IsWin32
		{
			get { return _request.Browser.Win32; }
		}

		public string AuthenticationType
		{
			get
			{
				return _request.LogonUserIdentity != null ? _request.LogonUserIdentity.AuthenticationType : null;
			}
		}

		public bool? IsAnonymous
		{
			get
			{
				return _request.LogonUserIdentity != null ? (bool?)_request.LogonUserIdentity.IsAnonymous : null;
			}
		}

		public bool? LoginUserIsAuthenticated
		{
			get
			{
				return _request.LogonUserIdentity != null ? (bool?)_request.LogonUserIdentity.IsAuthenticated : null;
			}
		}

		public bool? IsGuest
		{
			get
			{
				return _request.LogonUserIdentity != null ? (bool?)_request.LogonUserIdentity.IsGuest : null;
			}
		}

		public bool? IsSystem
		{
			get
			{
				return _request.LogonUserIdentity != null ? (bool?)_request.LogonUserIdentity.IsSystem : null;
			}
		}

		public string LoginUserName
		{
			get
			{
				return _request.LogonUserIdentity != null ? _request.LogonUserIdentity.Name : null;
			}
		}

		public string Url
		{
			get
			{
				return _request.Url != null ? _request.Url.AbsoluteUri : null;
			}
		}

		public string UrlPath
		{
			get
			{
				return _request.Url != null ? _request.Url.AbsolutePath : null;
			}
		}

		public string ReferrerUrl
		{
			get
			{
				return _request.UrlReferrer != null ? _request.UrlReferrer.AbsoluteUri : null;
			}
		}

		public string ReferrerUrlPath
		{
			get
			{
				return _request.UrlReferrer != null ? _request.UrlReferrer.AbsolutePath : null;
			}
		}

		public string HostName
		{
			get
			{
				IPAddress ipAddress = IPAddress.Parse(UserHostAddress);
				IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
				return hostEntry.HostName;
			}
		}

		public string ComputerName
		{
			get
			{
				string[] values = HostName.Split('.');
				return values.First();
			}
		}
	}
}