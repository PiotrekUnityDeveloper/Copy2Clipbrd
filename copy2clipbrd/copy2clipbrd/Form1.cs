//-----------------------------------------------------------------------------
// Developed by Piotrek4, Piotrek4Software is an unregistered trademark
// used for software distribution. All software created by me is free to use,
// modify, sell, and redistribute, provided this disclaimer is included.
//
// No rights reserved.
//
// This code is licensed under the DONT ASK license.
// For more information, please visit: https://piotrekunitydeveloper.github.io/dontask/
//-----------------------------------------------------------------------------


using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace copy2clipbrd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static bool isEnabled = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            SyncItemPreferences();
            richTextBox1.HideSelection = false;

            if (IsContextMenuRegistered())
            {
                //it is

                isEnabled = true;
                SetEnabled();
            }
            else
            {
                //it is not

                isEnabled = false;
                SetDisabled();
            }
        }



        public void ToggleService()
        {
            if (isEnabled)
            {
                DisableService();
            }
            else
            {
                EnableService();
            }
        }

        private void SetEnabled()
        {
            activationButton.BackgroundImage = copy2clipbrd.Properties.Resources.on;
            activationStatusLabel.Text = "CURRENT STATUS: ON";
            activationStatusLabel.ForeColor = Color.Lime;

            isEnabled = true;
        }

        private void SetDisabled()
        {
            activationButton.BackgroundImage = copy2clipbrd.Properties.Resources.off;
            activationStatusLabel.Text = "CURRENT STATUS: OFF";
            activationStatusLabel.ForeColor = Color.Red;

            isEnabled = false;
        }

        public void TryEnableService()
        {
            PrintLogEntry("Generating new resources...", true, Color.Yellow);
            ExportAllResources(Environment.CurrentDirectory + "\\");

            PrintLogEntry("Restarting Menu Initialization process right now...", true, Color.Orange);
            PrintLogEntry("if you are seeing this message multiple times when starting the service, please move this software to another disk location, like program files or create an issue on our github", true, Color.Orange);
            RegisterContextMenu();
            PrintLogEntry("Wait...", true, Color.Yellow);
            SetEnabled();

            button2.Enabled = true;

            PrintLogEntry("Done!", true, Color.Lime);
            PrintLogEntry("The Service is Running...!", true, Color.Lime);
        }

        public void EnableService()
        {
            if (CheckForResources(Environment.CurrentDirectory + "\\") == false)
            {
                ExportAllResources(Environment.CurrentDirectory + "\\");
            }

            RegisterContextMenu();
            SetEnabled();

            button2.Enabled = true;
        }

        public void DisableService()
        {
            TryUnregisterContextMenu();
            SetDisabled();

            button2.Enabled = false;
            refreshalert.Hide();
        }

        private void SyncItemPreferences()
        {
            string registryKey_fullpath = @"*\shell\Copy2Clipbrd_fullpath";
            string registryKey_filename = @"*\shell\Copy2Clipbrd_filename";
            string registryKey_filenameext = @"*\shell\Copy2Clipbrd_filenameext";

            // Check if the registry key exists
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(registryKey_fullpath))
                {
                    //return key != null;

                    if (key != null)
                    {
                        enableCopyFullPath.Checked = true;
                    }
                    else
                    {
                        enableCopyFullPath.Checked = false;
                    }
                }

                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(registryKey_filename))
                {
                    //return key != null;

                    if (key != null)
                    {
                        enableCopyFileName.Checked = true;
                    }
                    else
                    {
                        enableCopyFileName.Checked = false;
                    }
                }

                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(registryKey_filenameext))
                {
                    //return key != null;

                    if (key != null)
                    {
                        enableCopyFilenameIncludingExtension.Checked = true;
                    }
                    else
                    {
                        enableCopyFilenameIncludingExtension.Checked = false;
                    }
                }
            }
            catch
            {
                //the keys are not there, reset the menu options to default
                enableCopyFileName.Checked = false;
                enableCopyFullPath.Checked = true;
                enableCopyFilenameIncludingExtension.Checked = false;
            }
        }

        private static bool IsContextMenuRegistered()
        {
            // Path to the registry keys
            string registryKey_fullpath = @"*\shell\Copy2Clipbrd_fullpath";
            string registryKey_filename = @"*\shell\Copy2Clipbrd_filename";
            string registryKey_filenameext = @"*\shell\Copy2Clipbrd_filenameext";

            // Check if the registry key exists
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(registryKey_fullpath))
            {
                //return key != null;

                if(key != null)
                {
                    return true;
                }
            }

            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(registryKey_filename))
            {
                //return key != null;

                if (key != null)
                {
                    return true;
                }
            }

            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(registryKey_filenameext))
            {
                //return key != null;

                if (key != null)
                {
                    return true;
                }
            }

            return false;
        }

        private void RegisterContextMenu()
        {
            // Path to the registry keys
            string registryKey_fullpath = @"*\shell\Copy2Clipbrd_fullpath";
            string registryKey_filename = @"*\shell\Copy2Clipbrd_filename";
            string registryKey_filenameext = @"*\shell\Copy2Clipbrd_filenameext";

            // Verify Resources

            PrintLogEntry("Verifying Resources...", true, Color.Yellow);

            PrintLogEntry(".", false, Color.Yellow);

            if (enableCopyFileName.Checked)
            {
                if (!File.Exists(Environment.CurrentDirectory + "\\savefilename.ico"))
                {
                    PrintLogEntry(". FAILED!", false, Color.Orange);
                    PrintLogEntry("Restarting...", false, Color.Orange);
                    TryEnableService();
                    return;
                }
            }

            PrintLogEntry(".", false, Color.Yellow);

            if (enableCopyFullPath.Checked)
            {
                if (!File.Exists(Environment.CurrentDirectory + "\\savefullpath.ico"))
                {
                    PrintLogEntry(". FAILED!", false, Color.Orange);
                    PrintLogEntry("Restarting...", false, Color.Orange);
                    TryEnableService();
                    return;
                }
            }

            PrintLogEntry(".", false, Color.Yellow);

            if (enableCopyFilenameIncludingExtension.Checked)
            {
                if (!File.Exists(Environment.CurrentDirectory + "\\savefilenameext.ico"))
                {
                    PrintLogEntry(". FAILED!", false, Color.Orange);
                    PrintLogEntry("Restarting...", false, Color.Orange);
                    TryEnableService();
                    return;
                }
            }

            PrintLogEntry(".", false, Color.Yellow);

            // Create all the required keys

            PrintLogEntry(".OK!", false, Color.Yellow);
            PrintLogEntry("Writing Keys...", true, Color.Yellow);

            if (enableCopyFullPath.Checked)
            {
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(registryKey_fullpath))
                {
                    if (key != null)
                    {
                        key.SetValue("", "Copy2Clipbrd - Copy Full Path"); // Menu item text
                        key.SetValue("Icon", Environment.CurrentDirectory + "\\savefullpath.ico"); // Optional: Icon for the menu item

                        // Create a command subkey
                        using (RegistryKey commandKey = key.CreateSubKey("command"))
                        {
                            if (commandKey != null)
                            {
                                //string exePath = Assembly.GetExecutingAssembly().Location;
                                string exePath = Environment.CurrentDirectory + "\\copytoclipboard.exe";

                                // Set the command to execute
                                // %1 represents the selected file
                                //commandKey.SetValue("", "\"C:\\Path\\To\\YourApp.exe\" \"%1\"");
                                commandKey.SetValue("", $"\"{exePath}\" \"%1\" " + "-fp");
                            }
                        }
                    }
                }
            }

            PrintLogEntry(".", false, Color.Yellow);

            if (enableCopyFileName.Checked)
            {
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(registryKey_filename))
                {
                    if (key != null)
                    {
                        key.SetValue("", "Copy2Clipbrd - Copy File Name"); // Menu item text
                        key.SetValue("Icon", Environment.CurrentDirectory + "\\savefilename.ico"); // Optional: Icon for the menu item

                        // Create a command subkey
                        using (RegistryKey commandKey = key.CreateSubKey("command"))
                        {
                            if (commandKey != null)
                            {
                                //string exePath = Assembly.GetExecutingAssembly().Location;
                                string exePath = Environment.CurrentDirectory + "\\copytoclipboard.exe";

                                // Set the command to execute
                                // %1 represents the selected file
                                //commandKey.SetValue("", "\"C:\\Path\\To\\YourApp.exe\" \"%1\"");
                                commandKey.SetValue("", $"\"{exePath}\" \"%1\" " + "-fn");
                            }
                        }
                    }
                }
            }

            PrintLogEntry(".", false, Color.Yellow);

            if (enableCopyFilenameIncludingExtension.Checked)
            {
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(registryKey_filenameext))
                {
                    if (key != null)
                    {
                        key.SetValue("", "Copy2Clipbrd - Copy File Name (Including Extension)"); // Menu item text
                        key.SetValue("Icon", Environment.CurrentDirectory + "\\savefilenameext.ico"); // Optional: Icon for the menu item

                        // Create a command subkey
                        using (RegistryKey commandKey = key.CreateSubKey("command"))
                        {
                            if (commandKey != null)
                            {
                                //string exePath = Assembly.GetExecutingAssembly().Location;
                                string exePath = Environment.CurrentDirectory + "\\copytoclipboard.exe";

                                // Set the command to execute
                                // %1 represents the selected file
                                //commandKey.SetValue("", "\"C:\\Path\\To\\YourApp.exe\" \"%1\"");
                                commandKey.SetValue("", $"\"{exePath}\" \"%1\" " + "-fnext");
                            }
                        }
                    }
                }
            }

            PrintLogEntry(".OK!", false, Color.Lime);
            PrintLogEntry("Service is Running...!", true, Color.Lime);
        }

        private void TryUnregisterContextMenu()
        {
            // Path to the registry keys
            string registryKey_fullpath = @"*\shell\Copy2Clipbrd_fullpath";
            string registryKey_filename = @"*\shell\Copy2Clipbrd_filename";
            string registryKey_filenameext = @"*\shell\Copy2Clipbrd_filenameext";

            try
            {
                // Delete the registry key if it exists
                Registry.ClassesRoot.DeleteSubKeyTree(registryKey_fullpath, false);
                Registry.ClassesRoot.DeleteSubKeyTree(registryKey_filename, false);
                Registry.ClassesRoot.DeleteSubKeyTree(registryKey_filenameext, false);
                Console.WriteLine("Context menu successfully unregistered.");
                PrintLogEntry("Context menu successfully unregistered.", true, Color.Yellow);
            }
            catch (ArgumentException)
            {
                // Key does not exist, no need to do anything
                Console.WriteLine("Context menu key does not exist.");
                PrintLogEntry("ERROR - Context menu key does not exist.", true, Color.Red);
            }
            catch (Exception ex)
            {
                // Other errors
                Console.WriteLine("An error occurred while trying to unregister the context menu: " + ex.Message);
                PrintLogEntry("ERROR - Unknown Error Occured, see below for details", true, Color.Red);
                PrintLogEntry(ex.Message, true, Color.Red);
            }
        }

        private void activationButton_Click(object sender, EventArgs e)
        {
            ToggleService();
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            richTextBox1.DeselectAll();
        }

        private void enableCopyFullPath_CheckedChanged(object sender, EventArgs e)
        {
            if (isEnabled) refreshalert.Show();
        }

        private void enableCopyFileName_CheckedChanged(object sender, EventArgs e)
        {
            if (isEnabled) refreshalert.Show();
        }

        private void enableCopyFilenameIncludingExtension_CheckedChanged(object sender, EventArgs e)
        {
            if(isEnabled) refreshalert.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refreshalert.Hide();
            button2.Text = "refreshing";
            button2.Font = new Font("Arial", 7);
            button2.BackColor = Color.Black;
            button2.ForeColor = Color.White;
            button2.Enabled = false;
            RefreshActivation();
        }

        public async void RefreshActivation()
        {
            PrintLogEntry("refreshing... please wait", true, Color.Yellow);
            await Task.Delay(1000);

            if (isEnabled)
            {
                PrintLogEntry("refreshing... disabling open services", true, Color.Yellow);
                DisableService();
            }

            await Task.Delay(1000);

            PrintLogEntry("refreshing... re-enabling", true, Color.Yellow);
            EnableService();


            button2.Text = "refresh";
            button2.BackColor = Color.FromKnownColor(KnownColor.ActiveBorder);
            button2.ForeColor = Color.Black;
            button2.Font = new Font("Microsoft Sans Serif", 8);
            PrintLogEntry("refreshing... done!", true, Color.Lime);
        }

        private void ExportAllResources(string targetDirectory)
        {
            var resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true);

            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceName = entry.Key.ToString();
                object resourceValue = entry.Value;

                string filePath = Path.Combine(targetDirectory, resourceName);

                if (resourceValue is Bitmap)
                {
                    filePath += ".png";
                    PrintLogEntry("extracting " + filePath + "...", true, Color.Yellow);
                    if (!File.Exists(filePath))
                    {
                        var bitmap = (Bitmap)resourceValue;
                        bitmap.Save(filePath);
                        PrintLogEntry(" OK!", false, Color.Lime);
                    }
                    else
                    {
                        PrintLogEntry(" FAILED!", false, Color.Red);
                    }
                }
                else if (resourceValue is Icon)
                {
                    filePath += ".ico";
                    PrintLogEntry("extracting " + filePath + "...", true, Color.Yellow);
                    if (!File.Exists(filePath))
                    {
                        var icon = (Icon)resourceValue;
                        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            icon.Save(fs);
                        }
                        PrintLogEntry(" OK!", false, Color.Lime);
                    }
                    else
                    {
                        PrintLogEntry(" FAILED!", false, Color.Red);
                    }
                }
                else if (resourceValue is string)
                {
                    filePath += ".txt";
                    PrintLogEntry("extracting " + filePath + "...", true, Color.Yellow);
                    if (!File.Exists(filePath))
                    {
                        var str = (string)resourceValue;
                        File.WriteAllText(filePath, str);
                        PrintLogEntry(" OK!", false, Color.Lime);
                    }
                    else
                    {
                        PrintLogEntry(" FAILED!", false, Color.Red);
                    }
                }
                else if (resourceValue is byte[])
                {
                    // Determine file extension based on some logic or naming convention
                    if (resourceName.EndsWith(".exe"))
                        filePath += ".exe";
                    else if (resourceName.EndsWith(".json"))
                        filePath += ".json";
                    else if (resourceName.EndsWith(".dll"))
                        filePath += ".dll";
                    else if (resourceName.EndsWith(".pdb"))
                        filePath += ".pdb";
                    else
                        filePath += ".bin"; // Default for unknown byte arrays

                    PrintLogEntry("extracting " + filePath + "...", true, Color.Yellow);

                    if (!File.Exists(filePath))
                    {
                        var bytes = (byte[])resourceValue;
                        File.WriteAllBytes(filePath, bytes);
                        PrintLogEntry(" OK!", false, Color.Lime);
                    }
                    else
                    {
                        PrintLogEntry(" FAILED!", false, Color.Red);
                    }
                }
                // Add more cases as needed for other types of resources
            }
        }


        //this will do for the most time if the files werent tampered with
        private bool CheckForResources(string targetDirectory)
        {
            var resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true);

            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceName = entry.Key.ToString();
                object resourceValue = entry.Value;

                string filePath = Path.Combine(targetDirectory, resourceName);

                if (resourceValue is Bitmap)
                {
                    filePath += ".png";
                    if (File.Exists(filePath))
                    {
                        PrintLogEntry("Resources were found, cancelling...", true, Color.Gray);
                        return true;
                    }
                }
                else if (resourceValue is Icon)
                {
                    filePath += ".ico";
                    if (File.Exists(filePath))
                    {
                        PrintLogEntry("Resources were found, cancelling...", true, Color.Gray);
                        return true;
                    }
                }
            }

            PrintLogEntry("No resources found, consider regeneration!", true, Color.Orange);
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string registryKey_fullpath = @"*\shell\Copy2Clipbrd\fullpath";
            string registryKey_filename = @"*\shell\Copy2Clipbrd\filename";
            string registryKey_filenameext = @"*\shell\Copy2Clipbrd\filenameext";
            string registryKey = @"*\shell\Copy2Clipbrd";

            try
            {
                // Delete the registry key if it exists
                Registry.ClassesRoot.DeleteSubKeyTree(registryKey_fullpath, false);
                Registry.ClassesRoot.DeleteSubKeyTree(registryKey_filename, false);
                Registry.ClassesRoot.DeleteSubKeyTree(registryKey_filenameext, false);
                Registry.ClassesRoot.DeleteSubKeyTree(registryKey, false);
                Console.WriteLine("Context menu successfully unregistered.");
                PrintLogEntry("Context menu successfully unregistered", true, Color.Yellow);
            }
            catch (ArgumentException)
            {
                // Key does not exist, no need to do anything
                Console.WriteLine("Context menu key does not exist.");
                PrintLogEntry("Failed to remove - Context menu key does not exist", true, Color.Red);
            }
            catch (Exception ex)
            {
                // Other errors
                Console.WriteLine("An error occurred while trying to unregister the context menu: " + ex.Message);
                PrintLogEntry("Failed to remove - Unknown Error Occured, see below for details", true, Color.Red);
                PrintLogEntry(ex.Message, true, Color.Red);
            }
        }

        public void PrintLogEntry(string entry, bool newLine, Color color)
        {
            bool wasReadOnly = richTextBox1.ReadOnly;
            richTextBox1.ReadOnly = false;
            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText((newLine ? "\n" : "") + entry);
            richTextBox1.SelectionColor = richTextBox1.ForeColor; // Reset to default color
            richTextBox1.ReadOnly = wasReadOnly;
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://piotrekunitydeveloper.github.io/piotrek4software_bundle_website/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PiotrekUnityDeveloper/Copy2Clipbrd/blob/main/README.md#usage");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PiotrekUnityDeveloper/Copy2Clipbrd");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PiotrekUnityDeveloper/Copy2Clipbrd");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PiotrekUnityDeveloper/Copy2Clipbrd/issues");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PiotrekUnityDeveloper/Copy2Clipbrd/pulls");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/PiotrekUnityDeveloper");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            try
            {
                File.WriteAllText(Environment.CurrentDirectory + "\\Log_" + rnd.Next(000, 999)
                + DateTime.Now.Day.ToString() + "."
                + DateTime.Now.Month.ToString() + "."
                + DateTime.Now.Year.ToString() + "."
                + DateTime.Now.Hour.ToString() + "."
                + DateTime.Now.Minute.ToString() + "."
                + DateTime.Now.Second.ToString() + "..("
                + DateTime.Now.Millisecond.ToString() + "..."
                + ".log", richTextBox1.Text);

                richTextBox1.Clear();
                PrintLogEntry("Log Saved To Root Folder", false, Color.White);
            }
            catch
            {
                PrintLogEntry("Failed to save log!", true, Color.Red);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}
