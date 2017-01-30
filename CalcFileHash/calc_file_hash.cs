using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcFileHash
{
  public partial class calc_file_hash_form : Form
  {
    SynchronizationContext sync_context;

    public calc_file_hash_form()
    {
      InitializeComponent();

      this.hash_type_cmb.SelectedIndex = 0;

      sync_context = SynchronizationContext.Current;
    }

    private void browse_button_Click(object sender, EventArgs e)
    {
      OpenFileDialog file_dialog = new OpenFileDialog();
      if (file_dialog.ShowDialog() == DialogResult.OK) {
        file_path_textbox.Text = file_dialog.FileName;
      }
    }

    // main thread
    private void file_path_textbox_TextChanged(object sender, EventArgs e)
    {
      start_hash_task();
    }

    private void hash_type_cmb_SelectedIndexChanged(object sender, EventArgs e)
    {
      start_hash_task();
    }

    private void actual_hash_textbox_TextChanged(object sender, EventArgs e)
    {
      set_hash_match_color();
    }

    private void expect_hash_textbox_TextChanged(object sender, EventArgs e)
    {
      set_hash_match_color();
    }

    private void start_hash_task()
    {
      string file_path = file_path_textbox.Text.Trim();
      if (file_path == "") {
        return;
      }

      set_start_hash_ui();
      string hash_type = hash_type_cmb.Text.Trim();
      Task.Factory.StartNew(() => { calc_hash_task(file_path, hash_type); });
    }

    private void set_start_hash_ui()
    {
      actual_hash_textbox.Text = "";
      performance_textbox.Text = "Calculating ...";

      hash_type_cmb.Enabled = false;
      browse_button.Enabled = false;
      file_path_textbox.Enabled = false;
      actual_hash_textbox.Enabled = false;
      expect_hash_textbox.Enabled = false;
      performance_textbox.Enabled = false;
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

    // hash task
    private void calc_hash_task(string file_path, string hash_type)
    {
      try {
        double elaps = calc_hash(file_path, hash_type);
        calc_performance(file_path, elaps);
      } catch (Exception e) {
        sync_context.Send(ui_task_set_performance, "");
        MessageBox.Show(e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      } finally {
        sync_context.Send(ui_task_end_hash, null);
      }
    }

    private double calc_hash(string file_path, string hash_type)
    {
      try {
        Stopwatch stw = new Stopwatch();
        stw.Start();

        HashAlgorithm hasher = create_hasher(hash_type);
        FileStream file = new FileStream(file_path, FileMode.Open);
        byte[] hash_value = hasher.ComputeHash(file);
        file.Close();

        string hash_string = format_hash(hash_value);
        sync_context.Send(ui_task_set_actual_hash, hash_string);

        stw.Stop();
        return stw.Elapsed.TotalMilliseconds;
      } catch (Exception e) {
        throw e;
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

    private void calc_performance(string file_path, double elaps_time)
    {
      try {
        FileInfo file_info = new FileInfo(file_path);
        string text = string.Format("  file size: {0}\r\n", format_file_size(file_info.Length));
        text += string.Format(" elaps time: {0}\r\n", format_hash_time(elaps_time));

        sync_context.Send(ui_task_set_performance, text);
      } catch {
        sync_context.Send(ui_task_set_performance, "");
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

    // ui sync task functions
    private void ui_task_end_hash(object sender)
    {
      hash_type_cmb.Enabled = true;
      browse_button.Enabled = true;
      file_path_textbox.Enabled = true;
      actual_hash_textbox.Enabled = true;
      expect_hash_textbox.Enabled = true;
      performance_textbox.Enabled = true;
    }

    private void ui_task_set_actual_hash(object hash_string)
    {
      actual_hash_textbox.Text = hash_string.ToString();
    }

    private void ui_task_set_performance(object text)
    {
      performance_textbox.Text = text.ToString();
    }
  }
}
