VERSION 5.00
Begin VB.Form frmMain 
   Appearance      =   0  'Flat
   BackColor       =   &H00000000&
   BorderStyle     =   0  'None
   Caption         =   " √ﬂÌœ «Ìﬁ«› «· ‘€Ì·"
   ClientHeight    =   2520
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   7230
   Icon            =   "frmMain.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Picture         =   "frmMain.frx":23D2
   RightToLeft     =   -1  'True
   ScaleHeight     =   2520
   ScaleWidth      =   7230
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin Project1.lvButtons_H lvButtons_H1 
      Height          =   375
      Index           =   0
      Left            =   360
      TabIndex        =   1
      Top             =   1320
      Width           =   1575
      _ExtentX        =   2778
      _ExtentY        =   661
      Caption         =   "OK „Ê«›ﬁ "
      CapAlign        =   2
      BackStyle       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   178
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   32768
      cFHover         =   16711680
      LockHover       =   2
      cGradient       =   0
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   3
      Image           =   "frmMain.frx":373C
      cBack           =   -2147483633
   End
   Begin VB.Timer Timer1 
      Interval        =   1000
      Left            =   6720
      Top             =   2160
   End
   Begin Project1.lvButtons_H lvButtons_H1 
      Cancel          =   -1  'True
      Default         =   -1  'True
      Height          =   375
      Index           =   1
      Left            =   360
      TabIndex        =   2
      Top             =   1800
      Width           =   1575
      _ExtentX        =   2778
      _ExtentY        =   661
      Caption         =   "«·€«¡ «·«„—"
      CapAlign        =   2
      BackStyle       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   178
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   255
      cFHover         =   16711680
      LockHover       =   2
      cGradient       =   0
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   3
      Image           =   "frmMain.frx":3B8F
      cBack           =   -2147483633
   End
   Begin VB.Label Label3 
      Alignment       =   1  'Right Justify
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "5"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   14.25
         Charset         =   178
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   345
      Left            =   2760
      RightToLeft     =   -1  'True
      TabIndex        =   5
      Top             =   1950
      Width           =   180
   End
   Begin VB.Label Label2 
      Alignment       =   1  'Right Justify
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   " √ﬂÌœ ⁄„·Ì…"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   9
         Charset         =   178
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Left            =   5400
      RightToLeft     =   -1  'True
      TabIndex        =   4
      Top             =   360
      Width           =   975
   End
   Begin VB.Image Image1 
      Height          =   720
      Left            =   6240
      Top             =   1560
      Width           =   720
   End
   Begin VB.Label lblCount 
      Alignment       =   1  'Right Justify
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "”  „ «·⁄„·Ì… »‘ﬂ· ¬·Ì Œ·«·:"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   9
         Charset         =   178
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   210
      Left            =   3120
      RightToLeft     =   -1  'True
      TabIndex        =   3
      Top             =   2040
      Width           =   2655
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
      BackStyle       =   0  'Transparent
      Caption         =   "”Ì „ «Ìﬁ«›  ‘€Ì· ‰Ÿ«„ «· ‘€Ì·"
      BeginProperty Font 
         Name            =   "Traditional Arabic"
         Size            =   14.25
         Charset         =   178
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   795
      Left            =   2520
      RightToLeft     =   -1  'True
      TabIndex        =   0
      Top             =   720
      Width           =   3345
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Const EWX_LogOff As Long = 0
Const EWX_SHUTDOWN As Long = 1
Const EWX_REBOOT As Long = 2
Const EWX_FORCE As Long = 4
Private Declare Function ExitWindows _
               Lib "User32" Alias "ExitWindowsEx" _
               (ByVal dwOptions As Long, ByVal dwReserved As Long) As Long


