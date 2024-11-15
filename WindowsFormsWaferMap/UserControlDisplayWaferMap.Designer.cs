
using System;

namespace HedgeHulkApp.Usercontrol
{
    partial class UserControlDisplayWaferMap
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UserControlDisplayWaferMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UserControlDisplayWaferMap";
            this.Size = new System.Drawing.Size(318, 201);
            this.Load += new System.EventHandler(this.UserControlDisplayWaferMap_Load);
            //this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UserControlWaferMap_MouseClick);
            
            this.Resize += new System.EventHandler(this.UserControlWaferMap_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
