Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Data.Odbc
Imports System.Data
Imports System.Globalization
Imports System.Runtime.InteropServices.ComTypes

Imports Newtonsoft.Json
Imports DirectShowLib

Public Class Frm_Gorilla

    Dim cadena, nombres, apellidos, nom1, nom2, ape1, ape2, ruta, Fecha_Hora As String

    Private Sub Frm_Gorilla_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        CaptureVideo()
        txt_rut.Focus()

    End Sub

    Private Sub txt_rut_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txt_rut.KeyPress

        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        e.KeyChar = UCase(e.KeyChar)
        If KeyAscii = 13 Then

            Fecha_Hora = Now()
            btn_accesos.Enabled = False
            btn_entrada.Enabled = False
            pic_scan.Visible = False
            lbl_fecha_hora.Text = Fecha_Hora
            Busca_Persona_Online()
            pnl_teclado.Visible = True

        End If

    End Sub

    Public Sub Busca_Persona_Online()

        Dim client = New WebClient With {.Encoding = System.Text.Encoding.UTF8}

        Dim JS = client.DownloadString("https://api.rutify.cl/search?q=" & txt_rut.Text)
        Dim res As List(Of RootObject) = CType(JsonConvert.DeserializeObject(JS, GetType(List(Of RootObject))), List(Of RootObject))

        If res.Count = 0 Then

            txt_nombres.Visible = True
            txt_nombres.Focus()
            pic_primero.Visible = True
            txt_apellidos.Visible = True
            lbl_estado.Text = "NO ENCONTRADO"

        End If

        For Each item In res

            cadena = item.name.ToString()
            ape1 = Mid(cadena, 1, cadena.IndexOf(" "))
            ape1 = StrConv(ape1, VbStrConv.ProperCase)
            cadena = Mid(cadena, cadena.IndexOf(" ") + 2, Len(cadena))
            ape2 = Mid(cadena, 1, cadena.IndexOf(" "))
            ape2 = StrConv(ape2, VbStrConv.ProperCase)
            cadena = Mid(cadena, cadena.IndexOf(" ") + 2, Len(cadena))

            apellidos = ape1 & " " & ape2

            nom1 = Mid(cadena, 1, cadena.IndexOf(" "))
            nom1 = StrConv(nom1, VbStrConv.ProperCase)
            nom2 = Mid(cadena, cadena.IndexOf(" ") + 2, Len(cadena))
            nom2 = StrConv(nom2, VbStrConv.ProperCase)

            nombres = nom1 & " " & nom2

            lbl_nombres.Visible = True
            lbl_apellidos.Visible = True
            lbl_nombres.Text = nombres
            lbl_apellidos.Text = apellidos
            lbl_apellidosdb.Text = apellidos
            lbl_nombresbd.Text = nombres
            lbl_estado.Text = "AUTORIZADO"
            txt_destino.Focus()
            pic_tercero.Visible = True
        Next

    End Sub

    Public Class RootObject
        Public Property name As String
        Public Property rut As String
    End Class

    Private Sub btn_entrada_Click(sender As System.Object, e As System.EventArgs) Handles btn_entrada.Click

        Dim sql As String

        If nombres = "" Then
            nombres = lbl_nombresbd.Text()
        End If
        If apellidos = "" Then
            apellidos = lbl_apellidosdb.Text()
        End If

        ruta = Replace(Fecha_Hora, " ", "")
        ruta = Replace(ruta, "-", "")
        ruta = Replace(ruta, ":", "")

        sql = Nothing
        sql = "Insert Into accesos_t(rut, nombres, apellidos, destino, fecha, lugar, ruta, estado) Values('" & txt_rut.Text & "', '" & nombres & "', '" & apellidos & "', '" & txt_destino.Text & "', '" & Fecha_Hora & "', '" & Lugar & "', '" & ruta & "', '" & lbl_estado.Text & "')"

        conec.Close()
        consul = insert(sql)

        Foto(ruta)

        pic_scan.Visible = True
        btn_entrada.Enabled = False
        btn_accesos.Enabled = True
        txt_rut.Text = ""
        txt_destino.Text = ""
        lbl_nombres.Text = ""
        lbl_apellidos.Text = ""
        lbl_fecha_hora.Text = ""
        lbl_estado.Text = ""
        pic_primero.Visible = False
        pic_segundo.Visible = False
        pic_tercero.Visible = False
        txt_rut.Focus()
        txt_nombres.Text = ""
        txt_apellidos.Text = ""
        txt_nombres.Visible = False
        txt_apellidos.Visible = False
        lbl_apellidos.Visible = False
        lbl_nombres.Visible = False
        nombres = ""
        apellidos = ""

    End Sub

    Private Sub btn_accesos_Click(sender As System.Object, e As System.EventArgs) Handles btn_accesos.Click
        Frm_Accesos.Show()
    End Sub

    Private Sub txt_destino_GotFocus(sender As Object, e As System.EventArgs) Handles txt_destino.GotFocus
        pic_primero.Visible = False
        pic_segundo.Visible = False
        pic_tercero.Visible = True
    End Sub

    Private Sub txt_destino_TextChanged(sender As System.Object, e As System.EventArgs) Handles txt_destino.TextChanged
        If txt_destino.Text <> "" Then
            btn_entrada.Enabled = True
        End If
        If txt_destino.Text = "" Then
            btn_entrada.Enabled = False
        End If
    End Sub

    Private Sub btn_enter_Click(sender As System.Object, e As System.EventArgs) Handles btn_enter.Click

        If pic_primero.Visible = True Then
            nombres = txt_nombres.Text()
            nombres = StrConv(nombres, VbStrConv.ProperCase)
            pic_segundo.Visible = True
            pic_primero.Visible = False
        Else
            If pic_segundo.Visible = True Then
                apellidos = txt_apellidos.Text()
                apellidos = StrConv(apellidos, VbStrConv.ProperCase)
                pic_tercero.Visible = True
                pic_segundo.Visible = False
            End If
        End If

        If pic_tercero.Visible = True Then
            If txt_destino.Text <> "" Then
                nombres = txt_nombres.Text()
                nombres = StrConv(nombres, VbStrConv.ProperCase)
                apellidos = txt_apellidos.Text()
                apellidos = StrConv(apellidos, VbStrConv.ProperCase)
                pnl_teclado.Visible = False
            End If
        End If
    End Sub

    Private Sub btn_q_Click(sender As System.Object, e As System.EventArgs) Handles btn_q.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "q"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "q"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "q"
        End If
    End Sub

    Private Sub btn_w_Click(sender As System.Object, e As System.EventArgs) Handles btn_w.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "w"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "w"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "w"
        End If
    End Sub

    Private Sub btn_e_Click(sender As System.Object, e As System.EventArgs) Handles btn_e.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "e"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "e"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "e"
        End If
    End Sub

    Private Sub btn_r_Click(sender As System.Object, e As System.EventArgs) Handles btn_r.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "r"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "r"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "r"
        End If
    End Sub

    Private Sub btn_t_Click(sender As System.Object, e As System.EventArgs) Handles btn_t.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "t"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "t"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "t"
        End If
    End Sub

    Private Sub btn_y_Click(sender As System.Object, e As System.EventArgs) Handles btn_y.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "y"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "y"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "y"
        End If
    End Sub

    Private Sub btn_u_Click(sender As System.Object, e As System.EventArgs) Handles btn_u.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "u"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "u"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "u"
        End If
    End Sub

    Private Sub btn_i_Click(sender As System.Object, e As System.EventArgs) Handles btn_i.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "i"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "i"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "i"
        End If
    End Sub

    Private Sub btn_o_Click(sender As System.Object, e As System.EventArgs) Handles btn_o.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "o"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "o"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "o"
        End If
    End Sub

    Private Sub btn_p_Click(sender As System.Object, e As System.EventArgs) Handles btn_p.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "p"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "p"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "p"
        End If
    End Sub

    Private Sub btn_a_Click(sender As System.Object, e As System.EventArgs) Handles btn_a.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "a"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "a"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "a"
        End If
    End Sub

    Private Sub btn_s_Click(sender As System.Object, e As System.EventArgs) Handles btn_s.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "s"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "s"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "s"
        End If
    End Sub

    Private Sub btn_d_Click(sender As System.Object, e As System.EventArgs) Handles btn_d.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "d"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "d"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "d"
        End If
    End Sub

    Private Sub btn_f_Click(sender As System.Object, e As System.EventArgs) Handles btn_f.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "f"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "f"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "f"
        End If
    End Sub

    Private Sub btn_g_Click(sender As System.Object, e As System.EventArgs) Handles btn_g.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "g"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "g"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "g"
        End If
    End Sub

    Private Sub btn_h_Click(sender As System.Object, e As System.EventArgs) Handles btn_h.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "h"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "h"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "h"
        End If
    End Sub

    Private Sub btn_j_Click(sender As System.Object, e As System.EventArgs) Handles btn_j.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "j"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "j"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "j"
        End If
    End Sub

    Private Sub btn_k_Click(sender As System.Object, e As System.EventArgs) Handles btn_k.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "k"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "k"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "k"
        End If
    End Sub

    Private Sub btn_l_Click(sender As System.Object, e As System.EventArgs) Handles btn_l.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "l"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "l"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "l"
        End If
    End Sub

    Private Sub btn_ñ_Click(sender As System.Object, e As System.EventArgs) Handles btn_ñ.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "ñ"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "ñ"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "ñ"
        End If
    End Sub

    Private Sub btn_z_Click(sender As System.Object, e As System.EventArgs) Handles btn_z.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "z"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "z"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "z"
        End If
    End Sub

    Private Sub btn_x_Click(sender As System.Object, e As System.EventArgs) Handles btn_x.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "x"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "x"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "x"
        End If
    End Sub

    Private Sub btn_c_Click(sender As System.Object, e As System.EventArgs) Handles btn_c.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "c"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "c"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "c"
        End If
    End Sub

    Private Sub btn_v_Click(sender As System.Object, e As System.EventArgs) Handles btn_v.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "v"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "v"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "v"
        End If
    End Sub

    Private Sub btn_b_Click(sender As System.Object, e As System.EventArgs) Handles btn_b.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "b"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "b"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "b"
        End If
    End Sub

    Private Sub btn_n_Click(sender As System.Object, e As System.EventArgs) Handles btn_n.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "n"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "n"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "n"
        End If
    End Sub

    Private Sub btn_m_Click(sender As System.Object, e As System.EventArgs) Handles btn_m.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "m"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "m"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "m"
        End If
    End Sub

    Private Sub b1_Click(sender As System.Object, e As System.EventArgs) Handles b1.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "1"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "1"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "1"
        End If
    End Sub

    Private Sub b2_Click(sender As System.Object, e As System.EventArgs) Handles b2.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "2"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "2"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "2"
        End If
    End Sub

    Private Sub b3_Click(sender As System.Object, e As System.EventArgs) Handles b3.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "3"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "3"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "3"
        End If
    End Sub

    Private Sub b4_Click(sender As System.Object, e As System.EventArgs) Handles b4.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "4"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "4"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "4"
        End If
    End Sub

    Private Sub b5_Click(sender As System.Object, e As System.EventArgs) Handles b5.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "5"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "5"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "5"
        End If
    End Sub

    Private Sub b6_Click(sender As System.Object, e As System.EventArgs) Handles b6.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "6"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "6"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "6"
        End If
    End Sub

    Private Sub b0_Click(sender As System.Object, e As System.EventArgs) Handles b0.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "0"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "0"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "0"
        End If
    End Sub

    Private Sub b7_Click(sender As System.Object, e As System.EventArgs) Handles b7.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "7"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "7"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "7"
        End If
    End Sub

    Private Sub b8_Click(sender As System.Object, e As System.EventArgs) Handles b8.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "8"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "8"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "8"
        End If
    End Sub

    Private Sub b9_Click(sender As System.Object, e As System.EventArgs) Handles b9.Click
        If pic_primero.Visible = True Then
            txt_nombres.Text = txt_nombres.Text & "9"
        End If
        If pic_segundo.Visible = True Then
            txt_apellidos.Text = txt_apellidos.Text & "9"
        End If
        If pic_tercero.Visible = True Then
            txt_destino.Text = txt_destino.Text & "9"
        End If
    End Sub

    Private Sub ButtonX15_Click(sender As System.Object, e As System.EventArgs) Handles ButtonX15.Click
        If pic_primero.Visible = True And txt_nombres.Text <> "" Then
            txt_nombres.Text = Mid(txt_nombres.Text, 1, Len(txt_nombres.Text) - 1)
        End If

        If pic_segundo.Visible = True And txt_apellidos.Text <> "" Then
            txt_apellidos.Text = Mid(txt_apellidos.Text, 1, Len(txt_apellidos.Text) - 1)
        End If

        If pic_tercero.Visible = True And txt_destino.Text <> "" Then
            txt_destino.Text = Mid(txt_destino.Text, 1, Len(txt_destino.Text) - 1)
        End If
    End Sub

    Private Sub txt_nombres_GotFocus(sender As Object, e As System.EventArgs) Handles txt_nombres.GotFocus
        pic_primero.Visible = True
        pic_segundo.Visible = False
        pic_tercero.Visible = False
    End Sub

    Private Sub txt_apellidos_GotFocus(sender As Object, e As System.EventArgs) Handles txt_apellidos.GotFocus
        pic_primero.Visible = False
        pic_segundo.Visible = True
        pic_tercero.Visible = False
    End Sub


End Class