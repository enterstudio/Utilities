using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.ComponentModel;

namespace ShomreiTorah.UpdatePublisher {
	/// <summary>
	/// Provides static methods to read system icons for both folders and files.
	/// </summary>
	/// <example>
	/// <code>IconReader.GetFileIcon("c:\\general.xls");</code>
	/// </example>
	static class IconReader {
		/// <summary>
		/// Returns an icon for a given file - indicated by the name parameter.
		/// </summary>
		/// <param name="name">Pathname for file.</param>
		/// <param name="size">Large or small</param>
		/// <param name="linkOverlay">Whether to include the link icon</param>
		/// <returns>System.Drawing.Icon</returns>
		public static Icon GetFileIcon(string name, IconSize size, bool linkOverlay) {
			NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
			uint flags = NativeMethods.SHGFI_ICON | NativeMethods.SHGFI_USEFILEATTRIBUTES;

			if (linkOverlay)
				flags += NativeMethods.SHGFI_LINKOVERLAY;

			flags += size == IconSize.Small ? NativeMethods.SHGFI_SMALLICON : NativeMethods.SHGFI_LARGEICON;


			NativeMethods.SHGetFileInfo(name,
				NativeMethods.FILE_ATTRIBUTE_NORMAL,
				ref shfi,
				(uint)Marshal.SizeOf(shfi),
				flags);

			// Copy (clone) the returned icon to a new object, thus allowing us to clean-up properly
			Icon icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();
			if (NativeMethods.DestroyIcon(shfi.hIcon) == 0)		// Cleanup
				throw new Win32Exception();
			return icon;
		}

		/// <summary>
		/// Used to access system folder icons.
		/// </summary>
		/// <param name="size">Specify large or small icons.</param>
		/// <param name="folderType">Specify open or closed FolderType.</param>
		/// <returns>System.Drawing.Icon</returns>
		public static Icon GetFolderIcon(IconSize size, FolderType folderType) {
			// Need to add size check, although errors generated at present!
			uint flags = NativeMethods.SHGFI_ICON | NativeMethods.SHGFI_USEFILEATTRIBUTES;

			if (folderType == FolderType.Open)
				flags += NativeMethods.SHGFI_OPENICON;

			flags += size == IconSize.Small ? NativeMethods.SHGFI_SMALLICON : NativeMethods.SHGFI_LARGEICON;

			// Get the folder icon
			NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
			NativeMethods.SHGetFileInfo(null,
				NativeMethods.FILE_ATTRIBUTE_DIRECTORY,
				ref shfi,
				(uint)System.Runtime.InteropServices.Marshal.SizeOf(shfi),
				flags);


			// Now clone the icon, so that it can be successfully stored in an ImageList
			var icon =  (Icon)Icon.FromHandle(shfi.hIcon).Clone();

			if (NativeMethods.DestroyIcon(shfi.hIcon) == 0)		// Cleanup
				throw new Win32Exception();
			return icon;
		}
	}
	/// <summary>
	/// Options to specify the size of icons to return.
	/// </summary>
	enum IconSize {
		/// <summary>
		/// Specify large icon - 32 pixels by 32 pixels.
		/// </summary>
		Large = 0,
		/// <summary>
		/// Specify small icon - 16 pixels by 16 pixels.
		/// </summary>
		Small = 1
	}

	/// <summary>
	/// Options to specify whether folders should be in the open or closed state.
	/// </summary>
	enum FolderType {
		/// <summary>
		/// Specify open folder.
		/// </summary>
		Open = 0,
		/// <summary>
		/// Specify closed folder.
		/// </summary>
		Closed = 1
	}

	/// <summary>
	/// Wraps necessary NativeMethods.dll structures and functions required to retrieve Icon Handles using SHGetFileInfo. Code
	/// courtesy of MSDN Cold Rooster Consulting case study.
	/// </summary>
	/// 

	// This code has been left largely untouched from that in the CRC example. The main changes have been moving
	// the icon reading code over to the IconReader type.
	static class NativeMethods {

		public const int MAX_PATH = 256;
		[StructLayout(LayoutKind.Sequential)]
		public struct SHITEMID {
			public ushort cb;
			[MarshalAs(UnmanagedType.LPArray)]
			public byte[] abID;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ITEMIDLIST {
			public SHITEMID mkid;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct BROWSEINFO {
			public IntPtr hwndOwner;
			public IntPtr pidlRoot;
			public IntPtr pszDisplayName;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpszTitle;
			public uint ulFlags;
			public IntPtr lpfn;
			public int lParam;
			public IntPtr iImage;
		}

		// Browsing for directory.
		public const uint BIF_RETURNONLYFSDIRS = 0x0001;
		public const uint BIF_DONTGOBELOWDOMAIN = 0x0002;
		public const uint BIF_STATUSTEXT = 0x0004;
		public const uint BIF_RETURNFSANCESTORS = 0x0008;
		public const uint BIF_EDITBOX = 0x0010;
		public const uint BIF_VALIDATE = 0x0020;
		public const uint BIF_NEWDIALOGSTYLE = 0x0040;
		public const uint BIF_USENEWUI = (BIF_NEWDIALOGSTYLE | BIF_EDITBOX);
		public const uint BIF_BROWSEINCLUDEURLS = 0x0080;
		public const uint BIF_BROWSEFORCOMPUTER = 0x1000;
		public const uint BIF_BROWSEFORPRINTER = 0x2000;
		public const uint BIF_BROWSEINCLUDEFILES = 0x4000;
		public const uint BIF_SHAREABLE = 0x8000;

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO {
			public const int NAMESIZE = 80;
			public IntPtr hIcon;
			public int iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAMESIZE)]
			public string szTypeName;
		};

		public const uint SHGFI_ICON = 0x000000100;     // get icon
		public const uint SHGFI_DISPLAYNAME = 0x000000200;     // get display name
		public const uint SHGFI_TYPENAME = 0x000000400;     // get type name
		public const uint SHGFI_ATTRIBUTES = 0x000000800;     // get attributes
		public const uint SHGFI_ICONLOCATION = 0x000001000;     // get icon location
		public const uint SHGFI_EXETYPE = 0x000002000;     // return exe type
		public const uint SHGFI_SYSICONINDEX = 0x000004000;     // get system icon index
		public const uint SHGFI_LINKOVERLAY = 0x000008000;     // put a link overlay on icon
		public const uint SHGFI_SELECTED = 0x000010000;     // show icon in selected state
		public const uint SHGFI_ATTR_SPECIFIED = 0x000020000;     // get only specified attributes
		public const uint SHGFI_LARGEICON = 0x000000000;     // get large icon
		public const uint SHGFI_SMALLICON = 0x000000001;     // get small icon
		public const uint SHGFI_OPENICON = 0x000000002;     // get open icon
		public const uint SHGFI_SHELLICONSIZE = 0x000000004;     // get shell size icon
		public const uint SHGFI_PIDL = 0x000000008;     // pszPath is a pidl
		public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;     // use passed dwFileAttribute
		public const uint SHGFI_ADDOVERLAYS = 0x000000020;     // apply the appropriate overlays
		public const uint SHGFI_OVERLAYINDEX = 0x000000040;     // Get the index of the overlay

		public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
		public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

		[DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
		/// <summary>
		/// Provides access to function required to delete handle. This method is used internally
		/// and is not required to be called separately.
		/// </summary>
		/// <param name="hIcon">Pointer to icon handle.</param>
		/// <returns>N/A</returns>
		[DllImport("User32.dll", SetLastError = true)]
		public static extern int DestroyIcon(IntPtr hIcon);
	}
}