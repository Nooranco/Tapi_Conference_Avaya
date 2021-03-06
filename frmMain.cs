using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Traysoft.AddTapi;

namespace OutgoingCalls
{
    public partial class frmMain : Form
    {
        delegate void AddListItem(string item);

        private string _internalAgentPhone;
        private string _agentPhone;
        private string _customerPhone;
        private string _customerPhone_NoConf= String.Empty;
        private bool startConference = true;
        TapiLine line=null;
        TapiCall ActiveCall;


        AddListItem m_addToLogDelegate;

        public frmMain()
        {
            InitializeComponent();
            m_addToLogDelegate = new AddListItem(AddToLog);
        }

        public frmMain(string internalAgentPhone, string agentPhone, string customerPhone) : this()
        {
            _internalAgentPhone = internalAgentPhone;
            _agentPhone = agentPhone;
            _customerPhone = customerPhone;
        }

        public frmMain(string internalAgentPhone, string phoneNumber ) : this()
        {
            _internalAgentPhone = internalAgentPhone;
            _customerPhone_NoConf = phoneNumber;
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
                if (_agentPhone != null)
                {
                    txtPhone1.Text = _agentPhone;
                }

                if (_customerPhone != null)
                {
                    txtPhone2.Text = _customerPhone;
                }
                if (_internalAgentPhone != null)
                {
                    comboBoxLine.SelectedItem = TapiApp.Lines.SingleOrDefault(q => q.Name.Contains(_internalAgentPhone));
                }
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
            startConference = false;
            timer1.Enabled = false;
            try
            {
                if (!String.IsNullOrEmpty(_internalAgentPhone))
                {
                    line = TapiApp.Lines.SingleOrDefault(q => q.Name.Contains(_internalAgentPhone));
                }
                else
                {
                    line = TapiApp.Lines.SingleOrDefault(q => q.Name.Contains(comboBoxLine.Text));
                }
                if (line == null) return;
                if (!line.IsOpen) line.Open(false, CallHandler);
                line.DisconnectOnBusy = true;
                line.NoAnswerTimeout = 15;
                ActiveCall = line.Dial(txtPhone1.Text, false);
                string msg = String.Format("شماره گیری {0} بر روی خط '{1}'", txtPhone2.Text, line.Name);
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
                            if (consulationCall.State == TapiCallState.Connected)
                            {
                                ActiveCall.CompleteTransfer(consulationCall, false);
                                ActiveCall.Disconnect();
                                completed = true;
                            }
                            else if (consulationCall.State == TapiCallState.Disconnected)
                            {
                                completed = true;
                                Application.Exit();
                            }
                        }
                    }
                    else if (ActiveCall.State == TapiCallState.Disconnected)
                    {
                        completed = true;
                        Application.Exit();
                    }
                }
            }
            catch (TapiException exc)
            {
                MessageBox.Show(exc.Message, "TapiException!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
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
            Application.Exit();
        }

        void OnTapiError(object sender, TapiErrorEventArgs args)
        {
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (startConference && !String.IsNullOrEmpty(_agentPhone))
            {
                startConference = false;
                buttonDial.PerformClick();                
            }else if (!String.IsNullOrEmpty(_customerPhone_NoConf))
            {
                btnDial.PerformClick();
            }
        }

        private void btnDial_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                if (!String.IsNullOrEmpty(_internalAgentPhone))
                {
                    line = TapiApp.Lines.SingleOrDefault(q => q.Name.Contains(_internalAgentPhone));
                }
                else if (!String.IsNullOrEmpty(comboBoxLine.Text))
                {
                    line = TapiApp.Lines.SingleOrDefault(q => q.Name.Contains(comboBoxLine.Text));
                }
                if (line == null) return;
                if (!line.IsOpen) line.Open(false, SimapleCallHandler);
                line.DisconnectOnBusy = true;
                line.NoAnswerTimeout = 15;
                ActiveCall = line.Dial(_customerPhone_NoConf, false, TapiMediaMode.AutomatedVoice, line.Addresses[0]);
                string msg = String.Format("شماره گیری {0} بر روی خط '{1}'", _customerPhone_NoConf, line.Name);
                AddToLog(msg);
                
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

        private void SimapleCallHandler(TapiCall call)
        {
        }
    }
}