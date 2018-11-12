Public Class Frm_Presentacion

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Timer1.Start()
        Me.Hide()
        Frm_Gorilla.Show()
        Timer1.Stop()
        Timer1.Enabled = False
    End Sub

    Private Sub Frm_Presentacion_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        XML()
        conectar_bd()
    End Sub
End Class
