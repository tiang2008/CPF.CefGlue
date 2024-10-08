using System;
using System.Collections.Generic;
using System.Text;
using CPF.Input;

namespace CPF.CefGlue
{
    public static class KeyInterop
    {
        public static Keys KeyFromVirtualKey(int virtualKey)
        {
            //Keys key = Keys.None;
            switch (virtualKey)
            {
                case 3:
                    return Keys.Cancel;
                case 8:
                    return Keys.Back;
                case 9:
                    return Keys.Tab;
                case 12:
                    return Keys.Clear;
                case 13:
                    return Keys.Return;
                case 19:
                    return Keys.Pause;
                case 20:
                    return Keys.Capital;
                case 21:
                    return Keys.KanaMode;
                case 23:
                    return Keys.JunjaMode;
                case 24:
                    return Keys.FinalMode;
                case 25:
                    return Keys.HanjaMode;
                case 27:
                    return Keys.Escape;
                case 28:
                    return Keys.ImeConvert;
                case 29:
                    return Keys.ImeNonConvert;
                case 30:
                    return Keys.ImeAccept;
                case 31:
                    return Keys.ImeModeChange;
                case 32:
                    return Keys.Space;
                case 33:
                    return Keys.Prior;
                case 34:
                    return Keys.Next;
                case 35:
                    return Keys.End;
                case 36:
                    return Keys.Home;
                case 37:
                    return Keys.Left;
                case 38:
                    return Keys.Up;
                case 39:
                    return Keys.Right;
                case 40:
                    return Keys.Down;
                case 41:
                    return Keys.Select;
                case 42:
                    return Keys.Print;
                case 43:
                    return Keys.Execute;
                case 44:
                    return Keys.Snapshot;
                case 45:
                    return Keys.Insert;
                case 46:
                    return Keys.Delete;
                case 47:
                    return Keys.Help;
                case 48:
                    return Keys.D0;
                case 49:
                    return Keys.D1;
                case 50:
                    return Keys.D2;
                case 51:
                    return Keys.D3;
                case 52:
                    return Keys.D4;
                case 53:
                    return Keys.D5;
                case 54:
                    return Keys.D6;
                case 55:
                    return Keys.D7;
                case 56:
                    return Keys.D8;
                case 57:
                    return Keys.D9;
                case 65:
                    return Keys.A;
                case 66:
                    return Keys.B;
                case 67:
                    return Keys.C;
                case 68:
                    return Keys.D;
                case 69:
                    return Keys.E;
                case 70:
                    return Keys.F;
                case 71:
                    return Keys.G;
                case 72:
                    return Keys.H;
                case 73:
                    return Keys.I;
                case 74:
                    return Keys.J;
                case 75:
                    return Keys.K;
                case 76:
                    return Keys.L;
                case 77:
                    return Keys.M;
                case 78:
                    return Keys.N;
                case 79:
                    return Keys.O;
                case 80:
                    return Keys.P;
                case 81:
                    return Keys.Q;
                case 82:
                    return Keys.R;
                case 83:
                    return Keys.S;
                case 84:
                    return Keys.T;
                case 85:
                    return Keys.U;
                case 86:
                    return Keys.V;
                case 87:
                    return Keys.W;
                case 88:
                    return Keys.X;
                case 89:
                    return Keys.Y;
                case 90:
                    return Keys.Z;
                case 91:
                    return Keys.LWin;
                case 92:
                    return Keys.RWin;
                case 93:
                    return Keys.Apps;
                case 95:
                    return Keys.Sleep;
                case 96:
                    return Keys.NumPad0;
                case 97:
                    return Keys.NumPad1;
                case 98:
                    return Keys.NumPad2;
                case 99:
                    return Keys.NumPad3;
                case 100:
                    return Keys.NumPad4;
                case 101:
                    return Keys.NumPad5;
                case 102:
                    return Keys.NumPad6;
                case 103:
                    return Keys.NumPad7;
                case 104:
                    return Keys.NumPad8;
                case 105:
                    return Keys.NumPad9;
                case 106:
                    return Keys.Multiply;
                case 107:
                    return Keys.Add;
                case 108:
                    return Keys.Separator;
                case 109:
                    return Keys.Subtract;
                case 110:
                    return Keys.Decimal;
                case 111:
                    return Keys.Divide;
                case 112:
                    return Keys.F1;
                case 113:
                    return Keys.F2;
                case 114:
                    return Keys.F3;
                case 115:
                    return Keys.F4;
                case 116:
                    return Keys.F5;
                case 117:
                    return Keys.F6;
                case 118:
                    return Keys.F7;
                case 119:
                    return Keys.F8;
                case 120:
                    return Keys.F9;
                case 121:
                    return Keys.F10;
                case 122:
                    return Keys.F11;
                case 123:
                    return Keys.F12;
                case 124:
                    return Keys.F13;
                case 125:
                    return Keys.F14;
                case 126:
                    return Keys.F15;
                case 127:
                    return Keys.F16;
                case 128:
                    return Keys.F17;
                case 129:
                    return Keys.F18;
                case 130:
                    return Keys.F19;
                case 131:
                    return Keys.F20;
                case 132:
                    return Keys.F21;
                case 133:
                    return Keys.F22;
                case 134:
                    return Keys.F23;
                case 135:
                    return Keys.F24;
                case 144:
                    return Keys.NumLock;
                case 145:
                    return Keys.Scroll;
                case 16:
                case 160:
                    return Keys.LeftShift;
                case 161:
                    return Keys.RightShift;
                case 17:
                case 162:
                    return Keys.LeftCtrl;
                case 163:
                    return Keys.RightCtrl;
                case 18:
                case 164:
                    return Keys.LeftAlt;
                case 165:
                    return Keys.RightAlt;
                case 166:
                    return Keys.BrowserBack;
                case 167:
                    return Keys.BrowserForward;
                case 168:
                    return Keys.BrowserRefresh;
                case 169:
                    return Keys.BrowserStop;
                case 170:
                    return Keys.BrowserSearch;
                case 171:
                    return Keys.BrowserFavorites;
                case 172:
                    return Keys.BrowserHome;
                case 173:
                    return Keys.VolumeMute;
                case 174:
                    return Keys.VolumeDown;
                case 175:
                    return Keys.VolumeUp;
                case 176:
                    return Keys.MediaNextTrack;
                case 177:
                    return Keys.MediaPreviousTrack;
                case 178:
                    return Keys.MediaStop;
                case 179:
                    return Keys.MediaPlayPause;
                case 180:
                    return Keys.LaunchMail;
                case 181:
                    return Keys.SelectMedia;
                case 182:
                    return Keys.LaunchApplication1;
                case 183:
                    return Keys.LaunchApplication2;
                case 186:
                    return Keys.Oem1;
                case 187:
                    return Keys.OemPlus;
                case 188:
                    return Keys.OemComma;
                case 189:
                    return Keys.OemMinus;
                case 190:
                    return Keys.OemPeriod;
                case 191:
                    return Keys.Oem2;
                case 192:
                    return Keys.Oem3;
                case 193:
                    return Keys.AbntC1;
                case 194:
                    return Keys.AbntC2;
                case 219:
                    return Keys.Oem4;
                case 220:
                    return Keys.Oem5;
                case 221:
                    return Keys.Oem6;
                case 222:
                    return Keys.Oem7;
                case 223:
                    return Keys.Oem8;
                case 226:
                    return Keys.Oem102;
                case 229:
                    return Keys.ImeProcessed;
                case 240:
                    return Keys.OemAttn;
                case 241:
                    return Keys.OemFinish;
                case 242:
                    return Keys.OemCopy;
                case 243:
                    return Keys.OemAuto;
                case 244:
                    return Keys.OemEnlw;
                case 245:
                    return Keys.OemBackTab;
                case 246:
                    return Keys.Attn;
                case 247:
                    return Keys.CrSel;
                case 248:
                    return Keys.ExSel;
                case 249:
                    return Keys.EraseEof;
                case 250:
                    return Keys.Play;
                case 251:
                    return Keys.Zoom;
                case 252:
                    return Keys.NoName;
                case 253:
                    return Keys.Pa1;
                case 254:
                    return Keys.OemClear;
                default:
                    return Keys.None;
            }
        }

