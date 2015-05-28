﻿using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using ReactiveUI;

namespace DemoApplication.Features.FolderBrowserDialog.ViewModels
{
    [Export]
    public class FolderBrowserTabContentViewModel : ReactiveObject
    {
        private readonly IDialogService dialogService;
        private readonly ReactiveCommand<object> browseFolderCommand;

        private string path;

        [ImportingConstructor]
        public FolderBrowserTabContentViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            
            browseFolderCommand = ReactiveCommand.Create();
            browseFolderCommand.Subscribe(_ => BrowseFolder());
        }

        public string Path
        {
            get { return path; }
            set { this.RaiseAndSetIfChanged(ref path, value); }
        }

        public ICommand BrowseFolderCommand
        {
            get { return browseFolderCommand; }
        }

        private void BrowseFolder()
        {
            var folderBrowserDialogViewModel = new FolderBrowserDialogViewModel
            {
                Description = "This is a description"
            };

            bool? success = dialogService.ShowFolderBrowserDialog(this, folderBrowserDialogViewModel);
            if (success == true)
            {
                Path = folderBrowserDialogViewModel.SelectedPath;
            }
        }
    }
}