Private Declare Function SendMessage Lib "User32" Alias "SendMessageA" (ByVal hWnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long
Private Declare Function ReleaseCapture Lib "User32" () As Long
Private Type POINTAPI
   x As Long
   Y As Long
End Type
Private Declare Function SetWindowPos Lib "User32" (ByVal hWnd As Long, ByVal hWndInsertAfter As Long, ByVal x As Long, ByVal Y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
Private Declare Function GetWindowLong Lib "User32" Alias "GetWindowLongA" (ByVal hWnd As Long, ByVal nIndex As Long) As Long
Private Declare Function SetWindowLong Lib "User32" Alias "SetWindowLongA" (ByVal hWnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
Private Declare Function SetLayeredWindowAttributes Lib "user32.dll" (ByVal hWnd As Long, ByVal crKey As Long, ByVal bAlpha As Byte, ByVal dwFlags As Long) As Long

Private Const HWND_TOPMOST = -1 'bring to top and stay there
Private Const HWND_NOTOPMOST = -2 'put the window into a normal position

Private Const SWP_NOMOVE = &H2 'don't move window
Private Const SWP_NOSIZE = &H1 'don't size window

Private RTime As Integer ' seconds
Private ShutType As String ' Logoff or ShutDown
Private Force As Boolean  ' Logoff or ShutDown By Force
Private msg As String  'MSG To Appear

Const GWL_EXSTYLE = (-20)
Const WS_EX_LAYERED = &H80000
Const LWA_COLORKEY = &H1

Private Sub Form_Load()
On Error Resume Next
    'lblCount = ""
    If Trim(Command) = "/?" Or Trim(Command) = "?" Or Trim(Command) = "help" Or Trim(Command) = "?help" Then
        MsgBox "Usage:Shutx logoff:shutdown:restart 60 Force", vbInformation, " ⁄·Ì„«  «·«” Œœ«„"
        End
    End If
    
    If Trim(Command) = "" Then End
    SetForm
    Dim x() As String
    x = Split(Command, " ")
    RTime = x(1)
    ShutType = x(0)
    Force = x(2)
    SetUI
    Label1 = Label1 & vbNewLine & "          " & "Â·  —Ìœ «·«” „—«—ø"
    Image1.Picture = Me.Icon
    Call SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
    
'    Me.LinkTopic = "Ashrafnet"
'    Me.LinkMode = Source
End Sub
Sub SetForm()
      Dim Ret As Long
  Dim CLR As Long
  Me.Hide

  CLR = RGB(0, 0, 0)  'this color is the color that will be transparent
  'Set the window style to 'Layered'
  Ret = GetWindowLong(Me.hWnd, GWL_EXSTYLE)
  Ret = Ret Or WS_EX_LAYERED
  SetWindowLong Me.hWnd, GWL_EXSTYLE, Ret
  'Set the opacity of the layered window to 128
  SetLayeredWindowAttributes Me.hWnd, CLR, 0, LWA_COLORKEY
'  OpenFile
'  SetTopMost
  'SetStartUp
  Me.Show

End Sub
Private Sub Form_MouseDown(Button As Integer, Shift As Integer, x As Single, Y As Single)
'Next two lines enable window drag from anywhere on form.  Remove them
'to allow window drag from title bar only.
    ReleaseCapture
    SendMessage Me.hWnd, &HA1, 2, 0&
End Sub

Private Sub Image1_MouseDown(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Form_MouseDown 0, 0, 0, 0
End Sub

Private Sub Label1_MouseDown(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Form_MouseDown 0, 0, 0, 0
End Sub

Private Sub Label2_MouseDown(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Form_MouseDown 0, 0, 0, 0
End Sub

Private Sub Label3_MouseDown(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Form_MouseDown 0, 0, 0, 0
End Sub

Private Sub lblCount_MouseDown(Button As Integer, Shift As Integer, x As Single, Y As Single)
    Form_MouseDown 0, 0, 0, 0
End Sub

Private Sub lvButtons_H1_Click(Index As Integer)
    Select Case Index
        Case 0
            DoShutDown
        Case 1
            Abort
            
    End Select
End Sub
Sub DoShutDown()
On Error GoTo er:
    If UCase(ShutType) = "LOGOFF" Then
        lblCount = "Ì „ «·«‰  ”ÃÌ· «·Œ—ÊÃ"
        If Force Then
            ExitWindows EWX_LogOff Or EWX_FORCE, &HFFFFFFFF
        Else
            ExitWindows EWX_LogOff, &HFFFFFFFF
        End If
    ElseIf UCase(ShutType) = "SHUTDOWN" Then
        lblCount = "Ì „ «·«‰ «Ìﬁ«› «· ‘€Ì·"
         If Force Then
            ExitWindows EWX_SHUTDOWN Or EWX_FORCE, &HFFFFFFFF
        Else
            ExitWindows EWX_SHUTDOWN, &HFFFFFFFF
        End If
    ElseIf UCase(ShutType) = "RESTART" Then
        lblCount = "Ì „ «·«‰ «⁄«œ… «· ‘€Ì·"
         If Force Then
            ExitWindows EWX_REBOOT Or EWX_FORCE, &HFFFFFFFF
        Else
            ExitWindows EWX_REBOOT, &HFFFFFFFF
        End If
    End If
    
    Exit Sub
er:
    lblCount = "Œÿ√ «À‰«¡ ⁄„·Ì… «Ìﬁ«› «· ‘€Ì·" & vbNewLine & Err.Description
    
End Sub
Sub SetUI()
        If UCase(ShutType) = "LOGOFF" Then
        Label1 = "”Ì „  ”ÃÌ· «·Œ—ÊÃ"
        Label2 = " √ﬂÌœ ⁄„·Ì…  ”ÃÌ· «·Œ—ÊÃ"
        lvButtons_H1(0).Caption = " ”ÃÌ· Œ—ÊÃ"
    ElseIf UCase(ShutType) = "SHUTDOWN" Then
        Label1 = "”Ì „ «Ìﬁ«›  ‘€Ì· ‰Ÿ«„ «· ‘€Ì·"
        Label2 = " √ﬂÌœ ⁄„·Ì… «Ìﬁ«› «· ‘€Ì·"
        lvButtons_H1(0).Caption = "«Ìﬁ«› «· ‘€Ì·"
    ElseIf UCase(ShutType) = "RESTART" Then
        Label1 = "”Ì „ «⁄«œ…  ‘€Ì· ‰Ÿ«„ «· ‘€Ì·"
        Label2 = " √ﬂÌœ ⁄„·Ì… «⁄«œ… «· ‘€Ì·"
        lvButtons_H1(0).Caption = "«⁄«œ… «· ‘€Ì·"
    End If
    RTime = RTime + 1
    Timer1_Timer
End Sub
Sub Abort()
    On Error GoTo er:
    Shell "shutdown -a", vbHide
    
    End
    Exit Sub
er:
    MsgBox Err.Description, vbCritical
End Sub
Private Sub Timer1_Timer()
    RTime = RTime - 1
    Label3 = RTime
    If RTime <= 0 Then
        DoEvents
        DoShutDown
    End If
End Sub
