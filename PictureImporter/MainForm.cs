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

        /// http://jeanne.wankuma.com/tips/csharp/directory/getfilesmostdeep.html
        /// ---------------------------------------------------------------------------------------
        /// <summary>
        ///     指定した検索パターンに一致するファイルを最下層まで検索しすべて返します。</summary>
        /// <param name="stRootPath">
        ///     検索を開始する最上層のディレクトリへのパス。</param>
        /// <param name="stPattern">
        ///     パス内のファイル名と対応させる検索文字列。</param>
        /// <returns>
        ///     検索パターンに一致したすべてのファイルパス。</returns>
        /// ---------------------------------------------------------------------------------------
        public static string[] GetFilesMostDeep(string stRootPath, string stPattern)
        {
            System.Collections.Specialized.StringCollection hStringCollection = (
                new System.Collections.Specialized.StringCollection()
            );

            // このディレクトリ内のすべてのファイルを検索する
            foreach (string stFilePath in System.IO.Directory.GetFiles(stRootPath, stPattern))
            {
                hStringCollection.Add(stFilePath);
            }

            // このディレクトリ内のすべてのサブディレクトリを検索する (再帰)
            foreach (string stDirPath in System.IO.Directory.GetDirectories(stRootPath))
            {
                string[] stFilePathes = GetFilesMostDeep(stDirPath, stPattern);

                // 条件に合致したファイルがあった場合は、ArrayList に加える
                if (stFilePathes != null)
                {
                    hStringCollection.AddRange(stFilePathes);
                }
            }

            // StringCollection を 1 次元の String 配列にして返す
            string[] stReturns = new string[hStringCollection.Count];
            hStringCollection.CopyTo(stReturns, 0);

            return stReturns;
        }

		// ラジオボタンの設定で上書きを許可するか否かを判断する
		private bool canOverwrite()
		{
			bool ret = false;

			if (radioButtonNotCopy.Checked) ret = false;
			if (radioButtonOverwrite.Checked) ret = true;

			return ret;
		}
		
		// ファイルをコピーする
		private void CopyImageFiles(string inputFile, string outputDir, bool overwriteFlag)
		{

		}

		// yyyy_MM_dd形式のディレクトリがなければ作成する
		private void MkDateDir(DateTime makeDirDate, string prefixDir)
		{
			// 日付フォーマットをyyyy_MM_ddに変更する
			string dateDir = makeDirDate.ToString(@"yyyy_MM_dd");

			// [PREFIX]\yyyy_MM_dd ディレクトリがなければ作成する
			if (!Directory.Exists(prefixDir + "\\" + dateDir))
			{
				Directory.CreateDirectory(prefixDir + "\\" + dateDir);
			}
		}
	}
}
