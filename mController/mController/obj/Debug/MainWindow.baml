   M S B A M L   `   `   `  ����F  BmController, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:mControllermController  5   	   b ^PresentationFramework.Aero2, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes 5   	       mController.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:mController     5   	   ^ThemesKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2     5   	   -   5          �Name$  MainWindow1��5   d    � Title$ mController��5	   	   $	��473��5
   	   $
��1170��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ icon.ico��5   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency .  � WindowStyle$
 None=�5   	   ��5   	   Z� .��5      � .+�5   
   �� $��1��6   $	��472��6(   $��0 0 0 0q�65   $	��Top=�6F   ���	#7F0000FF6^   �� 5   
   $��1��6   $	��424��6(   $
��1168��65   $��1,47,1,1q�6B   $	��Top=�6T   ���	#CC0000006l   �� 5   
   $��1��6   $��1��6(   $
��1168��63   $��0 46 0 0q�6@   $	��Top=�6R   ���	#7F0000FF6j   �� 5   
   -   5        $��1��5      $��45��6(   $
��1168��64   $��1 1 1 1q�6A   $	��Top=�6R   ���	#E50000006{   �� 5   
    �� Orientation$ 
Horizontal=�6   $	��0 1q�6.   $	��Top=�6;   $��Center=�6S   $y�Hidden=�6p   .+�5      �� -   5         ��Name$ button_reload��5         MinimizeButtonStyle#���   6,   $��10 0 0 0q�6Z   $��10q�6l   $��45��6y   $��16(�6�   .��5      �� .��5      �� $ 
Horizontal=�6!   .+�5       �� $��Calibri)�6$   �� 6:   $	�⟳��6>   6J   5!      5#      5$      	� 5'      -   5        	 	�Name$	 controller1rb��5'      $��Center=�61   $	��5 0q�6L   $
��2 -6q�6Y   $��21(�6h   $��Center=�6v   $��Calibri)�6�   #��� ��6�   $��Controller 1��6�   	� 5(      -   5        $	 controller2rb��5(      $��Center=�61   $	��5 0q�6L   $
��2 -6q�6Y   $��21(�6h   $��Center=�6v   $��Calibri)�6�   #��� ��6�   $��Controller 2��6�   	� 5)      -   5        $	 controller3rb��5)      $��Center=�61   $	��5 0q�6L   $
��2 -6q�6Y   $��21(�6h   $��Center=�6v   $��Calibri)�6�   #��� ��6�   $��Controller 3��6�   	� 5*      -   5        $	 controller4rb��5*      $��Center=�61   $	��5 0q�6L   $
��2 -6q�6Y   $��21(�6h   $��Center=�6v   $��Calibri)�6�   #��� ��6�   $��Controller 4��6�   5+      ]� 5-   
   .+�5.      ]� $	��Top=�6   .+�5/      �� -   5        $
��Left=�5/      $��Calibri)�62   $��21.333(�6G   $��
10 10 0 10q�6Y   #��� ��6m   �� 6�   $�mController��6�   6�   �� 51      -	   5        $ button_close��51        CloseButtonStyle#���  6/   $��Right=�6Z   $��Right=�6q   $��0 1 1 1q�6�   $��10q�6�   $��45��6�   $��16(�6�   .��53      �� .��54      �� $ 
Horizontal=�6%   .+�55      �� -
   5        
 ��Name$
 	textClose��55   (    �� Padding$	 0 4q�6;   $��Calibri)�6I   �� 6_   $�X��6c   6o   56      58      5:      �� 5<      -   5        $ button_hide��5<      #���   6.   $��Right=�6\   $��Right=�6s   $	��0 1q�6�   $��10q�6�   $��16(�6�   .��5=      �� .��5>      �� $ 
Horizontal=�6%   .+�5?      �� -   5        $
 textHide��5?   (   $��Calibri)�6:   �� 6P   $�_��6T   6`   5@      5B      5D      d� 5F      -   5         d�Name$ button_disable��5F        DisableButtonStyle#���  67   $��Right=�6d   $��1q�6{   $��Center=�6�   $��10q�6�   $��45��6�   $��16(�6�   .��5G      �� .��5H      �� $ 
Horizontal=�6%   .+�5I      �� -   5        $
 textDisable��5I   (   $��Calibri)�6=   �� 6S   $�Disable Temporarily��6W   6u   5J      5L      5M      5P      � 5S      $��Bottom=�6   .+�5T      �� -   5         ��Name$ startAtStartup��5T      $��10,11,1031,10q�63   $��_Run at startup��6J   #��� ��6d   $��15��6�   �� 5U      $��Right=�6   $��5q�68   $
 10 5q�6C   #��� ��6R   $��	Underline{�6�   �� 6�   -   5        $X��Rhttps://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=33VV9979AHN7C?�5U   �   .�6T  �� -   5         �Name$ AboutButton��5U   X  $�About��6k  6{  6�  5V      � 5Y      $��Center=�6    � ColumnDefinitions 5Z      �� 5[      $��1* �6'   �� 5\      $��1* �6'   .+�5_      �� -   5        $
 instrucciones1��5_      $��0��64   $��Bottom=�6D   $
��Wrap=�6\    �� TextAlignment$ Right=�6p   $��21(�6�   $
��auto��6�   $	��5 0q�6�   $��Calibri)�6�   #��� ��6�   �� 6  $
��Bold"�6  $�A��6$  �� 6/  $� ��63  �� 6>  �� 6J  �� 6V  $
��Bold"�6Z  $�B��6l  �� 6w  �� 6�  �� 6�  �� 6�  $
��Bold"�6�  $�Move Left Joystic��6�  �� 6�  $
��Bold"�6�  $�k��6�  �� 6�  �� 6�  �� 6�  �� 6  $
��Bold"�6	  $�Push Left Joystick��6  �� 67  �� 6C  �� 6I  �� 6U  $
��Bold"�6Y  $4�.Move Right Joystick Horizontally or Vertically��6k  �� 6�  �� 6�  �� 6�  �� 6�  $
��Bold"�6�  $�Left/Right Trigger��6�  �� 6�  �� 6�  �� 6  �� 6  $
��Bold"�6  $*�$Trigger Left + Trigger Right + Start��6'  6V  �� 5`      -   5        $
 instrucciones2��5`      $��1��64   $��Bottom=�6D   $
��Wrap=�6\   $
 Left=�6p   $��21(�6�   $
��auto��6�   $	��5 0q�6�   $��Calibri)�6�   #��� ��6�   �� 6  $�Primary click��6  �� 6(  �� 64  �� 6:  �� 6F  $�Secondary click��6J  �� 6c  �� 6o  �� 6u  �� 6�  $�Move cursor��6�  �� 6�  �� 6�  �� 6�  �� 6�  $�	Mid Click��6�  �� 6�  �� 6�  �� 6�  �� 6�  $�Horizontal or Vertical ��6�  �� 6  $�Scroll��6  �� 6&  �� 62  �� 68  �� 6D  $�Decrease/Increase speed��6H  �� 6i  �� 6u  �� 6{  �� 6�  $�Hide/Show this interface��6�  6�  5c      5g      5h      5i      