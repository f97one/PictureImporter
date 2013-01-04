using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;

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

        // 接続されているリムーバブルディスクのうち一番若いドライブレターを返す
        private string GetFirstRemoveableDrive()
        {
            // リムーバブルドライブの一覧だけを、LINQであらかじめ取得する
            var removableDrives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Removable);

            // ダイレクトにToArray()せず、いったん格納用配列を初期化する
            string[] drives = new string[removableDrives.Count() + 1];

            int idxCount = 0;

            // 配列にドライブレターを一つずつ格納する
			// A:\とB:\はFDDなので無視する
			// （両方とも否定なのでAND条件にしている）
            foreach (DriveInfo d in removableDrives)
            {
				if (d.Name != "A:\\" && d.Name != "B:\\")
				{
					drives[idxCount] = d.Name;
					idxCount++;
				}
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
		private bool CopyImageFiles(string inputFile, string outputDir, bool overwriteFlag)
		{
			// ファイルコピーの結果をしめすフラグ
			bool result = false;

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
			}
		}

		private void buttonExec_Click(object sender, EventArgs e)
		{
			// 入出力ディレクトリの取得
			string inDirBase = textBoxImportDir.Text;	// 入力フォルダ
			string outDirBase = textBoxExportDir.Text;	// 出力フォルダ

			// コピーするファイルの一覧を取得
			string[] arrayCopyFiles = GetFilesMostDeep(inDirBase, "*.jpg");

			int successFiles = 0;
			int failedFiles = 0;


			// ファイルをコピーする
			foreach (string copyFile in arrayCopyFiles)
			{
				// ファイルコピーの結果をbooleanで取得する
				bool result = CopyImageFiles(copyFile, outDirBase, canOverwrite());

				// コピー成否をカウントする
				if (result)
				{
					successFiles++;
				}
				else
				{
					failedFiles++;
				}
			}

			// ファイルコピー結果をポップアップで知らせる
			string showMsg="ファイルをコピーしました！" +"\r\n"+
				"　成功ファイル数：" + successFiles.ToString() +"\r\n"+
				"　失敗ファイル数：" +failedFiles.ToString();

			MessageBox.Show(showMsg, "Picture Importer"); 
		}
	}
}
