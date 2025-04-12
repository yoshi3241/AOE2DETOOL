using AOE2DETOOL.Models.Logic;
using AOE2DETOOL.Utilities;
using System.Data;
using System.Runtime.InteropServices;

namespace AOE2DETOOL
{
    public partial class Aoe2deOverlayForm : Form
    {
        private AOE2DEDataProcess? _aOE2DEDataProcess = null;
        private Form1? _form1 = null;
        private Bitmap _canvas;
        private Graphics _g;

        public Aoe2deOverlayForm()
        {
            InitializeComponent();
        }

        private void Aoe2deOverlayForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.AllowTransparency = true;
            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;
            this.ShowInTaskbar = false;
        }

        public void StartOverlay()
        {
            FormResize();

            timer1.Start();
        }

        public void InitData(Form1 form1, AOE2DEDataProcess aOE2DEDataProcess)
        {
            _form1 = form1;
            _aOE2DEDataProcess = aOE2DEDataProcess;
        }

        public void FormResize()
        {
            var bounds = WindowInfo.GetAoE2WindowBounds();
            if (bounds.HasValue)
            {
                Bounds = bounds.Value; // フォームをAoE2DEの上にぴったり合わせる
            }

            //描画先とするImageオブジェクトを作成する
            _canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            _g = Graphics.FromImage(_canvas);

            pictureBox1.Image = _canvas;
        }

        private void BildInfoProc()
        {
            lock (AOE2DEDataProcess.BuildInfoDataLock)
            {
                if (_aOE2DEDataProcess is null || _aOE2DEDataProcess.BildInfoList.Count == 0)
                {
                    return;
                }

                var buildInfos = _aOE2DEDataProcess.BildInfoList.Where(a =>
                    Math.Abs(a.X - _aOE2DEDataProcess.ViewX) <= 50 &&
                    Math.Abs(a.Y - _aOE2DEDataProcess.ViewY) <= 50);

                if (!buildInfos.Any())
                {
                    return;
                }

                // 12=戦士小屋
                // 70=家

                foreach (var buildInfo in buildInfos)
                {
                    var myselfPlayerInfo = _aOE2DEDataProcess.PlayerInfoList.Where((a) => a.IsMyself).FirstOrDefault();
                    var targetPlayerInfo = _aOE2DEDataProcess.PlayerInfoList.Where((a) => a.Number == buildInfo.PlayerId).FirstOrDefault();
                    if (myselfPlayerInfo is null || targetPlayerInfo is null) return;

                    if (myselfPlayerInfo.TeamId == targetPlayerInfo.TeamId)
                    {
                        continue;
                    }

                    // タイルサイズ（仮値）※必要に応じて調整可能
                    float TILE_WIDTH = 240;
                    float TILE_HEIGHT = 240;

                    // オーバーレイフォーム中心を基準にする（画面中央）
                    float screenCenterX = this.Width / 2f;
                    float screenCenterY = this.Height / 2f;

                    // マップ座標差分
                    float dx = buildInfo.X - _aOE2DEDataProcess.ViewX;
                    float dy = buildInfo.Y - _aOE2DEDataProcess.ViewY;
                    float dxEnd = buildInfo.XEnd - _aOE2DEDataProcess.ViewX;
                    float dyEnd = buildInfo.YEnd - _aOE2DEDataProcess.ViewY;

                    // アイソメトリック変換
                    float screenX = screenCenterX - (dx - dy) * (TILE_WIDTH / 2f);
                    float screenY = screenCenterY + (dx + dy) * (TILE_HEIGHT / 2f);
                    float screenXEnd = screenCenterX - (dxEnd - dyEnd) * (TILE_WIDTH / 2f);
                    float screenYEnd = screenCenterY + (dxEnd + dyEnd) * (TILE_HEIGHT / 2f);

                    screenX = screenX * 0.20f;
                    screenY = screenY * 0.40f;
                    screenXEnd = screenXEnd * 0.20f;
                    screenYEnd = screenYEnd * 0.40f;

                    var y = ((int)screenX - 200 / 2) + 620;
                    var x = ((int)screenY - 100 / 2 + 1350);
                    var yEnd = ((int)screenXEnd - 200 / 2) + 620;
                    var xEnd = ((int)screenYEnd - 100 / 2 + 1350);

                    var str = "";
                    switch (buildInfo.BuildingId)
                    {
                        case 12:
                            str = "戦士";
                            break;

                        case 48:
                            str = "攻囲";
                            break;

                        case 68:
                            str = "粉引";
                            break;

                        case 70:
                            str = " 家";
                            break;

                        case 72:
                            str = " 柵";
                            break;

                        case 79:
                            str = " 塔";
                            break;

                        case 82:
                            str = " 城";
                            break;

                        case 84:
                            str = "市場";
                            break;

                        case 87:
                            str = "弓小";
                            break;

                        case 101:
                            str = "馬小";
                            break;

                        case 103:
                            str = "鍛冶";
                            break;

                        case 104:
                            str = "修道";
                            break;

                        case 109:
                            str = "町中";
                            break;

                        case 209:
                            str = "大学";
                            break;

                        case 487:
                            str = "強門";
                            break;

                        case 562:
                            str = "伐採";
                            break;

                        case 584:
                            str = "採掘";
                            break;

                        case 792:
                            str = "柵門";
                            break;

                    }

                    if (buildInfo.BuildingId == 72)
                    {
                        var color = targetPlayerInfo.GetColor();
                        Pen pen = new Pen(color, 20);
                        _g.DrawLine(pen, x + 130, y + 50, xEnd + 130, yEnd + 50);
                        pen.Dispose();
                    }
                    else
                    {
                        var fnt = new Font("MS UI Gothic", 50);
                        var color = targetPlayerInfo.GetColor();
                        var brushColor = BrushHelper.ColorToBrush(color);
                        _g.DrawString(str, fnt, brushColor, x, y);
                        //brushColor.Dispose();
                        fnt.Dispose();
                    }
                }
            }
        }

