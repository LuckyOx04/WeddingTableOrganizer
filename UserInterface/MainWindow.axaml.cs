using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Logic.Composite;
using WeddingTableOrganizer.ViewModels;

namespace WeddingTableOrganizer;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public MainWindow()
    {
        InitializeComponent();
        _mainWindowViewModel = new MainWindowViewModel();
        DataContext = _mainWindowViewModel;
    }

    private void AssignFamiliesFirst_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindowViewModel.ArrangeFamilyFirst();
    }

    private void AssignPeopleFirst_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindowViewModel.ArrangePeopleFirst();
    }

    private void AddFamily_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindowViewModel.AddFamily();
    }

    private void AddPerson_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindowViewModel.AddPersonToFamily();
    }

    private void AddTable_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindowViewModel.AddTable();
    }

    private void AddConflict_Click(object? sender, RoutedEventArgs e)
    {
        _mainWindowViewModel.AddConflict();
    }
}