Imports System.Windows.Input
Public Class Form1
    Dim direction = "TopLeft"
    Dim player1
    Dim player2

    Dim x = 3
    Dim y = 6

    Sub movementTopLeft()
        Panel6.Top -= x
        Panel6.Left -= y

        direction = "TopLeft"
    End Sub
    Sub movementTopRight()
        Panel6.Top -= x
        Panel6.Left += y

        direction = "TopRight"
    End Sub
    Sub movementBottomLeft()
        Panel6.Top += x
        Panel6.Left -= y

        direction = "BottomLeft"
    End Sub
    Sub movementBottomRight()
        Panel6.Top += x
        Panel6.Left += y

        direction = "BottomRight"
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Select Case keyData

            'Jugador 2
            Case Keys.Up
                If pnl_Player2.Top > 0 Then
                    pnl_Player2.Top -= My.Computer.Screen.Bounds.Height / 24
                End If
            Case Keys.Down
                If pnl_Player2.Top < Me.Height - pnl_Player2.Height Then
                    pnl_Player2.Top += My.Computer.Screen.Bounds.Height / 24
                End If
        End Select


        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Dim keysPressed As New HashSet(Of Keys)

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        'Add the pressed key into the list
        keysPressed.Add(e.KeyCode)

        If keysPressed.Contains(Keys.W) AndAlso keysPressed.Contains(Keys.Up) Then

            If pnl_Player1.Top > 0 Then
                pnl_Player1.Top -= My.Computer.Screen.Bounds.Height / 24
            End If

            If pnl_Player2.Top > 0 Then
                pnl_Player2.Top -= My.Computer.Screen.Bounds.Height / 24
            End If

        End If

        If keysPressed.Contains(Keys.W) AndAlso keysPressed.Contains(Keys.Down) Then
            If pnl_Player1.Top > 0 Then
                pnl_Player1.Top -= My.Computer.Screen.Bounds.Height / 24
            End If

            If pnl_Player2.Top < Me.Height - pnl_Player2.Height Then
                pnl_Player2.Top += My.Computer.Screen.Bounds.Height / 24
            End If

        End If

        If keysPressed.Contains(Keys.S) AndAlso keysPressed.Contains(Keys.Down) Then
            If pnl_Player1.Top < Me.Height - pnl_Player1.Height Then
                pnl_Player1.Top += My.Computer.Screen.Bounds.Height / 24
            End If

            If pnl_Player2.Top < Me.Height - pnl_Player2.Height Then
                pnl_Player2.Top += My.Computer.Screen.Bounds.Height / 24
            End If
        End If


        If keysPressed.Contains(Keys.S) AndAlso keysPressed.Contains(Keys.Up) Then
            If pnl_Player1.Top < Me.Height - pnl_Player1.Height Then
                pnl_Player1.Top += My.Computer.Screen.Bounds.Height / 24
            End If

            If pnl_Player2.Top > 0 Then
                pnl_Player2.Top -= My.Computer.Screen.Bounds.Height / 24
            End If
        End If


        Select Case e.KeyCode
            Case Keys.W
                If pnl_Player1.Top > 0 Then
                    pnl_Player1.Top -= My.Computer.Screen.Bounds.Height / 24
                End If
            Case Keys.S
                If pnl_Player1.Top < Me.Height - pnl_Player1.Height Then
                    pnl_Player1.Top += My.Computer.Screen.Bounds.Height / 24
                End If

            Case Keys.Enter
                Timer1.Interval = 1

            Case Keys.Space
                Timer1.Start()
        End Select
    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        'Remove the pressed key from the list 
        keysPressed.Remove(e.KeyCode)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        'deteccion de obstaculos, muro superior e inferior
        Select Case Panel6.Top
            Case Is < 0
                Select Case direction
                    Case "TopLeft"
                        direction = "BottomLeft"
                    Case "TopRight"
                        direction = "BottomRight"
                End Select

            Case Is > Me.Height - Panel6.Height
                Select Case direction
                    Case "BottomLeft"
                        direction = "TopLeft"
                    Case "BottomRight"
                        direction = "TopRight"
                End Select
        End Select

        'deteccion de obstaculos, paletas
        Select Case Panel6.Left


            Case Is < pnl_Player1.Left + pnl_Player1.Width
                If Panel6.Top > (pnl_Player1.Top - (Panel6.Height / 2)) And Panel6.Top < pnl_Player1.Top + pnl_Player1.Height Then
                    If Timer1.Interval > 1 Then
                        Timer1.Interval -= 1
                        Label4.Text = "Golpes para 'Hardcore': " & Timer1.Interval

                    Else
                        Label4.Visible = False
                        Label3.Visible = True
                        x += 1
                        y += 1
                    End If

                    Select Case direction
                        Case "TopLeft"
                            direction = "TopRight"

                        Case "BottomLeft"
                            direction = "BottomRight"
                    End Select

                Else
                    player2 += 1
                    Label2.Text = player2
                    Panel6.Top = (Me.Height / 2) - (Panel6.Width / 2)
                    Panel6.Left = (Me.Width / 2) - (Panel6.Width / 2)

                    If player2 > 2 Then
                        Label3.Visible = True
                        Label3.Text = "Jugador 2 a ganado"

                        x = 3
                        y = 6
                        Timer1.Interval = 10
                    End If

                    Timer1.Stop()
                End If

            Case Is > Me.Width - (pnl_Player2.Right - pnl_Player2.Width)

                If Panel6.Top > (pnl_Player2.Top - (Panel6.Height / 2)) And Panel6.Top < pnl_Player2.Top + pnl_Player2.Height Then
                    If Timer1.Interval > 1 Then
                        Timer1.Interval -= 1
                        Label4.Text = "Golpes para 'Hardcore': " & Timer1.Interval

                    Else
                        Label4.Visible = False
                        Label3.Visible = True
                        x += 1
                        y += 1
                    End If

                    Select Case direction
                        Case "TopRight"
                            direction = "TopLeft"

                        Case "BottomRight"
                            direction = "BottomLeft"
                    End Select

                Else
                    player1 += 1
                    Label1.Text = player1
                    Panel6.Top = (Me.Height / 2) - (Panel6.Width / 2)
                    Panel6.Left = (Me.Width / 2) - (Panel6.Width / 2)

                    If player1 > 2 Then
                        Label3.Visible = True
                        Label3.Text = "Jugador 1 a ganado"

                        x = 3
                        y = 6
                        Timer1.Interval = 10
                    End If

                    Timer1.Stop()
                End If

        End Select

        If direction = "TopLeft" Then
            movementTopLeft()
        End If
        If direction = "TopRight" Then
            movementTopRight()
        End If
        If direction = "BottomLeft" Then
            movementBottomLeft()
        End If
        If direction = "BottomRight" Then
            movementBottomRight()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel6.Top = (Me.Height / 2) - (Panel6.Width / 2)
        Panel6.Left = (Me.Width / 2) - (Panel6.Width / 2)

        Label3.Top = (Me.Height / 2) - (Label3.Width / 2)
        Label3.Left = (Me.Width / 2) - (Label3.Width / 2)
    End Sub
End Class