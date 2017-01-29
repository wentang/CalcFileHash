namespace CalcFileHash
{
  partial class calc_file_hash_form
  {
    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要修改
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
      this.browse_button = new System.Windows.Forms.Button();
      this.actual_label = new System.Windows.Forms.Label();
      this.expect_label = new System.Windows.Forms.Label();
      this.file_path_textbox = new System.Windows.Forms.TextBox();
      this.actual_hash_textbox = new System.Windows.Forms.TextBox();
      this.expect_hash_textbox = new System.Windows.Forms.TextBox();
      this.hash_type_cmb = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // browse_button
      // 
      this.browse_button.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.browse_button.Location = new System.Drawing.Point(470, 10);
      this.browse_button.Name = "browse_button";
      this.browse_button.Size = new System.Drawing.Size(82, 24);
      this.browse_button.TabIndex = 2;
      this.browse_button.Text = "&browse...";
      this.browse_button.UseVisualStyleBackColor = true;
      this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
      // 
      // actual_label
      // 
      this.actual_label.AutoSize = true;
      this.actual_label.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.actual_label.Location = new System.Drawing.Point(12, 53);
      this.actual_label.Name = "actual_label";
      this.actual_label.Size = new System.Drawing.Size(47, 12);
      this.actual_label.TabIndex = 3;
      this.actual_label.Text = "Actual:";
      // 
      // expect_label
      // 
      this.expect_label.AutoSize = true;
      this.expect_label.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.expect_label.Location = new System.Drawing.Point(12, 82);
      this.expect_label.Name = "expect_label";
      this.expect_label.Size = new System.Drawing.Size(47, 12);
      this.expect_label.TabIndex = 5;
      this.expect_label.Text = "Expect:";
      // 
      // file_path_textbox
      // 
      this.file_path_textbox.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.file_path_textbox.Location = new System.Drawing.Point(96, 12);
      this.file_path_textbox.Name = "file_path_textbox";
      this.file_path_textbox.Size = new System.Drawing.Size(369, 21);
      this.file_path_textbox.TabIndex = 1;
      this.file_path_textbox.TextChanged += new System.EventHandler(this.file_path_textbox_TextChanged);
      // 
      // actual_hash_textbox
      // 
      this.actual_hash_textbox.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.actual_hash_textbox.Location = new System.Drawing.Point(65, 50);
      this.actual_hash_textbox.Name = "actual_hash_textbox";
      this.actual_hash_textbox.ReadOnly = true;
      this.actual_hash_textbox.Size = new System.Drawing.Size(487, 21);
      this.actual_hash_textbox.TabIndex = 4;
      this.actual_hash_textbox.TextChanged += new System.EventHandler(this.actual_hash_textbox_TextChanged);
      // 
      // expect_hash_textbox
      // 
      this.expect_hash_textbox.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.expect_hash_textbox.Location = new System.Drawing.Point(65, 78);
      this.expect_hash_textbox.Name = "expect_hash_textbox";
      this.expect_hash_textbox.Size = new System.Drawing.Size(487, 21);
      this.expect_hash_textbox.TabIndex = 6;
      this.expect_hash_textbox.TextChanged += new System.EventHandler(this.expect_hash_textbox_TextChanged);
      // 
      // hash_type_cmb
      // 
      this.hash_type_cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.hash_type_cmb.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.hash_type_cmb.FormattingEnabled = true;
      this.hash_type_cmb.Items.AddRange(new object[] {
            "MD5",
            "SHA-1",
            "SHA-256"});
      this.hash_type_cmb.Location = new System.Drawing.Point(12, 12);
      this.hash_type_cmb.Name = "hash_type_cmb";
      this.hash_type_cmb.Size = new System.Drawing.Size(78, 20);
      this.hash_type_cmb.Sorted = true;
      this.hash_type_cmb.TabIndex = 0;
      this.hash_type_cmb.SelectedIndexChanged += new System.EventHandler(this.hash_type_cmb_SelectedIndexChanged);
      // 
      // calc_file_hash_form
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(564, 112);
      this.Controls.Add(this.hash_type_cmb);
      this.Controls.Add(this.expect_hash_textbox);
      this.Controls.Add(this.actual_hash_textbox);
      this.Controls.Add(this.file_path_textbox);
      this.Controls.Add(this.expect_label);
      this.Controls.Add(this.actual_label);
      this.Controls.Add(this.browse_button);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "calc_file_hash_form";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Calculate File Hash";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button browse_button;
    private System.Windows.Forms.Label actual_label;
    private System.Windows.Forms.Label expect_label;
    private System.Windows.Forms.TextBox file_path_textbox;
    private System.Windows.Forms.TextBox actual_hash_textbox;
    private System.Windows.Forms.TextBox expect_hash_textbox;
    private System.Windows.Forms.ComboBox hash_type_cmb;
  }
}

