

Imports Teoria_da_Computação___Trabalho

Public Class Form1
    Dim PintarBitmap As Bitmap
    Dim Pintura As Graphics
    Dim Caneta As New Pen(Color.Black, 2)

    Dim linhas1 As New List(Of Linha)
    Dim linhas2 As New List(Of Linha)


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '' Desenha fluxograma
        desenha()

    End Sub

    Private Sub desenha()
        PintarBitmap = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Pintura = Graphics.FromImage(PintarBitmap)
        PictureBox1.Image = PintarBitmap
        Pintura.Clear(Color.White)
        Pintura.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        '' Desenha partida:
        partida()

        'Pintura.DrawLine(Caneta, 25, 25, 50, 50)

    End Sub

    Private Sub partida()
        '' Tamanho do texto:
        Dim Tamanho As SizeF = Pintura.MeasureString("Partida", DefaultFont)

        '' largura (meio):
        Dim x As Integer = (PictureBox1.Width / 2) - (Tamanho.Width + 10) / 2

        '' Altura (superior):
        Dim y As Integer = 10

        '' Escreve texto:
        Pintura.DrawString("Partida", DefaultFont, Brushes.Black, x + 5, y + 5)

        '' Desenha retangulo:
        Pintura.DrawRectangle(Pens.Black, x, y, Tamanho.Width + 10, Tamanho.Height + 10)

        '' Ponto central abaixo do retangulo
        Dim zx As Integer = x + (Tamanho.Width + 10) / 2
        Dim zy As Integer = y + Tamanho.Height + 10

        Pintura.DrawLine(Caneta, zx, zy, zx, zy + 20)

    End Sub

    '' Botao Etapa 1 - Transforma entrada:
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '' Pega todas linhas da entrada:
        Dim x1() As String = RichTextBox1.Lines
        Dim x2() As String = RichTextBox2.Lines

        '' Limpa entrada:
        RichTextBox1.Clear()
        RichTextBox2.Clear()

        '' Escreve linhas formatadas
        RichTextBox1.Text = addLinhas(linhas1, x1)
        RichTextBox2.Text = addLinhas(linhas2, x2)

        '' Limpando caixas:
        RichTextBox3.Clear()
        RichTextBox4.Clear()

        '' Adicionando titulo
        RichTextBox3.Text &= "Programa 1 com " & linhas1.Count & " linhas:" & vbNewLine & vbNewLine
        RichTextBox4.Text &= "Programa 2 com " & linhas2.Count & " linhas:" & vbNewLine & vbNewLine

        etapa1(linhas1, RichTextBox3)
        etapa1(linhas2, RichTextBox4)


        '' Testa os programas:
        'testarPrograma(linhas1, RichTextBox3)
        'testarPrograma(linhas2, RichTextBox4)
    End Sub

    Private Sub etapa1(ByVal linhas As List(Of Linha), ByRef richText3 As RichTextBox)

        Dim cnt As Integer = 1

        For Each x In linhas
            '' Se for FUNCAO
            If x.funcao Then
                If x.funcao_va_para = x.nr Then '' Se for Ciclo
                    richText3.Text &= cnt & ": (ciclo,w),(ciclo,w)" & vbNewLine : cnt += 1
                ElseIf x.funcao_va_para > linhas.Count Then '' Se for parada
                    richText3.Text &= cnt & ": (" & x.funcao_nome & ",ε),(" & x.funcao_nome & ",ε)" & vbNewLine : cnt += 1
                Else
                    richText3.Text &= cnt & ": (" & x.funcao_nome & "," & x.funcao_va_para & "),(" & x.funcao_nome & "," & x.funcao_va_para & ")" & vbNewLine : cnt += 1
                End If


            Else '' se for TESTE
                '' Se os dois forem ciclo:
                If x.teste_se_V = x.nr And x.teste_se_F = x.nr Then
                    richText3.Text &= cnt & ": (ciclo,ω),(ciclo,ω)" & vbNewLine : cnt += 1
                Else
                    '' Inicio V:
                    richText3.Text &= cnt & ": " : cnt += 1

                    If x.teste_se_V = x.nr Then '' Se for ciclo:
                        richText3.Text &= "(ciclo,ω),"
                    ElseIf x.teste_se_V = 0 Or x.teste_se_V > linhas.Count Then '' Se for parada:
                        richText3.Text &= "(Parada, ε),"
                    Else
                        richText3.Text &= "(" & linhas(x.teste_se_V - 1).funcao_nome & "," & linhas(x.teste_se_V - 1).funcao_va_para & "),"
                    End If

                    '' INICIO F:
                    If x.teste_se_F = x.nr Then '' Se for ciclo:
                        richText3.Text &= "(ciclo,ω)" & vbNewLine
                    ElseIf x.teste_se_F = 0 Or x.teste_se_F > linhas.Count Then '' Se for parada
                        richText3.Text &= "(Parada, ε)" & vbNewLine
                    Else
                        richText3.Text &= "(" & linhas(x.teste_se_F - 1).funcao_nome & "," & linhas(x.teste_se_F - 1).funcao_va_para & ")" & vbNewLine
                    End If
                End If
            End If
        Next

    End Sub

    Private Function addLinhas(ByRef linhas As List(Of Linha), ByVal x() As String)
        '' Limpa dados anteriores:
        linhas.Clear()

        '' Variavel que armazena texto formatado
        Dim formatado As String = ""

        '' Verifica o que é linha utilizavel:
        For i = 0 To x.Count - 1
            Try
                If x(i).Contains(":") And x(i) <> "" Then
                    Dim y() As String = x(i).Split(":")
                    linhas.Add(New Linha(y(1).Trim, y(0).Trim))
                    formatado &= x(i) & vbNewLine
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Next

        Return formatado
    End Function

    Private Sub testarPrograma(ByRef linhas As List(Of Linha), ByRef richTexto As RichTextBox)
        '' Variavel contadora temporaria
        Dim cnt As Integer = 0

        '' Imprimir linha por linha
        For i = 0 To linhas.Count - 1
