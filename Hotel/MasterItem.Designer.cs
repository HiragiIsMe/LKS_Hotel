namespace Hotel
{
    partial class MasterItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxFee = new System.Windows.Forms.TextBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCan = new System.Windows.Forms.Button();
            this.buttonSav = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonIn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxFee
            // 
            this.textBoxFee.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFee.Location = new System.Drawing.Point(255, 268);
            this.textBoxFee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxFee.Name = "textBoxFee";
            this.textBoxFee.Size = new System.Drawing.Size(410, 28);
            this.textBoxFee.TabIndex = 57;
            this.textBoxFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFee_KeyPress);
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPrice.Location = new System.Drawing.Point(255, 217);
            this.textBoxPrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(410, 28);
            this.textBoxPrice.TabIndex = 56;
            this.textBoxPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPrice_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(48, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 24);
            this.label4.TabIndex = 55;
            this.label4.Text = "Compensation Fee :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(48, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 24);
            this.label3.TabIndex = 54;
            this.label3.Text = "Request Price :";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(255, 169);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(410, 28);
            this.textBoxName.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(48, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 24);
            this.label2.TabIndex = 52;
            this.label2.Text = "Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(520, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 29);
            this.label1.TabIndex = 58;
            this.label1.Text = "Item";
            // 
            // buttonCan
            // 
            this.buttonCan.BackColor = System.Drawing.Color.Yellow;
            this.buttonCan.FlatAppearance.BorderSize = 0;
            this.buttonCan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCan.ForeColor = System.Drawing.Color.Black;
            this.buttonCan.Location = new System.Drawing.Point(994, 634);
            this.buttonCan.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCan.Name = "buttonCan";
            this.buttonCan.Size = new System.Drawing.Size(111, 35);
            this.buttonCan.TabIndex = 64;
            this.buttonCan.Text = "Cancel";
            this.buttonCan.UseVisualStyleBackColor = false;
            this.buttonCan.Click += new System.EventHandler(this.buttonCan_Click);
            // 
            // buttonSav
            // 
            this.buttonSav.BackColor = System.Drawing.Color.Yellow;
            this.buttonSav.FlatAppearance.BorderSize = 0;
            this.buttonSav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSav.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSav.ForeColor = System.Drawing.Color.Black;
            this.buttonSav.Location = new System.Drawing.Point(740, 634);
            this.buttonSav.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSav.Name = "buttonSav";
            this.buttonSav.Size = new System.Drawing.Size(111, 35);
            this.buttonSav.TabIndex = 63;
            this.buttonSav.Text = "Save";
            this.buttonSav.UseVisualStyleBackColor = false;
            this.buttonSav.Click += new System.EventHandler(this.buttonSav_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.BackColor = System.Drawing.Color.Yellow;
            this.buttonDel.FlatAppearance.BorderSize = 0;
            this.buttonDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDel.ForeColor = System.Drawing.Color.Black;
            this.buttonDel.Location = new System.Drawing.Point(489, 634);
            this.buttonDel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(111, 35);
            this.buttonDel.TabIndex = 62;
            this.buttonDel.Text = "Delete";
            this.buttonDel.UseVisualStyleBackColor = false;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.BackColor = System.Drawing.Color.Yellow;
            this.buttonUp.FlatAppearance.BorderSize = 0;
            this.buttonUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp.ForeColor = System.Drawing.Color.Black;
            this.buttonUp.Location = new System.Drawing.Point(242, 634);
            this.buttonUp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(111, 35);
            this.buttonUp.TabIndex = 61;
            this.buttonUp.Text = "Update";
            this.buttonUp.UseVisualStyleBackColor = false;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonIn
            // 
            this.buttonIn.BackColor = System.Drawing.Color.Yellow;
            this.buttonIn.FlatAppearance.BorderSize = 0;
            this.buttonIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIn.ForeColor = System.Drawing.Color.Black;
            this.buttonIn.Location = new System.Drawing.Point(12, 634);
            this.buttonIn.Margin = new System.Windows.Forms.Padding(4);
            this.buttonIn.Name = "buttonIn";
            this.buttonIn.Size = new System.Drawing.Size(111, 35);
            this.buttonIn.TabIndex = 60;
            this.buttonIn.Text = "Insert";
            this.buttonIn.UseVisualStyleBackColor = false;
            this.buttonIn.Click += new System.EventHandler(this.buttonIn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 390);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1095, 226);
            this.dataGridView1.TabIndex = 59;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // MasterItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1116, 675);
            this.Controls.Add(this.buttonCan);
            this.Controls.Add(this.buttonSav);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.buttonIn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFee);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Name = "MasterItem";
            this.Text = "MasterItem";
            this.Load += new System.EventHandler(this.MasterItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFee;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCan;
        private System.Windows.Forms.Button buttonSav;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonIn;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}