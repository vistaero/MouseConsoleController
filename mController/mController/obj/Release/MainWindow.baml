   M S B A M L   `   `   `  ���� F  BmController, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:mControllermController      mController.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:mController     -     �Name$  MainWindow1�� � Title$ mController��$	��645��$
��1170�� � WindowStartupLocation$ CenterScreen=� � Icon$ icon.ico�� � 
ResizeMode$ NoResize=� � AllowsTransparency .  � WindowStyle$
 None=���Z� .��� .+��� $��1��$	��645��$��0 0 0 0q�$	��Top=����	#7F0000FF�� $��1��$��45��$
��1168��$��0 1 0 0q�$	��Top=����	#E5000000�� $��1��$	��597��$
��1168��$��0 47 0 0q�$	��Top=����	#CC000000�� $��1��$��1��$
��1168��$��0 46 0 0q�$	��Top=����	#7F0000FF��  �� Orientation$ 
Horizontal=�$
��0 17q�$	��Top=�$��Center=�.+�	� -    	�Name$ controller1rb��$��Center=�$	��5 0q�$
��0 -6q�$��21(�$��Center=�$��Calibri)�#��� ��.��Controller 1	� -   $ controller2rb��$��Center=�$	��5 0q�$
��0 -6q�$��21(�$��Center=�$��Calibri)�#��� ��.��Controller 2	� -   $ controller3rb��$��Center=�$	��5 0q�$
��0 -6q�$��21(�$��Center=�$��Calibri)�#��� ��.��Controller 3	� -   $ controller4rb��$��Center=�$	��5 0q�$
��0 -6q�$��21(�$��Center=�$��Calibri)�#��� ��.��Controller 4]� .+�]� $	��Top=�.+��� $
��Left=�$��Calibri)�$��21.333(�$��
10 10 0 10q�#��� ��mController�� $��Right=�$��
0, 1, 0, 0q�$ 
Horizontal=�$��Bottom=�$��Right=�.+��� -   	 ��Name$	 button_disable��   DisableButtonStyle#���   $��10 0 0 0q�$��10q�$��45��$��16(�.���� .���� $ 
Horizontal=�.+��� -   
 ��Name$
 textDisable��$��Calibri)�Disable Temporarily�� -   $	 button_hide��  MinimizeButtonStyle#���  $��10 0 0 0q�$��10q�$��16(�.���� .���� $ 
Horizontal=�.+��� -	   $
 textHide��$��Calibri)�_�� -
   $	 button_close��  CloseButtonStyle#���  $��0 0 1 0q�$��10q�$��45��$��16(�.���� .���� $ 
Horizontal=�.+��� -   $
 	textClose�� �� Padding$	 0 4q�$��Calibri)�X� $��Bottom=�.+��� $��10q�$
��Left=�$��Center=�$��16(�$
��5 -4q�#��� ��.��Iniciar con Windows�� -   $	 
HelpButton��$��10q�$��Right=�$��16(�#��� �����	#00DDDDDD��Z� .��?�� -   $
 instrucciones��$��Bottom=�$
��Wrap=� �� TextAlignment$ Center=�$��21(�$
��auto��$��10q�$��Calibri)�#��� ���� $
��Bold"�$�A���� $� ���� �� $�Primary click���� �� �� �� $
��Bold"�$�B���� �� $�Secondary click���� �� �� �� $
��Bold"�$�Move Left Joystick���� �� $�Move cursor���� �� �� �� $
��Bold"�$�Push Left Joystick���� �� $�	Mid Click���� �� �� �� $
��Bold"�$4�.Move Right Joystick Horizontally or Vertically���� �� $�Horizontal or Vertical ���� $�Scroll���� �� �� �� $
��Bold"�$�Left/Right Trigger���� �� $�Decrease/Increase speed���� �� �� �� $
��Bold"�$*�$Trigger Left + Trigger Right + Start���� �� $�Hide/Show this interface��