inicio:
            '' Caso entre em loop infinito
            cnt += 1 'Toda vez que passar add mais um ao contador:
            If cnt > linhas.Count * 5 Then  ' Caso contador senha 5 vezes o tamanho do programa, ele para a execução
                richTexto.Text &= "LOOP INFINITO?"
                GoTo fim
            End If
            Try

                '' Se for FUNÇÃO:
                If linhas.Item(i).funcao Then
                    '' Se ja passou por por essa função:
                    If linhas.Item(i).passou = True Then
                        ' Pode acontecer, nada demais
                    End If
                    '' Caso va para PARADA, podendo ser 0 ou linha fora do escopo
                    If linhas.Item(i).funcao_va_para = 0 Or linhas.Item(i).funcao_va_para > linhas.Count Then
                        richTexto.Text &= "Fazendo " & linhas.Item(i).funcao_nome & " e PARADA." & vbNewLine
                        richTexto.Text &= "PARADA" & vbNewLine
                        'finaliza
                        linhas.Item(i).passou = True
                        i = linhas.Count - 1
                        GoTo inicio
                    Else
                        richTexto.Text &= "Fazendo " & linhas.Item(i).funcao_nome & " e indo para linha " & linhas.Item(i).funcao_va_para & vbNewLine
                        linhas.Item(i).passou = True
                        i = linhas.Item(i).funcao_va_para - 1
                        GoTo inicio
                    End If

                    '' Se for TESTE:
                Else
                    If linhas.Item(i).passou = True Then
                        'RichTextBox3.Text &= "JÁ PASSOU, escolher FALSO" & vbNewLine

                        If linhas.Item(i).teste_se_F = 0 Or linhas.Item(i).teste_se_F > linhas.Count Then
                            richTexto.Text &= "Teste " & linhas.Item(i).teste_nome & " Falso -> FIM " & vbNewLine
                            richTexto.Text &= "PARADA" & vbNewLine
                            GoTo fim
                        Else
                            richTexto.Text &= "Teste " & linhas.Item(i).teste_nome & " Falso -> linha " & linhas.Item(i).teste_se_F & vbNewLine
                            If i = linhas.Item(i).teste_se_F - 1 Then
                                richTexto.Text &= "Anterior entrou em LOOP" & vbNewLine
                                i = linhas.Item(i).teste_se_F
                                GoTo inicio
                            End If
                            i = linhas.Item(i).teste_se_F - 1
                        End If

                        GoTo inicio
                    Else

                    End If

                    If linhas.Item(i).teste_se_V = 0 Or linhas.Item(i).teste_se_V > linhas.Count Then
                        richTexto.Text &= "Teste " & linhas.Item(i).teste_nome & " Verdadeiro -> FIM " & vbNewLine
                        linhas.Item(i).passou = True
                        GoTo fim
                    Else
                        richTexto.Text &= "Teste " & linhas.Item(i).teste_nome & " Verdadeiro -> linha " & linhas.Item(i).teste_se_V & vbNewLine
                        linhas.Item(i).passou = True
                        i = linhas.Item(i).teste_se_V - 1
                        GoTo inicio
                    End If
                End If
            Catch ex As Exception
                richTexto.Text &= "PARADA" & vbNewLine
            End Try
        Next
fim:
    End Sub

    '' Etapa Etapa 2:
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        etapa2(linhas1, RichTextBox5)
        etapa2(linhas2, RichTextBox6)

    End Sub

    Private Sub etapa2(ByVal linhas As List(Of Linha), ByRef richText As RichTextBox)
        Dim cnt As Integer = 0
        Dim anterior As String = "ε"

        richText.Text &= "A" & cnt & " = {" & anterior & "}" & vbNewLine : cnt += 1
        anterior &= ",2,1"
        richText.Text &= "A" & cnt & " = {" & anterior & "}" & vbNewLine : cnt += 1

    End Sub

End Class
