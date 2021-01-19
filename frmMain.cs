using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Traysoft.AddTapi;

namespace OutgoingCalls
{
    public partial class frmMain : Form
    {
        delegate void AddListItem(string item);
        AddListItem m_addToLogDelegate;
        TapiLine line;
        TapiCall ActiveCall;
        public frmMain()
        {
            InitializeComponent();
            m_addToLogDelegate = new AddListItem(AddToLog);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                TapiApp.Initialize("تماسهای گرفته شده");
                TapiApp.TapiError += OnTapiError;
                TapiApp.LineAdded += OnLineAdded;
                TapiApp.LineClosed += OnLineClosed;
                TapiApp.LineRemoved += OnLineRemoved;
                TapiApp.CallDisconnected += OnCallDisconnected;
                UpdateLinesCombobox();
            }
            catch (TapiException exc)
            {
                MessageBox.Show(exc.Message, "TapiException!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                TapiApp.Shutdown();
            }
            catch (TapiException exc)
            {
                MessageBox.Show(exc.Message, "TapiException!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDial_Click(object sender, EventArgs e)
        {
            line = (TapiLine)comboBoxLine.SelectedItem;
            if (line == null)
            {
                MessageBox.Show("لطفاً ابتدا یک خط انتخاب کنید.");
                return;
            }
            // Check if a number was entered
            string number = txtPhone1.Text;
            if (number == null || number.Length == 0)
            {
                MessageBox.Show("لطفاً شماره ای را برای تماس وارد کنید.");
                return;
            }
            try
            {
                if (!line.IsOpen) line.Open(false, CallHandler);
                line.NoAnswerTimeout = 60;
                ActiveCall = line.Dial(number, false);
                string msg = String.Format("شماره گیری {0} بر روی خط '{1}'", number, line.Name);
                AddToLog(msg);
                bool completed = false;
                while (!completed)
                {
                    Application.DoEvents();
                    if (ActiveCall.State == TapiCallState.Connected)
                    {
                        ActiveCall.Hold();
                        TapiCall consulationCall = line.Dial(txtPhone2.Text.Trim(), false);
                        while (!completed)
                        {
                            if (consulationCall.State == TapiCallState.Connected){
                                ActiveCall.CompleteTransfer(consulationCall, false);
                                ActiveCall.Disconnect();
                                completed = true;
                            }
                        }
                    }
                }

            }
            catch (TapiException exc)
            {
                MessageBox.Show(exc.Message, "TapiException!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CallHandler(TapiCall call)
        {
            try
            {
                string msg = String.Format("تماس به {0}.", call.CalledID);
                listBoxLog.Invoke(m_addToLogDelegate, msg);
            }
            catch (TapiDisconnectException)
            {
                listBoxLog.Invoke(m_addToLogDelegate, "طرف دعوت شده تلفن را قطع کرد.");
            }
            catch (Exception exc)
            {
                string msg = String.Format("Exception in CallHandler! {0}", exc.Message);
                listBoxLog.Invoke(m_addToLogDelegate, msg);
            }
            finally
            {
                call.Disconnect();
            }
        }

        void OnCallDisconnected(object sender, TapiEventArgs args)
        {
            string msg = String.Format("Disconnected call to {0} on line '{1}'",
                args.Call.CalledID, args.Line.Name);
            // Check why the call was disconnected
            switch (args.Call.DisconnectMode)
            {
                case TapiDisconnectMode.Busy:
                    msg += ", the number was busy";
                    break;
                case TapiDisconnectMode.NoAnswer:
                    msg += ", there was no answer";
                    break;
                case TapiDisconnectMode.NoDialTone:
                    msg += ", there was no dialtone";
                    break;
                case TapiDisconnectMode.Error:
                    msg += ", an error occurred";
                    break;
            }
            AddToLog(msg);
        }

        // This event handler is called when AddTapi error occurs asynchronously
        void OnTapiError(object sender, TapiErrorEventArgs args)
        {
            // Display error in the log
            string msg;
            if (args.Line != null)
            {
                msg = String.Format("TapiError event, line '{0}'. {1}",
                        args.Line.Name, args.Message);
            }
            else
            {
                msg = String.Format("TapiError event. {0}", args.Message);
            }
            AddToLog(msg);
        }

        void OnLineAdded(object sender, TapiEventArgs args)
        {
            UpdateLinesCombobox();
        }

        void OnLineClosed(object sender, TapiEventArgs args)
        {
            string msg = String.Format("LineClosed event, line '{0}' was forcibly closed by Windows.",
                            args.Line.Name);
            AddToLog(msg);
            UpdateLinesCombobox();
        }

        void OnLineRemoved(object sender, TapiEventArgs args)
        {
            UpdateLinesCombobox();
        }

        private void AddToLog(string msg)
        {
            int ind = listBoxLog.Items.Add(msg);
            listBoxLog.TopIndex = ind;
        }

        private void UpdateLinesCombobox()
        {
            comboBoxLine.Items.Clear();
            foreach (TapiLine line in TapiApp.Lines)
            {
                comboBoxLine.Items.Add(line);
            }
        }

    }
}
