using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;

namespace WpfThemer
{
    public class DynamicSymbol(string name, Theme.eSymbolColor symbolColor, Uri value)
    {
        public string Name { get; set; } = name;
        public Theme.eSymbolColor SymbolColor { get; set; } = symbolColor;
        public Uri? Value { get; set; } = value;
    }


    public static class ThemeSymbolManager
    {
        public static Dictionary<Theme.eSymbolColor, List<DynamicSymbol>> SymbolLists = [];
        public static List<DynamicSymbol> ActiveSymbols = [];

        public static void Initialize()
        {
            foreach (Theme.eSymbolColor color in Enum.GetValues(typeof(Theme.eSymbolColor)))
            {
                BuildSymbolList(color);
            }

            ThemeManager.ThemeChanged += (sender, e) =>
            {
                ActiveSymbols = SymbolLists.GetValueOrDefault(ThemeManager.ActiveTheme.SymbolColor) ?? [];
            };
        }

        private static void BuildSymbolList(Theme.eSymbolColor color)
        {
            List<DynamicSymbol> list = new();
            SymbolLists.Add(color, list);

            string colorName = color.ToString().Substring(1);
            
            list.Add(new("add", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/add.png")));
            list.Add(new("add_attachment", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/add_attachment.png")));
            list.Add(new("add_image", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/add_image.png")));
            list.Add(new("arrow_back", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/arrow_back.png")));
            list.Add(new("arrow_drop_down", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/arrow_drop_down.png")));
            list.Add(new("arrow_forward", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/arrow_forward.png")));
            list.Add(new("arrow_right", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/arrow_right.png")));
            list.Add(new("attach_email", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/attach_email.png")));
            list.Add(new("attach_file", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/attach_file.png")));
            list.Add(new("book", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/book.png")));
            list.Add(new("bookmark", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/bookmark.png")));
            list.Add(new("bookmark_add", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/bookmark_add.png")));
            list.Add(new("bookmarks", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/bookmarks.png")));
            list.Add(new("cancel", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/cancel.png")));
            list.Add(new("check", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/check.png")));
            list.Add(new("check_circle", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/check_circle.png")));
            list.Add(new("chevron_left", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/chevron_left.png")));
            list.Add(new("chevron_right", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/chevron_right.png")));
            list.Add(new("close", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/close.png")));
            list.Add(new("contact", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/contact.png")));
            list.Add(new("content_copy", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/content_copy.png")));
            list.Add(new("content_cut", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/content_cut.png")));
            list.Add(new("content_paste", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/content_paste.png")));
            list.Add(new("delete", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/delete.png")));
            list.Add(new("draft", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/draft.png")));
            list.Add(new("drafts", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/drafts.png")));
            list.Add(new("edit", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/edit.png")));
            list.Add(new("edit2", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/edit2.png")));
            list.Add(new("email", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/email.png")));
            list.Add(new("expand_circle", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/expand_circle.png")));
            list.Add(new("explore", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/explore.png")));
            list.Add(new("favorite", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/favorite.png")));
            list.Add(new("file_open", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/file_open.png")));
            list.Add(new("find", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/find.png")));
            list.Add(new("folder", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/folder.png")));
            list.Add(new("format_align_center", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/format_align_center.png")));
            list.Add(new("format_align_left", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/format_align_left.png")));
            list.Add(new("format_align_right", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/format_align_right.png")));
            list.Add(new("format_bold", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/format_bold.png")));
            list.Add(new("format_italic", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/format_italic.png")));
            list.Add(new("format_size", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/format_size.png")));
            list.Add(new("format_underlined", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/format_underlined.png")));
            list.Add(new("group", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/group.png")));
            list.Add(new("help", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/help.png")));
            list.Add(new("home", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/home.png")));
            list.Add(new("image", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/image.png")));
            list.Add(new("inbox", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/inbox.png")));
            list.Add(new("language", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/language.png")));
            list.Add(new("library_add", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/library_add.png")));
            list.Add(new("link", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/link.png")));
            list.Add(new("login", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/login.png")));
            list.Add(new("mail", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/mail.png")));
            list.Add(new("mark_as_unread2", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/mark_as_unread2.png")));
            list.Add(new("mark_email_unread", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/mark_email_unread.png")));
            list.Add(new("menu", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/menu.png")));
            list.Add(new("menu_book", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/menu_book.png")));
            list.Add(new("minimize", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/minimize.png")));
            list.Add(new("more_horiz", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/more_horiz.png")));
            list.Add(new("more_vert", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/more_vert.png")));
            list.Add(new("outbox", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/outbox.png")));
            list.Add(new("person", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/person.png")));
            list.Add(new("person_add", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/person_add.png")));
            list.Add(new("question_mark", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/question_mark.png")));
            list.Add(new("redo", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/redo.png")));
            list.Add(new("refresh", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/refresh.png")));
            list.Add(new("remove", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/remove.png")));
            list.Add(new("search", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/search.png")));
            list.Add(new("searchbox", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/searchbox.png")));
            list.Add(new("send", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/send.png")));
            list.Add(new("settings", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/settings.png")));
            list.Add(new("share", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/share.png")));
            list.Add(new("sort", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/sort.png")));
            list.Add(new("star", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/star.png")));
            list.Add(new("sync", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/sync.png")));
            list.Add(new("toggle_off", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/toggle_off.png")));
            list.Add(new("toggle_on", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/toggle_on.png")));
            list.Add(new("trash", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/trash.png")));
            list.Add(new("undo", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/undo.png")));
            list.Add(new("zoom_in", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/zoom_in.png")));
            list.Add(new("zoom_out", color, new($"pack://application:,,,/WpfThemer;component/Symbols/{colorName}/zoom_out.png")));            
        }

        public static void AddSymbol(string name, Theme.eSymbolColor symbolColor, Uri value)
        {
            List<DynamicSymbol>? list = SymbolLists.GetValueOrDefault(ThemeManager.ActiveTheme.SymbolColor);
            if (list is null) return;
            list.Add(new DynamicSymbol(name, symbolColor, value));
        }

        public static DynamicSymbol? GetSymbol(string name)
        {
            List<DynamicSymbol>? list = SymbolLists.GetValueOrDefault(ThemeManager.ActiveTheme.SymbolColor);
            if(list is null) return null;

            return list.FirstOrDefault(s =>
            (s.SymbolColor == ThemeManager.ActiveTheme.SymbolColor) &&
            (s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)));
        }
        
        
    }
}
