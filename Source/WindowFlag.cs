namespace SmashFramework;

public enum WindowFlag : ulong
{
    Fullscreen = 1uL,
    Occluded = 4uL,
    Hidden = 8uL,
    Borderless = 0x10uL,
    Resizable = 0x20uL,
    Minimized = 0x40uL,
    Maximized = 0x80uL,
    MouseGrabbed = 0x100uL,
    InputFocus = 0x200uL,
    MouseFocus = 0x400uL,
    External = 0x800uL,
    Modal = 0x1000uL,
    HighPixelDensity = 0x2000uL,
    MouseCapture = 0x4000uL,
    MouseRelativeMode = 0x8000uL,
    AlwaysOnTop = 0x10000uL,
    Utility = 0x20000uL,
    Tooltip = 0x40000uL,
    PopupMenu = 0x80000uL,
    KeyboardGrabbed = 0x100000uL,
    WindowFillDocument = 0x200000uL,
    Transparent = 0x40000000uL,
    NotFocusable = 0x80000000uL
}
