   M S B A M L   `   `   `  ����=  9QC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:QCQC  5   	       QC.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:QC     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=null@Material1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  	MAIN PAGE��5	   	   $	��720��6   $
��1366��6(    � Icon$ icon.ico��65   $��Tahoma)�6E    � WindowStartupLocation$ CenterScreen=�6Y   .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��25 �6    � ColumnDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   � -   5        $��0��5      $��1��6   $��2��6,   ���	#FF0404C5.+�5      �� -   5         ��Name$ title��5      $>��8Copyright © 2020 by IT Administrator Dept - Thaco Mazda��6'   $
��Left=�6f   $��10,2,0,5q�6�   $��Center=�6�   
�������$��Tahoma)�6�   $��13(�6�   $
��Bold"�6�   $
��auto��6�   5/      � 53   
   $��0��6   $��0��6   
�������$	��500��6@   $	��Top=�6M   $��50��6e   .+�54      �� -   5        $��btnList��54      
������ 
������ $��70��6m   $��50��6y   $��Center=�6�   $	��Top=�6�    � ToolTip$" Danh sách lỗi/ Error List��6�   $��Tahoma)�6�   $��14(�6�   .��55      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ClipboardList=�6$   $��50��69   $��50��6E   
�������6P   56      �� 58      -   5        $��	btnSearch��58      
������ 
������ $��70��6q   $��50��6}   $��Center=�6�   $	��Top=�6�   $��0,70,0,0q�6�   $+ %Nhập thông tin / Input Information��6�   $��Tahoma)�6�   $��14(�6  .��59        $ Import=�6$   $��50��62   $��50��6>   
�������6I   5:      �� 5;      -   5        $��
btnDiagram��5;      
������ 
������ $��70��6s   $��50��6   $��Center=�6�   $	��Top=�6�   $��	0,140,0,0q�6�   $ Thống kê/ Statistic��6�   $��Tahoma)�6�   $��14(�6  .��5<        $ GoogleAnalytics=�6$   $��50��6;   $��50��6G   
�������6R   5=      �� 5>      -   5        $��btnDiagram_Shop��5>      
������ 
������ $��70��6}   $��50��6�   $��Center=�6�   $	��Top=�6�   $��	0,210,0,0q�6�   $ Thống kê/ Statistic��6�   $��Tahoma)�6�   $��14(�6  .��5?        $ ChartArc=�6$   $��50��64   $��50��6@   
�������6K   5@      �� 5A      -   5        $��btnDiagram_Section��5A      
������ 
������ $��70��6�   $��50��6�   $��Center=�6�   $	��Top=�6�   $��	0,280,0,0q�6�   $ Thống kê/ Statistic��6�   $��Tahoma)�6   $��14(�6  .��5B        $ BarChart=�6$   $��50��64   $��50��6@   
�������6K   5C      �� 5E      -   5        $��
btnSetting��5E      
������ 
������ $��70��6s   $��50��6   $��Center=�6�   $	��Top=�6�   $��	0,350,0,0q�6�   $ Thống kê/ Statistic��6�   $��Tahoma)�6�   $��14(�6  .��5F        $ Settings=�6$   $��50��64   $��50��6@   
�������6K   5G      5N      � 5O   
   $��1��6   $��0��6   ���	#FFE2E0E0.+�5P      ]� -	   5        $��dpShow��5P      $��5q�6&   ��5Q      �� 5R       �� ImageSource$ asset/hinhnen.png��6!   5T      5U      5X      5Y      