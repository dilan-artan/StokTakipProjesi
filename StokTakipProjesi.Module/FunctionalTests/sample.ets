;#DropDB StokTakipProjesiEasyTest

#Application StokTakipProjesiWin
#Application StokTakipProjesiBlazor

*FillForm
 User Name = Admin
*Action Log In

#IfDef StokTakipProjesiWin

*Action About...
 
*Action OK

#EndIf

#IfDef StokTakipProjesiBlazor
*Action Navigation(My Details)
#EndIf
