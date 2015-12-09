Imports System.Runtime.InteropServices
Imports System.Windows.Interop
Imports Microsoft.Win32



Public Class AboutOf

    Inherits Window
    <DllImport("user32.dll")>
    Friend Shared Function SetWindowCompositionAttribute(hwnd As IntPtr, ByRef data As WindowCompositionAttributeData) As Integer
    End Function

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

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        EnableBlur()

    End Sub

    Private Sub Rectangle_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Me.DragMove()

    End Sub

    Private Sub button_close_Click(sender As Object, e As RoutedEventArgs) Handles button_close.Click
        Me.Close()

    End Sub

    Private Sub TitleTextBlock_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles TitleTextBlock.MouseDown
        Me.DragMove()

    End Sub

    Private Sub Hyperlink_RequestNavigate(sender As Object, e As RequestNavigateEventArgs)
        Process.Start(New ProcessStartInfo(e.Uri.AbsoluteUri))
        e.Handled = True
    End Sub

End Class