        public static int VirtualKeyFromKey(Keys key)
        {
            //int num = 0;
            if (CPF.Platform.Application.OperatingSystem == CPF.Platform.OperatingSystemType.OSX)
            {
                if (key.ToString() == Keys.Enter.ToString())
                {
                    return kVK_ANSI_KeypadEnter;
                }
                if (s_KeyMap.TryGetValue(key, out var v))
                {
                    return v;
                }
            }
            switch (key)
            {
                case Keys.Cancel:
                    return 3;
                case Keys.Back:
                    return 8;
                case Keys.Tab:
                    return 9;
                case Keys.Clear:
                    return 12;
                case Keys.Return:
                    return 13;
                case Keys.Pause:
                    return 19;
                case Keys.Capital:
                    return 20;
                case Keys.KanaMode:
                    return 21;
                case Keys.JunjaMode:
                    return 23;
                case Keys.FinalMode:
                    return 24;
                case Keys.HanjaMode:
                    return 25;
                case Keys.Escape:
                    return 27;
                case Keys.ImeConvert:
                    return 28;
                case Keys.ImeNonConvert:
                    return 29;
                case Keys.ImeAccept:
                    return 30;
                case Keys.ImeModeChange:
                    return 31;
                case Keys.Space:
                    return 32;
                case Keys.Prior:
                    return 33;
                case Keys.Next:
                    return 34;
                case Keys.End:
                    return 35;
                case Keys.Home:
                    return 36;
                case Keys.Left:
                    return 37;
                case Keys.Up:
                    return 38;
                case Keys.Right:
                    return 39;
                case Keys.Down:
                    return 40;
                case Keys.Select:
                    return 41;
                case Keys.Print:
                    return 42;
                case Keys.Execute:
                    return 43;
                case Keys.Snapshot:
                    return 44;
                case Keys.Insert:
                    return 45;
                case Keys.Delete:
                    return 46;
                case Keys.Help:
                    return 47;
                case Keys.D0:
                    return 48;
                case Keys.D1:
                    return 49;
                case Keys.D2:
                    return 50;
                case Keys.D3:
                    return 51;
                case Keys.D4:
                    return 52;
                case Keys.D5:
                    return 53;
                case Keys.D6:
                    return 54;
                case Keys.D7:
                    return 55;
                case Keys.D8:
                    return 56;
                case Keys.D9:
                    return 57;
                case Keys.A:
                    return 65;
                case Keys.B:
                    return 66;
                case Keys.C:
                    return 67;
                case Keys.D:
                    return 68;
                case Keys.E:
                    return 69;
                case Keys.F:
                    return 70;
                case Keys.G:
                    return 71;
                case Keys.H:
                    return 72;
                case Keys.I:
                    return 73;
                case Keys.J:
                    return 74;
                case Keys.K:
                    return 75;
                case Keys.L:
                    return 76;
                case Keys.M:
                    return 77;
                case Keys.N:
                    return 78;
                case Keys.O:
                    return 79;
                case Keys.P:
                    return 80;
                case Keys.Q:
                    return 81;
                case Keys.R:
                    return 82;
                case Keys.S:
                    return 83;
                case Keys.T:
                    return 84;
                case Keys.U:
                    return 85;
                case Keys.V:
                    return 86;
                case Keys.W:
                    return 87;
                case Keys.X:
                    return 88;
                case Keys.Y:
                    return 89;
                case Keys.Z:
                    return 90;
                case Keys.LWin:
                    return 91;
                case Keys.RWin:
                    return 92;
                case Keys.Apps:
                    return 93;
                case Keys.Sleep:
                    return 95;
                case Keys.NumPad0:
                    return 96;
                case Keys.NumPad1:
                    return 97;
                case Keys.NumPad2:
                    return 98;
                case Keys.NumPad3:
                    return 99;
                case Keys.NumPad4:
                    return 100;
                case Keys.NumPad5:
                    return 101;
                case Keys.NumPad6:
                    return 102;
                case Keys.NumPad7:
                    return 103;
                case Keys.NumPad8:
                    return 104;
                case Keys.NumPad9:
                    return 105;
                case Keys.Multiply:
                    return 106;
                case Keys.Add:
                    return 107;
                case Keys.Separator:
                    return 108;
                case Keys.Subtract:
                    return 109;
                case Keys.Decimal:
                    return 110;
                case Keys.Divide:
                    return 111;
                case Keys.F1:
                    return 112;
                case Keys.F2:
                    return 113;
                case Keys.F3:
                    return 114;
                case Keys.F4:
                    return 115;
                case Keys.F5:
                    return 116;
                case Keys.F6:
                    return 117;
                case Keys.F7:
                    return 118;
                case Keys.F8:
                    return 119;
                case Keys.F9:
                    return 120;
                case Keys.F10:
                    return 121;
                case Keys.F11:
                    return 122;
                case Keys.F12:
                    return 123;
                case Keys.F13:
                    return 124;
                case Keys.F14:
                    return 125;
                case Keys.F15:
                    return 126;
                case Keys.F16:
                    return 127;
                case Keys.F17:
                    return 128;
                case Keys.F18:
                    return 129;
                case Keys.F19:
                    return 130;
                case Keys.F20:
                    return 131;
                case Keys.F21:
                    return 132;
                case Keys.F22:
                    return 133;
                case Keys.F23:
                    return 134;
                case Keys.F24:
                    return 135;
                case Keys.NumLock:
                    return 144;
                case Keys.Scroll:
                    return 145;
                case Keys.LeftShift:
                    return 160;
                case Keys.RightShift:
                    return 161;
                case Keys.LeftCtrl:
                    return 162;
                case Keys.RightCtrl:
                    return 163;
                case Keys.LeftAlt:
                    return 164;
                case Keys.RightAlt:
                    return 165;
                case Keys.BrowserBack:
                    return 166;
                case Keys.BrowserForward:
                    return 167;
                case Keys.BrowserRefresh:
                    return 168;
                case Keys.BrowserStop:
                    return 169;
                case Keys.BrowserSearch:
                    return 170;
                case Keys.BrowserFavorites:
                    return 171;
                case Keys.BrowserHome:
                    return 172;
                case Keys.VolumeMute:
                    return 173;
                case Keys.VolumeDown:
                    return 174;
                case Keys.VolumeUp:
                    return 175;
                case Keys.MediaNextTrack:
                    return 176;
                case Keys.MediaPreviousTrack:
                    return 177;
                case Keys.MediaStop:
                    return 178;
                case Keys.MediaPlayPause:
                    return 179;
                case Keys.LaunchMail:
                    return 180;
                case Keys.SelectMedia:
                    return 181;
                case Keys.LaunchApplication1:
                    return 182;
                case Keys.LaunchApplication2:
                    return 183;
                case Keys.Oem1:
                    return 186;
                case Keys.OemPlus:
                    return 187;
                case Keys.OemComma:
                    return 188;
                case Keys.OemMinus:
                    return 189;
                case Keys.OemPeriod:
                    return 190;
                case Keys.Oem2:
                    return 191;
                case Keys.Oem3:
                    return 192;
                case Keys.AbntC1:
                    return 193;
                case Keys.AbntC2:
                    return 194;
                case Keys.Oem4:
                    return 219;
                case Keys.Oem5:
                    return 220;
                case Keys.Oem6:
                    return 221;
                case Keys.Oem7:
                    return 222;
                case Keys.Oem8:
                    return 223;
                case Keys.Oem102:
                    return 226;
                case Keys.ImeProcessed:
                    return 229;
                case Keys.OemAttn:
                    return 240;
                case Keys.OemFinish:
                    return 241;
                case Keys.OemCopy:
                    return 242;
                case Keys.OemAuto:
                    return 243;
                case Keys.OemEnlw:
                    return 244;
                case Keys.OemBackTab:
                    return 245;
                case Keys.Attn:
                    return 246;
                case Keys.CrSel:
                    return 247;
                case Keys.ExSel:
                    return 248;
                case Keys.EraseEof:
                    return 249;
                case Keys.Play:
                    return 250;
                case Keys.Zoom:
                    return 251;
                case Keys.NoName:
                    return 252;
                case Keys.Pa1:
                    return 253;
                case Keys.OemClear:
                    return 254;
                case Keys.DeadCharProcessed:
                    return 0;
                default:
                    return 0;
            }
        }

