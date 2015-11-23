'Imports System.Runtime.InteropServices
'Imports System.Windows.Interop


Imports System.Configuration
Imports System.Globalization
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Text

Class MainWindow

    ' Joystick Izquierdo
    Dim PorcentajeX As Double
    Dim PorcentajeY As Double

    Dim VelocidadX As Double
    Dim VelocidadY As Double

    ' Velocidad de desplazamiento
    Dim Dividendo As Byte
    Dim VelocidadNormal As Byte = 8
    Dim VelocidadLenta As Byte = 25
    Dim VelocidadRapida As Byte = 3

    ' Controles Mouse

    Declare Function apimouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32,
        ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean

    Const MOUSEEVENTF_LEFTDOWN = &H2
    Const MOUSEEVENTF_LEFTUP = &H4
    Const MOUSEEVENTF_RIGHTDOWN = &H8
    Const MOUSEEVENTF_RIGHTUP = &H10
    Const MOUSEEVENTF_WHEEL = &H800
    Const MOUSEEVENTF_HWHEEL = &H1000
    Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Const MOUSEEVENTF_MIDDLEUP = &H40
    Const MOUSEEVENTF_MOVE = &H1

    ' Mando
    Dim myController

    ' Timers

    Dim ThumbLeftTimer As New System.Windows.Threading.DispatcherTimer
    Dim ButtonsTimer As New System.Windows.Threading.DispatcherTimer
    Dim ThumbRightTimer As New System.Windows.Threading.DispatcherTimer
    Dim InterfaceToggleTimer As New System.Windows.Threading.DispatcherTimer
    Dim LeftClick As New System.Windows.Threading.DispatcherTimer
    Dim RightClick As New System.Windows.Threading.DispatcherTimer
    Dim MidClick As New System.Windows.Threading.DispatcherTimer
    Dim InterfaceToggle As New System.Windows.Threading.DispatcherTimer

    ' Idioma
    Dim Idioma As String

    'Eliminar botonera de la barra de título
    'Private Const GWL_STYLE As Integer = -16
    'Private Const WS_SYSMENU As Integer = &H80000
    '<DllImport("user32.dll", SetLastError:=True)>
    'Private Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    'End Function
    '<DllImport("user32.dll")>
    'Private Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    'End Function


    Private Sub MainWindow1_Loaded(sender As Object, e As EventArgs) Handles MainWindow1.Loaded
        'Eliminar botonera de la barra de título
        'Dim hwnd = New WindowInteropHelper(Me).Handle
        'SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) And Not WS_SYSMENU)

        cambiaIdioma(Idioma)

        Select Case BrandonPotter.XBox.XBoxController.GetConnectedControllers.Count
            Case 0
                controller1rb.IsEnabled = False
                controller2rb.IsEnabled = False
                controller3rb.IsEnabled = False
                controller4rb.IsEnabled = False
            Case 1
                controller1rb.IsEnabled = True
                controller2rb.IsEnabled = False
                controller3rb.IsEnabled = False
                controller4rb.IsEnabled = False

            Case 2
                controller1rb.IsEnabled = True
                controller2rb.IsEnabled = True
                controller3rb.IsEnabled = False
                controller4rb.IsEnabled = False

            Case 3
                controller1rb.IsEnabled = True
                controller2rb.IsEnabled = True
                controller3rb.IsEnabled = True
                controller4rb.IsEnabled = False

            Case 4
                controller1rb.IsEnabled = True
                controller2rb.IsEnabled = True
                controller3rb.IsEnabled = True
                controller4rb.IsEnabled = True

        End Select

        If BrandonPotter.XBox.XBoxController.GetConnectedControllers.Count > 0 Then
            myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(0)
            myController.SnapDeadZoneTolerance = 15
            controller1rb.IsChecked = True

            AddHandler ButtonsTimer.Tick, AddressOf ButtonsTimer_Tick
            AddHandler ThumbLeftTimer.Tick, AddressOf ThumbLeftTimer_Tick
            AddHandler ThumbRightTimer.Tick, AddressOf ThumbRightTimer_Tick
            AddHandler LeftClick.Tick, AddressOf LeftClick_Tick
            AddHandler RightClick.Tick, AddressOf RightClick_Tick
            AddHandler MidClick.Tick, AddressOf MidClick_Tick
            AddHandler InterfaceToggle.Tick, AddressOf InterfaceToggle_Tick
            AddHandler InterfaceToggleTimer.Tick, AddressOf InterfaceToggleTimer_Tick


            ButtonsTimer.Interval = New TimeSpan(0, 0, 0, 0, 1)
            ButtonsTimer.IsEnabled = True

            ThumbLeftTimer.Interval = New TimeSpan(0, 0, 0, 0, 1)
            ThumbLeftTimer.IsEnabled = True

            ThumbRightTimer.Interval = New TimeSpan(0, 0, 0, 0, 1)
            ThumbRightTimer.IsEnabled = True

            LeftClick.Interval = New TimeSpan(0, 0, 0, 0, 1)
            LeftClick.IsEnabled = True

            RightClick.Interval = New TimeSpan(0, 0, 0, 0, 1)
            RightClick.IsEnabled = True

            MidClick.Interval = New TimeSpan(0, 0, 0, 0, 1)
            MidClick.IsEnabled = True

            InterfaceToggle.Interval = New TimeSpan(0, 0, 0, 0, 1)
            InterfaceToggle.IsEnabled = True

            InterfaceToggleTimer.Interval = New TimeSpan(0, 0, 0, 0, 1)
            InterfaceToggleTimer.IsEnabled = True
        End If



    End Sub

    Private Sub cambiaIdioma(idioma As String)
        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(idioma)
        'System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(idioma)
        idioma = System.Globalization.CultureInfo.CurrentCulture.Name

        If idioma.StartsWith("es") Then
            textClose.Text = "Dejar de utilizar el mando y cerrar"
            textDisable.Text = "Deshabilitar temporalmente y ocultar"
            textHide.Text = "Utilizar el mando y ocultar"
            controller1rb.Content = "Mando 1"
            controller2rb.Content = "Mando 2"
            controller3rb.Content = "Mando 3"
            controller4rb.Content = "Mando 4"

            instrucciones.Text = System.IO.File.ReadAllText(My.Application.Info.DirectoryPath & "\languages\es-ES.txt", Encoding.Unicode)
        Else
            textClose.Text = "Stop using controller and close"
            textDisable.Text = "Hide and disable temporarily"
            textHide.Text = "Hide and keep using controller"
            controller1rb.Content = "Controller 1"
            controller2rb.Content = "Controller 2"
            controller3rb.Content = "Controller 3"
            controller4rb.Content = "Controller 4"

            instrucciones.Text = System.IO.File.ReadAllText(My.Application.Info.DirectoryPath & "\languages\en-EN.txt", Encoding.Unicode)
        End If

    End Sub

    Private Sub InterfaceToggleTimer_Tick(sender As Object, e As EventArgs)
        If myController.ButtonStartPressed = True And myController.TriggerLeftPressed = True And myController.TriggerRightPressed = True Then
            InterfaceTogglePressed = True
            If InterfaceToggle.IsEnabled = False Then
                InterfaceToggle.IsEnabled = True
            End If
        Else
            InterfaceTogglePressed = False

        End If
    End Sub

    Private Sub ButtonsTimer_Tick(sender As Object, e As EventArgs)

        If myController.TriggerRightPressed = True Then
            Dividendo = VelocidadRapida
        ElseIf myController.TriggerLeftPressed = True Then
            Dividendo = VelocidadLenta
        Else
            Dividendo = VelocidadNormal
        End If

        If myController.ButtonAPressed = True Then
            LeftPressed = True
            If LeftClick.IsEnabled = False Then
                LeftClick.IsEnabled = True
            End If
        Else
            LeftPressed = False
        End If

        If myController.ButtonBPressed = True Then
            RightPressed = True
            If RightClick.IsEnabled = False Then
                RightClick.IsEnabled = True
            End If
        Else
            RightPressed = False
        End If

        If myController.ThumbpadLeftPressed = True Then
            MidPressed = True
            If MidClick.IsEnabled = False Then
                MidClick.IsEnabled = True
            End If
        Else
            MidPressed = False
        End If

    End Sub

    Private Sub ThumbLeftTimer_Tick(sender As Object, e As EventArgs)
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


        Try
            Call apimouse_event(MOUSEEVENTF_MOVE, VelocidadX, VelocidadY, 0, 0)
        Catch ex As Exception

        End Try



    End Sub

    Dim ThumbRightPorcentajeX As Double
    Dim ThumbLeftPorcentajeY As Double

    Dim ScrollXVelocidad As Double
    Dim ScrollYVelocidad As Double

    Dim ScrollX As Double
    Dim ScrollY As Double

    Private Sub ThumbRightTimer_Tick(sender As Object, e As EventArgs)

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

    Private Sub LeftClick_Tick(sender As Object, e As EventArgs)
        If LeftPressed = True And ClickPerformed = False Then
            Call apimouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
            ClickPerformed = True
            ClickReleased = False
        End If

        If LeftPressed = False And ClickPerformed = True And ClickReleased = False Then
            Call apimouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            ClickReleased = True
            ClickPerformed = False
            LeftClick.IsEnabled = False
        End If
    End Sub

    Dim RightPressed As Boolean = False
    Dim RightClickPerformed As Boolean = False
    Dim RightClickReleased As Boolean = False

    Private Sub RightClick_Tick(sender As Object, e As EventArgs)
        If RightPressed = True And RightClickPerformed = False Then
            Call apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            RightClickPerformed = True
            RightClickReleased = False
        End If

        If RightPressed = False And RightClickPerformed = True And RightClickReleased = False Then
            Call apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            RightClickReleased = True
            RightClickPerformed = False
            RightClick.IsEnabled = False
        End If
    End Sub

    Dim MidPressed As Boolean = False
    Dim MidClickPerformed As Boolean = False
    Dim MidClickReleased As Boolean = False

    Private Sub MidClick_Tick(sender As Object, e As EventArgs)
        If MidPressed = True And MidClickPerformed = False Then
            Call apimouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0)
            MidClickPerformed = True
            MidClickReleased = False
        End If

        If MidPressed = False And MidClickPerformed = True And MidClickReleased = False Then
            Call apimouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0)
            MidClickReleased = True
            MidClickPerformed = False
            MidClick.IsEnabled = False
        End If
    End Sub

    Dim InterfaceTogglePressed As Boolean = False
    Dim InterfaceTogglePerformed As Boolean = False
    Dim InterfaceToggleReleased As Boolean = False

    Private Sub InterfaceToggle_Tick(sender As Object, e As EventArgs)
        If InterfaceTogglePressed = True And InterfaceTogglePerformed = False Then

            InterfaceVisibility()

            InterfaceTogglePerformed = True
            InterfaceToggleReleased = False
        End If

        If InterfaceTogglePressed = False And InterfaceTogglePerformed = True And InterfaceToggleReleased = False Then



            InterfaceToggleReleased = True
            InterfaceTogglePerformed = False
            InterfaceToggle.IsEnabled = False
        End If
    End Sub

    Private Sub InterfaceVisibility(ByVal Optional Disable As Boolean = False)
        If MainWindow1.Visibility = Visibility.Visible Then
            MainWindow1.Visibility = Visibility.Hidden

            If Disable = True Then
                ThumbLeftTimer.IsEnabled = False
                ThumbRightTimer.IsEnabled = False
                ButtonsTimer.IsEnabled = False
            End If

        Else
            MainWindow1.Visibility = Visibility.Visible

            ThumbLeftTimer.IsEnabled = True
            ThumbRightTimer.IsEnabled = True
            ButtonsTimer.IsEnabled = True

        End If
    End Sub


    Private Sub button_close_Click(sender As Object, e As RoutedEventArgs) Handles button_close.Click
        Me.Close()

    End Sub

    Private Sub button_disable_Click(sender As Object, e As RoutedEventArgs) Handles button_disable.Click
        InterfaceVisibility(True)
    End Sub

    Private Sub button_hide_Click(sender As Object, e As RoutedEventArgs) Handles button_hide.Click
        MainWindow1.Visibility = Visibility.Hidden
    End Sub

    Private Sub MainWindow1_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles MainWindow1.MouseDown
        Me.DragMove()
    End Sub

    Private Sub controller1rb_Checked(sender As Object, e As RoutedEventArgs) Handles controller1rb.Checked
        myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(0)
    End Sub

    Private Sub controller2rb_Checked(sender As Object, e As RoutedEventArgs) Handles controller2rb.Checked
        myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(1)
    End Sub

    Private Sub controller3rb_Checked(sender As Object, e As RoutedEventArgs) Handles controller3rb.Checked
        myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(2)
    End Sub

    Private Sub controller4rb_Checked(sender As Object, e As RoutedEventArgs) Handles controller4rb.Checked
        myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(3)
    End Sub
End Class
