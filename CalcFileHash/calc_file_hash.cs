using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CalcFileHash
{
  public partial class calc_file_hash_form : Form
  {
    public calc_file_hash_form()
    {
      InitializeComponent();

      this.hash_type_cmb.SelectedIndex = 0;
    }

    private void browse_button_Click(object sender, EventArgs e)
    {
      OpenFileDialog file_dialog = new OpenFileDialog();
      if (file_dialog.ShowDialog() == DialogResult.OK) {
        file_path_textbox.Text = file_dialog.FileName;
      }
    }

    private void file_path_textbox_TextChanged(object sender, EventArgs e)
    {
      calc_hash();
    }

    private void hash_type_cmb_SelectedIndexChanged(object sender, EventArgs e)
    {
      calc_hash();
    }

    private void actual_hash_textbox_TextChanged(object sender, EventArgs e)
    {
      set_hash_match_color();
    }

    private void expect_hash_textbox_TextChanged(object sender, EventArgs e)
    {
      set_hash_match_color();
    }

    private void calc_hash()
    {
      actual_hash_textbox.Text = calc_hash(file_path_textbox.Text, hash_type_cmb.Text);
    }

    private string calc_hash(string file_path, string hash_type)
    {
      try {
        HashAlgorithm hasher = create_hasher(hash_type);
        FileStream file = new FileStream(file_path, FileMode.Open);
        byte[] hash_value = hasher.ComputeHash(file);
        file.Close();
        return format_hash(hash_value);
      } catch (Exception) {
        return "";
      }
    }

    private HashAlgorithm create_hasher(string type)
    {
      if (string.Compare(type, "SHA-1", true) == 0) {
        return SHA1.Create();
      }

      if (string.Compare(type, "SHA-256", true) == 0) {
        return SHA256.Create();
      }

      return MD5.Create();
    }

    private string format_hash(byte[] hash_value)
    {
      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < hash_value.Length; i++) {
        builder.Append(hash_value[i].ToString("x2"));
      }

      return builder.ToString();
    }

    private bool is_hash_matched()
    {
      string actual_hash = actual_hash_textbox.Text.Trim();
      string expect_hash = expect_hash_textbox.Text.Trim();

      if ((actual_hash == "") || (expect_hash == "")) {
        return true;
      }

      return (string.Compare(actual_hash, expect_hash, true) == 0);
    }

    private void set_hash_match_color()
    {
      if (is_hash_matched()) {
        actual_hash_textbox.ForeColor = System.Drawing.SystemColors.WindowText;
        actual_hash_textbox.BackColor = System.Drawing.SystemColors.Control;
        return;
      }

      actual_hash_textbox.ForeColor = System.Drawing.Color.White;
      actual_hash_textbox.BackColor = System.Drawing.Color.OrangeRed;
    }
  }
}
