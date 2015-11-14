Imports System.ComponentModel
Imports System.Threading

Public Class Form1

    ' Joystick Izquierdo
    Dim PorcentajeX As Double
    Dim PorcentajeY As Double

    Dim VelocidadX As Double
    Dim VelocidadY As Double

    ' Velocidad de desplazamiento
    Dim Dividendo As Byte = 10

    ' Controles Mouse
    Const MOUSEEVENTF_LEFTDOWN = &H2
    Const MOUSEEVENTF_LEFTUP = &H4
    Const MOUSEEVENTF_RIGHTDOWN = &H8
    Const MOUSEEVENTF_RIGHTUP = &H10
    Const MOUSEEVENTF_WHEEL = &H800
    Const MOUSEEVENTF_HWHEEL = &H1000
    Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Const MOUSEEVENTF_MIDDLEUP = &H40
    Const MOUSEEVENTF_MOVE = &H1

    Declare Function apimouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32,
        ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean

    ' Mando
    Dim myController

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers().FirstOrDefault()
        myController.SnapDeadZoneTolerance = 15

    End Sub

    Private Sub ButtonsTimer_Tick(sender As Object, e As EventArgs) Handles ButtonsTimer.Tick

        If myController.TriggerRightPressed = True Then
            Dividendo = 3
        ElseIf myController.TriggerLeftPressed = True Then
            Dividendo = 30
        Else
            Dividendo = 10
        End If

        If myController.ButtonAPressed = True Then
            LeftPressed = True
            If LeftClick.Enabled = False Then
                LeftClick.Enabled = True
            End If
        Else
            LeftPressed = False
        End If

        If myController.ButtonStartPressed = True And myController.TriggerLeftPressed = True And myController.TriggerRightPressed = True Then
            InterfaceTogglePressed = True
            If InterfaceToggle.Enabled = False Then
                InterfaceToggle.Enabled = True
            End If
        Else
            InterfaceTogglePressed = False

        End If

        If myController.ButtonBPressed = True Then
            RightPressed = True
            If RightClick.Enabled = False Then
                RightClick.Enabled = True
            End If
        Else
            RightPressed = False
        End If

        If myController.ThumbpadLeftPressed = True Then
            MidPressed = True
            If MidClick.Enabled = False Then
                MidClick.Enabled = True
            End If
        Else
            MidPressed = False
        End If

    End Sub

    Private Sub ThumbLeftTimer_Tick(sender As Object, e As EventArgs) Handles ThumbLeftTimer.Tick
        PorcentajeX = myController.ThumbLeftX
        Select Case PorcentajeX
            Case 50
                VelocidadX = 0
            Case < 50
                VelocidadX = -(50 - PorcentajeX) / Dividendo

            Case > 50
                VelocidadX = (PorcentajeX - 50) / Dividendo

        End Select

        PorcentajeY = myController.ThumbLeftY
        Select Case PorcentajeY
            Case 50
                VelocidadY = 0
            Case < 50
                VelocidadY = (50 - PorcentajeY) / Dividendo
            Case > 50
                VelocidadY = -(PorcentajeY - 50) / Dividendo

        End Select



        Call apimouse_event(MOUSEEVENTF_MOVE, VelocidadX, VelocidadY, 0, 0)


    End Sub

    Dim ThumbRightPorcentajeX As Double
    Dim ThumbLeftPorcentajeY As Double

    Dim ScrollXVelocidad As Double
    Dim ScrollYVelocidad As Double

    Dim ScrollX As Double
    Dim ScrollY As Double

    Private Sub ThumbRightTimer_Tick(sender As Object, e As EventArgs) Handles ThumbRightTimer.Tick

        ThumbRightPorcentajeX = myController.ThumbRightX
        Select Case ThumbRightPorcentajeX
            Case 50
                ScrollXVelocidad = 0
                ScrollX = 0
            Case < 50
                ScrollXVelocidad = -(50 - ThumbRightPorcentajeX) / Dividendo
            Case > 50
                ScrollXVelocidad = (ThumbRightPorcentajeX - 50) / Dividendo

        End Select

        ThumbLeftPorcentajeY = myController.ThumbRightY
        Select Case ThumbLeftPorcentajeY
            Case 50
                ScrollYVelocidad = 0
                ScrollY = 0
            Case < 50
                ScrollYVelocidad = -(50 - ThumbLeftPorcentajeY) / Dividendo
            Case > 50
                ScrollYVelocidad = (ThumbLeftPorcentajeY - 50) / Dividendo

        End Select

        Call apimouse_event(MOUSEEVENTF_WHEEL, 0, 0, ScrollYVelocidad, 0)
        Call apimouse_event(MOUSEEVENTF_HWHEEL, 0, 0, ScrollXVelocidad, 0)


    End Sub

    Dim LeftPressed As Boolean = False
    Dim ClickPerformed As Boolean = False
    Dim ClickReleased As Boolean = False

    Private Sub LeftClick_Tick(sender As Object, e As EventArgs) Handles LeftClick.Tick
        If LeftPressed = True And ClickPerformed = False Then
            Call apimouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
            ClickPerformed = True
            ClickReleased = False
        End If

        If LeftPressed = False And ClickPerformed = True And ClickReleased = False Then
            Call apimouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            ClickReleased = True
            ClickPerformed = False
            LeftClick.Enabled = False
        End If
    End Sub

    Dim RightPressed As Boolean = False
    Dim RightClickPerformed As Boolean = False
    Dim RightClickReleased As Boolean = False

    Private Sub RightClick_Tick(sender As Object, e As EventArgs) Handles RightClick.Tick
        If RightPressed = True And RightClickPerformed = False Then
            Call apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            RightClickPerformed = True
            RightClickReleased = False
        End If

        If RightPressed = False And RightClickPerformed = True And RightClickReleased = False Then
            Call apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            RightClickReleased = True
            RightClickPerformed = False
            RightClick.Enabled = False
        End If
    End Sub

    Dim MidPressed As Boolean = False
    Dim MidClickPerformed As Boolean = False
    Dim MidClickReleased As Boolean = False

    Private Sub MidClick_Tick(sender As Object, e As EventArgs) Handles MidClick.Tick
        If MidPressed = True And MidClickPerformed = False Then
            Call apimouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0)
            MidClickPerformed = True
            MidClickReleased = False
        End If

        If MidPressed = False And MidClickPerformed = True And MidClickReleased = False Then
            Call apimouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0)
            MidClickReleased = True
            MidClickPerformed = False
            MidClick.Enabled = False
        End If
    End Sub

    Dim InterfaceTogglePressed As Boolean = False
    Dim InterfaceTogglePerformed As Boolean = False
    Dim InterfaceToggleReleased As Boolean = False

    Private Sub InterfaceToggle_Tick(sender As Object, e As EventArgs) Handles InterfaceToggle.Tick
        If InterfaceTogglePressed = True And InterfaceTogglePerformed = False Then
            InterfaceTogglePerformed = True
            InterfaceToggleReleased = False
        End If

        If InterfaceTogglePressed = False And InterfaceTogglePerformed = True And InterfaceToggleReleased = False Then

            If Me.Visible = True Then
                Me.Hide()
            Else
                Me.Show()
            End If

            InterfaceToggleReleased = True
            InterfaceTogglePerformed = False
            InterfaceToggle.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim confirmacion = MessageBox.Show("Si cierra el programa, no podrá volver a mostrar esta interfaz con la combinación de botones del mando. ¿Está seguro?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmacion = DialogResult.Yes Then Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub


End Class
