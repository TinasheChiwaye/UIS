Public Class IDValidation
    Public Shared Function IsValidID(ByVal IdNumber As String) As Boolean
        If Len(IdNumber) = 13 And IsNumeric(IdNumber) Then
            If (((Mid(IdNumber, 1, 1) + ((Mid(IdNumber, 2, 1) * 2) Mod 10) + (Mid(IdNumber, 2, 1) * 2 - ((Mid(IdNumber, 2, 1) * 2) Mod 10)) / 10 + Mid(IdNumber, 3, 1) + ((Mid(IdNumber, 4, 1) * 2) Mod 10) + (Mid(IdNumber, 4, 1) * 2 - ((Mid(IdNumber, 4, 1) * 2) Mod 10)) / 10 + Mid(IdNumber, 5, 1) + ((Mid(IdNumber, 6, 1) * 2) Mod 10) + (Mid(IdNumber, 6, 1) * 2 - ((Mid(IdNumber, 6, 1) * 2) Mod 10)) / 10 + Mid(IdNumber, 7, 1) + ((Mid(IdNumber, 8, 1) * 2) Mod 10) + (Mid(IdNumber, 8, 1) * 2 - ((Mid(IdNumber, 8, 1) * 2) Mod 10)) / 10 + Mid(IdNumber, 9, 1) + ((Mid(IdNumber, 10, 1) * 2) Mod 10) + (Mid(IdNumber, 10, 1) * 2 - ((Mid(IdNumber, 10, 1) * 2) Mod 10)) / 10 + Mid(IdNumber, 11, 1) + ((Mid(IdNumber, 12, 1) * 2) Mod 10) + (Mid(IdNumber, 12, 1) * 2 - ((Mid(IdNumber, 12, 1) * 2) Mod 10)) / 10 + Mid(IdNumber, 13, 1)) Mod 10) <> 0) Then
                Return False
            Else
                Return True
            End If
        ElseIf Len(IdNumber) <> 13 And IsNumeric(IdNumber) Then
            Return True

        Else
            Return False
        End If
    End Function
End Class
