using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;
using System.IO;
using DevExpress.XtraTreeList;
using System.Diagnostics.CodeAnalysis;

namespace ShomreiTorah.UpdatePublisher {
	partial class UpdateDetails : XtraUserControl {
		public UpdateDetails() {
			InitializeComponent();
		}
		public void SetData(UpdateKind kind, Version version, string description, string baseDir) {
			caption.Text = (kind == UpdateKind.Old ? "Existing version: " : "New version: ") + version.ToString();
			descriptionText.Text = description;
			descriptionText.Properties.ReadOnly = kind == UpdateKind.Old;

			var filesData = new List<UpdateFile>(Directory.GetFiles(baseDir, "*.*", SearchOption.AllDirectories).Select(p => new UpdateFile(p)));
			filesData.AddRange(Directory.GetDirectories(baseDir, "*.*", SearchOption.AllDirectories).Select(p => new UpdateFile(p)));

			files.RootValue = baseDir;
			files.DataSource = filesData;
		}
		protected override void OnVisibleChanged(EventArgs e) {
			base.OnVisibleChanged(e);
			files.BestFitColumns();
		}

		public string Description { get { return descriptionText.Text; } }

		sealed class UpdateFile {
			public UpdateFile(string path) {
				FullPath = path;
				Size = Directory.Exists(path) ? -1 : (int)new FileInfo(path).Length;
			}

			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Data Binding")]
			public string ParentDirectory { get { return Path.GetDirectoryName(FullPath); } }
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Data Binding")]
			public string Name { get { return Path.GetFileName(FullPath); } }
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Data Binding")]
			public string Extension { get { return Path.GetExtension(FullPath); } }

			public string FullPath { get; private set; }
			public int Size { get; private set; }
			public string SizeString { get { return Size == -1 ? "" : ToSizeString(Size); } }
		}
		static string ToSizeString(double bytes) {
			var culture = CultureInfo.CurrentUICulture;
			const string format = "#,0.0";

			if (bytes < 1024)
				return bytes.ToString("#,0", culture) + " bytes";
			bytes /= 1024;
			if (bytes < 1024)
				return bytes.ToString(format, culture) + " KB";
			bytes /= 1024;
			if (bytes < 1024)
				return bytes.ToString(format, culture) + " MB";
			bytes /= 1024;
			return bytes.ToString(format, culture) + " GB";
		}

		Dictionary<string, int> loadedIcons = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

		private void files_GetStateImage(object sender, GetStateImageEventArgs e) {
			var file = files.GetDataRecordByNode(e.Node) as UpdateFile;

			int index;

			if (!loadedIcons.TryGetValue(file.FullPath, out index)) {
				using (var icon = IconReader.GetFileIcon(file.FullPath, IconSize.Small, false))
					index = loadedIcons[file.FullPath] = icons.Images.Add(icon.ToBitmap());
			}
			e.NodeImageIndex = index;
		}

		private void files_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e) {
			if (e.Column == colSize)
				e.Value = (files.GetDataRecordByNode(e.Node) as UpdateFile).SizeString;
		}
	}
	enum UpdateKind {
		Old, New
	}
}