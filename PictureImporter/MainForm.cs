using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;

namespace PictureImporter
{
    public partial class MainForm : Form
    {
		private int copiedFiles;

        private int succesFilesCount;
        private int failedFilesCount;

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

			// 処理中ファイルを表示するラベルの文字を消去
			labelOperatedFiles.Text = "";
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
            // チェックボックスがはずれている場合は、出力先フォルダの
            // テキストボックスをクリアし、テキストボックスに入力
            // できるようにする。
            //   実際には、自身の状態で判断ができない(!)ようので、
            //   操作する出力先テキストボックスの状態で判断するように
            //   している(^^;)。
            if (!textBoxExportDir.Enabled)
            {
				// チェックを外した時の処理
                textBoxExportDir.Text = "";
                textBoxExportDir.Enabled = true;
				buttonExportBrowse.Enabled = true;
            }
            else
            {
				// チェックを入れたときの処理
                textBoxExportDir.Text = GetMyPicturePath();
                textBoxExportDir.Enabled = false;
				buttonExportBrowse.Enabled = false;
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

        /// <summary>
        /// 接続されているリムーバブルディスクのうち、一番若いドライブレターを返す。
        /// </summary>
        /// <returns>一番若いドライブレターのリムーバブルディスク、見つからないときはnull</returns>
        private string GetFirstRemoveableDrive()
        {
            var removable = DriveInfo.GetDrives()
                                .Where(d => d.DriveType == DriveType.Removable)
                                .FirstOrDefault(drv => (drv.Name != "A:\\" && drv.Name != "B:\\"));

            return removable == null ? null : removable.Name;
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
			// ファイルリスト取得開始をデバッグ出力
			Debug.Print(DateTime.Now + " Started to get files recursivery.");
		
            StringCollection hStringCollection = new StringCollection();

            // このディレクトリ内のすべてのファイルを検索する
            foreach (string stFilePath in Directory.GetFiles(stRootPath, stPattern))
            {
                hStringCollection.Add(stFilePath);
				Debug.Print(DateTime.Now + " Found image file, Filename = " + stFilePath);
            }

            // このディレクトリ内のすべてのサブディレクトリを検索する (再帰)
            foreach (string stDirPath in Directory.GetDirectories(stRootPath))
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

			// ファイルリスト取得終了をデバッグ出力
			Debug.Print(DateTime.Now + " Finished to get files recursivery.");

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
		private bool CopyImageFiles(string inputFile, string outputDir, bool overwriteFlag)
		{
			// ファイルコピーの結果をしめすフラグ
			bool result = false;

			copiedFiles++;
			//labelOperatedFiles.Text = copiedFiles.ToString();

			// コピーするファイルの名前だけを取得
			string filenameBody = Path.GetFileName(inputFile);

			// コピー元ファイルの最終更新日を取得して、ディレクトリを作る
			DateTime lastModDate = File.GetLastWriteTime(inputFile);
			MkDateDir(lastModDate, outputDir);

			string outputPath = outputDir + "\\" + lastModDate.ToString(@"yyyy_MM_dd") + "\\" + filenameBody;
			
			// ファイルをコピーする
			try
			{
				File.Copy(@inputFile, @outputPath, overwriteFlag);
				
				result = true;
			}
			catch (Exception)
			{
				result = false;
				//throw;
			}

			return result;
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
				// 
				Debug.Print(DateTime.Now + " Created directory, path = " + prefixDir + "\\" + dateDir);
			}
		}

		private void buttonExec_Click(object sender, EventArgs e)
        {
            // BackgroundWorker動作中は無視する
            if (backgroundWorker1.IsBusy)
            {
                return;
            }

            ChangeUILockState(false);

            labelOperatedFiles.Text = "0";

            backgroundWorker1.RunWorkerAsync();

        }

        private static void RaiseResult(int successFiles, int failedFiles)
        {
            // ファイルコピー結果をポップアップで知らせる
            string showMsg = "ファイルをコピーしました！" + Environment.NewLine +
                "　成功ファイル数：" + successFiles.ToString() + Environment.NewLine +
                "　失敗ファイル数：" + failedFiles.ToString();

            MessageBox.Show(showMsg, "Picture Importer");
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            copiedFiles = 0;

            // 入出力ディレクトリの取得
            string inDirBase = textBoxImportDir.Text;   // 入力フォルダ
            string outDirBase = textBoxExportDir.Text;  // 出力フォルダ

            // コピーするファイルの一覧を取得
            string[] arrayCopyFiles = GetFilesMostDeep(inDirBase, "*.jpg");

            foreach (string copyFile in arrayCopyFiles)
            {
                // 処理中ファイル数を表示
                //   処理中なので、処理に入った後の結果の合計より1少ないはずなので、
                //   表示は1加算した状態にする
                //labelOperatedFiles.Text = (successFiles + failedFiles + 1).ToString();

                // ファイルコピーの結果をbooleanで取得する
                bool result = CopyImageFiles(copyFile, outDirBase, canOverwrite());

                // コピー成否をカウントする
                if (result)
                {
                    succesFilesCount++;
                }
                else
                {
                    failedFilesCount++;
                }

                copiedFiles = succesFilesCount + failedFilesCount;

                backgroundWorker1.ReportProgress((int)(copiedFiles / arrayCopyFiles.Count()));
            }

            Debug.Print(DateTime.Now + " Finished to copy image files.");

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            labelOperatedFiles.Text = e.ProgressPercentage.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            RaiseResult(succesFilesCount, failedFilesCount);

            ChangeUILockState(true);
        }

        /// <summary>
        /// コントロールのロック状態を変更する。
        /// </summary>
        /// <param name="lockState">使用可とする場合true、使用不可とする場合false</param>
        private void ChangeUILockState(bool lockState)
        {
            textBoxImportDir.Enabled = lockState;
            textBoxExportDir.Enabled = lockState;
            buttonImportBrowse.Enabled = lockState;
            buttonExportBrowse.Enabled = lockState;
            checkBoxMyPicture.Enabled = lockState;
            groupBoxTreatSameFile.Enabled = lockState;
            buttonExec.Enabled = lockState;
        }
    }
}
