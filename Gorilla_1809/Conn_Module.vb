Imports System.Xml

Module Conn_Module

    Public Proveedor As String
    Public Servidor As String
    Public Basedatos As String
    Public Usuario As String
    Public Opcion As String
    Public Lugar As String
    Public Tamaño As String
    Public Tipo As String

    Public conec As New Data.Odbc.OdbcConnection
    Public consul As Odbc.OdbcDataReader
    Public Reader As Odbc.OdbcDataReader

    Public Sub XML()

        Try
            Dim m_xmlr As XmlTextReader

            m_xmlr = New XmlTextReader("C:\AppConfig\Gorilla_Settings.xml")
            m_xmlr.WhitespaceHandling = WhitespaceHandling.None
            m_xmlr.Read()
            m_xmlr.Read()

            While Not m_xmlr.EOF

                m_xmlr.Read()
                If Not m_xmlr.IsStartElement() Then Exit While
                Dim mCodigo = m_xmlr.GetAttribute("codigo")
                m_xmlr.Read()
                Dim mProvider = m_xmlr.ReadElementString("provider")
                Dim mServer = m_xmlr.ReadElementString("server")
                Dim mDatabase = m_xmlr.ReadElementString("database")
                Dim mUser = m_xmlr.ReadElementString("user")
                Dim mOption = m_xmlr.ReadElementString("option")
                Dim mPlace = m_xmlr.ReadElementString("place")
                Dim mSize = m_xmlr.ReadElementString("size")
                Dim mType = m_xmlr.ReadElementString("type")

                Proveedor = mProvider
                Servidor = mServer
                Basedatos = mDatabase
                Usuario = mUser
                Opcion = mOption
                Lugar = mPlace
                Tamaño = mSize
                Tipo = mType

            End While
            m_xmlr.Close()

        Catch Ex As Exception
            End
        End Try
    End Sub

    Public Sub conectar_bd()
        Try
            If conec.State = ConnectionState.Closed Then
                conec.ConnectionString = "Driver=" & Proveedor & ";Server=" & Servidor & ";Database=" & Basedatos & ";User=" & Usuario & ";Pwd=gorilla123;Option=" & Opcion & ";"
                conec.Open()
            End If
        Catch ex As Exception
            conec.Close()
        End Try
    End Sub

    Function insert(ByVal query As String) As Odbc.OdbcDataReader

        If Not conec.State = ConnectionState.Open Then
            conec.Open()
        End If
        Dim comando As New Odbc.OdbcCommand(query, conec)
        comando.ExecuteNonQuery()
        conec.Close()

    End Function


    Function consulta(ByVal query As String) As Odbc.OdbcDataReader
        If Not conec.State = ConnectionState.Open Then
            conec.Open()
        End If
        Dim comando As New Odbc.OdbcCommand(query, conec)
        comando.ExecuteNonQuery()
        Reader = comando.ExecuteReader()
        Return Reader
        conec.Close()
    End Function




End Module
