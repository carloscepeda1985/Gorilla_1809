Public Class popup_acceso

    Private Sub btn_volver_Click(sender As System.Object, e As System.EventArgs) Handles btn_volver.Click
        Me.Close()
    End Sub

    Private Sub popup_acceso_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Frm_Accesos.Enabled = True
    End Sub

End Class