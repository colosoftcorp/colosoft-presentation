﻿using System.ComponentModel;

namespace Colosoft.Presentation.Input
{
    [TypeConverter(typeof(KeyConverter))]
    public enum Key
    {
        None,

        Cancel,

        Back,

        Tab,

        LineFeed,

        Clear,

        Return,

        Enter = Return,

        Pause,

        Capital,

        CapsLock = Capital,

        KanaMode,

        HangulMode = KanaMode,

        JunjaMode,

        FinalMode,

        HanjaMode,

        KanjiMode = HanjaMode,

        Escape,

        ImeConvert,

        ImeNonConvert,

        ImeAccept,

        ImeModeChange,

        Space,

        Prior,

        PageUp = Prior,

        Next,

        PageDown = Next,

        End,

        Home,

        Left,

        Up,

        Right,

        Down,

        Select,

        Print,

        Execute,

        Snapshot,

        PrintScreen = Snapshot,

        Insert,

        Delete,

        Help,

        D0, // 0

        D1, // 1

        D2, // 2

        D3, // 3

        D4, // 4

        D5, // 5

        D6, // 6

        D7, // 7

        D8, // 8

        D9, // 9

        A,

        B,

        C,

        D,

        E,

        F,

        G,

        H,

        I,

        J,

        K,

        L,

        M,

        N,

        O,

        P,

        Q,

        R,

        S,

        T,

        U,

        V,

        W,

        X,

        Y,

        Z,

        LWin,

        RWin,

        Apps,

        Sleep,

        NumPad0,

        NumPad1,

        NumPad2,

        NumPad3,

        NumPad4,

        NumPad5,

        NumPad6,

        NumPad7,

        NumPad8,

        NumPad9,

        Multiply,

        Add,

        Separator,

        Subtract,

        Decimal,

        Divide,

        F1,

        F2,

        F3,

        F4,

        F5,

        F6,

        F7,

        F8,

        F9,

        F10,

        F11,

        F12,

        F13,

        F14,

        F15,

        F16,

        F17,

        F18,

        F19,

        F20,

        F21,

        F22,

        F23,

        F24,

        NumLock,

        Scroll,

        LeftShift,

        RightShift,

        LeftCtrl,

        RightCtrl,

        LeftAlt,

        RightAlt,

        BrowserBack,

        BrowserForward,

        BrowserRefresh,

        BrowserStop,

        BrowserSearch,

        BrowserFavorites,

        BrowserHome,

        VolumeMute,

        VolumeDown,

        VolumeUp,

        MediaNextTrack,

        MediaPreviousTrack,

        MediaStop,

        MediaPlayPause,

        LaunchMail,

        SelectMedia,

        LaunchApplication1,

        LaunchApplication2,

        Oem1,

        OemSemicolon = Oem1,

        OemPlus,

        OemComma,

        OemMinus,

        OemPeriod,

        Oem2,

        OemQuestion = Oem2,

        Oem3,

        OemTilde = Oem3,

        AbntC1,

        AbntC2,

        Oem4,

        OemOpenBrackets = Oem4,

        Oem5,

        OemPipe = Oem5,

        Oem6,

        OemCloseBrackets = Oem6,

        Oem7,

        OemQuotes = Oem7,

        Oem8,

        Oem102,

        OemBackslash = Oem102,

        ImeProcessed,

        System,

        OemAttn,

        DbeAlphanumeric = OemAttn,

        OemFinish,

        DbeKatakana = OemFinish,

        OemCopy,

        DbeHiragana = OemCopy,

        OemAuto,

        DbeSbcsChar = OemAuto,

        OemEnlw,

        DbeDbcsChar = OemEnlw,

        OemBackTab,

        DbeRoman = OemBackTab,

        Attn,

        DbeNoRoman = Attn,

        CrSel,

        DbeEnterWordRegisterMode = CrSel,

        ExSel,

        DbeEnterImeConfigureMode = ExSel,

        EraseEof,

        DbeFlushString = EraseEof,

        Play,

        DbeCodeInput = Play,

        Zoom,

        DbeNoCodeInput = Zoom,

        NoName,

        DbeDetermineString = NoName,

        Pa1,

        DbeEnterDialogConversionMode = Pa1,

        OemClear,

        DeadCharProcessed,
    }
}