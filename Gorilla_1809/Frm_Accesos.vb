Public Class Frm_Accesos

    Private Sub btn_volver_Click(sender As System.Object, e As System.EventArgs) Handles btn_volver.Click
        Me.Close()
    End Sub

    Private Sub Frm_Accesos_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Frm_Gorilla.txt_rut.Focus()
    End Sub

    Private Sub Frm_Accesos_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim sql_t As String
        Dim I As Integer

        sql_t = Nothing
        sql_t = "Select * from accesos_t Order By ID Desc Limit 10 "
   
        conec.Close()
        consul = consulta(sql_t)

        If consul.HasRows = True Then

            While consul.Read

                With grd_registros
                    .RowCount = .RowCount + 1

                    .Item(0, I).Value = consul.Item("id")
                    .Item(1, I).Value = consul.Item("fecha")
                    .Item(2, I).Value = consul.Item("destino")
                    .Item(3, I).Value = consul.Item("rut")
                    .Item(4, I).Value = consul.Item("nombres")
                    .Item(5, I).Value = consul.Item("apellidos")
                    .Item(6, I).Value = consul.Item("estado")
                    .Item(7, I).Value = consul.Item("ruta")

                End With
                I += 1

            End While
        End If

    End Sub

    Private Sub ButtonX1_Click(sender As System.Object, e As System.EventArgs) Handles ButtonX1.Click

        Try

            Dim sql_t As String
            Dim I As Integer
            Dim Id_acceso As String

            Id_acceso = grd_registros.Item(0, 9).Value

            sql_t = Nothing
            sql_t = "Select * from accesos_t where id < '" & Id_acceso & "' Order By ID Desc Limit 10 "

            conec.Close()
            consul = consulta(sql_t)

            If consul.HasRows = True Then

                grd_registros.RowCount = 0

                While consul.Read

                    With grd_registros
                        .RowCount = .RowCount + 1

                        .Item(0, I).Value = consul.Item("id")
                        .Item(1, I).Value = consul.Item("fecha")
                        .Item(2, I).Value = consul.Item("destino")
                        .Item(3, I).Value = consul.Item("rut")
                        .Item(4, I).Value = consul.Item("nombres")
                        .Item(5, I).Value = consul.Item("apellidos")
                        .Item(6, I).Value = consul.Item("estado")
                        .Item(7, I).Value = consul.Item("ruta")

                    End With
                    I += 1

                End While
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub grd_registros_Click(sender As Object, e As System.EventArgs) Handles grd_registros.Click
        If grd_registros.RowCount <> 0 Then

            Dim id As String
            Dim y As Integer

            Try
                y = grd_registros.CurrentCell.RowIndex
                id = ""
                id = grd_registros.Item(7, y).Value

                popup_acceso.lbl_rut.Text = grd_registros.Item(3, y).Value
                popup_acceso.pic_acceso.ImageLocation = Tipo & ":\Gorilla_Files\" & id & ".jpg"
                popup_acceso.pic_acceso.SizeMode = PictureBoxSizeMode.StretchImage

                popup_acceso.Show()

                Me.Enabled = False

            Catch ex As Exception

            End Try

        End If
    End Sub
End Class