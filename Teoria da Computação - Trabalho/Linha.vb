Public Class Linha
    Public texto As String
    Public nr As Integer
    Public teste As Boolean
    Public teste_nome As String
    Public teste_se_V As Integer
    Public teste_se_F As Integer
    Public funcao As Boolean
    Public funcao_nome As String
    Public funcao_va_para As Integer
    Public passou As Boolean

    Public Sub New(ByVal linha As String, ByVal nrLinha As Integer)
        nr = nrLinha
        passou = False
        funcao = False
        teste = False

        texto = linha
        Dim f() As String = linha.Split(" ")

        '' Se for função:
        If f(0).ToLower.Trim = "faça" Then
            funcao = True
            funcao_nome = f(1).ToUpper.Trim
            funcao_va_para = f(3).Trim

            '' Se for Teste:
        ElseIf f(0).ToLower.Trim = "se" Then
            teste = True
            teste_nome = f(1).ToUpper.Trim
            teste_se_V = f(4).Trim
            teste_se_F = f(7).Trim
        End If
    End Sub


End Class
