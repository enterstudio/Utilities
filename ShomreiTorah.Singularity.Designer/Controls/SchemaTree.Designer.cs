namespace ShomreiTorah.Singularity.Designer.Controls {
	partial class SchemaTree {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule treeListFormatRule1 = new DevExpress.XtraTreeList.StyleFormatConditions.TreeListFormatRule();
			DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
			this.tree = new DevExpress.XtraTreeList.TreeList();
			this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colSqlSchema = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colIsDisabled = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			((System.ComponentModel.ISupportInitialize)(this.tree)).BeginInit();
			this.SuspendLayout();
			// 
			// tree
			// 
			this.tree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName,
            this.colSqlSchema,
            this.colIsDisabled});
			this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
			treeListFormatRule1.ApplyToRow = true;
			treeListFormatRule1.Name = "Disabled";
			formatConditionRuleValue1.Appearance.ForeColor = System.Drawing.SystemColors.GrayText;
			formatConditionRuleValue1.Appearance.Options.UseForeColor = true;
			formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
			formatConditionRuleValue1.Value1 = true;
			treeListFormatRule1.Rule = formatConditionRuleValue1;
			this.tree.FormatRules.Add(treeListFormatRule1);
			this.tree.Location = new System.Drawing.Point(0, 0);
			this.tree.Name = "tree";
			this.tree.Size = new System.Drawing.Size(418, 312);
			this.tree.TabIndex = 0;
			this.tree.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tree_FocusedNodeChanged);
			this.tree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tree_MouseDoubleClick);
			// 
			// colName
			// 
			this.colName.Caption = "Name";
			this.colName.FieldName = "Name";
			this.colName.Name = "colName";
			this.colName.OptionsColumn.AllowEdit = false;
			this.colName.OptionsColumn.AllowFocus = false;
			this.colName.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.colName.Visible = true;
			this.colName.VisibleIndex = 0;
			// 
			// colSqlSchema
			// 
			this.colSqlSchema.Caption = "SQL Schema";
			this.colSqlSchema.FieldName = "SqlSchemaName";
			this.colSqlSchema.Name = "colSqlSchema";
			this.colSqlSchema.OptionsColumn.AllowEdit = false;
			this.colSqlSchema.OptionsColumn.AllowFocus = false;
			this.colSqlSchema.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.colSqlSchema.Visible = true;
			this.colSqlSchema.VisibleIndex = 1;
			// 
			// colIsDisabled
			// 
			this.colIsDisabled.Caption = "treeListColumn1";
			this.colIsDisabled.FieldName = "IsDisabled";
			this.colIsDisabled.Name = "colIsDisabled";
			this.colIsDisabled.OptionsColumn.ShowInCustomizationForm = false;
			// 
			// SchemaTree
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tree);
			this.Name = "SchemaTree";
			this.Size = new System.Drawing.Size(418, 312);
			((System.ComponentModel.ISupportInitialize)(this.tree)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraTreeList.TreeList tree;
		private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
		private DevExpress.XtraTreeList.Columns.TreeListColumn colSqlSchema;
		private DevExpress.XtraTreeList.Columns.TreeListColumn colIsDisabled;
	}
}
