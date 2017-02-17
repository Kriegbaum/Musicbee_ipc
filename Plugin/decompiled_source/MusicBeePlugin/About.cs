using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MusicBeePlugin
{
	internal class About : Form
	{
		private IContainer components;

		private TableLayoutPanel tableLayoutPanel;

		private Label labelProductName;

		private Label labelVersion;

		private Label labelCopyright;

		private TextBox textBoxDescription;

		private Button okButton;

		public string AssemblyTitle
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (customAttributes.Length > 0)
				{
					AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
					if (assemblyTitleAttribute.Title != "")
					{
						return assemblyTitleAttribute.Title;
					}
				}
				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyDescription
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)customAttributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
			}
		}

		public About()
		{
			this.InitializeComponent();
			this.Text = string.Format("About {0}", this.AssemblyTitle);
			this.labelProductName.Text = this.AssemblyProduct;
			this.labelVersion.Text = string.Format("Version {0}", this.AssemblyVersion);
			this.labelCopyright.Text = this.AssemblyCopyright;
			this.textBoxDescription.Text = this.AssemblyDescription;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.tableLayoutPanel = new TableLayoutPanel();
			this.labelProductName = new Label();
			this.labelVersion = new Label();
			this.labelCopyright = new Label();
			this.textBoxDescription = new TextBox();
			this.okButton = new Button();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.labelProductName, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelVersion, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.labelCopyright, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.okButton, 0, 4);
			this.tableLayoutPanel.Dock = DockStyle.Fill;
			this.tableLayoutPanel.Location = new Point(9, 9);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 5;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(286, 184);
			this.tableLayoutPanel.TabIndex = 0;
			this.labelProductName.Dock = DockStyle.Fill;
			this.labelProductName.Location = new Point(6, 0);
			this.labelProductName.Margin = new Padding(6, 0, 3, 0);
			this.labelProductName.MaximumSize = new Size(0, 17);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new Size(277, 17);
			this.labelProductName.TabIndex = 19;
			this.labelProductName.Text = "Product Name";
			this.labelProductName.TextAlign = ContentAlignment.MiddleLeft;
			this.labelVersion.Dock = DockStyle.Fill;
			this.labelVersion.Location = new Point(6, 17);
			this.labelVersion.Margin = new Padding(6, 0, 3, 0);
			this.labelVersion.MaximumSize = new Size(0, 17);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new Size(277, 17);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Version";
			this.labelVersion.TextAlign = ContentAlignment.MiddleLeft;
			this.labelCopyright.Dock = DockStyle.Fill;
			this.labelCopyright.Location = new Point(6, 34);
			this.labelCopyright.Margin = new Padding(6, 0, 3, 0);
			this.labelCopyright.MaximumSize = new Size(0, 17);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new Size(277, 17);
			this.labelCopyright.TabIndex = 21;
			this.labelCopyright.Text = "Copyright";
			this.labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
			this.textBoxDescription.Dock = DockStyle.Fill;
			this.textBoxDescription.Location = new Point(6, 54);
			this.textBoxDescription.Margin = new Padding(6, 3, 3, 3);
			this.textBoxDescription.Multiline = true;
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.ReadOnly = true;
			this.textBoxDescription.ScrollBars = ScrollBars.Both;
			this.textBoxDescription.Size = new Size(277, 84);
			this.textBoxDescription.TabIndex = 23;
			this.textBoxDescription.TabStop = false;
			this.textBoxDescription.Text = "Description";
			this.okButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.okButton.DialogResult = DialogResult.Cancel;
			this.okButton.Location = new Point(208, 158);
			this.okButton.Name = "okButton";
			this.okButton.Size = new Size(75, 23);
			this.okButton.TabIndex = 24;
			this.okButton.Text = "&OK";
			this.okButton.Click += new EventHandler(this.okButton_Click);
			base.AcceptButton = this.okButton;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(304, 202);
			base.Controls.Add(this.tableLayoutPanel);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "About";
			base.Padding = new Padding(9);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "About";
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
