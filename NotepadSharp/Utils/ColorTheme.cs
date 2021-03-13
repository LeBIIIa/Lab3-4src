using FastColoredTextBoxNS;

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NotepadSharp.Utils
{
    public enum TypeOfTheme
    {
        Light,
        Dark,
        Special
    }


    public class ThemeColor
    {
        public Color FormBackColor { get; set; } = Control.DefaultBackColor;
        public Color DefaultBackColor { get; set; } = Control.DefaultBackColor;
        public Color DefaultForeColor { get; set; } = Control.DefaultForeColor;
        public Color TabBackColor { get; set; } = Control.DefaultBackColor;
        public Color ToolButtonsBackColor { get; set; } = Color.Black;
        public Color MainMenuBackColor { get; set; } = Color.Black;
        public Color StatusStripBackColor { get; set; } = Color.Black;
    }

    internal class ColorTheme
    {
        public static IReadOnlyDictionary<TypeOfTheme, ThemeColor> themes = new Dictionary<TypeOfTheme, ThemeColor>()
        {
            { TypeOfTheme.Light, new ThemeColor() 
            {
                ToolButtonsBackColor = Color.AntiqueWhite,
                MainMenuBackColor = Color.WhiteSmoke,
                StatusStripBackColor = Color.AntiqueWhite,

            } },
            { TypeOfTheme.Dark, new ThemeColor() 
            {
                DefaultBackColor = Color.DimGray,
                DefaultForeColor = Color.White,
                FormBackColor = Color.DimGray
            } },
            { TypeOfTheme.Special, new ThemeColor() 
            {
                DefaultBackColor = Color.DimGray,
                DefaultForeColor = Color.LawnGreen,
                FormBackColor = Color.Black
            } },
        };
        public static void ColorChangerDeveloperForm(Form form, MenuStrip mainMenu,
            ToolStrip toolButtons,
            StatusStrip statusStrip,
            TabControl tabOption,
            FastColoredTextBox fastColoredTextBox,
            TypeOfTheme typeOfTheme)
        {
            TabPage tabpage = tabOption.SelectedTab;
            if (tabpage != null)
            {
                tabpage.BorderStyle = BorderStyle.None;
                tabpage.BackColor = Color.Black;
                ColorChangerTextBox(fastColoredTextBox, typeOfTheme);
            }

            form.BackColor = themes[typeOfTheme].FormBackColor;

            mainMenu.BackColor = themes[typeOfTheme].MainMenuBackColor;
            mainMenu.ForeColor = themes[typeOfTheme].DefaultForeColor;

            tabOption.BackColor = themes[typeOfTheme].DefaultBackColor;
            tabOption.ForeColor = themes[typeOfTheme].DefaultForeColor;

            toolButtons.BackColor = themes[typeOfTheme].ToolButtonsBackColor;
            toolButtons.ForeColor = themes[typeOfTheme].DefaultForeColor;

            statusStrip.BackColor = themes[typeOfTheme].StatusStripBackColor;
            statusStrip.ForeColor = themes[typeOfTheme].DefaultForeColor;
        }

        public static void ColorChangerTextBox(FastColoredTextBox fastColoredTextBox, TypeOfTheme typeOfTheme)
        {
            fastColoredTextBox.BackColor = themes[typeOfTheme].DefaultBackColor;
            fastColoredTextBox.ForeColor = themes[typeOfTheme].DefaultForeColor;
            fastColoredTextBox.IndentBackColor = themes[typeOfTheme].DefaultBackColor;
            fastColoredTextBox.LineNumberColor = themes[typeOfTheme].DefaultForeColor;
        }
    }
}