        #region mac
        const int kVK_ANSI_A = 0x00;
        const int kVK_ANSI_S = 0x01;
        const int kVK_ANSI_D = 0x02;
        const int kVK_ANSI_F = 0x03;
        const int kVK_ANSI_H = 0x04;
        const int kVK_ANSI_G = 0x05;
        const int kVK_ANSI_Z = 0x06;
        const int kVK_ANSI_X = 0x07;
        const int kVK_ANSI_C = 0x08;
        const int kVK_ANSI_V = 0x09;
        const int kVK_ANSI_B = 0x0B;
        const int kVK_ANSI_Q = 0x0C;
        const int kVK_ANSI_W = 0x0D;
        const int kVK_ANSI_E = 0x0E;
        const int kVK_ANSI_R = 0x0F;
        const int kVK_ANSI_Y = 0x10;
        const int kVK_ANSI_T = 0x11;
        const int kVK_ANSI_1 = 0x12;
        const int kVK_ANSI_2 = 0x13;
        const int kVK_ANSI_3 = 0x14;
        const int kVK_ANSI_4 = 0x15;
        const int kVK_ANSI_6 = 0x16;
        const int kVK_ANSI_5 = 0x17;
        //const int kVK_ANSI_Equal = 0x18;
        const int kVK_ANSI_9 = 0x19;
        const int kVK_ANSI_7 = 0x1A;
        const int kVK_ANSI_Minus = 0x1B;
        const int kVK_ANSI_8 = 0x1C;
        const int kVK_ANSI_0 = 0x1D;
        const int kVK_ANSI_RightBracket = 0x1E;
        const int kVK_ANSI_O = 0x1F;
        const int kVK_ANSI_U = 0x20;
        const int kVK_ANSI_LeftBracket = 0x21;
        const int kVK_ANSI_I = 0x22;
        const int kVK_ANSI_P = 0x23;
        const int kVK_ANSI_L = 0x25;
        const int kVK_ANSI_J = 0x26;
        const int kVK_ANSI_Quote = 0x27;
        const int kVK_ANSI_K = 0x28;
        const int kVK_ANSI_Semicolon = 0x29;
        const int kVK_ANSI_Backslash = 0x2A;
        const int kVK_ANSI_Comma = 0x2B;
        //const int kVK_ANSI_Slash = 0x2C;
        const int kVK_ANSI_N = 0x2D;
        const int kVK_ANSI_M = 0x2E;
        const int kVK_ANSI_Period = 0x2F;
        //const int kVK_ANSI_Grave = 0x32;
        const int kVK_ANSI_KeypadDecimal = 0x41;
        const int kVK_ANSI_KeypadMultiply = 0x43;
        const int kVK_ANSI_KeypadPlus = 0x45;
        const int kVK_ANSI_KeypadClear = 0x47;
        const int kVK_ANSI_KeypadDivide = 0x4B;
        const int kVK_ANSI_KeypadEnter = 0x4C;
        const int kVK_ANSI_KeypadMinus = 0x4E;
        //const int kVK_ANSI_KeypadEquals = 0x51;
        const int kVK_ANSI_Keypad0 = 0x52;
        const int kVK_ANSI_Keypad1 = 0x53;
        const int kVK_ANSI_Keypad2 = 0x54;
        const int kVK_ANSI_Keypad3 = 0x55;
        const int kVK_ANSI_Keypad4 = 0x56;
        const int kVK_ANSI_Keypad5 = 0x57;
        const int kVK_ANSI_Keypad6 = 0x58;
        const int kVK_ANSI_Keypad7 = 0x59;
        const int kVK_ANSI_Keypad8 = 0x5B;
        const int kVK_ANSI_Keypad9 = 0x5C;
        const int kVK_Return = 0x24;
        const int kVK_Tab = 0x30;
        const int kVK_Space = 0x31;
        const int kVK_Delete = 0x33;
        const int kVK_Escape = 0x35;
        const int kVK_Command = 0x37;
        const int kVK_Shift = 0x38;
        const int kVK_CapsLock = 0x39;
        const int kVK_Option = 0x3A;
        const int kVK_Control = 0x3B;
        const int kVK_RightCommand = 0x36;
        const int kVK_RightShift = 0x3C;
        const int kVK_RightOption = 0x3D;
        const int kVK_RightControl = 0x3E;
        //const int kVK_Function = 0x3F;
        const int kVK_F17 = 0x40;
        const int kVK_VolumeUp = 0x48;
        const int kVK_VolumeDown = 0x49;
        const int kVK_Mute = 0x4A;
        const int kVK_F18 = 0x4F;
        const int kVK_F19 = 0x50;
        const int kVK_F20 = 0x5A;
        const int kVK_F5 = 0x60;
        const int kVK_F6 = 0x61;
        const int kVK_F7 = 0x62;
        const int kVK_F3 = 0x63;
        const int kVK_F8 = 0x64;
        const int kVK_F9 = 0x65;
        const int kVK_F11 = 0x67;
        const int kVK_F13 = 0x69;
        const int kVK_F16 = 0x6A;
        const int kVK_F14 = 0x6B;
        const int kVK_F10 = 0x6D;
        const int kVK_F12 = 0x6F;
        const int kVK_F15 = 0x71;
        const int kVK_Help = 0x72;
        const int kVK_Home = 0x73;
        const int kVK_PageUp = 0x74;
        const int kVK_ForwardDelete = 0x75;
        const int kVK_F4 = 0x76;
        const int kVK_End = 0x77;
        const int kVK_F2 = 0x78;
        const int kVK_PageDown = 0x79;
        const int kVK_F1 = 0x7A;
        const int kVK_LeftArrow = 0x7B;
        const int kVK_RightArrow = 0x7C;
        const int kVK_DownArrow = 0x7D;
        const int kVK_UpArrow = 0x7E;

