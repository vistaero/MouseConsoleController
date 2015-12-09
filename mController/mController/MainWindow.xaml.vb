Imports System.Configuration
Imports System.Globalization
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Interop
Imports Microsoft.Win32

Friend Enum AccentState
    ACCENT_DISABLED = 0
    ACCENT_ENABLE_GRADIENT = 1
    ACCENT_ENABLE_TRANSPARENTGRADIENT = 2
    ACCENT_ENABLE_BLURBEHIND = 3
    ACCENT_INVALID_STATE = 4
End Enum

<StructLayout(LayoutKind.Sequential)>
Friend Structure AccentPolicy
    Public AccentState As AccentState
    Public AccentFlags As Integer
    Public GradientColor As Integer
    Public AnimationId As Integer
End Structure

<StructLayout(LayoutKind.Sequential)>
Friend Structure WindowCompositionAttributeData
    Public Attribute As WindowCompositionAttribute
    Public Data As IntPtr
    Public SizeOfData As Integer
End Structure

Friend Enum WindowCompositionAttribute
    ' ...
    WCA_ACCENT_POLICY = 19
    ' ...
End Enum

Class MainWindow

    Inherits Window
    <DllImport("user32.dll")>
    Friend Shared Function SetWindowCompositionAttribute(hwnd As IntPtr, ByRef data As WindowCompositionAttributeData) As Integer
    End Function

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

    Dim ControllerChecker As New System.Windows.Threading.DispatcherTimer
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

    Friend Sub EnableBlur()
        Dim windowHelper = New WindowInteropHelper(Me)

        Dim accent = New AccentPolicy()
        accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND

        Dim accentStructSize = Marshal.SizeOf(accent)

        Dim accentPtr = Marshal.AllocHGlobal(accentStructSize)
        Marshal.StructureToPtr(accent, accentPtr, False)

        Dim data = New WindowCompositionAttributeData()
        data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY
        data.SizeOfData = accentStructSize
        data.Data = accentPtr

        SetWindowCompositionAttribute(windowHelper.Handle, data)

        Marshal.FreeHGlobal(accentPtr)
    End Sub

    Private Sub MainWindow1_Loaded(sender As Object, e As EventArgs) Handles MainWindow1.Loaded
        'Eliminar botonera de la barra de título
        'Dim hwnd = New WindowInteropHelper(Me).Handle
        'SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) And Not WS_SYSMENU)

        EnableBlur()

        lectorIdioma()

        ComprobarArranqueAlInicio()

        EsperarMando()

    End Sub

    Private Sub EsperarMando()
        AddHandler ControllerChecker.Tick, AddressOf ControllerCheckerTimer_Tick
        ControllerChecker.Interval = New TimeSpan(0, 0, 0, 0, 1000)
        ControllerChecker.IsEnabled = True


    End Sub
    Dim NumeroMandos As Byte

    Private Sub ControllerCheckerTimer_Tick(sender As Object, e As EventArgs)

        NumeroMandos = BrandonPotter.XBox.XBoxController.GetConnectedControllers.Count

        If NumeroMandos > 0 Then
            HabilitarMando()
            ControllerChecker.IsEnabled = False

        End If


        Select Case NumeroMandos

            Case 0
                controller1rb.IsEnabled = False
                controller1rb.IsChecked = False

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

    End Sub


    Private Sub HabilitarMando()
        If BrandonPotter.XBox.XBoxController.GetConnectedControllers.Count > 0 And InterfaceToggle.IsEnabled = False Then
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

    Private Sub lectorIdioma()
        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(idioma)
        'System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(idioma)

        Idioma = System.Globalization.CultureInfo.CurrentCulture.Name
        Dim idiomasDisponibles As New List(Of String)

        For Each file In System.IO.Directory.EnumerateFiles(My.Application.Info.DirectoryPath & "\languages\")
            idiomasDisponibles.Add(IO.Path.GetFileNameWithoutExtension(file))
        Next

        If idiomasDisponibles.Contains(Idioma) = True Then

            Dim Frases As New List(Of String)
            For Each line In System.IO.File.ReadAllLines(My.Application.Info.DirectoryPath & "\languages\" & Idioma & ".txt", Encoding.Unicode)
                Frases.Add(line)
            Next

            Dim textoInstrucciones1 As String = ""
            textoInstrucciones1 += Frases(0) & (vbNewLine & vbNewLine)
            textoInstrucciones1 += Frases(2) & (vbNewLine & vbNewLine)
            textoInstrucciones1 += Frases(4) & (vbNewLine & vbNewLine)
            textoInstrucciones1 += Frases(6) & (vbNewLine & vbNewLine)
            textoInstrucciones1 += Frases(8) & (vbNewLine & vbNewLine)
            textoInstrucciones1 += Frases(10) & (vbNewLine & vbNewLine)
            textoInstrucciones1 += Frases(12)
            instrucciones1.Text = textoInstrucciones1

            Dim textoInstrucciones2 As String = ""
            textoInstrucciones2 += Frases(1) & (vbNewLine & vbNewLine)
            textoInstrucciones2 += Frases(3) & (vbNewLine & vbNewLine)
            textoInstrucciones2 += Frases(5) & (vbNewLine & vbNewLine)
            textoInstrucciones2 += Frases(7) & (vbNewLine & vbNewLine)
            textoInstrucciones2 += Frases(9) & (vbNewLine & vbNewLine)
            textoInstrucciones2 += Frases(11) & (vbNewLine & vbNewLine)
            textoInstrucciones2 += Frases(13)
            instrucciones2.Text = textoInstrucciones2

            textDisable.Text = Frases(14)
            controller1rb.Content = Frases(15)
            controller2rb.Content = Frases(16)
            controller3rb.Content = Frases(17)
            controller4rb.Content = Frases(18)
            startAtStartup.Content = Frases(19)
            AboutButton.Text = Frases(20)

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

    Private Sub button_disable_Checked(sender As Object, e As RoutedEventArgs) Handles button_disable.Checked
        InterfaceVisibility(True)
    End Sub

    Private Sub button_disable_Unchecked(sender As Object, e As RoutedEventArgs) Handles button_disable.Unchecked
        EnabledControls(True)
    End Sub

    Private Sub EnabledControls(ByVal Enabled As Boolean)
        If Enabled = True Then
            ThumbLeftTimer.IsEnabled = True
            ThumbRightTimer.IsEnabled = True
            ButtonsTimer.IsEnabled = True
            button_disable.IsChecked = False
        Else
            ThumbLeftTimer.IsEnabled = False
            ThumbRightTimer.IsEnabled = False
            ButtonsTimer.IsEnabled = False
            button_disable.IsChecked = True
        End If
    End Sub

    Private Sub InterfaceVisibility(ByVal Optional Disable As Boolean = False)
        If Disable = True Then
            EnabledControls(False)

        Else
            EnabledControls(True)

        End If

        If MainWindow1.WindowState = WindowState.Normal Then
            MainWindow1.WindowState = WindowState.Minimized

        Else
            MainWindow1.WindowState = WindowState.Normal
            EnabledControls(True)

        End If

    End Sub

    Private Sub button_close_Click(sender As Object, e As RoutedEventArgs) Handles button_close.Click
        End

    End Sub

    Private Sub button_hide_Click(sender As Object, e As RoutedEventArgs) Handles button_hide.Click
        MainWindow1.WindowState = WindowState.Minimized

    End Sub

    Private Sub controller1rb_Checked(sender As Object, e As RoutedEventArgs) Handles controller1rb.Checked
        'myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(0)
    End Sub

    Private Sub controller2rb_Checked(sender As Object, e As RoutedEventArgs) Handles controller2rb.Checked
        'myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(1)
    End Sub

    Private Sub controller3rb_Checked(sender As Object, e As RoutedEventArgs) Handles controller3rb.Checked
        'myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(2)
    End Sub

    Private Sub controller4rb_Checked(sender As Object, e As RoutedEventArgs) Handles controller4rb.Checked
        'myController = BrandonPotter.XBox.XBoxController.GetConnectedControllers(3)
    End Sub

    Public Shared Function ProductName() As String
        If Windows.Application.ResourceAssembly Is Nothing Then
            Return Nothing
        End If

        Return Windows.Application.ResourceAssembly.GetName().Name
    End Function

    Private Function RunAtStartUp(ByVal Check As Boolean, ByVal bCreate As Boolean) As String
        Const key As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
        Dim subClave As String = ProductName()
        Dim msg As String = ""
        Try
            Dim Registro As RegistryKey = Registry.CurrentUser.CreateSubKey(key, RegistryKeyPermissionCheck.ReadWriteSubTree)
            With Registro
                .OpenSubKey(key, True)
                If Check = False Then
                    Select Case bCreate
                        Case True
                            .SetValue(subClave,
                                      System.Reflection.Assembly.GetExecutingAssembly().Location)
                        Case False
                            If .GetValue(subClave, "").ToString <> "" Then
                                .DeleteValue(subClave)
                            End If
                    End Select
                ElseIf Check = True Then
                    If .GetValue(subClave, "").ToString <> "" Then
                        Return True
                    Else
                        Return False

                    End If
                End If

            End With
        Catch ex As Exception
            msg = ex.Message.ToString
        End Try
        Return Nothing
    End Function

    Private Sub ComprobarArranqueAlInicio()
        If RunAtStartUp(True, False) = True Then
            startAtStartup.IsChecked = True
        End If
    End Sub

    Private Sub startAtStartup_Checked(sender As Object, e As RoutedEventArgs) Handles startAtStartup.Checked
        RunAtStartUp(False, True)

    End Sub

    Private Sub startAtStartup_Unchecked(sender As Object, e As RoutedEventArgs) Handles startAtStartup.Unchecked
        RunAtStartUp(False, False)
    End Sub

    Private Sub Rectangle_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Me.DragMove()

    End Sub

    Private Sub TextBlock_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Me.DragMove()

    End Sub

    Private Sub AboutButton_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles AboutButton.MouseLeftButtonUp
        Dim AboutWindow As New AboutOf
        AboutWindow.Show()
    End Sub

    Private Sub Hyperlink_RequestNavigate(sender As Object, e As RequestNavigateEventArgs)
        Dim AboutWindow As New AboutOf
        AboutWindow.Show()
    End Sub

    Private Sub button_reload_Click(sender As Object, e As RoutedEventArgs) Handles button_reload.Click

    End Sub
End Class
