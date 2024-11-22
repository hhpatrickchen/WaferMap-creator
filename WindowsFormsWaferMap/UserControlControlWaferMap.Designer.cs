
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
            this.textBoxDieHeight = new System.Windows.Forms.TextBox();
            this.textBoxDieWidth = new System.Windows.Forms.TextBox();
            this.buttonNewMap = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxWaferDiemeter = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxWaferBorderness = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(5, 238);
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
            this.ZoomOut.Location = new System.Drawing.Point(91, 208);
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
            this.buttonResetWafer.Location = new System.Drawing.Point(91, 238);
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
            this.ZoomIn.Location = new System.Drawing.Point(3, 208);
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
            this.buttonResetZoom.Location = new System.Drawing.Point(3, 269);
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
            this.buttonJump.Location = new System.Drawing.Point(118, 346);
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
            this.textBoxAxisX.Location = new System.Drawing.Point(33, 310);
            this.textBoxAxisX.Name = "textBoxAxisX";
            this.textBoxAxisX.Size = new System.Drawing.Size(50, 22);
            this.textBoxAxisX.TabIndex = 24;
            this.textBoxAxisX.Text = "10";
            // 
            // textBoxAxisY
            // 
            this.textBoxAxisY.Location = new System.Drawing.Point(118, 310);
            this.textBoxAxisY.Name = "textBoxAxisY";
            this.textBoxAxisY.Size = new System.Drawing.Size(50, 22);
            this.textBoxAxisY.TabIndex = 25;
            this.textBoxAxisY.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "Factor:";
            // 
            // textBoxFactor
            // 
            this.textBoxFactor.Location = new System.Drawing.Point(49, 350);
            this.textBoxFactor.Name = "textBoxFactor";
            this.textBoxFactor.Size = new System.Drawing.Size(50, 22);
            this.textBoxFactor.TabIndex = 28;
            this.textBoxFactor.Text = "4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Die Height:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "Die Widh:";
            // 
            // textBoxDieHeight
            // 
            this.textBoxDieHeight.Location = new System.Drawing.Point(118, 44);
            this.textBoxDieHeight.Name = "textBoxDieHeight";
            this.textBoxDieHeight.Size = new System.Drawing.Size(56, 22);
            this.textBoxDieHeight.TabIndex = 31;
            this.textBoxDieHeight.Text = "2000";
            // 
            // textBoxDieWidth
            // 
            this.textBoxDieWidth.Location = new System.Drawing.Point(118, 17);
            this.textBoxDieWidth.Name = "textBoxDieWidth";
            this.textBoxDieWidth.Size = new System.Drawing.Size(56, 22);
            this.textBoxDieWidth.TabIndex = 30;
            this.textBoxDieWidth.Text = "3000";
            // 
            // buttonNewMap
            // 
            this.buttonNewMap.Location = new System.Drawing.Point(124, 136);
            this.buttonNewMap.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewMap.Name = "buttonNewMap";
            this.buttonNewMap.Size = new System.Drawing.Size(50, 26);
            this.buttonNewMap.TabIndex = 34;
            this.buttonNewMap.Text = "Create";
            this.buttonNewMap.UseVisualStyleBackColor = true;
            this.buttonNewMap.Click += new System.EventHandler(this.buttonNewMap_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "Wafer Diemeter:";
            // 
            // textBoxWaferDiemeter
            // 
            this.textBoxWaferDiemeter.Location = new System.Drawing.Point(118, 72);
            this.textBoxWaferDiemeter.Name = "textBoxWaferDiemeter";
            this.textBoxWaferDiemeter.Size = new System.Drawing.Size(56, 22);
            this.textBoxWaferDiemeter.TabIndex = 35;
            this.textBoxWaferDiemeter.Text = "20";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "Wafer Bordeness:";
            // 
            // textBoxWaferBorderness
            // 
            this.textBoxWaferBorderness.Location = new System.Drawing.Point(118, 100);
            this.textBoxWaferBorderness.Name = "textBoxWaferBorderness";
            this.textBoxWaferBorderness.Size = new System.Drawing.Size(56, 22);
            this.textBoxWaferBorderness.TabIndex = 37;
            this.textBoxWaferBorderness.Text = "2";
            // 
            // UserControlControlWaferMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxWaferBorderness);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxWaferDiemeter);
            this.Controls.Add(this.buttonNewMap);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDieHeight);
            this.Controls.Add(this.textBoxDieWidth);
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
            this.Size = new System.Drawing.Size(309, 409);
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
        private System.Windows.Forms.TextBox textBoxDieHeight;
        private System.Windows.Forms.TextBox textBoxDieWidth;
        private System.Windows.Forms.Button buttonNewMap;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxWaferDiemeter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxWaferBorderness;
    }
}
