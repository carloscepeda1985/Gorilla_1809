<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class popup_acceso
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pic_acceso = New System.Windows.Forms.PictureBox()
        Me.lbl_rut = New DevComponents.DotNetBar.LabelX()
        Me.btn_volver = New DevComponents.DotNetBar.ButtonX()
        CType(Me.pic_acceso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pic_acceso
        '
        Me.pic_acceso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pic_acceso.Location = New System.Drawing.Point(71, 27)
        Me.pic_acceso.Name = "pic_acceso"
        Me.pic_acceso.Size = New System.Drawing.Size(528, 385)
        Me.pic_acceso.TabIndex = 903
        Me.pic_acceso.TabStop = False
        '
        'lbl_rut
        '
        '
        '
        '
        Me.lbl_rut.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbl_rut.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rut.Location = New System.Drawing.Point(71, 418)
        Me.lbl_rut.Name = "lbl_rut"
        Me.lbl_rut.Size = New System.Drawing.Size(528, 77)
        Me.lbl_rut.TabIndex = 904
        Me.lbl_rut.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'btn_volver
        '
        Me.btn_volver.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btn_volver.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btn_volver.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_volver.Image = Global.Gorilla_1809.My.Resources.Resources.flecha_hacia_la_izquierda
        Me.btn_volver.Location = New System.Drawing.Point(181, 522)
        Me.btn_volver.Name = "btn_volver"
        Me.btn_volver.Size = New System.Drawing.Size(314, 91)
        Me.btn_volver.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7
        Me.btn_volver.TabIndex = 906
        Me.btn_volver.Text = " Volver"
        '
        'popup_acceso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 640)
        Me.Controls.Add(Me.btn_volver)
        Me.Controls.Add(Me.lbl_rut)
        Me.Controls.Add(Me.pic_acceso)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "popup_acceso"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "popup_acceso"
        CType(Me.pic_acceso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pic_acceso As System.Windows.Forms.PictureBox
    Friend WithEvents lbl_rut As DevComponents.DotNetBar.LabelX
    Friend WithEvents btn_volver As DevComponents.DotNetBar.ButtonX
End Class
