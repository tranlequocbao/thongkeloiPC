   M S B A M L   `   `   `  ����=  9QC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:QCQC  5          QC.listError   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      "localclr-namespace:QC     5      V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=null>Design1http://materialdesigninxaml.net/winfx/xaml/themes  5      
�������.��5      �   � RowDefinitions  5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   � $��0��6   ���	#FFFFFCD6.+�5      �� $��Tahoma)�6   $��15(�6,   $��DANH SÁCH LỖI��6:   $��Center=�6O   $��Center=�6l   
���   �$
��Bold"�6�   5      � 5   
   $��1��6   ���	#FFFFFCD6 � ColumnDefinitions 5      �� 5      $��70 �6#   �� 5      $	��160 �6#   �� 5      $��80 �6#   �� 5      $	��160 �6#   �� 5      $��90 �6#   �� 5      $	��160 �6#   �� 5       $��90 �6#   �� 5!      $	��160 �6#   �� 5#      $��70 �6#   �� 5$      $��* �6#   .+�5'      �� $��	Xưởng:��6   $��5,0,5,0q�6&   $��0��67   $��0��6G   $��Tahoma)�6T   $��16(�6h   $��DemiBold"�6v   $��Right=�6�   $��Center=�6�   �� 5(      $��1��6   $��0��6%    �� CornerRadius$ 5��62   $��5q�6C   
���   �$��1q�6b   ._�5)      �� -   5        $��cbbShop��5)      
����  �
������ 
������ $��Tahoma)�6p   $��16(�6�   $��Center=�6�   6�   5*      �� 5+      $��	Type Err:��6   $��5,0,5,0q�6)   $��2��6:   $��0��6J   $��Tahoma)�6W   $��16(�6k   $��DemiBold"�6y   $��Right=�6�   $��Center=�6�   �� 5,      $��3��6   $��0��6%   $ 5��62   $��5q�6C   
���   �$��1q�6b   ._�5-      �� -   5        $��cbbType��5-      
����  �
������ 
������ $��Tahoma)�6p   $��16(�6�   $��Center=�6�   5.      �� 5/      $��
Ngày BĐ:��6   $��5,0,5,0q�6(   $��4��69   $��0��6I   $��Tahoma)�6V   $��16(�6j   $��DemiBold"�6x   $��Right=�6�   $��Center=�6�   �� 50      $��5��6   $��0��6%   $ 5��62   $��5q�6C   
���   �$��1q�6b   ._�51      (  "System.Windows.Controls.DatePicker  -   5        $��cbbDateStart��51      
����  �
������ 
������ $��Tahoma)�6w   $��16(�6�   $��Center=�6�   52      �� 53      $��	Ngày KT:��6   $��5,0,5,0q�6(   $��6��69   $��0��6I   $��Tahoma)�6V   $��16(�6j   $��DemiBold"�6x   $��Right=�6�   $��Center=�6�   �� 54      $��7��6   $��0��6%   $ 5��62   $��5q�6C   
���   �$��1q�6b   ._�55        -   5        $��
cbbDateEnd��55      
����  �
������ 
������ $��Tahoma)�6u   $��16(�6�   $��Center=�6�   56      �� 57      $��
Loại xe:��6   $��5,0,5,0q�6(   $��8��69   $��0��6I   $��Tahoma)�6V   $��16(�6j   $��DemiBold"�6x   $��Right=�6�   $��Center=�6�   �� 58      $��9��6   $��0��6%   $ 5��62   $��5q�6C   
���   �$��1q�6b   $	��170��6v   $	��250��6�   $
��Left=�6�   ._�59      �� -   5        $��cbbModel��59      
����  �
������ 
������ $��Tahoma)�6r   $��16(�6�   $��Center=�6�   5:      5<      �� 5?   
   -   5        $��lstData��5?      $��0��6"   $��2��62   $��8��6?   $��5q�6S   $��Tahoma)�6^   $��14(�6r   ��5@      �� 5A       �� 
TargetType" ��6   .�5C      ��  �� Property � �� �� Value
 �����6C   �� 5D       � ��$ 0,0,0,1q�68   6J   �� 5E       � ��$ 45��6/   5F       � 	Resources 5H         HeaderHeight   QC.OrdinalConverter  OrdinalConverter  OrdinalColumnDataTemplate �� RelativeSource �� Mode	 �� AncestorType
 �� 	Converter��%'  &	        '��     &	 \    &	 o    7  �� 5I      " ��6-   .�5J      ��  � ��$ 35��6/   �� 5K       � �� �	#FF03517E64   �� 5L       � ��$ 1q�68   �� 5M       � �� �	#FF03517E63   �� 5N       � ��
 �����63   �� 5O       � ��$
 Bold"�63   5P      �� 5Q      " ��6   .�5R      ��  � ��$ Stretch=�6C   5S        5T      6)   �� 5V      .R�5W      �� $��Right=�5X   O   ��5W       ��  �� $ FindAncestor=�$	 ListViewItemL�8
   5X   O   5Y      5Z       �� View 5[      �� 5\      ! �� ColumnHeaderContainerStyle# [  6   .P�5]      �� $	��STT��6%    �� Width$ 50��62    �� CellTemplate# [ 6=   �� 5^      $��ID Lỗi��6%   $	 150��65    �� DisplayMemberBinding 6A   �� *	IDError+�� 5_      $��Số VIN��6%   $	 200��65    6A   �� *	Vincode+�� 5`      $��Số LOT��6%   $ 80��65    6@   �� *LOT+�� 5a      $��	Loại Xe��6%   $	 200��66    6B   �� *Model+�� 5b      $��Ngày ghi nhận��6%   $	 150��6<    6H   �� *Date+�� 5c      $��Xưởng��6%   $	 120��64    6@   �� *Shop+�� 5d      $
