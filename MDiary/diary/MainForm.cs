/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2019/1/25 星期五
 * 时间: 15:12
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace diary
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void ToolStripButton1Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "MarkDown文件|*.MD|文本文件|*.txt|所有文件|*.*";
			ofd.ShowDialog();
			string path = ofd.FileName;
			if (path == ""){return;}
			using (FileStream fsRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
			{
			byte[] buffer = new byte[1024 * 1024 * 5];
			int r = fsRead.Read(buffer, 0, buffer.Length);
			textBox1.Text = Encoding.Default.GetString(buffer, 0, r);
			}
		}
		
		void ToolStripButton2Click(object sender, EventArgs e)
		{
			try {
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "MarkDown文件|*.MD|文本文件|*.txt|所有文件|*.*";
				sfd.FileName= DateTime.Now.ToString("yyyy-MM-dd"); 
				sfd.ShowDialog();
				string path = sfd.FileName;
				if (path == ""){return;}
				using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
				{
					byte[] buffer = Encoding.Default.GetBytes(textBox1.Text+"\r\n\r\n\r\n\r\n\r\n\r\n"+textBox2.Text);
					fsWrite.Write(buffer, 0, buffer.Length);
				}
			} catch (Exception) {
			}
				
		}		
		void ToolStripButton3Click(object sender, EventArgs e)
		{
			textBox1.Text+="**粗体**"+"\r\n";
		}
		void ToolStripButton4Click(object sender, EventArgs e)
		{
			textBox1.Text+="*斜体*"+"\r\n";
		}
		
		void ToolStripButton5Click(object sender, EventArgs e)
		{
			textBox1.Text+="<p  align=\"center\">居中</p>"+"\r\n";
		}
		void ToolStripButton6Click(object sender, EventArgs e)
		{
			textBox1.Text+="<font color=\"violet\"> </font>"+"\r\n";
		}
							
		void ToolStripButton7Click(object sender, EventArgs e)
		{
			 textBox1.Text+="1.  "+"\r\n"+"2. "+"\r\n"+"3. "+"\r\n"+"4. "+"\r\n";
		}
		
		void ToolStripButton8Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://mahua.jser.me/"); 
		}
		void ToolStripButton9Click(object sender, EventArgs e)
		{
			textBox1.Text+="&nbsp;&nbsp;"+"\r\n";
		}
		void ToolStripButton10Click(object sender, EventArgs e)
		{
			textBox1.Text+="<br/>"+"\r\n";
		}

		
		//-----------------------------------------------------//		
		void Button1Click(object sender, EventArgs e)
		{
			textBox1.Text+="*"+DateTime.Now.ToString()+"*"+"\r\n";    
		}

		void Button2MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if(textBox3.Text !=""){
				string img="![]["+GetRandomString(6)+"]";
				textBox1.Text+=img+"\r\n";
				
				string base64="["+GetRandomString(6)+"]:data:image/png;base64,";
				textBox2.Text+=base64+ImgToBase64String(textBox3.Text.Trim())+"\r\n";
				}
			}
			else if(e.Button == MouseButtons.Right)
			{
			 if(textBox3.Text !=""){
				string img="![]["+GetRandomString(6)+"]";
				string base64="["+GetRandomString(6)+"]:data:image/png;base64,";
				string clip=img+"\r\n"+base64+ImgToBase64String(textBox3.Text.Trim())+"\r\n";
				 Clipboard.SetDataObject(clip); 
				}
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
			if(textBox3.Text !=""){
				string link="[链接]("+textBox3.Text.Trim()+")";
				textBox1.Text+=link+"\r\n";
			}
	
		}
				
		void TextBox1KeyPress(object sender, KeyPressEventArgs e)
		{
	   		 TextBox textBox = sender as TextBox;
		    if (textBox == null)
		        return;
		    if (e.KeyChar == (char)1)       // Ctrl-A 相当于输入了AscII=1的控制字符
		    {
		        textBox.SelectAll();
		        e.Handled = true;      // 不再发出“噔”的声音
		    }
		}
				
		void TextBox3DragDrop(object sender, DragEventArgs e)
		{
	        string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString(); 
	      	 System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
	      	 if(System.Math.Ceiling(fileInfo.Length / 1024.0) >100)
	      	 {MessageBox.Show("请保持图片大小低于100KB");}
	      	 else
	      	 {textBox3.Text = path;}
		}
		void TextBox3DragEnter(object sender, DragEventArgs e)
		{
	            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;   //重要代码：表明是所有类型的数据，比如文件路径
           	    else
                e.Effect = DragDropEffects.None;
		}
		
	
		//------------------------------函数------------------------//
		/// <summary>
		/// 图片转为base64编码的字符串
		/// </summary>
		/// <param name="Imagefilename"></param>
		/// <returns></returns>
		public string ImgToBase64String(string Imagefilename)
		{
		try
		{
		Bitmap bmp = new Bitmap(Imagefilename);
		
		MemoryStream ms = new MemoryStream();
		bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
		byte[] arr = new byte[ms.Length];
		ms.Position = 0;
		ms.Read(arr, 0, (int)ms.Length);
		ms.Close();
		return Convert.ToBase64String(arr);
		}
		catch (Exception)
		{
		return null;
		}
		}

		 string GetRandomString(int count)
        {
            int number;
            string checkCode = String.Empty;     //存放随机码的字符串 

            System.Random random = new Random();

            for (int i = 0; i < count; i++) //产生4位校验码 
            {
                number = random.Next();
                number = number % 36;
                if (number < 10)
                {
                    number += 48;    //数字0-9编码在48-57 
                }
                else
                {
                    number += 55;    //字母A-Z编码在65-90 
                }

                checkCode += ((char)number).ToString();
            }
            return checkCode;
        }





		//**********************************************************//
	}
}
