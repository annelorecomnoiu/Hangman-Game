   M S B A M L   `   `   `  ����@  <Tema2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Tema2Tema2  5   	   3clr-namespace:Tema2.ViewModelsTema2.ViewModels  5   
       Tema2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Tema2     5   	   5
viewmodelsclr-namespace:Tema2.ViewModels     5   
     � Title$  SignIn��5	   	   $	��450��6   $	��800��6%   
 � Top$	 190��61    � Left$	 370��6;    � DataContext 5
      $   Tema2.ViewModels.UserViewModel  5   
   .��5      � .+�5   
   �� $
��Left=�6   $��37��6/   $��
171,30,0,0q�6;   $
��Wrap=�6O   $*��$Welcome to Hangman! Enjoy this game!��6c   $	��Top=�6�   $	��414��6�   $��Trebuchet MS)�6�   $
��Bold"�6�   $��22(�6�   �� 5   
   $��New User��6   $
��Left=�6D   $	��Top=�6_   $��95��6w   $��187,361,0,0q�6�   $��Trebuchet MS)�6�   $
��Bold"�6�   $��16(�6�   $��36��6�   ��6$   �� *
AddCommand+6�   �� 5   
   $��Show local users��6   $
��Left=�6R   $	��Top=�6m   $	��156��6�   $��
10,361,0,0q�6�   $��Trebuchet MS)�6�   $
��Bold"�6�   $��16(�6�   $��36��6�   ��6,   �� *ShowUsersCommand+6�   �� 5   
   -   5        $��delete��5      $��Delete User��6   $
��Left=�6W   $	��Top=�6r   $	��116��6�   $��303,361,0,0q�6�   $��16(�6�   $
��Bold"�6�   $��Trebuchet MS)�6�   $��36��6�   ��65   �� *DeleteCommand+6�   �� 5   
   -   5        $
��play��5      $
��Play��6   $
��Left=�6?   $	��Top=�6Z   $��75��6r    J� RenderTransformOrigin$ 2.415,7.457&�6}   $��510,361,0,0q�6�   $��36��6�   $��Trebuchet MS)�6�   $��16(�6�   $
��Bold"�6�   �� 5   
   -   5        $��Cancel��5      $
��Left=�67   $	��Top=�6R   $��75��6j   $��653,361,0,0q�6u   $��36��6�   $��16(�6�   $��Trebuchet MS)�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ username��5      $��552,80,83.6,310q�6L   $��Trebuchet MS)�6e   $��18(�6   $
��Bold"�6�    �� TextAlignment$ Center=�6�   ��6&   �� *CurrentUser.UserName+6�   �� 5   
   -   5         ��Name$ image��5      $
��Left=�6I   $	��175��6d   $	��Top=�6q   $	��191��6�   $��537,115,0,0q�6�   $
��Fill=�6�   ��6   �� *CurrentUser.ImagePath+6�   �� 5   
   -   5        $ nr_Games��5      $y�Hidden=�6M   $��552,80,83.6,310q�6a   $��Trebuchet MS)�6z   $��18(�6�   $
��Bold"�6�   $ Center=�6�   ��6&   �� *CurrentUser.NrOfGames+6�   �� 5   
   -   5        $ nr_Wons��5      $y�Hidden=�6W   $��552,80,83.6,310q�6k   $��Trebuchet MS)�6�   $��18(�6�   $
��Bold"�6�   $ Center=�6�   ��6%   �� *" CurrentUser.NrOfGamesWonCategory+6�   �� 5   
   -   5        $ 	indexGame��5      $y�Hidden=�6O   $��552,80,83.6,310q�6c   ��6'   �� *CurrentUser.IndexGames+6c   �� 5   
   -	   5        $��Avatar��5      $��Choose avatar��6    $
��Left=�68   $	��Top=�6S   $	��118��6k   $ 6.872,7.907&�6w   $��578,295,0,0q�6�   $��32��6�   $��16(�6�   $��Trebuchet MS)�6�   $
��Bold"�6  ��6�   �� *AvatarCommand+6  &   System.Windows.Controls.DataGrid  5   
   $
��Left=�6   $	��247��6.   $��	10,80,0,0q�6;   $	��Top=�6N   $	��409��6f      AutoGenerateColumns .  $
��Bold"�6�   $��Trebuchet MS)�6�   $��16(�6�   ��6r   �� *Users+	 �� SelectedItem	 6�   �� *CurrentUser+
   Columns
 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ Username��6&      Width5  /System.Windows.Controls.DataGridLengthConverter$	 400 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *
UserName+6U   5$      5'      5(      