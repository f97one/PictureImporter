using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Management.Instrumentation;

namespace PictureImporter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 出力フォルダの初期値を取得してテキストボックスと出力ダイアログに入れる
            textBoxExportDir.Text = GetMyPicturePath();
            folderBrowserDialogExport.SelectedPath = GetMyPicturePath();

            // 入力フォルダの初期値に、「最初に見つかったリムーバブルドライブ」を
            // セットする
            textBoxImportDir.Text = GetFirstRemoveableDrive();
            folderBrowserDialogImport.SelectedPath = GetFirstRemoveableDrive();
        }

        // ユーザーの[マイピクチャ]を返す
        private string GetMyPicturePath()
        {
            string myPciturePath = "";

            // スペシャルフォルダの「マイピクチャ」を取得する
            myPciturePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            return myPciturePath;
        }

        private void checkBoxMyPicture_CheckedChanged(object sender, EventArgs e)
        {
            // チェックボックスがはずれている場合は、
            // 出力先フォルダのテキストボックスをクリアし、
            // テキストボックスに入力できるようにする。
            //   実際には、自身の状態で判断ができない(!)ようので、
            //   操作する出力先テキストボックスの状態で判断するように
            //   している(^^;)。
            if (!textBoxExportDir.Enabled)
            {
                textBoxExportDir.Text = "";
                textBoxExportDir.Enabled = true;
            }
            else
            {
                textBoxExportDir.Text = GetMyPicturePath();
                textBoxExportDir.Enabled = false;
            }
        }

        private void buttonImportBrowse_Click(object sender, EventArgs e)
        {
            // フォルダ選択ダイアログの値をテキストボックスに置く
            if (folderBrowserDialogImport.ShowDialog() == DialogResult.OK)
            {
                textBoxImportDir.Text = folderBrowserDialogImport.SelectedPath;
            }
        }

        private void buttonExportBrowse_Click(object sender, EventArgs e)
        {
            // フォルダ選択ダイアログの値をテキストボックスに置く
            if (folderBrowserDialogExport.ShowDialog() == DialogResult.OK)
            {
                textBoxExportDir.Text = folderBrowserDialogExport.SelectedPath;
            }
        }

        // 接続されているリムーバブルディスクのうち一番若いドライブレターを返す
        private string GetFirstRemoveableDrive()
        {
            // リムーバブルドライブの一覧だけをあらかじめ取得する
            var removableDrives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Removable);

            // D～Zまでの23個分の配列をあらかじめ確保しておく
            string[] drives = new string[23];
            int idxCount = 0;

            // 配列にドライブレターを一つずつ格納する
            foreach (DriveInfo d in removableDrives)
            {
                drives[idxCount] = d.Name;
                idxCount++;
            }

            // 配列の最初のみ返す
            return drives[0];
        }
    }
}