��Tổ��6%   $	 120��61    6=   �� *	Section+�� 5e      $��Trạm��6%   $	 100��63    6?   �� *	Station+�� 5f      $��Giờ phát hiện��6%   $	 100��6<    6H   �� *
Timedetect+�� 5g      $��Giờ sản xuất��6%   $	 100��6;    6G   �� *Timeproduct+�� 5h      $��Vị trí phát hiện��6%   $	 100��6?    6K   �� *
Position+�� 5i      $��SL lỗi��6%   $ 50��65    6@   �� *Amount+�� 5j      $��Loại Lỗi��6%   $	 200��67    6C   �� *Type+�� 5k      $��Mô Tả Lỗi��6%   $	 200��68    6D   �� *
Descript+�� 5l      $��4M��6%   $	 200��61    6=   �� *T4M+�� 5m      $��Nguyên do lỗi��6%   $	 200��6<    6H   �� *	Kindman+�� 5n      $��	Chịu TN��6%   $ 80��66    6A   �� *Human+�� 5o      $��Nguyên Nhân��6%   $ 80��6:    6E   �� *Cause+�� 5p      $��Giải Pháp��6%   $ 80��68    6C   �� *
Solution+�� 5q      $��Ghi chú��6%   $ 80��66    6A   �� *Note+�� 5r      $��	Báo cáo��6%   $ 80��66    6A   �� *Report+�� 5s      $��Level��6%   $ 80��64    6?   �� *Level+5u       � ContextMenu 5w      �� 5x      $��Tahoma)�6   $��14(�62   
����  �.@�5y      m� -   5        $
��show��5y   /   $��Detail��6    m� Icon 5z      '  !MaterialDesignThemes.Wpf.PackIcon  5{         Kind$ ClipboardListOutline=�6.   5}      m� 5~      -   5        $��Delete��5~   /   $��Delete��6    5        5�      $ Delete=�6.   5�      5�      5�      �� 5�   
   $��3��6   $ 3��6   $��5q�60   
��� � �$��1q�6O   $	��130��6d   $	��130��6s   $��Right=�6�   ._�5�      �� -	   5        $��	btnSearch��5�      
�������
����  �
������ $��Tahoma)�6g   $��Search��6{   $��16(�6�   $��Center=�6�   6�   5�      �� 5�   
   $��3��6   $ 3��6   $��	5,5,180,5q�60   
��� � �$��1q�6W   $	��130��6l   $	��130��6{   $��Right=�6�   ._�5�      �� -
   5        $��	btnExport��5�      
�������
��� � �
������ $��Tahoma)�6h   $��Export��6|   $��16(�6�   $��Center=�6�   6�   5�      �� 5�   
   $��
BÁO CÁO:��6   $��5,0,5,0q�6$   $��3��67   $��Tahoma)�6D   $��16(�6X   $��DemiBold"�6f   $
��Left=�6|   $��Center=�6�   �� 5�   
   $��3��6   $ 5��6   $��	180,5,5,5q�60   $��20��6C   $��20��6N   
���   �$��1q�6n   $
��Left=�6�   ._�5�      �� -   5        $��	cbReport1��5�      
����  �
������ 
������ $��Tahoma)�6n   $��16(�6�   $��Center=�6�   5�      �� 5�   
   $��Đã BC��6   $��	205,0,5,0q�6!   $��3��66   $��Tahoma)�6C   $��16(�6W   $��DemiBold"�6e   $
��Left=�6{   $��Center=�6�   �� 5�   
   $��3��6   $ 5��6   $��90,5,5,5q�60   $��20��6B   $��20��6M   
���   �$��1q�6m   $
��Left=�6�   ._�5�      �� -   5        $��cbALL��5�      
����  �
������ 
������ $��Tahoma)�6j   $��16(�6~   $��Center=�6�   6�   5�      �� 5�   
   $��
Tất cả��6   $��	115,0,5,0q�6"   $��3��67   $��Tahoma)�6D   $��16(�6X   $��DemiBold"�6f   $
��Left=�6|   $��Center=�6�   �� 5�   
   $��VIN CODE��6   $��	300,0,5,0q�6$   $��3��69   $��Tahoma)�6F   $��16(�6Z   $��DemiBold"�6h   $
��Left=�6~   $��Center=�6�   �� 5�   
   -   5         ��Name$ tbSearchVIN��5�   -   $��3��6   $	��400��6   $��1��6C   $��	400,0,5,5q�6S   $��2q�6f   $��Cambria)�6z   $��Center=�6�   $��18(�6�   $
��Left=�6�   $
��auto��6�   .��6'  ��5�      �� 5�       �� ImageSource$ asset/timkiem.PNG��6    j� 
AlignmentX$
 Left=�6=    j� Stretch$
 None=�6O   .��5�   "   5�      5�      5�      