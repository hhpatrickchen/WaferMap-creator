
namespace HedgeHulkApp.Usercontrol
{
    partial class UserControlControlWaferMap
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
            this.buttonRun = new System.Windows.Forms.Button();
            this.ZoomOut = new System.Windows.Forms.Button();
            this.buttonResetWafer = new System.Windows.Forms.Button();
            this.ZoomIn = new System.Windows.Forms.Button();
            this.buttonResetZoom = new System.Windows.Forms.Button();
            this.buttonJump = new System.Windows.Forms.Button();
            this.textBoxAxisX = new System.Windows.Forms.TextBox();
            this.textBoxAxisY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFactor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxRow = new System.Windows.Forms.TextBox();
            this.textBoxCol = new System.Windows.Forms.TextBox();
            this.buttonNewMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(18, 140);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(81, 27);
            this.buttonRun.TabIndex = 20;
            this.buttonRun.Text = "Demo Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // ZoomOut
            // 
            this.ZoomOut.Location = new System.Drawing.Point(106, 110);
            this.ZoomOut.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.Size = new System.Drawing.Size(83, 26);
            this.ZoomOut.TabIndex = 19;
            this.ZoomOut.Text = "ZoomOut";
            this.ZoomOut.UseVisualStyleBackColor = true;
            this.ZoomOut.Click += new System.EventHandler(this.ZoomOut_Click);
            // 
            // buttonResetWafer
            // 
            this.buttonResetWafer.Location = new System.Drawing.Point(106, 140);
            this.buttonResetWafer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonResetWafer.Name = "buttonResetWafer";
            this.buttonResetWafer.Size = new System.Drawing.Size(83, 27);
            this.buttonResetWafer.TabIndex = 21;
            this.buttonResetWafer.Text = "Reset Wafer";
            this.buttonResetWafer.UseVisualStyleBackColor = true;
            this.buttonResetWafer.Click += new System.EventHandler(this.buttonResetWafer_Click);
            // 
            // ZoomIn
            // 
            this.ZoomIn.Location = new System.Drawing.Point(18, 110);
            this.ZoomIn.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Size = new System.Drawing.Size(83, 26);
            this.ZoomIn.TabIndex = 18;
            this.ZoomIn.Text = "ZoomIn";
            this.ZoomIn.UseVisualStyleBackColor = true;
            this.ZoomIn.Click += new System.EventHandler(this.ZoomIn_Click);
            // 
            // buttonResetZoom
            // 
            this.buttonResetZoom.Location = new System.Drawing.Point(16, 171);
            this.buttonResetZoom.Margin = new System.Windows.Forms.Padding(2);
            this.buttonResetZoom.Name = "buttonResetZoom";
            this.buttonResetZoom.Size = new System.Drawing.Size(83, 26);
            this.buttonResetZoom.TabIndex = 22;
            this.buttonResetZoom.Text = "Reset Zoom";
            this.buttonResetZoom.UseVisualStyleBackColor = true;
            this.buttonResetZoom.Click += new System.EventHandler(this.buttonResetZoom_Click);
            // 
            // buttonJump
            // 
            this.buttonJump.Location = new System.Drawing.Point(118, 248);
            this.buttonJump.Margin = new System.Windows.Forms.Padding(2);
            this.buttonJump.Name = "buttonJump";
            this.buttonJump.Size = new System.Drawing.Size(50, 26);
            this.buttonJump.TabIndex = 23;
            this.buttonJump.Text = "Jump";
            this.buttonJump.UseVisualStyleBackColor = true;
            this.buttonJump.Click += new System.EventHandler(this.buttonJump_Click);
            // 
            // textBoxAxisX
            // 
            this.textBoxAxisX.Location = new System.Drawing.Point(33, 212);
            this.textBoxAxisX.Name = "textBoxAxisX";
            this.textBoxAxisX.Size = new System.Drawing.Size(50, 22);
            this.textBoxAxisX.TabIndex = 24;
            this.textBoxAxisX.Text = "10";
            // 
            // textBoxAxisY
            // 
            this.textBoxAxisY.Location = new System.Drawing.Point(118, 212);
            this.textBoxAxisY.Name = "textBoxAxisY";
            this.textBoxAxisY.Size = new System.Drawing.Size(50, 22);
            this.textBoxAxisY.TabIndex = 25;
            this.textBoxAxisY.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "Factor:";
            // 
            // textBoxFactor
            // 
            this.textBoxFactor.Location = new System.Drawing.Point(49, 252);
            this.textBoxFactor.Name = "textBoxFactor";
            this.textBoxFactor.Size = new System.Drawing.Size(50, 22);
            this.textBoxFactor.TabIndex = 28;
            this.textBoxFactor.Text = "4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Row:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "Col:";
            // 
            // textBoxRow
            // 
            this.textBoxRow.Location = new System.Drawing.Point(148, 17);
            this.textBoxRow.Name = "textBoxRow";
            this.textBoxRow.Size = new System.Drawing.Size(50, 22);
            this.textBoxRow.TabIndex = 31;
            this.textBoxRow.Text = "20";
            // 
            // textBoxCol
            // 
            this.textBoxCol.Location = new System.Drawing.Point(53, 17);
            this.textBoxCol.Name = "textBoxCol";
            this.textBoxCol.Size = new System.Drawing.Size(50, 22);
            this.textBoxCol.TabIndex = 30;
            this.textBoxCol.Text = "30";
            // 
            // buttonNewMap
            // 
            this.buttonNewMap.Location = new System.Drawing.Point(148, 53);
            this.buttonNewMap.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewMap.Name = "buttonNewMap";
            this.buttonNewMap.Size = new System.Drawing.Size(50, 26);
            this.buttonNewMap.TabIndex = 34;
            this.buttonNewMap.Text = "New";
            this.buttonNewMap.UseVisualStyleBackColor = true;
            this.buttonNewMap.Click += new System.EventHandler(this.buttonNewMap_Click);
            // 
            // UserControlControlWaferMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonNewMap);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxRow);
            this.Controls.Add(this.textBoxCol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxFactor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAxisY);
            this.Controls.Add(this.textBoxAxisX);
            this.Controls.Add(this.buttonJump);
            this.Controls.Add(this.buttonResetZoom);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.ZoomOut);
            this.Controls.Add(this.buttonResetWafer);
            this.Controls.Add(this.ZoomIn);
            this.Name = "UserControlControlWaferMap";
            this.Size = new System.Drawing.Size(364, 409);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button ZoomOut;
        private System.Windows.Forms.Button buttonResetWafer;
        private System.Windows.Forms.Button ZoomIn;
        private System.Windows.Forms.Button buttonResetZoom;
        private System.Windows.Forms.Button buttonJump;
        private System.Windows.Forms.TextBox textBoxAxisX;
        private System.Windows.Forms.TextBox textBoxAxisY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFactor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxRow;
        private System.Windows.Forms.TextBox textBoxCol;
        private System.Windows.Forms.Button buttonNewMap;
    }
}