        private void MoveInfoProc()
        {
            lock (AOE2DEDataProcess.MoveInfoDataLock)
            {
                if (_aOE2DEDataProcess is null || _aOE2DEDataProcess.BildInfoList.Count == 0)
                {
                    return;
                }

                var moveInfos = _aOE2DEDataProcess.MoveInfoList.Where(a =>
                    Math.Abs(a.X - _aOE2DEDataProcess.ViewX) <= 50 &&
                    Math.Abs(a.Y - _aOE2DEDataProcess.ViewY) <= 50 &&
                    a.Sequence > _aOE2DEDataProcess.GameTime - 20000);

                if (!moveInfos.Any())
                {
                    return;
                }

                foreach (var moveInfo in moveInfos)
                {
                    var myselfPlayerInfo = _aOE2DEDataProcess.PlayerInfoList.Where((a) => a.IsMyself).First();
                    var targetPlayerInfo = _aOE2DEDataProcess.PlayerInfoList.Where((a) => a.Number == moveInfo.PlayerId).First();

                    if (myselfPlayerInfo.TeamId == targetPlayerInfo.TeamId)
                    {
                        continue;
                    }

                    // タイルサイズ（仮値）※必要に応じて調整可能
                    float TILE_WIDTH = 240;
                    float TILE_HEIGHT = 240;

                    // オーバーレイフォーム中心を基準にする（画面中央）
                    float screenCenterX = this.Width / 2f;
                    float screenCenterY = this.Height / 2f;

                    // マップ座標差分
                    float dx = moveInfo.X - _aOE2DEDataProcess.ViewX;
                    float dy = moveInfo.Y - _aOE2DEDataProcess.ViewY;

                    // アイソメトリック変換
                    float screenX = screenCenterX - (dx - dy) * (TILE_WIDTH / 2f);
                    float screenY = screenCenterY + (dx + dy) * (TILE_HEIGHT / 2f);
                    screenX = screenX * 0.20f;
                    screenY = screenY * 0.40f;

                    var y = ((int)screenX - 200 / 2) + 650;
                    var x = ((int)screenY - 100 / 2 + 1380);

                    var str = "✕";

                    //フォントオブジェクトの作成
                    var fnt = new Font("MS UI Gothic", 50);

                    var color = targetPlayerInfo.GetColor();
                    var brushColor = BrushHelper.ColorToBrush(color);
                    _g.DrawString(str, fnt, brushColor, x, y);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (_aOE2DEDataProcess is null) return;

            _g.Clear(Color.Magenta);

            BildInfoProc();
            MoveInfoProc();

            pictureBox1.Refresh();

            timer1.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            //リソースを解放する
            _g?.Dispose();
            _canvas?.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect(); // 世代1・2まで確実に回収したいときにもう一度
        }

        bool _isView = true;

        private void AgeOfEmpiresActive()
        {
            bool ok = WindowActivator.FocusWindowByTitle("Age of Empires II: Definitive Edition");
            if (!ok) Console.WriteLine("ウィンドウが見つかりませんでした");
        }

        public void Active()
        {
            if (_g is null) return;

            _isView = true;
            timer1.Start();

            AgeOfEmpiresActive();
        }

        public void Inactive()
        {
            if (_g is null) return;

            _isView = false;
            timer1.Stop();
            _g.Clear(Color.Magenta);
            pictureBox1.Refresh();
        }

        public void StartMonitoring()
        {
            _form1?.FileListUpdate();
            _form1?.DoPlay();
            AgeOfEmpiresActive();
        }

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_LAYERED = 0x80000;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SetWindowExTransparent(this.Handle);
        }

        private void SetWindowExTransparent(IntPtr hwnd)
        {
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
    }
}
