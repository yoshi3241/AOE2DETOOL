using AOE2DETOOL.Models.Logic;
using AOE2DETOOL.Utilities;

namespace AOE2DETOOL
{
    public partial class Aoe2deControllForm : Form
    {
        private AOE2DEDataProcess? _aOE2DEDataProcess = null;
        private Form1? _form1 = null;
        bool _isView = true;
        Aoe2deOverlayForm _overlayForm;

        public Aoe2deControllForm()
        {
            InitializeComponent();
        }

        private void Aoe2deControllForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.AllowTransparency = true;
            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;
            this.ShowInTaskbar = false;
        }

        public void InitData(Form1 form1, Aoe2deOverlayForm overlayForm, AOE2DEDataProcess aOE2DEDataProcess)
        {
            _form1 = form1;
            _overlayForm = overlayForm;
            _aOE2DEDataProcess = aOE2DEDataProcess;
            _aOE2DEDataProcess.AddStartCallback(OnStart);
        }

        public void OnStart()
        {
            if (_aOE2DEDataProcess is null) return;

            var myselfPlayerInfo = _aOE2DEDataProcess.PlayerInfoList.Where((a) => a.IsMyself).FirstOrDefault();

            Invoke(new Action(() =>
            {
                if (myselfPlayerInfo is not null)
                {
                    label1.BackColor = myselfPlayerInfo.FrontEnemy.GetColor();
                    label2.BackColor = myselfPlayerInfo.BackEnemy.GetColor();
                    label3.BackColor = myselfPlayerInfo.GetColor();
                    label3.Text = myselfPlayerInfo.IsFrontline ? "前衛" : "後衛";
                }
            }));
        }

        private void AgeOfEmpiresActive()
        {
            bool ok = WindowActivator.FocusWindowByTitle("Age of Empires II: Definitive Edition");
            if (!ok) Console.WriteLine("ウィンドウが見つかりませんでした");
        }

        public void StartOverlay()
        {
            FormResize();
        }

        public void FormResize()
        {
            var bounds = WindowInfo.GetAoE2WindowBounds();
            if (bounds.HasValue)
            {
                Bounds = bounds.Value; // フォームをAoE2DEの上にぴったり合わせる
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isView)
            {
                button1.Text = "表示";

                _isView = false;
                _overlayForm.Inactive();
            }
            else
            {
                button1.Text = "非表示";

                _isView = true;
                _overlayForm.Activate();
            }

            AgeOfEmpiresActive();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _overlayForm.StartMonitoring();

            AgeOfEmpiresActive();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Aoe2deControllForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _aOE2DEDataProcess?.RemoveStartCallback(OnStart);
            base.OnFormClosed(e);
        }
    }
}