        public static Dictionary<Keys, int> s_KeyMap = new Dictionary<Keys, int>
 {
    {Keys.A,kVK_ANSI_A },
    {Keys.S, kVK_ANSI_S},
    {Keys.D,kVK_ANSI_D },
    {Keys.F,kVK_ANSI_F },
    {Keys.H, kVK_ANSI_H},
    {Keys.G, kVK_ANSI_G},
    {Keys.Z, kVK_ANSI_Z},
    {Keys.X, kVK_ANSI_X},
    {Keys.C, kVK_ANSI_C},
    {Keys.V, kVK_ANSI_V},
    {Keys.B, kVK_ANSI_B},
    {Keys.Q, kVK_ANSI_Q},
    {Keys.W, kVK_ANSI_W},
    {Keys.E, kVK_ANSI_E},
    {Keys.R, kVK_ANSI_R},
    {Keys.Y, kVK_ANSI_Y},
    {Keys.T, kVK_ANSI_T},
    {Keys.D1, kVK_ANSI_1},
    {Keys.D2, kVK_ANSI_2},
    {Keys.D3, kVK_ANSI_3},
    {Keys.D4, kVK_ANSI_4},
    {Keys.D6, kVK_ANSI_6},
    {Keys.D5, kVK_ANSI_5},
    //{kVK_ANSI_EKeys.qual, ?},
    {Keys.D9, kVK_ANSI_9},
    {Keys.D7, kVK_ANSI_7},
    {Keys.D8, kVK_ANSI_8},
    {Keys.D0, kVK_ANSI_0},
    { Keys.O,kVK_ANSI_O},
    { Keys.U,kVK_ANSI_U},
    { Keys.I,kVK_ANSI_I},
    { Keys.P,kVK_ANSI_P},
    { Keys.L,kVK_ANSI_L},
    { Keys.J,kVK_ANSI_J},
    { Keys.K,kVK_ANSI_K},
    //{kVK_ANSI_Slash, ?},
    {Keys.N, kVK_ANSI_N},
    {Keys.M, kVK_ANSI_M},
    {Keys.OemPeriod, kVK_ANSI_Period},
    //{kVK_ANSI_Grave, ?},
    { Keys.Decimal,  kVK_ANSI_KeypadDecimal},
    {Keys.Multiply,   kVK_ANSI_KeypadMultiply},
    {Keys. OemPlus,         kVK_ANSI_KeypadPlus},
    {Keys.Clear,      kVK_ANSI_KeypadClear},
    {Keys.Divide,kVK_ANSI_KeypadDivide },
    {Keys.OemMinus, kVK_ANSI_KeypadMinus},
    //{Keys.OemMinus, kVK_ANSI_Minus},
    {Keys.OemCloseBrackets, kVK_ANSI_RightBracket},
    {Keys.OemOpenBrackets,kVK_ANSI_LeftBracket },
    {Keys.OemQuotes, kVK_ANSI_Quote},
    {Keys.OemSemicolon,kVK_ANSI_Semicolon },
    {Keys.OemBackslash,kVK_ANSI_Backslash },
    { Keys.OemComma,kVK_ANSI_Comma},
    //{kVK_ANSI_KeypadEquals, ?},
    {Keys.NumPad0, kVK_ANSI_Keypad0},
    {Keys.NumPad1, kVK_ANSI_Keypad1},
    {Keys.NumPad2, kVK_ANSI_Keypad2},
    {Keys.NumPad3, kVK_ANSI_Keypad3},
    {Keys.NumPad4, kVK_ANSI_Keypad4},
    {Keys.NumPad5, kVK_ANSI_Keypad5},
    {Keys.NumPad6, kVK_ANSI_Keypad6},
    {Keys.NumPad7, kVK_ANSI_Keypad7},
    {Keys.NumPad8, kVK_ANSI_Keypad8},
    {Keys.NumPad9, kVK_ANSI_Keypad9},
    {Keys.Tab, kVK_Tab},
    {Keys.Space , kVK_Space  },
    {Keys.Back  , kVK_Delete },
    //{Keys.Enter, kVK_ANSI_KeypadEnter},
    {Keys.Return, kVK_Return },
    {Keys.Escape, kVK_Escape },
    {Keys.LWin  , kVK_Command},
    {Keys.LeftShift,kVK_Shift    },
    {Keys.CapsLock ,kVK_CapsLock },
    {Keys.LeftAlt  ,kVK_Option   },
    { Keys.LeftCtrl,kVK_Control    },
    {Keys.RWin      , kVK_RightCommand},
    {Keys.RightShift, kVK_RightShift  },
    {Keys.RightAlt  , kVK_RightOption },
    {Keys.RightCtrl , kVK_RightControl},
    //{kVK_Function, ?},
    {Keys.F18, kVK_F18},
    {Keys.F19, kVK_F19},
    {Keys.F20, kVK_F20},
    {Keys.F5, kVK_F5},
    {Keys.F6, kVK_F6},
    {Keys.F7, kVK_F7},
    {Keys.F3, kVK_F3},
    {Keys.F8, kVK_F8},
    {Keys.F9, kVK_F9},
    { Keys.F11,kVK_F11},
    { Keys.F13,kVK_F13},
    { Keys.F16,kVK_F16},
    { Keys.F14,kVK_F14},
    { Keys.F10,kVK_F10},
    { Keys.F12,kVK_F12},
    { Keys.F17,kVK_F17},
    {Keys.F4,kVK_F4 },
    {Keys.F2,kVK_F2 },
    {Keys.F1,kVK_F1 },
    {Keys.F15  , kVK_F15},
    {Keys.End ,  kVK_End},
    {Keys.Help, kVK_Help},
    {Keys.Home, kVK_Home},
    {Keys.VolumeUp,kVK_VolumeUp },
    {Keys.VolumeDown, kVK_VolumeDown},
    {Keys.PageUp,kVK_PageUp },
    { Keys.VolumeMute,kVK_Mute},
    {Keys.Delete, kVK_ForwardDelete},
    { Keys.PageDown,kVK_PageDown},
    {Keys.Left,kVK_LeftArrow },
    {Keys.Right,kVK_RightArrow },
    {Keys.Down, kVK_DownArrow},
    {Keys.Up, kVK_UpArrow}
};
        #endregion

    }
}
