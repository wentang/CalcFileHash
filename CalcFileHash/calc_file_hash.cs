using System;
using System.Diagnostics;
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
      TimeSpan start = Process.GetCurrentProcess().TotalProcessorTime;
      Stopwatch stw = new Stopwatch();
      stw.Start();

      actual_hash_textbox.Text = calc_hash(file_path_textbox.Text, hash_type_cmb.Text);

      stw.Stop();
      double elaps = Process.GetCurrentProcess().TotalProcessorTime.Subtract(start).TotalMilliseconds;

      calc_performance(file_path_textbox.Text, elaps, stw.Elapsed.TotalMilliseconds);
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

    private void calc_performance(string file_path, double cpu_time, double clock_time)
    {
      try {
        FileInfo file_info = new FileInfo(file_path);
        performance_textbox.Text = string.Format("  file size: {0}\r\n", format_file_size(file_info.Length));
        performance_textbox.Text += string.Format("   cpu time: {0}\r\n", format_hash_time(cpu_time));
        performance_textbox.Text += string.Format(" clock time: {0}\r\n", format_hash_time(clock_time));
      } catch {
        performance_textbox.Text = "";
      }
    }

    private string format_file_size(long file_size)
    {
      int unit_index = 0;
      double output_size = file_size;
      while ((output_size / 1024) > 9) {
        output_size /= 1024;
        ++unit_index;
      }

      string[] unit = new string[] { "B", "KB", "MB", "GB", "TB" };
      return string.Format("{0:N0} {1}", output_size, unit[unit_index]);
    }

    private string format_hash_time(double hash_time)
    {
      int unit_index = 0;
      double output_time = hash_time;
      if (output_time > 1000) {
        ++unit_index;
        output_time /= 1000;
      }

      string[] unit = new string[] { "ms", "s" };
      return string.Format("{0:F2} {1}", output_time, unit[unit_index]);
    }
  }
}
