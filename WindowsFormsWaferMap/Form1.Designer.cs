
namespace WindowsFormsWaferMap
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.userControlDisplayWaferMap1 = new HedgeHulkApp.Usercontrol.UserControlDisplayWaferMap();
            this.userControlControlWaferMap1 = new HedgeHulkApp.Usercontrol.UserControlControlWaferMap();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.userControlDisplayWaferMap1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 450);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.userControlControlWaferMap1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(492, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(295, 450);
            this.panel2.TabIndex = 1;
            // 
            // userControlDisplayWaferMap1
            // 
            this.userControlDisplayWaferMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlDisplayWaferMap1.Location = new System.Drawing.Point(0, 0);
            this.userControlDisplayWaferMap1.Name = "userControlDisplayWaferMap1";
            this.userControlDisplayWaferMap1.Size = new System.Drawing.Size(492, 450);
            this.userControlDisplayWaferMap1.TabIndex = 0;
            // 
            // userControlControlWaferMap1
            // 
            this.userControlControlWaferMap1.Location = new System.Drawing.Point(29, 12);
            this.userControlControlWaferMap1.Name = "userControlControlWaferMap1";
            this.userControlControlWaferMap1.Size = new System.Drawing.Size(254, 343);
            this.userControlControlWaferMap1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private HedgeHulkApp.Usercontrol.UserControlDisplayWaferMap userControlDisplayWaferMap1;
        private HedgeHulkApp.Usercontrol.UserControlControlWaferMap userControlControlWaferMap1;
    }
}

