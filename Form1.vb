Public Class Form1

    Dim currentInput As String = ""
    Dim lastButtonClicked As String = ""
    Dim decimalAdded As Boolean = False

    Private Sub NumberButtonClick(sender As Object, e As EventArgs) Handles zero.Click, one.Click, two.Click, three.Click, four.Click, five.Click, six.Click, seven.Click, eight.Click, nine.Click
        'for clicking numbers
        Dim numberButton As Button = CType(sender, Button)
        currentInput += numberButton.Text
        UpdateDisplay()
        ' Reset the decimalAdded flag when a new digit is added
        decimalAdded = False
    End Sub

    Private Sub DecimalButtonClick(sender As Object, e As EventArgs) Handles dot.Click
        ' Check if the current number already contains a decimal point
        If Not decimalAdded Then
            ' If the last button clicked was an operator or none, allow decimal
            If lastButtonClicked = "" OrElse lastButtonClicked = "+" OrElse lastButtonClicked = "-" OrElse lastButtonClicked = "*" OrElse lastButtonClicked = "/" Then
                currentInput += "."
                decimalAdded = True
            Else
                ' Check if the current number contains an operator
                Dim parts() As String = currentInput.Split("+"c, "-"c, "*"c, "/"c)
                Dim lastNumber As String = parts.Last()
                If Not lastNumber.Contains(".") Then
                    currentInput += "."
                    decimalAdded = True
                End If
            End If
            UpdateDisplay()
        End If
    End Sub


    Private Sub UpdateDisplay()
        ' Update the display with the current input
        TextBox3.Text = currentInput
    End Sub

    Private Sub OperatorButtonClick(sender As Object, e As EventArgs) Handles plus.Click, minus.Click, divide.Click, multiply.Click
        'for clicking operator
        Dim operatorButton As Button = CType(sender, Button)
        currentInput += operatorButton.Text
        lastButtonClicked = operatorButton.Text
        UpdateDisplay()
    End Sub

    Private Sub EqualsButtonClick(sender As Object, e As EventArgs) Handles equals.Click
        ' Evaluate the expression and update the display
        Try
            Dim result = EvaluateExpression()
            currentInput = result.ToString()
            UpdateDisplay()
        Catch ex As Exception
            currentInput = "ERROR"
            UpdateDisplay()
        End Try
    End Sub

    Private Function EvaluateExpression() As Double
        ' Use DataTable.Compute to evaluate the expression
        Dim table As New DataTable()
        Dim result As Object = table.Compute(currentInput, "")
        Return Convert.ToDouble(result)
    End Function

    Private Sub ClearButtonClick(sender As Object, e As EventArgs) Handles clear.Click
        ' Clear the current input
        currentInput = ""
        lastButtonClicked = ""
        UpdateDisplay()
    End Sub

End Class
