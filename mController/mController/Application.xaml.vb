Class Application

    ' Los eventos de nivel de aplicación, como Startup, Exit y DispatcherUnhandledException
    ' se pueden controlar en este archivo.

    Private Sub App_Startup(sender As Object, e As StartupEventArgs)
        ' Application is running
        ' Process command line args
        Dim startMinimized As Boolean = False
        Dim i As Integer = 0
        While i <> e.Args.Length
            If e.Args(i) = "/minimized" Then
                startMinimized = True
            End If
            i += 1
        End While

        ' Create main application window, starting minimized if specified
        Dim mainWindow As New MainWindow()
        If startMinimized Then
            mainWindow.WindowState = WindowState.Minimized
        End If
        mainWindow.Show()
    End Sub

End Class
